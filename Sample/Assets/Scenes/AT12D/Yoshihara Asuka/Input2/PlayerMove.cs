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

    private Vector2 Player_Move;                            // プレイヤーの移動する方向
    private Rigidbody rb;                                   

    [SerializeField] private float MasSpeed = 5.0f;         // プレイヤーの最高速度
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Player_Move = move.ReadValue<Vector2>();
        rb.AddForce(Player_Move,ForceMode.Impulse);
    }
}
