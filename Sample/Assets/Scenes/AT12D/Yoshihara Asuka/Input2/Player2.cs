using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---変数宣言
    private Game_pad PlayerActionAsset;                 // InputActionで生成したものを使用
    private InputAction move;                           // InputActionのmoveを扱う
    private InputAction Jump;                           // InputActionのmoveを扱う

    private Rigidbody rb;
    [SerializeField] private float maxSpeed;            // 移動スピード
    [SerializeField] private float JumpPower;           // ジャンプ

    private Vector2 ForceDirection = Vector2.zero;      // 移動する方向
    private Vector2 AttackDirection = Vector2.zero;     // 攻撃する方向

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // Game_padの初期化
    }
    //---ボタンンの入力を結び付ける
    private void OnEnable()
    {
        //---攻撃
        PlayerActionAsset.Player.Attack.started += Attack;  // started...ボタンが押された瞬間

        //---移動
        move = PlayerActionAsset.Player.Move;
        
    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= Attack;  // started...ボタンが押された瞬間
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
        //---移動処理
        ForceDirection += move.ReadValue<Vector2>();
        rb.AddForce(ForceDirection,ForceMode.Impulse);
        ForceDirection = Vector2.zero;
    }


    private void Attack(InputAction.CallbackContext obj)
    {
        Debug.Log("攻撃した！");

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

