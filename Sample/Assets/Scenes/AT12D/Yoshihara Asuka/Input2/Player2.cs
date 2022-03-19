using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---変数宣言
    private Game_pad PlayerActionAsset;                 // InputActionで生成したものを使用
    private InputAction move;                           // InputActionのmoveを扱う
    private Rigidbody rb;

    private Vector2 ForceDirection = Vector2.zero;      // 移動する方向

    [SerializeField] private float maxSpeed = 1;        // 移動スピード
    [SerializeField] private float JumpForce = 5;       // ジャンプ力
    public float GravityForce = -10;                    // 重力
    private bool JumpNow = false;                       // ジャンプしているかどうか

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // InputActionインスタンスを生成
    }


    //---ボタンンの入力を結び付ける
    private void OnEnable()
    {
        //---Actionイベント登録
        PlayerActionAsset.Player.Attack.started += OnAttack;      // started...ボタンが押された瞬間
        PlayerActionAsset.Player.Jump.canceled += OnJump;       　// performed...ボタンが押された瞬間

        //---移動
        move = PlayerActionAsset.Player.Move;

        //---InputActionの有効化
        PlayerActionAsset.Player.Enable();
        
    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= OnAttack;  // started...ボタンが押された瞬間

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
        //---ジャンプ中なら移動処理をしない
        if(JumpNow == true)
        {
            Gravity(); 
            return;
        }

        //---移動処理
        SpeedCheck();
        ForceDirection += move.ReadValue<Vector2>();
        rb.AddForce(ForceDirection * maxSpeed, ForceMode.Impulse);
        ForceDirection = Vector2.zero;

    }

    private void OnMove(InputAction.CallbackContext obj)
    {

    }

    //---攻撃処理
    private void OnAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("攻撃した！");

    }

    //---ジャンプ処理
    private void OnJump(InputAction.CallbackContext obj)
    {
        //---ジャンプ中であればジャンプしない
        if(JumpNow == true)
        {
            return;
        }
        Debug.Log("ジャンプ！");
        rb.AddForce(transform.up * JumpForce,ForceMode.Impulse);
        JumpNow = true;
    }

    //---ジャンプ中の重力を強くする(ジャンプが俊敏に見える効果がある)
    private void Gravity()
    {
        if(JumpNow == true)
        {
            rb.AddForce(new Vector3(0.0f,GravityForce,0.0f));
        }
    }

    //---当たり判定処理
    private void OnCollisionEnter(Collision collision)
    {
        if(JumpNow == true)
        {
            //---Tag"Ground"と接触している間の処理
            if(collision.gameObject.CompareTag("Ground"));
            {
                JumpNow = false;
            }
        }
    }

    //---AddForceで力がかかりすぎてしまうため、maxSpeedの値に近い値に固定する処理
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

        //---ゲームパッドとつながっている時に表示される。
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

