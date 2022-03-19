using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---�ϐ��錾
    private Game_pad PlayerActionAsset;                 // InputAction�Ő����������̂��g�p
    private InputAction move;                           // InputAction��move������
    private Rigidbody rb;

    private Vector2 ForceDirection = Vector2.zero;      // �ړ��������

    [SerializeField] private float maxSpeed = 1;        // �ړ��X�s�[�h
    [SerializeField] private float JumpForce = 5;       // �W�����v��
    public float GravityForce = -10;                    // �d��
    private bool JumpNow = false;                       // �W�����v���Ă��邩�ǂ���

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // InputAction�C���X�^���X�𐶐�
    }


    //---�{�^�����̓��͂����ѕt����
    private void OnEnable()
    {
        //---Action�C�x���g�o�^
        PlayerActionAsset.Player.Attack.started += OnAttack;      // started...�{�^���������ꂽ�u��
        PlayerActionAsset.Player.Jump.canceled += OnJump;       �@// performed...�{�^���������ꂽ�u��

        //---�ړ�
        move = PlayerActionAsset.Player.Move;

        //---InputAction�̗L����
        PlayerActionAsset.Player.Enable();
        
    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= OnAttack;  // started...�{�^���������ꂽ�u��

        PlayerActionAsset.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //---�W�����v���Ȃ�ړ����������Ȃ�
        if(JumpNow == true)
        {
            Gravity(); 
            return;
        }

        //---�ړ�����
        SpeedCheck();
        ForceDirection += move.ReadValue<Vector2>();
        rb.AddForce(ForceDirection * maxSpeed, ForceMode.Impulse);
        ForceDirection = Vector2.zero;

    }

    private void OnMove(InputAction.CallbackContext obj)
    {

    }

    //---�U������
    private void OnAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("�U�������I");

    }

    //---�W�����v����
    private void OnJump(InputAction.CallbackContext obj)
    {
        //---�W�����v���ł���΃W�����v���Ȃ�
        if(JumpNow == true)
        {
            return;
        }
        Debug.Log("�W�����v�I");
        rb.AddForce(transform.up * JumpForce,ForceMode.Impulse);
        JumpNow = true;
    }

    //---�W�����v���̏d�͂���������(�W�����v���r�q�Ɍ�������ʂ�����)
    private void Gravity()
    {
        if(JumpNow == true)
        {
            rb.AddForce(new Vector3(0.0f,GravityForce,0.0f));
        }
    }

    //---�����蔻�菈��
    private void OnCollisionEnter(Collision collision)
    {
        if(JumpNow == true)
        {
            //---Tag"Ground"�ƐڐG���Ă���Ԃ̏���
            if(collision.gameObject.CompareTag("Ground"));
            {
                JumpNow = false;
            }
        }
    }

    //---AddForce�ŗ͂������肷���Ă��܂����߁AmaxSpeed�̒l�ɋ߂��l�ɌŒ肷�鏈��
    private void SpeedCheck()
    {
        Vector3 PlayerVelocity  = rb.velocity;
        PlayerVelocity.z = 0;

        if(PlayerVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = PlayerVelocity.normalized * maxSpeed;
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
    }

}

