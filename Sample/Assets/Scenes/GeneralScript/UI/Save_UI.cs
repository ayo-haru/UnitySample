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
    private GameObject warpcharacter;
    private GameObject WarpCharacter;
    [SerializeField]
    private GameObject susumucharacter;
    private GameObject SusumuCharacter;

    [SerializeField]
    private GameObject stick;
    private GameObject Stick;

    private Canvas canvas;                  // ���̃V�[���̃L�����o�X��ۑ�

    private int select;                     // �I����ۑ�
    private bool isDecision;                // ���肳�ꂽ

    private Game_pad UIActionAssets;        // InputAction��UI������
    private InputAction LeftStickSelect;    // InputAction��select������
    private InputAction RightStickSelect;   // InputAction��select������


    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�

        select = 0; // �I����������
        isDecision = false; // �����������

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();    // �L�����o�X��ۑ�

        // ���ԉ�
        SaveCharacter = Instantiate(savecharacter);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);
        Stick = Instantiate(stick);
        WarpCharacter = Instantiate(warpcharacter);
        SusumuCharacter = Instantiate(susumucharacter);

        // �L�����o�X�̎q�ɂ���
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);
        Stick.transform.SetParent(this.canvas.transform,false);
        WarpCharacter.transform.SetParent(this.canvas.transform, false);
        SusumuCharacter.transform.SetParent(this.canvas.transform, false);

        // �ʏ�͔�\��
        SaveCharacter.GetComponent<UIBlink>().isHide = true;
        YesCharacter.GetComponent<UIBlink>().isHide = true;
        NoCharacter.GetComponent<UIBlink>().isHide = true;
        Stick.GetComponent<UIBlink>().isHide = true;
        WarpCharacter.GetComponent<UIBlink>().isHide = true;
        SusumuCharacter.GetComponent<UIBlink>().isHide = true;

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

        if (Player.HitSavePointColorisRed)
        {
            //----- ���[�v -----
            if (!Warp.canWarp)
            {
                // �Z�[�u�͔�\��
                SaveCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ���[�v�͉B��
                WarpCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isBlink = false;

                if (Player.isHitSavePoint)  // �Z�[�u�|�C���g�ɓ��������瑀����@��\��
                {
                    Stick.GetComponent<UIBlink>().isHide = false;
                }

                return; // �Z�[�u�ł��Ȃ��Ƃ��͈ȉ��̏����͕K�v�Ȃ��̂ŕԂ�
            }
            else
            {
                GamePadManager.onceTiltStick = false;
                // �Z�[�u�͔�\��
                SaveCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ���[�v�͕\��
                WarpCharacter.GetComponent<UIBlink>().isHide = false;
                SusumuCharacter.GetComponent<UIBlink>().isHide = false;
                NoCharacter.GetComponent<RectTransform>().localPosition = new Vector3(327.0f,115.0f,0.0f);
                NoCharacter.GetComponent<UIBlink>().isHide = false;


                //Pause.isPause = true;   // UI�\�����̓|�[�Y
                Debug.Log("UI�\�����|�[�Y");
            }
            // ���[�v�\�ɂȂ�����I��������
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

            // �I���̌���
            if (select == 0)
            {
                SusumuCharacter.GetComponent<UIBlink>().isBlink = true;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // ���艹
                    Pause.isPause = false;  // �|�[�Y��߂�
                    Debug.Log("���[�v����̉���");
                    Warp.canWarp = false;    // ���[�v�\����
                    Warp.shouldWarp = true;  // ���[�v����ׂ��Ȃ̂Ńt���O�𗧂Ă�
                    SusumuCharacter.GetComponent<UIBlink>().isHide = true; // UI�\�����B��
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    SusumuCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
                    isDecision = false;
                    
                }
            }
            else
            {
                SusumuCharacter.GetComponent<UIBlink>().isBlink = false;
                NoCharacter.GetComponent<UIBlink>().isBlink = true;

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // ���艹
                    Warp.canWarp = false;    // ���[�v�\����
                    Pause.isPause = false;  // �|�[�Y����߂�
                    Debug.Log("���[�v���Ȃ��̉���");
                    SusumuCharacter.GetComponent<UIBlink>().isHide = true; // UI�\�����B��
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
                    isDecision = false;
                }
            }

        }
        else
        {
            //----- �Z�[�u -----
            if (!SaveManager.canSave)
            {
                // �Z�[�u�\�łȂ��Ƃ���UI���B��
                SaveCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ���[�v�͉B��
                WarpCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isHide = true;

                if (Player.isHitSavePoint)  // �Z�[�u�|�C���g�ɓ��������瑀����@��\��
                {
                    Stick.GetComponent<UIBlink>().isHide = false;
                }

                return; // �Z�[�u�ł��Ȃ��Ƃ��͈ȉ��̏����͕K�v�Ȃ��̂ŕԂ�
            }
            else
            {
                GamePadManager.onceTiltStick = false;

                // �Z�[�u�\�ɂȂ�����UI��\��
                SaveCharacter.GetComponent<UIBlink>().isHide = false;
                YesCharacter.GetComponent<UIBlink>().isHide = false;
                NoCharacter.GetComponent<RectTransform>().localPosition = new Vector3(146.0f, 115.0f, 0.0f);
                NoCharacter.GetComponent<UIBlink>().isHide = false;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ���[�v�͔�\��
                WarpCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isHide = true;


                //Pause.isPause = true;   // UI�\�����̓|�[�Y
                Debug.Log("�Z�[�uUI�\�����̃|�[�Y");
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

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // ���艹
                    Pause.isPause = false;  // �|�[�Y��߂�
                    Debug.Log("�Z�[�u����̃|�[�Y");
                    SaveManager.canSave = false;    // �Z�[�u�\����
                    SaveManager.shouldSave = true;  // �Z�[�u����ׂ��Ȃ̂Ńt���O�𗧂Ă�
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UI�\�����B��
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    YesCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
                    isDecision = false;
                }
            }
            else
            {
                YesCharacter.GetComponent<UIBlink>().isBlink = false;   // �_�ł�����
                NoCharacter.GetComponent<UIBlink>().isBlink = true;     // UI��_��

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // ���艹
                    Pause.isPause = false;  // �|�[�Y����߂�
                    Debug.Log("�Z�[�u���Ȃ��̃|�[�Y");
                    SaveManager.canSave = false;    // �Z�[�u�\������
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UI������
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;    // UI�̓_�ł�����
                    isDecision = false;
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
        UIActionAssets.UI.Decision.started += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (SaveManager.canSave || Warp.canWarp)
        {
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
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (SaveManager.canSave || Warp.canWarp)
        {
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

    /// <summary>
    /// ����{�^��
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if (SaveManager.canSave || Warp.canWarp)
        {
            isDecision = true;
        }
    }
}
