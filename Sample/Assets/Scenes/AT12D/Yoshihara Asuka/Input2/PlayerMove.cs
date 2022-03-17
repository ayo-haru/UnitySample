using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //---�ϐ��錾
    private Game_pad PlayerAction_Pad;                      // �Q�[���p�b�h�Őݒ肵����������
    private InputAction move;                               // InputSystem�̐ݒ��move������
    private InputAction jump;                               // InputSystem�̐ݒ��jump������
    private InputAction attack;                             // InputSystem�̐ݒ��attack������

    private Vector2 Player_Move = Vector2.zero;                            // �v���C���[�̈ړ��������
    private Rigidbody rb;                                   

    [SerializeField] private float MaxSpeed = 5.0f;         // �v���C���[�̍ō����x
    [SerializeField] private float JumpPower = 5.0f;        // �v���C���[�̃W�����v��

   private void Awake()
    {
        rb = GetComponent<Rigidbody>();                     // RigidBody�̃R���|�[�l���g�擾
        PlayerAction_Pad = new Game_pad();                  // Game_pad�̃R���|�[�l���g�擾

    }

    //--�{�^���̓��͂Ɗ֐������ѕt��
    private void OnEnable()
    {
        move = PlayerAction_Pad.Player.Move;                // move�̎擾�ƕ��������ѕt���B
    }
    private void OnDisable()
    {
        
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        Player_Move = new Vector2(movementVector.x, 0.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //---�R���g���[���[���ڑ����ꂢ�Ȃ��Ƃ��ANULL�ɂȂ�(�������Ȃ�)
        if(Gamepad.current == null)
        {
            return;
        }

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Button North�������ꂽ�I");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("Button South�������ꂽ�I");
        }

        
    }

    private void FixedUpdate()
    {
        rb.AddForce(Player_Move * MaxSpeed, ForceMode.Impulse);
        Player_Move = Vector2.zero;
    }

    private void OnGUI()
    {
        if(Gamepad.current == null)
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
    //void FixedUpdate()
    //{
    //    Player_Move = move.ReadValue<Vector2>();
    //    rb.AddForce(Player_Move,ForceMode.Impulse);
    //}
}
