//=============================================================================
//
// �Z�[�u�|�C���g�ɂ��Z�[�u��UI�Ǘ�
//
// �쐬��:2022/04
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/04/19 �Z�[�u�|�C���g�ɂ��Z�[�u����
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Save_UI : MonoBehaviour
{
    // �\������UI�̕ϐ�
    [SerializeField]
    private GameObject savecharacter;
    private GameObject SaveCharacter;
    [SerializeField]
    private GameObject yescharacter;
    private GameObject YesCharacter;
    [SerializeField]
    private GameObject nocharacter;
    private GameObject NoCharacter;
    [SerializeField]
    private GameObject stick;
    private GameObject Stick;

    private Canvas canvas;  // ���̃V�[���̃L�����o�X��ۑ�

    private int select; // �I����ۑ�

    private Game_pad UIActionAssets;                // InputAction��UI������
    private InputAction LeftStickSelect;            // InputAction��select������
    private InputAction RightStickSelect;           // InputAction��select������


    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�

        select = 0; // �I����������

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();

        // ���ԉ�
        SaveCharacter = Instantiate(savecharacter);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);
        Stick = Instantiate(stick);

        // �L�����o�X�̎q�ɂ���
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);
        Stick.transform.SetParent(this.canvas.transform,false);

        // �ʏ�͔�\��
        SaveCharacter.GetComponent<UIBlink>().isHide = true;
        YesCharacter.GetComponent<UIBlink>().isHide = true;
        NoCharacter.GetComponent<UIBlink>().isHide = true;
        Stick.GetComponent<UIBlink>().isHide = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ���͏���������
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;


        if (!SaveManager.canSave)
        {
            // �Z�[�u�\�łȂ��Ƃ���UI���B��
            SaveCharacter.GetComponent<UIBlink>().isHide = true;
            YesCharacter.GetComponent<UIBlink>().isHide = true;
            NoCharacter.GetComponent<UIBlink>().isHide = true;
            YesCharacter.GetComponent<UIBlink>().isBlink = false;
            NoCharacter.GetComponent<UIBlink>().isBlink = false;
            Stick.GetComponent<UIBlink>().isHide = true;
            if (Player.isHitSavePoint)  // �Z�[�u�|�C���g�ɓ��������瑀����@��\��
            {
                Stick.GetComponent<UIBlink>().isHide = false;
            }

            return; // �Z�[�u�ł��Ȃ��Ƃ��͈ȉ��̏����͕K�v�Ȃ��̂ŕԂ�
        }
        else
        {
            // �Z�[�u�\�ɂȂ�����UI��\��
            SaveCharacter.GetComponent<UIBlink>().isHide = false;
            YesCharacter.GetComponent<UIBlink>().isHide = false;
            NoCharacter.GetComponent<UIBlink>().isHide = false;
            Stick.GetComponent<UIBlink>().isHide = true;

            Pause.isPause = true;   // UI�\�����̓|�[�Y
        }


        // �Z�[�u�\�ɂȂ�����I��������
        if (keyboard.leftArrowKey.wasReleasedThisFrame)
        {
            SelectUp();
            return;
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                SelectUp();
                return;
            }
        }
        if (keyboard.rightArrowKey.wasReleasedThisFrame)
        {
            SelectDown();
            return;
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                SelectDown();
                return;
            }
        }


        // �I�����Ă�����̂������ŕ���
        if (select == 0)
        {
            YesCharacter.GetComponent<UIBlink>().isBlink = true;    // UI��_��
            NoCharacter.GetComponent<UIBlink>().isBlink = false;    // �_�ł�����

            if (keyboard.enterKey.wasReleasedThisFrame) // �I�����m��
            {
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // ���艹
                Pause.isPause = false;  // �|�[�Y��߂�
                SaveManager.canSave = false;    // �Z�[�u�\����
                SaveManager.shouldSave = true;  // �Z�[�u����ׂ��Ȃ̂Ńt���O�𗧂Ă�
                YesCharacter.GetComponent<UIBlink>().isHide = true; // UI�\�����B��
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // ���艹
                    Pause.isPause = false;  // �|�[�Y��߂�
                    SaveManager.canSave = false;    // �Z�[�u�\����
                    SaveManager.shouldSave = true;  // �Z�[�u����ׂ��Ȃ̂Ńt���O�𗧂Ă�
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UI�\�����B��
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    YesCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
                }
            }
        }
        else
        {
            YesCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
            NoCharacter.GetComponent<UIBlink>().isBlink = true;     // UI��_��

            if (keyboard.enterKey.wasReleasedThisFrame) // �I�����m��
            {
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // ���艹
                SaveManager.canSave = false;    // �Z�[�u�\������
                Pause.isPause = false;  // �|�[�Y����߂�
                YesCharacter.GetComponent<UIBlink>().isHide = true; // UI������
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;    // UI�̓_�ł�����
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // ���艹
                    SaveManager.canSave = false;    // �Z�[�u�\������
                    Pause.isPause = false;  // �|�[�Y����߂�
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UI������
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;    // UI�̓_�ł�����
                }
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
        if (select > 1)
        {
            select = 0;
        }
    }

    private void OnEnable() {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Action�C�x���g��o�^
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!SaveManager.canSave)
        {
            return;
        }

        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.x > 0.0f)
        {
            SelectDown();
        }
        else if (doLeftStick.x < 0.0f)
        {
            SelectUp();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!SaveManager.canSave)
        {
            return;
        }

        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.x > 0.0f)
        {
            SelectDown();
        }
        else if (doRightStick.x < 0.0f)
        {
            SelectUp();
        }

    }

}
