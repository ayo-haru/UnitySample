//=============================================================================
//
// SE�ݒ�
//
// �쐬��:2022/04/26
// �쐬��:����T�q
//
// <�J������>
// 2022/04/26    �쐬
// 2022/05/10    �p�b�h���S�Ή�
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OptionSE : MonoBehaviour
{
    //�X���C�_�[
    private Slider SeSlider;
    //�I��p�t���O
    public bool selectFlag = false;
    //�ړ���
    public float moveSpeed = 0.005f;
    // InputAction��UI������
    private Game_pad UIActionAssets;
    // InputAction��select������
    private InputAction LeftStickSelect;
    // InputAction��select������
    private InputAction RightStickSelect;



    // Start is called before the first frame update
    void Awake()
    {  
        // InputAction�C���X�^���X�𐶐�
        UIActionAssets = new Game_pad();

        //�R���|�[�l���g�擾
        SeSlider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        //Awake���Ƃ܂��f�[�^�����[�h����ĂȂ�����start�Ŏ��s
        //�T�E���h�}�l�[�W���[����SE�̉��ʂ��擾
        SeSlider.value = SoundManager.seVolume;
    }

    // Update is called once per frame
    void Update()
    {
        //�I������ĂȂ������烊�^�[��
        if (!selectFlag)
        {
            return;
        }

        // �R���g���[���[������
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }


        //�E���L�[�ŉ��ʃv���X
        if (Input.GetKey(KeyCode.RightArrow))
        {
            VolUp();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.right.isPressed)
            {
                VolUp();
            }
        }
        OnRightStick();

        //�����L�[�ŉ��ʁ|
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            VolDown();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.left.isPressed)
            {
                VolDown();
            }
        }
        OnLeftStick();

    }

    private void OnEnable()
    {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Action�C�x���g��o�^
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }

    //�ݒ��ʕ����Ƃ��ɉ��ʕۑ�
    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();

        //SaveManager.saveSEVolume(SoundManager.seVolume);
    }

    // �{�����[��������
    private void VolUp()
    {
        SeSlider.value += moveSpeed;
        //���ʐݒ�
        SoundManager.seVolume = SeSlider.value;

    }

    // �{�����[��������
    private void VolDown()
    {
        SeSlider.value -= moveSpeed;
        //���ʐݒ�
        SoundManager.seVolume = SeSlider.value;

    }

    private void OnLeftStick()
    {
        if (!selectFlag)
        {
            return;
        }

        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.x > 0.5f)
        {
            VolUp();
        }
        else if (doLeftStick.x < -0.5f)
        {
            VolDown();
        }
    }

    private void OnRightStick()
    {
        if (!selectFlag)
        {
            return;
        }

        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.x > 0.5f)
        {
            VolUp();
        }
        else if (doRightStick.x < -0.5f)
        {
            VolDown();
        }

    }

}
