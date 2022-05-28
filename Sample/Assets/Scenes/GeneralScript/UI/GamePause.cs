using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private enum eSTATEPAUSE    // �|�[�Y����s������
    {
        RETURNGAME = 0, // �Q�[���ɖ߂�
        RETURNTITLE,    // �^�C�g���ɖ߂�
        QUITGAME,       // ���[�ނ���߂�
        OPTION,         //�I�v�V����

        MAX_STATE
    }

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
    private GameObject panel;
    private GameObject Panel;
    [SerializeField]
    private GameObject selectbox;
    private GameObject SelectBox;
    [SerializeField]
    private GameObject decision;
    private GameObject Decision;

    //---�\���Ɏg�p����Canvas
    Canvas canvas;
    Canvas canvas2;

    private bool isDecision;                // ����

    private Game_pad UIActionAssets;        // InputAction��UI������
    private InputAction LeftStickSelect;    // InputAction��select������
    private InputAction RightStickSelect;   // InputAction��select������


    private int select;                     // �I��
    private bool isCalledOncce = false;     // �|�[�Y����񂵂��Ă΂Ȃ�
       
    private float UIBasePosx;               // UI�\���̊�ʒu
    private float UIMoveSpeed = 1.5f;       // UI�\���𓮂����Ƃ��̃X�s�[�h
    //private float UIPosx = 0.0f;            // RectTransform��pos

    private bool notShowPause;

    private GameObject hpUI;                // �|�[�Y�ɓ�������UI��������������
    private GameObject minimapUI;             

    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
    }


    private void Start() {
        select = (int)eSTATEPAUSE.RETURNGAME;   // �I���̃��[�h�̏�����

        UIBasePosx = 0.0f;  // UI�̕\���ʒu�̊�ʒu������
        //UIPosx = 0.0f;

        notShowPause = false;   // true���\�����Ȃ��Ƃ�

        hpUI = GameObject.Find("HPSystem(2)(Clone)");   // �T���Ċi�[
        minimapUI = GameObject.Find("MiniMapFrame");

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        }
        // ���ԉ�
        PauseFrame = Instantiate(pauseframe);
        PauseCharacter = Instantiate(pausecharacter);
        BackGame = Instantiate(backgame);
        GameEnd = Instantiate(gameend);
        BackTitle = Instantiate(backtitle);
        Option = Instantiate(option);
        Optionmanager = Instantiate(optionmanager);
        Panel = Instantiate(panel);
        SelectBox = Instantiate(selectbox);
        Decision = Instantiate(decision);


        // �L�����o�X�̎q�ɂ���
        //Panel.transform.SetParent(this.canvas.transform, false);
        PauseFrame.transform.SetParent(this.transform, false);
        SelectBox.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);
        BackGame.transform.SetParent(this.canvas.transform, false);
        GameEnd.transform.SetParent(this.canvas.transform, false);
        BackTitle.transform.SetParent(this.canvas.transform, false);
        Option.transform.SetParent(this.canvas.transform, false);
        Decision.transform.SetParent(this.canvas.transform, false);
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Optionmanager.transform.SetParent(this.canvas2.transform, false);
        }
        else
        {
            Optionmanager.transform.SetParent(this.canvas.transform, false);
        }


        // �Q�[���X�^�[�g���͔�\��
        SelectBox.GetComponent<UIBlink>().isBlink = true;
        SelectBox.GetComponent<UIBlink>().isHide = true;
        SelectBox.GetComponent<Image>().enabled = false;
        SelectBox.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
        PauseFrame.GetComponent<Image>().enabled = false;
        PauseCharacter.GetComponent<Image>().enabled = false;
        BackGame.GetComponent<Image>().enabled = false;
        GameEnd.GetComponent<Image>().enabled = false;
        BackTitle.GetComponent<Image>().enabled = false;
        Option.GetComponent<Image>().enabled = false;
        BackGame.GetComponent<Image>().enabled = false;
        GameEnd.GetComponent<Image>().enabled = false;
        BackTitle.GetComponent<Image>().enabled = false;
        Option.GetComponent<Image>().enabled = false;
        Panel.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Panel.GetComponent<Image>().enabled = false;
        Optionmanager.SetActive(false);
        Decision.GetComponent<Image>().enabled = false;
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
            SelectBox.GetComponent<Image>().enabled = false;
            PauseCharacter.GetComponent<Image>().enabled = false;
            BackGame.GetComponent<Image>().enabled = false;
            GameEnd.GetComponent<Image>().enabled = false;
            BackTitle.GetComponent<Image>().enabled = false;
            Option.GetComponent<Image>().enabled = false;
            //Panel.GetComponent<Image>().enabled = false;
            isCalledOncce = false;
            hpUI.SetActive(true);
            minimapUI.SetActive(true);
            Decision.GetComponent<Image>().enabled = false;

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
                SelectBox.GetComponent<Image>().enabled = true;
                PauseCharacter.GetComponent<Image>().enabled = true;
                BackGame.GetComponent<Image>().enabled = true;
                GameEnd.GetComponent<Image>().enabled = true;
                BackTitle.GetComponent<Image>().enabled = true;
                Option.GetComponent<Image>().enabled = true;
                //Panel.GetComponent<Image>().enabled = true;
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
            //if (UIPosx < 1920.0f)
            {
                StartUIMove();
                SelectBoxPosUpdete();   // �I���̘g�̕\���ʒu�̍X�V
            }
        }


        //----- �|�[�Y���̏��� -----
        //�I�v�V�������J���Ă�Ԃ͖����ɂ���
        if (!Optionmanager.activeSelf)
        {
            // �I�v�V����������Ȃ��Ƃ��ɑ��������\��
            Decision.GetComponent<Image>().enabled = true;

            // �|�[�Y�ɂȂ�����I��������
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
        else
        {
            // �I�v�V�������̓I�v�V�������̑������������̂ł���
            Decision.GetComponent<Image>().enabled = false;
        }


        // �I�����Ă�����̂������ŕ���
        if (select == (int)eSTATEPAUSE.RETURNGAME)
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
        else if (select == (int)eSTATEPAUSE.RETURNTITLE)
        {
            //----- �^�C�g���ɖ߂� -----
            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // �V�[���֘A
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                SceneManager.LoadScene(nextSceneName);
                // �������
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            //----- �Q�[������߂� -----
            if (isDecision) // �I�����m��
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

        }
        else if(select == (int)eSTATEPAUSE.OPTION)
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

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause)
        {
            // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
            return;
        }

        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

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
        if (select == (int)eSTATEPAUSE.RETURNGAME)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = BackGame.GetComponent<RectTransform>().localPosition;
        }
        else if (select == (int)eSTATEPAUSE.RETURNTITLE)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = BackTitle.GetComponent<RectTransform>().localPosition;
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = GameEnd.GetComponent<RectTransform>().localPosition;
        }
        else if (select == (int)eSTATEPAUSE.OPTION)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = Option.GetComponent<RectTransform>().localPosition;
        }
    }

    /// <summary>
    /// ������I��
    /// </summary>
    private void SelectUp() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0) // ��O����
        {
            select = 0;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �������I��
    /// </summary>
    private void SelectDown() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select >= (int)eSTATEPAUSE.MAX_STATE)   // ��O����
        {
            select = (int)eSTATEPAUSE.OPTION;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }


    /// <summary>
    /// �|�[�Y�ɓ�������UI�𓮂���
    /// </summary>
    private void StartUIMove() {
        Camera camera = Camera.main; // ���C���J�������w��
        //float cameraPosX = 0.0f;

        // �^�C���ŉ�ʂ̕\���ʒu�̊�����ς���
        UIBasePosx += Time.deltaTime * UIMoveSpeed;
        //cameraPosX = UIBasePosx;
        //UIPosx += UIBasePosx * 1920;
        if (UIBasePosx > 0.3125f)      // ��O����
        {
            // 0.3125(600/1920)��UI�̉������J�����̍��W�ɂ��킹�����
            UIBasePosx = 0.3125f;
        }

        //if(UIPosx > 1920.0f)
        //{
        //    UIPosx = 1920.0f;
        //}

        camera.rect = new Rect(UIBasePosx, 0.0f, 1.0f - UIBasePosx, 1.0f);
        //PauseFrame.GetComponent<RectTransform>().position = new Vector3(PauseFrame.GetComponent<RectTransform>().position.x + UIPosx, 0,0);
        //BackGame.GetComponent<RectTransform>().localPosition.x = ;
    }

    /// <summary>
    /// �|�[�Y�I�������UI��߂�
    /// </summary>
    private void FinUIMove() {
        Camera camera = Camera.main; // ���C���J�������w��

        // �^�C���ŉ�ʂ̕\���ʒu�̊�����ς���
        UIBasePosx -= Time.deltaTime * UIMoveSpeed;
        //UIPosx = UIBasePosx * 1920 + UIPosx;

        if (UIBasePosx < 0.0f)  // ��O����
        {
            UIBasePosx = 0.0f;
        }
        //if (UIPosx < -1390.0f)
        //{
        //    UIPosx = -1390.0f;
        //}

        camera.rect = new Rect(UIBasePosx, 0.0f, 1.0f - UIBasePosx, 1.0f);
        //PauseFrame.GetComponent<RectTransform>().localPosition = new Vector3(PauseFrame.GetComponent<RectTransform>().localPosition.x + UIPosx, 0, 0);
    }

}