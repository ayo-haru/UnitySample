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
    private InputAction Attack;                         // InputActionのmoveを扱う

    private Rigidbody rb;
    public GameObject prefab;

    private Vector2 ForceDirection = Vector2.zero;      // 移動する方向
    private Vector2 AttackDirection = Vector2.zero;     // 攻撃方向
    private Vector3 PlayerPos;                          // プレイヤーの座標


    [SerializeField] private float maxSpeed = 1;        // 移動スピード
    [SerializeField] private float JumpForce = 5;       // ジャンプ力
    public float GravityForce = -10.0f;                 // 重力

    private bool JumpNow = false;                       // ジャンプしているかどうか


    //---攻撃
    public float AttckPosHeight = 8.0f;                 // シールド位置上下
    public float AttckPosWidth = 8.0f;                  // シールド位置左右
    public float DestroyTime = 3.0f;                    // シールドが消える時間　


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // InputActionインスタンスを生成
    }


    //---ボタンンの入力を結び付ける
    private void OnEnable()
    {
        //---移動
        move = PlayerActionAsset.Player.Move;
        Attack = PlayerActionAsset.Player.Attack;

        //---Actionイベント登録
        PlayerActionAsset.Player.Attack.started += OnAttack;      // started...ボタンが押された瞬間
        PlayerActionAsset.Player.Jump.canceled += OnJump;       　// performed...ボタンが押された瞬間


        //---InputActionの有効化(これかかないと、入力とれない。)
        PlayerActionAsset.Player.Enable();
        
    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= OnAttack;  // started...ボタンが押された瞬間

        //---InputActionの無効化
        PlayerActionAsset.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Weapon");
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
        PlayerPos = transform.position;
        AttackDirection += Attack.ReadValue<Vector2>(); 
        Debug.Log("AttackDirection(正規化前):" + AttackDirection);

        AttackDirection.Normalize();
        GameObject weapon = Instantiate(prefab,new Vector3(PlayerPos.x + (AttackDirection.x * AttckPosWidth),
                                                           PlayerPos.y + (AttackDirection.y * AttckPosHeight),
                                                           PlayerPos.z),Quaternion.identity);
        //---
        if (AttackDirection.x < 0.0f)
        {
            AttackDirection.y *= -1;
        }
        if (AttackDirection.y <= 0.0f)
        {
            JumpNow = true;
        }


        weapon.transform.Rotate(new Vector3(0,0,(90 * AttackDirection.y)));
        //Debug.Log("攻撃した！(Weapon)");
        Debug.Log("AttackDirection(正規化後):"+ AttackDirection);
        AttackDirection = Vector2.zero;
        Destroy(weapon,DestroyTime);
        return;
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
            if(collision.gameObject.tag == "Ground" && !(this.gameObject.tag == "Weapon")){
                JumpNow = false;
                ForceDirection = Vector2.zero;
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
        GUILayout.Label($"JumpFlg:{JumpNow}");
    }

}

