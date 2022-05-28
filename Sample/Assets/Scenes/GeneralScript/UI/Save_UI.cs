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
    private GameObject abutton;
    private GameObject AButton;

    [SerializeField]
    private GameObject selectbox;
    private GameObject SelectBox;

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
        SelectBox = Instantiate(selectbox);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);
        AButton = Instantiate(abutton);
        WarpCharacter = Instantiate(warpcharacter);
        SusumuCharacter = Instantiate(susumucharacter);

        // �L�����o�X�̎q�ɂ���
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        SelectBox.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);
        AButton.transform.SetParent(this.canvas.transform,false);
        WarpCharacter.transform.SetParent(this.canvas.transform, false);
        SusumuCharacter.transform.SetParent(this.canvas.transform, false);

        // �ʏ�͔�\��        
        SelectBox.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
        SelectBox.GetComponent<UIBlink>().isBlink = true;
        SelectBox.GetComponent<UIBlink>().isHide = true;
        SelectBox.GetComponent<Image>().enabled = false;
        SaveCharacter.GetComponent<Image>().enabled = false;
        YesCharacter.GetComponent<Image>().enabled = false;
        NoCharacter.GetComponent<Image>().enabled = false;
        AButton.GetComponent<UIBlink>().isHide = true;
        WarpCharacter.GetComponent<Image>().enabled = false;
        SusumuCharacter.GetComponent<Image>().enabled = false;

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
                // ���ׂĔ�\��
                SelectBox.GetComponent<UIBlink>().isBlink = true;
                SelectBox.GetComponent<UIBlink>().isHide = true;
                SelectBox.GetComponent<Image>().enabled = false;
                SaveCharacter.GetComponent<Image>().enabled = false;
                YesCharacter.GetComponent<Image>().enabled = false;
                NoCharacter.GetComponent<Image>().enabled = false;
                AButton.GetComponent<Image>().enabled = false;
                WarpCharacter.GetComponent<Image>().enabled = false;
                SusumuCharacter.GetComponent<Image>().enabled = false;

                if (Player.isHitSavePoint)  // �Z�[�u�|�C���g�ɓ��������瑀����@��\��
                {
                    AButton.GetComponent<UIBlink>().isHide = false;
                }

                return; // �Z�[�u�ł��Ȃ��Ƃ��͈ȉ��̏����͕K�v�Ȃ��̂ŕԂ�
            }
            else
            {
                //GamePadManager.onceTiltStick = false;
                SelectBox.GetComponent<Image>().enabled = true;
                // �Z�[�u�͔�\��
                SaveCharacter.GetComponent<Image>().enabled = false;
                YesCharacter.GetComponent<Image>().enabled = false;
                AButton.GetComponent<UIBlink>().isHide = true;
                // ���[�v�͕\��
                WarpCharacter.GetComponent<Image>().enabled = true;
                SusumuCharacter.GetComponent<Image>().enabled = true;
                NoCharacter.GetComponent<Image>().enabled = true;

                SelectBoxPosUpdete();   // �I��g�̍X�V
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
            if (select == 0)    // ���[�v�͂�
            {
                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // ���艹
                    Pause.isPause = false;  // �|�[�Y��߂�
                    Warp.canWarp = false;    // ���[�v�\����
                    Warp.shouldWarp = true;  // ���[�v����ׂ��Ȃ̂Ńt���O�𗧂Ă�
                    isDecision = false;
                    
                }
            }
            else
            {
                if (isDecision) // ���[�v������
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // ���艹
                    Warp.canWarp = false;    // ���[�v�\����
                    Pause.isPause = false;  // �|�[�Y����߂�
                    isDecision = false;
                }
            }

        }
        else
        {
            //----- �Z�[�u -----
            if (!SaveManager.canSave)
            {
                // ���ׂĔ�\��
                SelectBox.GetComponent<UIBlink>().isBlink = true;
                SelectBox.GetComponent<UIBlink>().isHide = true;
                SelectBox.GetComponent<Image>().enabled = false;
                SaveCharacter.GetComponent<Image>().enabled = false;
                YesCharacter.GetComponent<Image>().enabled = false;
                NoCharacter.GetComponent<Image>().enabled = false;
                AButton.GetComponent<UIBlink>().isHide = true;
                WarpCharacter.GetComponent<Image>().enabled = false;
                SusumuCharacter.GetComponent<Image>().enabled = false;

                if (Player.isHitSavePoint)  // �Z�[�u�|�C���g�ɓ��������瑀����@��\��
                {
                    AButton.GetComponent<UIBlink>().isHide = false;
                }

                return; // �Z�[�u�ł��Ȃ��Ƃ��͈ȉ��̏����͕K�v�Ȃ��̂ŕԂ�
            }
            else
            {
                //GamePadManager.onceTiltStick = false;
                Pause.isPause = true;

                // �Z�[�u�\�ɂȂ�����UI��\��
                SelectBox.GetComponent<Image>().enabled = true;
                SaveCharacter.GetComponent<Image>().enabled = true;
                YesCharacter.GetComponent<Image>().enabled = true;
                NoCharacter.GetComponent<Image>().enabled = true;
                AButton.GetComponent<UIBlink>().isHide = true;
                // ���[�v�͔�\��
                WarpCharacter.GetComponent<Image>().enabled = false;
                SusumuCharacter.GetComponent<Image>().enabled = false;

                SelectBoxPosUpdete();   // �I��g�̍X�V
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
                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // ���艹
                    SaveManager.canSave = false;    // �Z�[�u�\����
                    SaveManager.shouldSave = true;  // �Z�[�u����ׂ��Ȃ̂Ńt���O�𗧂Ă�
                    Pause.isPause = false;
                    isDecision = false;
                }
            }
            else
            {
                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // ���艹
                    SaveManager.canSave = false;    // �Z�[�u�\������
                    Pause.isPause = false;
                    isDecision = false;
                }
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
        }else if(Player.isHitSavePoint && !SaveManager.canSave)
        {
            SaveManager.canSave = true;
        }
    }

    private void SelectUp() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    private void SelectDown() {
        // ��
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select > 1)
        {
            select = 0;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �I��g�̍��W�X�V
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * �I��g�̈ʒu�̍X�V
         * ���ꂼ��̕�����RectTransform�ƍ��킹�邱�Ƃœ����ʒu�ɕ\�����ł���
         */
        if (select == 0)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = YesCharacter.GetComponent<RectTransform>().localPosition;
        }
        else if (select == 1)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = NoCharacter.GetComponent<RectTransform>().localPosition;
        }
    }

}
