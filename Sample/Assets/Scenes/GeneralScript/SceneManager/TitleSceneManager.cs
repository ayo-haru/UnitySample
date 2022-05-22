using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour {
    private enum eSTATETITLE // �^�C�g���V�[������s������
    {
        FROMBIGINING = 0,   // ���߂���
        FROMCONTINUE,       // ��������
        QUIT,               // ��߂�
        OPTION,             // �I�v�V����

        MAX_STATE
    }



    //---�ϐ��錾
    private Game_pad UIActionAssets;                // InputAction��UI������
    private InputAction LeftStickSelect;            // InputAction��select������
    private InputAction RightStickSelect;           // InputAction��select������


    public int select;                             // �I������Ă��郂�[�h�̔ԍ� 
    private bool isDecision;


    private bool isPressButton = false;             // �{�^���������ꂽ���̔���p
    //private bool oncePressButton = false;           // �{�^���������ꂽ�Ƃ��Ɉ�񂾂�����������p
    //private bool onceTiltStick = false;

    private GameObject TitleLogo;
    private GameObject PressAnyButton;
    private GameObject GameStart;
    private GameObject GameContinue;
    private GameObject GameEnd;
    private GameObject Option;
    private GameObject Optionmanager;
    private GameObject SelectFrame;

    private TitlePlayer titlePlayer;
    private bool weight = false;    //���o�҂����ǂ���
    private int timer = 0;

    private void Awake() {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
        select = (int)eSTATETITLE.FROMBIGINING;     // �����l�͂͂��߂���
        isDecision = false;


        Application.targetFrameRate = 60;           // �t���[�����[�g���Œ�

        //----- �V�[�� -----
        SaveManager.load();
        GameData.LoadData();
        GameData.CurrentMapNumber = (int)GameData.eSceneState.TITLE_SCENE;

        //----- �T�E���h -----
        for (int i = 0; i < SoundData.TitleAudioList.Length; ++i)
        {
            SoundData.TitleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager.Play(SoundData.eBGM.BGM_TITLE, SoundData.TitleAudioList);// ���炷

        //----- UI -----
        TitleLogo = GameObject.Find("Titlelogo");               // UI�������Ċi�[���Ă���
        PressAnyButton = GameObject.Find("PressAnyButton");
        GameStart = GameObject.Find("GameStart");
        GameContinue = GameObject.Find("GameContinue");
        GameEnd = GameObject.Find("GameEnd");
        Option = GameObject.Find("Option");
        Optionmanager = GameObject.Find("OptionManager");
        SelectFrame = GameObject.Find("SelectFrame");
        GameStart.GetComponent<UIBlink>().isHide = true;        // �Q�[�����n�܂����u�Ԃ͗v��Ȃ��̂ŏ���
        //GameStart.GetComponent<Move2DTheta>().enabled = false;
        GameContinue.GetComponent<UIBlink>().isHide = true;
        //GameContinue.GetComponent<Move2DTheta>().enabled = false;
        GameEnd.GetComponent<UIBlink>().isHide = true;
        //GameEnd.GetComponent<Move2DTheta>().enabled = false;
        Option.GetComponent<UIBlink>().isHide = true;
        //Option.GetComponent<Move2DTheta>().enabled = false;
        Optionmanager.SetActive(false);
        SelectFrame.GetComponent<UIBlink>().isHide = true;
        SelectFrame.GetComponent<UIBlink>().isBlink = false;

        //---�^�C�g���p�v���C���[�擾
        titlePlayer = GameObject.Find("Rulaby").GetComponent<TitlePlayer>();

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

        // �����{�^���������ꂽ��
        if ((keyboard.anyKey.wasReleasedThisFrame || GamePadManager.ReleaseAnyButton(GamePadManager.eGamePadType.ALLTYPE)) && !isPressButton)
        {
            isPressButton = true;
            PressAnyButton.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            PressAnyButton.GetComponent<UIBlink>().isHide = true;   // �{�^���������ꂽ�����
            TitleLogo.GetComponent<UIBlink>().isHide = true;
            GameStart.GetComponent<UIBlink>().isHide = false;       // �{�^���������ꂽ��\������
            GameContinue.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;
            Option.GetComponent<UIBlink>().isHide = false;
            SelectFrame.GetComponent<UIBlink>().isHide = false;
            SelectFrame.GetComponent<RectTransform>().position = GameStart.GetComponent<RectTransform>().position;
            SelectFrame.GetComponent<UI_Parry>().enabled = true;
            GameStart.GetComponent<UI_Parry>().enabled = true;


            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

            titlePlayer.pressAnyButtonFlag = true;

            return;
        }


        // �v���X�{�^������Ă��Ȃ�������ȉ��̏��������Ȃ�
        if (!isPressButton)
        {
            PressAnyButton.GetComponent<UIBlink>().isBlink = true;
            return;
        }


        // �v���X�{�^�����ꂽ�Ƃ������[�h�̑I���ɂȂ�̂�
        // ���L�[�őI��������
        //�I�v�V�����J���Ă�Ƃ��͖����ɂ���
        if (!Optionmanager.activeSelf)
        {
            if (keyboard.leftArrowKey.wasReleasedThisFrame)
            {
                SelectUp();
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
                {
                    SelectUp();
                }
            }

            if (keyboard.rightArrowKey.wasReleasedThisFrame)
            {
                SelectDown();
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
                {
                    SelectDown();
                }
            }
        }




        // �I�����Ă�����̂������ŕ���
        if (select == (int)eSTATETITLE.FROMBIGINING)
        {
            GameStart.GetComponent<UIBlink>().isBlink = true; // UI��_��
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            Option.GetComponent<UIBlink>().isBlink = false;     //UI�̓_�ł�����

            SelectFrame.GetComponent<RectTransform>().position = GameStart.GetComponent<RectTransform>().position;  //�I��g�̈ʒu�ݒ�

            GameStart.GetComponent<UI_Parry>().enabled = true;  //�㉺�ړ�����
            GameContinue.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ��L��
            GameEnd.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            Option.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����

            if (isDecision)
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                // �I��g���B��
                //SelectFrame.GetComponent<UIBlink>().isHide = true;

                weight = true;
                titlePlayer.decisionFlag = true;
                isDecision = false;

            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
                // ���ׂẴf�[�^�̏�����
                GameData.InitData();

                // �V�[���֘A
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial1;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }

        }
        else if (select == (int)eSTATETITLE.FROMCONTINUE)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UI��_��
            GameContinue.GetComponent<UIBlink>().isBlink = true; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            Option.GetComponent<UIBlink>().isBlink = false;     //UI�̓_�ł�����

            SelectFrame.GetComponent<RectTransform>().position = GameContinue.GetComponent<RectTransform>().position;  //�I��g�̈ʒu�ݒ�

            GameStart.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            GameContinue.GetComponent<UI_Parry>().enabled = true;  //�㉺�ړ��L��
            GameEnd.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            Option.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����

            if (isDecision)
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                titlePlayer.decisionFlag = true;
                isDecision = false;
                weight = true;
            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
                // �V�[���֘A
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else if (select == (int)eSTATETITLE.QUIT)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UI��_��
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = true; // UI�̓_�ł�����
            Option.GetComponent<UIBlink>().isBlink = false;     //UI�̓_�ł�����

            SelectFrame.GetComponent<RectTransform>().position = GameEnd.GetComponent<RectTransform>().position;  //�I��g�̈ʒu�ݒ�

            GameStart.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            GameContinue.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            GameEnd.GetComponent<UI_Parry>().enabled = true;  //�㉺�ړ��L��
            Option.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����

            if (isDecision)
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                isDecision = false;
                weight = true;
                titlePlayer.decisionFlag = true;
            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }



        }
        else if (select == (int)eSTATETITLE.OPTION)
        {
            Option.GetComponent<UIBlink>().isBlink = true;     //UI��_��
            GameStart.GetComponent<UIBlink>().isBlink = false; // UI��_�ł�����
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����

            SelectFrame.GetComponent<RectTransform>().position = Option.GetComponent<RectTransform>().position;  //�I��g�̈ʒu�ݒ�

            GameStart.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            GameContinue.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            GameEnd.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ�����
            Option.GetComponent<UI_Parry>().enabled = true;  //�㉺�ړ��L��

            if (isDecision)
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                titlePlayer.decisionFlag = true;
                isDecision = false;
                //���o�҂�
                weight = true;

            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
                //�I�v�V�����}�l�[�W���[���A�N�e�B�u�ɂ���
                Optionmanager.SetActive(true);
                Option.GetComponent<UI_Parry>().enabled = false;  //�㉺�ړ��L��
                SelectFrame.GetComponent<UIBlink>().isHide = false;
                SelectFrame.GetComponent<UIParry>().enabled = true;
            }



        }
    }

    private void SelectUp() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);

        select++;
        if (select >= (int)eSTATETITLE.MAX_STATE)
        {
            //select = (int)eSTATETITLE.QUIT;
            select = (int)eSTATETITLE.OPTION;
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

    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }

        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.x < 0.0f)
        {
            SelectUp();
        }
        else if (doLeftStick.x > 0.0f)
        {
            SelectDown();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }

        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.x < 0.0f)
        {
            SelectUp();
        }
        else if (doRightStick.x > 0.0f)
        {
            SelectDown();
        }

    }

    /// <summary>
    /// ����{�^��
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj) {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }
        isDecision = true;
    }

}


