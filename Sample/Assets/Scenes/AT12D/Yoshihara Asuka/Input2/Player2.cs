using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---�ϐ��錾
    private Game_pad PlayerActionAsset;                 // InputAction�Ő����������̂��g�p
    private InputAction move;                           // InputAction��move������
    private InputAction Jump;                           // InputAction��move������

    private Rigidbody rb;
    [SerializeField] private float maxSpeed;            // �ړ��X�s�[�h
    [SerializeField] private float JumpPower;           // �W�����v

    private Vector2 ForceDirection = Vector2.zero;      // �ړ��������
    private Vector2 AttackDirection = Vector2.zero;     // �U���������

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // Game_pad�̏�����
    }
    //---�{�^�����̓��͂����ѕt����
    private void OnEnable()
    {
        //---�U��
        PlayerActionAsset.Player.Attack.started += Attack;  // started...�{�^���������ꂽ�u��

        //---�ړ�
        move = PlayerActionAsset.Player.Move;
        
    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= Attack;  // started...�{�^���������ꂽ�u��
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
        ForceDirection += move.ReadValue<Vector2>();
        rb.AddForce(ForceDirection,ForceMode.Impulse);
        ForceDirection = Vector2.zero;
    }


    private void Attack(InputAction.CallbackContext obj)
    {
        Debug.Log("�U�������I");

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
    }

}

