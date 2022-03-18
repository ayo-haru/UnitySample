using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    //---変数宣言
    public InputAction playermove;                  // インプットアクションを追加
    private Vector2 moveValue;                      // InputSystemからのパラメータを格納
    [SerializeField] private float speed = 1.0f;

   private void Awake()
    {

    }

    //--ボタンの入力と関数を結び付け
    private void OnEnable()
    {
        playermove.Enable();                        // 入力を受け取る開始命令  
    }
    private void OnDisable()
    {
        playermove.Disable();                       // 入力の受け取り終了命令

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
         moveValue = playermove.ReadValue<Vector2>();   // moveValueに入力の値を格納
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
