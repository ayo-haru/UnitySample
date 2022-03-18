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
    [SerializeField] private float JumpForce = 200;     // �W�����v��
    public float GravityForce = -100;                   // �d��
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
        //---�ړ�����
        if(JumpNow == true)
        {
            return;
        }

        SpeedCheck();
        Gravity();
        ForceDirection += move.ReadValue<Vector2>();
        rb.AddForce(ForceDirection * maxSpeed, ForceMode.Impulse);
        ForceDirection = Vector2.zero;

    }

    private void OnMove(InputAction.CallbackContext obj)
    {

    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("�U�������I");

    }

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
    private void OnCollisionEnter(Collision collision)
    {
        if(JumpNow == true)
        {
            if(collision.gameObject.CompareTag("Ground"));
            {
                JumpNow = false;
            }
        }
    }

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

