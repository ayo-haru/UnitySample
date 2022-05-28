using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    //----- �萔��` -----
    private enum eSTATEPAUSE    // �|�[�Y����s������
    {
        RETURNGAME = 0, // �Q�[���ɖ߂�
        RETURNTITLE,    // �^�C�g���ɖ߂�
        QUITGAME,       // ���[�ނ���߂�
        OPTION,         // �I�v�V����
        QUITQUESTION,   // �{���ɂ�߂܂���

        MAX_STATE
    }
    private enum eQuitState {
        NONE,
        YES,
        NO
    }

    //----- �ϐ���` -----
    //---�\������UI
    [SerializeField]
    private GameObject pauseframe;
    private GameObject PauseFrame;
    [SerializeField]
    private GameObject pausecharacter;
    private GameObject PauseCharacter;
    [SerializeField]
    private GameObject backgame;
    private GameObject BackGame;
    [SerializeField]
    private GameObject gameend;
    private GameObject GameEnd;
    [SerializeField]
    private GameObject backtitle;
    private GameObject BackTitle;
    [SerializeField]
    private GameObject option;
    private GameObject Option;
    [SerializeField]
    private GameObject optionmanager;
    private GameObject Optionmanager;
    [SerializeField]
    private GameObject selectbox_1;
    private GameObject SelectBox_1; // �ʏ�̑I���Ŏg�p
    [SerializeField]
    private GameObject selectbox_2;
    private GameObject SelectBox_2; // �ŏI�m�F�Ŏg�p
    [SerializeField]
    private GameObject decision;
    private GameObject Decision;

    [SerializeField]
    private GameObject quitquestion;
    private GameObject QuitQuestion;
    [SerializeField]
    private GameObject quityes;
    private GameObject QuitYes;
    [SerializeField]
    private GameObject quitno;
    private GameObject QuitNo;

    Canvas canvas;                          // �\���Ɏg�p����Canvas
    Canvas canvas2;

    private bool isDecision;                // ����
    private bool isConfirm;                   // �t�@�C�i���A���T�[�H

    private Game_pad UIActionAssets;        // InputAction��UI������
    private InputAction LeftStickSelect;    // InputAction��select������
    private InputAction RightStickSelect;   // InputAction��select������

    private int pauseSelect;                // �|�[�Y�I��
    private int quitSelect;                 // �ŏI�m�F�I��
    private int returnSelect;               // �߂�Ȃ��Ⴂ���Ȃ��I����
    private bool isCalledOncce = false;     // �|�[�Y����񂵂��Ă΂Ȃ�
       
    private float UIBasePosx;               // UI�\���̊�ʒu
    private float UIMoveSpeed = 1.5f;       // UI�\���𓮂����Ƃ��̃X�s�[�h

    private bool notShowPause;              // �|�[�Y�ł͂Ȃ��Ƃ���UI��\�����Ȃ��悤�ɂ��邽�߂̕ϐ�

    private GameObject hpUI;                // �|�[�Y�ɓ�������UI��������������
    private GameObject minimapUI;    




    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
    }




    private void Start() {
        quitSelect = (int)eQuitState.YES;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // �I���̃��[�h�̏�����
        returnSelect = pauseSelect;                      // �I���̃��[�h�̏�����

        UIBasePosx = 0.0f;  // UI�̕\���ʒu�̊�ʒu������

        notShowPause = false;   // true���\�����Ȃ��Ƃ�

        isConfirm = false;         // �{���ɂ�߂邩���m�肳�ꂽ��true


        //---�T���Ċi�[
        hpUI = GameObject.Find("HPSystem(2)(Clone)");   
        minimapUI = GameObject.Find("MiniMapFrame");

        //---�L�����o�X���w��
        canvas = GetComponent<Canvas>();
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        }

        //---���ԉ�
        PauseFrame = Instantiate(pauseframe);           // �|�[�Y�g
        PauseCharacter = Instantiate(pausecharacter);   // �|�[�Y�̕���
        BackGame = Instantiate(backgame);               // �Q�[���ɖ߂�̕���
        GameEnd = Instantiate(gameend);                 // �Q�[����߂�̕���
        BackTitle = Instantiate(backtitle);             // �^�C�g�����h���̕���
        Option = Instantiate(option);                   // �I�v�V�����̕���
        Optionmanager = Instantiate(optionmanager);     // �I�v�V�����̉��
        SelectBox_1 = Instantiate(selectbox_1);         // �I��g
        SelectBox_2 = Instantiate(selectbox_2);
        Decision = Instantiate(decision);               // ���葀���������
        QuitQuestion = Instantiate(quitquestion);       // �{���ɂ�߂܂�������
        QuitYes = Instantiate(quityes);                 // �{���ɂ�߂܂����C�G�X
        QuitNo = Instantiate(quitno);                   // �{���ɂ�߂܂����́[

        //---�L�����o�X�̎q�ɂ���
        PauseFrame.transform.SetParent(this.transform, false);              // �|�[�Y�g
        SelectBox_1.transform.SetParent(this.canvas.transform, false);      // �I��g
        SelectBox_2.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);   // �|�[�Y�̕���
        BackGame.transform.SetParent(this.canvas.transform, false);         // �Q�[���ɖ߂�̕���
        GameEnd.transform.SetParent(this.canvas.transform, false);          // �Q�[����߂�̕���
        BackTitle.transform.SetParent(this.canvas.transform, false);        // �^�C�g�����h���̕���
        Option.transform.SetParent(this.canvas.transform, false);           // �I�v�V�����̕���
        Decision.transform.SetParent(this.canvas.transform, false);         // ���葀���������
        QuitQuestion.transform.SetParent(this.canvas.transform, false);     // �{���ɂ�߂܂�������
        QuitYes.transform.SetParent(this.canvas.transform, false);          // �{���ɂ�߂܂����C�G�X
        QuitNo.transform.SetParent(this.canvas.transform, false);           // �{���ɂ�߂܂����́[
        // �I�v�V�������
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Optionmanager.transform.SetParent(this.canvas2.transform, false);
        }
        else
        {
            Optionmanager.transform.SetParent(this.canvas.transform, false);
        }


        //---�Q�[���X�^�[�g���͔�\��
        // �I��g
        SelectBox_1.GetComponent<UIBlink>().isBlink = true;
        SelectBox_1.GetComponent<UIBlink>().isHide = true;
        SelectBox_1.GetComponent<Image>().enabled = false;
        SelectBox_1.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
        SelectBox_2.GetComponent<UIBlink>().isBlink = true;
        SelectBox_2.GetComponent<UIBlink>().isHide = true;
        SelectBox_2.GetComponent<Image>().enabled = false;
        SelectBox_2.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
        // �|�[�Y�g
        PauseFrame.GetComponent<Image>().enabled = false;
        // �|�[�Y���ĕ���
        PauseCharacter.GetComponent<Image>().enabled = false;
        // �Q�[���ɖ߂�̕���
        BackGame.GetComponent<Image>().enabled = false;
        // �Q�[���I���̕���
        GameEnd.GetComponent<Image>().enabled = false;
        // �^�C�g�����h���̕���
        BackTitle.GetComponent<Image>().enabled = false;
        // �I�v�V�����̕���
        Option.GetComponent<Image>().enabled = false;
        // �I�v�V�����̉��
        Optionmanager.SetActive(false);
        // ����̑���
        Decision.GetComponent<Image>().enabled = false;
        // �{���ɂ�낵���ł���
        QuitQuestion.GetComponent<Image>().enabled = false;
        // �{���ɂ�낵���ł����̃C�G�X
        QuitYes.GetComponent<Image>().enabled = false;
        // �{���ɂ�낵���ł����̂́[
        QuitNo.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        //----- �R���g���[���[������ -----
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;

        //----- �|�[�Y��UI���o���Ȃ��ׂ�if(���̏������������Ȃ��Ă��₾��������ʂŏ����������) -----
        notShowPause = false; // true���\�����Ȃ��Ƃ�
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE ||
            GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial1 ||
            GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial2 ||
            GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial3)
        {
            notShowPause = true;
        }

        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || notShowPause)
        {
            //----- �|�[�Y���ł͂Ȃ����̏��� -----
            //���y�Đ�
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE)
            {
                GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                if (kitchenBgmObject)
                {
                    kitchenBgmObject.GetComponent<AudioSource>().UnPause();
                }
            }

            // ��\��
            PauseFrame.GetComponent<Image>().enabled = false;
            SelectBox_1.GetComponent<Image>().enabled = false;
            SelectBox_2.GetComponent<Image>().enabled = false;
            PauseCharacter.GetComponent<Image>().enabled = false;
            BackGame.GetComponent<Image>().enabled = false;
            GameEnd.GetComponent<Image>().enabled = false;
            BackTitle.GetComponent<Image>().enabled = false;
            Option.GetComponent<Image>().enabled = false;
            isCalledOncce = false;
            hpUI.SetActive(true);
            minimapUI.SetActive(true);
            Decision.GetComponent<Image>().enabled = false;
            QuitQuestion.GetComponent<Image>().enabled = false;
            QuitYes.GetComponent<Image>().enabled = false;
            QuitNo.GetComponent<Image>().enabled = false;



            if (UIBasePosx > 0.0f)
            //if (UIPosx > -1390.0f)
            {
                FinUIMove();
            }

            return;
        }
        else if (Pause.isPause)
        {
            //----- �|�[�Y���̏��� -----
            if (!isCalledOncce)
            {
                //----- �|�[�Y�ɓ���������݂̂��� -----
                // �|�[�Y���ɂȂ�����\��
                PauseFrame.GetComponent<Image>().enabled = true;
                SelectBox_1.GetComponent<Image>().enabled = true;
                PauseCharacter.GetComponent<Image>().enabled = true;
                BackGame.GetComponent<Image>().enabled = true;
                GameEnd.GetComponent<Image>().enabled = true;
                BackTitle.GetComponent<Image>().enabled = true;
                Option.GetComponent<Image>().enabled = true;
                hpUI.SetActive(false);
                minimapUI.SetActive(false);


                //���y��~
                if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE)
                {
                    GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                    if (kitchenBgmObject)
                    {
                        kitchenBgmObject.GetComponent<AudioSource>().Pause();
                    }
                }
            }
            isCalledOncce = true;   // �������if���ɓ���Ȃ��悤�Ƀt���O�𔽓]

            if (UIBasePosx < 0.3125f)
            {
                StartUIMove();
                SelectBoxPosUpdete();   // �I���̘g�̕\���ʒu�̍X�V
            }
        }


        //----- �|�[�Y���̏��� -----
        //---�I��
        //�I�v�V�������J���Ă�Ԃ͖����ɂ���
        if (!Optionmanager.activeSelf)
        {
            // �I�v�V����������Ȃ��Ƃ��ɑ��������\��
            Decision.GetComponent<RectTransform>().localPosition = new Vector3(-460, -500, 0);
            Decision.GetComponent<Image>().enabled = true;

            // �|�[�Y�ɂȂ�����I��������
            if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
            {
                if (keyboard.leftArrowKey.wasReleasedThisFrame)
                {
                    SelectLeft();
                    return;
                }
                else if (isSetGamePad)
                {
                    if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
                    {
                        SelectLeft();
                        return;
                    }
                }

                if (keyboard.rightArrowKey.wasReleasedThisFrame)
                {
                    SelectRight();
                    return;
                }
                else if (isSetGamePad)
                {
                    if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
                    {
                        SelectRight();
                        return;
                    }
                }

            }
            else
            {
                if (keyboard.upArrowKey.wasReleasedThisFrame)
                {
                    SelectUp();
                    return;
                }
                else if (isSetGamePad)
                {
                    if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
                    {
                        SelectUp();
                        return;
                    }
                }

                if (keyboard.downArrowKey.wasReleasedThisFrame)
                {
                    SelectDown();
                    return;
                }
                else if (isSetGamePad)
                {
                    if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
                    {
                        SelectDown();
                        return;
                    }
                }
            }
        }
        else
        {
            // �I�v�V�������̓I�v�V�������̑������������̂ł���
            Decision.GetComponent<Image>().enabled = false;
        }


        //---�I�����Ă�����̂������ŕ���
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME)
        {
            //----- �Q�[���ɖ߂� -----
            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // �|�[�Y����
                Pause.isPause = false;
                // �������
                isDecision = false;
            }
        }
        else if(pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {
            //----- �ŏI�m�F -----
            SelectBox_2.GetComponent<Image>().enabled = true;

            //---�{���ɂ������̊m�F���o�����߂̌���
            if (isDecision)
            {
                pauseSelect = returnSelect;

                isConfirm = true;
                isDecision = false;
            }
        }
        else if (pauseSelect == (int)eSTATEPAUSE.RETURNTITLE)
        {
            //----- �^�C�g���ɖ߂� -----
            //---�{���ɂ������̊m�F���o�����߂̌���
            if (isDecision)
            {
                //---�{���ɂ�����UI
                QuitQuestion.GetComponent<Image>().enabled = true;
                QuitYes.GetComponent<Image>().enabled = true;
                QuitNo.GetComponent<Image>().enabled = true;

                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                returnSelect = (int)eSTATEPAUSE.RETURNTITLE;
                pauseSelect = (int)eSTATEPAUSE.QUITQUESTION;  // �I����{���ɂ�߂邩���[�h�ɂ���
                quitSelect = (int)eQuitState.YES;
                SelectBoxPosUpdete();
                isDecision = false;
            }
            //---�{���ɂ��������m��
            if (isConfirm)
            {
                if (quitSelect == (int)eQuitState.YES)
                {
                    //---�{���ɂ�����UI
                    QuitQuestion.GetComponent<Image>().enabled = false;
                    QuitYes.GetComponent<Image>().enabled = false;
                    QuitNo.GetComponent<Image>().enabled = false;

                    // �V�[���֘A
                    GameData.OldMapNumber = GameData.CurrentMapNumber;
                    string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                    SceneManager.LoadScene(nextSceneName);
                }
                else if (quitSelect == (int)eQuitState.NO)
                {
                    //---�{���ɂ�����UI
                    QuitQuestion.GetComponent<Image>().enabled = false;
                    QuitYes.GetComponent<Image>().enabled = false;
                    QuitNo.GetComponent<Image>().enabled = false;
                    SelectBox_2.GetComponent<Image>().enabled = false;
                }
                isConfirm = false;
            }
            else
            {
                isConfirm = false;
            }
        }
        else if (pauseSelect == (int)eSTATEPAUSE.QUITGAME)
        {
            //----- �Q�[������߂� -----
            if (isDecision)
            {
                //---�{���ɂ�����UI
                QuitQuestion.GetComponent<Image>().enabled = true;
                QuitYes.GetComponent<Image>().enabled = true;
                QuitNo.GetComponent<Image>().enabled = true;

                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                returnSelect = (int)eSTATEPAUSE.QUITGAME;
                pauseSelect = (int)eSTATEPAUSE.QUITQUESTION;  // �I����{���ɂ�߂邩���[�h�ɂ���
                quitSelect = (int)eQuitState.YES;
                SelectBoxPosUpdete();
                isDecision = false;
            }

            //---�{���ɂ��������m��
            if (isConfirm)
            {
                if (quitSelect == (int)eQuitState.YES)
                {
                    // ���艹
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                    // �������
                    isDecision = false;

                    /*
                     * Unity�G�f�B�^���exe�ƂŃQ�[���̏I���̂��������������̂ŕ���
                     */
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                }
                else if (quitSelect == (int)eQuitState.NO)
                {
                    //---�{���ɂ�����UI
                    QuitQuestion.GetComponent<Image>().enabled = false;
                    QuitYes.GetComponent<Image>().enabled = false;
                    QuitNo.GetComponent<Image>().enabled = false;
                    SelectBox_2.GetComponent<Image>().enabled = false;
                }
                isConfirm = false;
            }
            else
            {
                isConfirm = false;
            }
    }
        else if(pauseSelect == (int)eSTATEPAUSE.OPTION)
        {
            //----- �I�v�V���� -----
            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // �I�v�V�����}�l�[�W���[���A�N�e�B�u�ɂ���
                Optionmanager.SetActive(true);
                // �������
                isDecision = false;
            }
        }
    }

    private void OnEnable() {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Action�C�x���g��o�^
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    /// <summary>
    /// ���X�e�B�b�N
    /// </summary>
    /// <param name="obj"></param>
    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause)
        {
            // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
            return;
        }

        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {
            //---�����ł��|���ꂽ�珈���ɓ���
            if (doLeftStick.x > 0.0f)
            {
                SelectRight();
            }
            else if (doLeftStick.x < 0.0f)
            {
                SelectLeft();
            }
        }
        else
        {
            //---�����ł��|���ꂽ�珈���ɓ���
            if (doLeftStick.y > 0.0f)
            {
                SelectUp();
            }
            else if (doLeftStick.y < 0.0f)
            {
                SelectDown();
            }
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause)
        {
            // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
            return;
        }

        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {
            //---�����ł��|���ꂽ�珈���ɓ���
            if (doRightStick.x > 0.0f)
            {
                SelectRight();
            }
            else if (doRightStick.x < 0.0f)
            {
                SelectLeft();
            }
        }
        else
        {
            //---�����ł��|���ꂽ�珈���ɓ���
            if (doRightStick.y > 0.0f)
            {
                SelectUp();
            }
            else if (doRightStick.y < 0.0f)
            {
                SelectDown();
            }
        }
    }

    /// <summary>
    /// ����{�^��
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj) {
        if (Pause.isPause || !SaveManager.canSave || !Warp.shouldWarp || !GameData.isFadeIn || !GameData.isFadeOut || !GameOver.GameOverFlag || notShowPause)
        {
            // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
            isDecision = true;
        }
    }

    /// <summary>
    /// �I��g�̍��W�X�V
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * �I��g�̈ʒu�̍X�V
         * ���ꂼ��̕�����RectTransform�ƍ��킹�邱�Ƃœ����ʒu�ɕ\�����ł���
         */
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = BackGame.GetComponent<RectTransform>().localPosition;
        }
        else if (pauseSelect == (int)eSTATEPAUSE.RETURNTITLE)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = BackTitle.GetComponent<RectTransform>().localPosition;
        }
        else if (pauseSelect == (int)eSTATEPAUSE.QUITGAME)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = GameEnd.GetComponent<RectTransform>().localPosition;
        }
        else if (pauseSelect == (int)eSTATEPAUSE.OPTION)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = Option.GetComponent<RectTransform>().localPosition;
        }
        else if(pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {

            if (quitSelect == (int)eQuitState.YES)
            {
                SelectBox_2.GetComponent<RectTransform>().localPosition = QuitYes.GetComponent<RectTransform>().localPosition;
            }
            else if(quitSelect == (int)eQuitState.NO)
            {
                SelectBox_2.GetComponent<RectTransform>().localPosition = QuitNo.GetComponent<RectTransform>().localPosition;
            }
        }
    }

    /// <summary>
    /// ������I��
    /// </summary>
    private void SelectUp() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        pauseSelect--;
        if (pauseSelect < 0) // ��O����
        {
            pauseSelect = 0;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �������I��
    /// </summary>
    private void SelectDown() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        pauseSelect++;
        if (pauseSelect >= (int)eSTATEPAUSE.MAX_STATE)   // ��O����
        {
            pauseSelect = (int)eSTATEPAUSE.OPTION;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }


    /// <summary>
    /// �I���E
    /// </summary>
    private void SelectRight() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect++;
        if (quitSelect > (int)eQuitState.NO)
        {
            quitSelect = (int)eQuitState.NO;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �I����
    /// </summary>
    private void SelectLeft() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect--;
        if (quitSelect < (int)eQuitState.YES)
        {
            quitSelect = (int)eQuitState.YES;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �|�[�Y�ɓ�������UI�𓮂���
    /// </summary>
    private void StartUIMove() {
        Camera camera = Camera.main; // ���C���J�������w��

        // �^�C���ŉ�ʂ̕\���ʒu�̊�����ς���
        UIBasePosx += Time.deltaTime * UIMoveSpeed;
        if (UIBasePosx > 0.3125f)      // ��O����
        {
            // 0.3125(600/1920)��UI�̉������J�����̍��W�ɂ��킹�����
            UIBasePosx = 0.3125f;
        }

        camera.rect = new Rect(UIBasePosx, 0.0f, 1.0f - UIBasePosx, 1.0f);
    }

    /// <summary>
    /// �|�[�Y�I�������UI��߂�
    /// </summary>
    private void FinUIMove() {
        Camera camera = Camera.main; // ���C���J�������w��

        // �^�C���ŉ�ʂ̕\���ʒu�̊�����ς���
        UIBasePosx -= Time.deltaTime * UIMoveSpeed;

        if (UIBasePosx < 0.0f)  // ��O����
        {
            UIBasePosx = 0.0f;
        }

        camera.rect = new Rect(UIBasePosx, 0.0f, 1.0f - UIBasePosx, 1.0f);
    }
}