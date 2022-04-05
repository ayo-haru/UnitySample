using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour {
   private enum eSTATETITLE // �^�C�g���V�[������s������
    {
        FROMBIGINING = 0,   // ���߂���
        FROMCONTINUE,       // ��������
        QUIT,               // ��߂�
       
        MAX_STATE
    }



    //---�ϐ��錾
    private Game_pad UIActionAssets;                // InputAction��UI������
    private InputAction LeftStickSelect;            // InputAction��select������
    private InputAction RightStickSelect;           // InputAction��select������

    private Vector2 doLeftStick = Vector2.zero;
    private Vector2 doRightStick = Vector2.zero;

    private int select;                             // �I������Ă��郂�[�h�̔ԍ� 

    private bool isPressButton = false;             // �{�^���������ꂽ���̔���p
    private bool oncePressButton = false;           // �{�^���������ꂽ�Ƃ��Ɉ�񂾂�����������p

    private GameObject TitleLogo;
    private GameObject PressAnyButton;
    private GameObject GameStart;
    private GameObject GameContinue;
    private GameObject GameEnd;

    private void Awake()
    {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
        select = (int)eSTATETITLE.FROMBIGINING;     // �����l�͂͂��߂���      

        Application.targetFrameRate = 60;           // �t���[�����[�g���Œ�
        
        //----- �V�[�� -----
        SaveManager.load();
        GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
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
        GameStart.GetComponent<UIBlink>().isHide = true;        // �Q�[�����n�܂����u�Ԃ͗v��Ȃ��̂ŏ���
        GameContinue.GetComponent<UIBlink>().isHide = true;
        GameEnd.GetComponent<UIBlink>().isHide = true;

    }


    // Update is called once per frame
    void Update() {
        // �����{�^���������ꂽ��
        if (Input.anyKeyDown && !isPressButton)
        {
            isPressButton = true;
            PressAnyButton.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            PressAnyButton.GetComponent<UIBlink>().isHide = true;   // �{�^���������ꂽ�����
            TitleLogo.GetComponent<UIBlink>().isHide = true;
            GameStart.GetComponent<UIBlink>().isHide = false;       // �{�^���������ꂽ��\������
            GameContinue.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;


            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
        }


        // �v���X�{�^������Ă��Ȃ�������ȉ��̏��������Ȃ�
        if (!isPressButton)
        {
            PressAnyButton.GetComponent<UIBlink>().isBlink = true;
            return;
        }


        // �v���X�{�^�����ꂽ�Ƃ������[�h�̑I���ɂȂ�̂�
        // ���L�[�őI��������
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
            select--;
            if (select < 0)
            {
                select = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);

            select++;
            if (select >= (int)eSTATETITLE.MAX_STATE)
            {
                select = (int)eSTATETITLE.QUIT;
            }
        }


        // �I�����Ă�����̂������ŕ���
        if (select == (int)eSTATETITLE.FROMBIGINING)
        {
            GameStart.GetComponent<UIBlink>().isBlink = true; // UI��_��
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����

            if (Input.GetKeyUp(KeyCode.Return)) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                GameData.InitData();

                // �V�[���֘A
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else if (select == (int)eSTATETITLE.FROMCONTINUE)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UI��_��
            GameContinue.GetComponent<UIBlink>().isBlink = true; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����

            if (Input.GetKeyUp(KeyCode.Return)) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

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

            if (Input.GetKeyUp(KeyCode.Return)) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

        }
    }

    private void OnEnable() {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Action�C�x���g��o�^
        UIActionAssets.UI.LeftStickSelect.performed += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.performed += OnRightStick;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj)
    {
        //---���X�e�b�N�̃X�e�b�N���͂��擾
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if(doLeftStick.x >=0.1f || doLeftStick.y >= 0.1f )
        {
            Debug.Log("���X�e�B�b�N���|���ꂽ");
            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);

        }
    }

    private void OnRightStick(InputAction.CallbackContext obj)
    {
        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.x >= 0.1f || doRightStick.y >= 0.1f)
        {
            Debug.Log("�E�X�e�B�b�N���|���ꂽ");

            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);

        }
    }

    private void OnGUI()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        //---�Q�[���p�b�h�ƂȂ����Ă��鎞�ɕ\�������B
        GUILayout.Label($"LeftStick:{Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"RightStick:{Gamepad.current.rightStick.ReadValue()}");
        GUILayout.Label($"ButtonNorth:{Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"ButtonSouth:{Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"ButtonEast:{Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"ButtonWast:{Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"LeftShoulder:{Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"LeftTrigger:{Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"RightShoulder:{Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"RighetTrigger:{Gamepad.current.rightTrigger.ReadValue()}");
        GUILayout.Label($"LeftStickUp:{Gamepad.current.leftStick.up.ReadValue()}");
        GUILayout.Label($"Space:{Keyboard.current.spaceKey.ReadValue()}");
        GUILayout.Label($"LeftStick:{doLeftStick}");

    }

}



