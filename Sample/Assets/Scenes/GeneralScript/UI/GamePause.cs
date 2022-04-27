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
    private GameObject panel;
    private GameObject Panel;


    Canvas canvas;

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

        // ���ԉ�
        PauseCharacter = Instantiate(pausecharacter);
        BackGame = Instantiate(backgame);
        GameEnd = Instantiate(gameend);
        BackTitle = Instantiate(backtitle);
        Panel = Instantiate(panel);
        

        // �L�����o�X�̎q�ɂ���
        Panel.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);
        BackGame.transform.SetParent(this.canvas.transform, false);
        GameEnd.transform.SetParent(this.canvas.transform, false);
        BackTitle.transform.SetParent(this.canvas.transform, false);

        // �Q�[���X�^�[�g���͔�\��
        PauseCharacter.GetComponent<UIBlink>().isHide = true;
        BackGame.GetComponent<UIBlink>().isHide = true;
        GameEnd.GetComponent<UIBlink>().isHide = true;
        BackTitle.GetComponent<UIBlink>().isHide = true;
        Panel.GetComponent<Image>().enabled = false;
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

        if (!Pause.isPause/* && !GameData.isFadeIn && !GameData.isFadeOut && !SaveManager.canSave && !Warp.shouldWarp*/)
        {
            PauseCharacter.GetComponent<UIBlink>().isHide = true;
            BackGame.GetComponent<UIBlink>().isHide = true;
            GameEnd.GetComponent<UIBlink>().isHide = true;
            BackTitle.GetComponent<UIBlink>().isHide = true;
            Panel.GetComponent<Image>().enabled = false;
            isCalledOncce = false;

            return;
        }
        else if (Pause.isPause  && !isCalledOncce)
        {
            // �|�[�Y���ɂȂ�����\��
            PauseCharacter.GetComponent<UIBlink>().isHide = false;
            BackGame.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;
            BackTitle.GetComponent<UIBlink>().isHide = false;
            Panel.GetComponent<Image>().enabled = true;

            isCalledOncce = true;
        }

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

        // �I�����Ă�����̂������ŕ���
        if (select == (int)eSTATEPAUSE.RETURNGAME)
        {
            BackGame.GetComponent<UIBlink>().isBlink = true; // UI��_��
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                Pause.isPause = false;
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

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                // �V�[���֘A
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                SceneManager.LoadScene(nextSceneName);
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            BackGame.GetComponent<UIBlink>().isBlink = false;  // UI�̓_�ł�����
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UI�̓_�ł�����
            GameEnd.GetComponent<UIBlink>().isBlink = true;    // UI��_��

            if (isDecision) // �I�����m��
            {
                // ���艹
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                isDecision = false;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

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
            select = (int)eSTATEPAUSE.QUITGAME;
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
        if (!Pause.isPause)
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
        if (!Pause.isPause)
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