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


    Canvas canvas;
    Canvas canvas2;

    private bool isDecision;

    private Game_pad UIActionAssets;                // InputAction��UI������
    private InputAction LeftStickSelect;            // InputAction��select������
    private InputAction RightStickSelect;           // InputAction��select������


    private int select;
    private bool isCalledOncce = false;

    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�

        select = (int)eSTATEPAUSE.RETURNGAME;

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();
        if(GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        }
        // ���ԉ�
        PauseCharacter = Instantiate(pausecharacter);
        BackGame = Instantiate(backgame);
        GameEnd = Instantiate(gameend);
        BackTitle = Instantiate(backtitle);
        Option = Instantiate(option);
        Optionmanager = Instantiate(optionmanager);
        Panel = Instantiate(panel);
        

        // �L�����o�X�̎q�ɂ���
        Panel.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);
        BackGame.transform.SetParent(this.canvas.transform, false);
        GameEnd.transform.SetParent(this.canvas.transform, false);
        BackTitle.transform.SetParent(this.canvas.transform, false);
        Option.transform.SetParent(this.canvas.transform, false);
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Optionmanager.transform.SetParent(this.canvas2.transform, false);
        }
        else
        {
            Optionmanager.transform.SetParent(this.canvas.transform, false);
        }
        

        // �Q�[���X�^�[�g���͔�\��
        PauseCharacter.GetComponent<UIBlink>().isHide = true;
        BackGame.GetComponent<UIBlink>().isHide = true;
        GameEnd.GetComponent<UIBlink>().isHide = true;
        BackTitle.GetComponent<UIBlink>().isHide = true;
        Option.GetComponent<UIBlink>().isHide = true;
        BackGame.GetComponent<UIBlink>().isBlink = false;
        GameEnd.GetComponent<UIBlink>().isBlink = false;
        BackTitle.GetComponent<UIBlink>().isBlink = false;
        Option.GetComponent<UIBlink>().isBlink = false;
        Panel.GetComponent<Image>().enabled = false;
        Optionmanager.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;

        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut)
        {
            //���y�Đ�
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE)
            {
                GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                if (kitchenBgmObject)
                {
                    kitchenBgmObject.GetComponent<AudioSource>().UnPause();
                }
            }

            PauseCharacter.GetComponent<UIBlink>().isHide = true;
            BackGame.GetComponent<UIBlink>().isHide = true;
            GameEnd.GetComponent<UIBlink>().isHide = true;
            BackTitle.GetComponent<UIBlink>().isHide = true;
            Option.GetComponent<UIBlink>().isHide = true;
            BackGame.GetComponent<UIBlink>().isBlink = false;
            GameEnd.GetComponent<UIBlink>().isBlink = false;
            BackTitle.GetComponent<UIBlink>().isBlink = false;
            Option.GetComponent<UIBlink>().isBlink = false;

            Panel.GetComponent<Image>().enabled = false;
            isCalledOncce = false;

            return;
        }
        else if (Pause.isPause && !isCalledOncce)
        {
            // �|�[�Y���ɂȂ�����\��
            PauseCharacter.GetComponent<UIBlink>().isHide = false;
            BackGame.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;
            BackTitle.GetComponent<UIBlink>().isHide = false;
            Option.GetComponent<UIBlink>().isHide = false;
            Panel.GetComponent<Image>().enabled = true;

            //���y��~
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE)
            {
                GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                if (kitchenBgmObject)
                {
                    kitchenBgmObject.GetComponent<AudioSource>().Pause();
                }
            }

            isCalledOncce = true;
        }

        //�I�v�V�������J���Ă�Ԃ͖����ɂ���
        if (!Optionmanager.activeSelf)
        {
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
        

        // �I�����Ă�����̂������ŕ���
        if (select == (int)eSTATEPAUSE.RETURNGAME)
        {
            BackGame.GetComponent<UIBlink>().isBlink = true; // UI��_��
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            Option.GetComponent<UIBlink>().isBlink = false; //UI�̓_�ł�����

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                Pause.isPause = false;  // �|�[�Y����
                Debug.Log("�|�[�Y�����̃|�[�Y����");
                backgame.GetComponent<UIBlink>().isHide = true;
                BackGame.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.RETURNTITLE)
        {
            BackGame.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            BackTitle.GetComponent<UIBlink>().isBlink = true; // UI��_��
            GameEnd.GetComponent<UIBlink>().isBlink = false;  // UI�̓_�ł�����
            Option.GetComponent<UIBlink>().isBlink = false; //UI�̓_�ł�����

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                // �V�[���֘A
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                SceneManager.LoadScene(nextSceneName);
                //Pause.isPause = false;  // �|�[�Y����
                Debug.Log("�V�[���J�ڂ̃|�[�Y����");
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            BackGame.GetComponent<UIBlink>().isBlink = false;  // UI�̓_�ł�����
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = true;    // UI��_��
            Option.GetComponent<UIBlink>().isBlink = false; //UI�̓_�ł�����

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                isDecision = false;
                //Pause.isPause = false;  //�@�|�[�Y����
                Debug.Log("�Q�[����߂�̃|�[�Y");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

            }

        }else if(select == (int)eSTATEPAUSE.OPTION)
        {
            BackGame.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UI��_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false;  // UI�̓_�ł�����
            Option.GetComponent<UIBlink>().isBlink = true; //UI�̓_��

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                //�I�v�V�����}�l�[�W���[���A�N�e�B�u�ɂ���
                Optionmanager.SetActive(true);
                isDecision = false;
            }
        }
    }

    private void SelectUp() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select >= (int)eSTATEPAUSE.MAX_STATE)
        {
            select = (int)eSTATEPAUSE.OPTION;
        }
    }

    private void OnEnable() {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Action�C�x���g��o�^
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        UIActionAssets.UI.Decision.started += OnDecision;


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
        if (!Pause.isPause || Optionmanager.activeSelf)
        {
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
        if (!Pause.isPause || Optionmanager.activeSelf)
        {
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
        if (Pause.isPause)
        {
            isDecision = true;
        }
    }

}