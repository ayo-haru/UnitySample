using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;                           // コントローラーを使うに当たってのライブラリ

public class Player_pad : MonoBehaviour
{
    //---変数宣言
    private Controller PlayerAction;                    // 

    private InputAction move;
    private Rigidbody rb;

    [SerializeField] private float MaxSpeed = 5.0f;     // プレイヤーの最高速度
    private Vector3 ForceDirection = Vector2.zero;      // プレイヤーの動く方向(初期値0)
    private Quaternion TargetRotation;                  // プレイヤーの回転を制御
    private bool AbleMove;                              // プレイヤーの動けるかどうか(true = 移動、false = 入力を受け付けない)

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();                 // RigidBodyのコンポーネントを取得
        PlayerAction = new Controller();                // Controllerのコンポーネント取得

        AbleMove = true;                                // 初期値true(移動許可)
        TargetRotation = transform.rotation;            // 現在の回転を取得
    }

    //---ボタン入力と関数の結びつけ
    private void OnEnable()                             // OnEnable...オブジェクトが表示されている時に有効
    {
        PlayerAction.Player.Attack.started += Attack;   // started ... ボタンが押されたときに処理される
                                                        // canceld ... ボタンが話されたときに処理される
        PlayerAction.Player.Enable();                   // アクションボタンの有効

        //---変数にmoveアクションを格納
        move = PlayerAction.Player.Move;
    }

    private void OnDisable()                           // OnDisable ... オブジェクトが非表示の時有効
    {
        PlayerAction.Player.Attack.started -= Attack;  // Playerが消えた時にマイナスすることでなくなる。
        PlayerAction.Player.Disable();                 // アクションボタンの無効


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
        //ForceDirection += move.ReadValue<Vector2>().x; //* GetCameraRight();
        rb.AddForce(ForceDirection, ForceMode.Impulse); // ForceMode.Impulse ... リジッドボディでかかる質量を一瞬で行う。
    }


    //---攻撃アニメーションの切り替え
    private void Attack(InputAction.CallbackContext obj)
    {
        /* 今後実装 */
    }

    //---カメラの向きを元に移動する
    //private Vector3 GetCameraRight()
    //{
    //    Vector3 right = PlayerCamera.transform.right;
    //    right.y = 0;

    //    return right.normalized;    // normalized ... ベクトルの正規化
    //}
}
