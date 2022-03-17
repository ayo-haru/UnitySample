using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //---変数宣言
    private Game_pad PlayerAction_Pad;                      // ゲームパッドで設定した情報を扱う
    private InputAction move;                               // InputSystemの設定のmoveを扱う
    private InputAction jump;                               // InputSystemの設定のjumpを扱う
    private InputAction attack;                             // InputSystemの設定のattackを扱う

    private Vector2 Player_Move = Vector2.zero;                            // プレイヤーの移動する方向
    private Rigidbody rb;                                   

    [SerializeField] private float MaxSpeed = 5.0f;         // プレイヤーの最高速度
    [SerializeField] private float JumpPower = 5.0f;        // プレイヤーのジャンプ力

   private void Awake()
    {
        rb = GetComponent<Rigidbody>();                     // RigidBodyのコンポーネント取得
        PlayerAction_Pad = new Game_pad();                  // Game_padのコンポーネント取得

    }

    //--ボタンの入力と関数を結び付け
    private void OnEnable()
    {
        move = PlayerAction_Pad.Player.Move;                // moveの取得と方向を結び付け。
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
        //---コントローラーが接続されいないとき、NULLになる(処理しない)
        if(Gamepad.current == null)
        {
            return;
        }

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Button Northが押された！");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("Button Southが離された！");
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
