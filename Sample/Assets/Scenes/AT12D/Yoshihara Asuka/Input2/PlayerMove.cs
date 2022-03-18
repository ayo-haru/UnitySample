using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    //---�ϐ��錾
    public InputAction playermove;                  // �C���v�b�g�A�N�V������ǉ�
    private Vector2 moveValue;                      // InputSystem����̃p�����[�^���i�[
    [SerializeField] private float speed = 1.0f;

   private void Awake()
    {

    }

    //--�{�^���̓��͂Ɗ֐������ѕt��
    private void OnEnable()
    {
        playermove.Enable();                        // ���͂��󂯎��J�n����  
    }
    private void OnDisable()
    {
        playermove.Disable();                       // ���͂̎󂯎��I������

    }

    private void OnMove(InputValue Value)
    {
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
         moveValue = playermove.ReadValue<Vector2>();   // moveValue�ɓ��͂̒l���i�[
        transform.Translate(moveValue.x * speed,
                            moveValue.y *speed,
                            0.0f);
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
