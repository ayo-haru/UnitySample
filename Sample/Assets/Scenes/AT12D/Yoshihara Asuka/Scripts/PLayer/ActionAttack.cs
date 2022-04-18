//=============================================================================
//
// PLayer Attack処理
//
// 作成日:2022/03/18
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/04/18   作成
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionAttack : MonoBehaviour
{
    //---変数宣言

    //---コントローラー関連
    private Game_pad PlayerActionAssets;                  // InputActionメソッド
    private InputAction Attack;

    private Rigidbody rb;
    private GameObject Weapon;                           // Weaponを格納

    private Vector2 AttackDirection = Vector2.zero;      // 攻撃方向
    private Vector3 CurrentScale;                        // 現在のプレイヤーのスケールの値を格納 
    private float AttckPosHeight = 6.0f;                 // シールド位置上下
    private float AttckPosWidth = 4.0f;                  // シールド位置左右
    private float DestroyTime = 0.5f;                    // シールドが消える時間
    private bool isAttack;                               // 攻撃フラグ
    [System.NonSerialized]
    public static bool UnderParryNow = false;            // 下パリィ中フラグ


    public int StopTime = 5;                             // 盾を出した時に止まる時間
    private int Timer = 0;                               // 停止時間計測

    [System.NonSerialized]
    public static bool GroundNow = false;                // 地面と接地中フラグ

    private void Awake()
    {
        //---各種コンポーネント取得
        rb = GetComponent<Rigidbody>();
        PlayerActionAssets = new Game_pad();
    }

    private void OnEnable()
    {
        //---スティックの入力の設定を取得
        Attack = PlayerActionAssets.Player.Attack;

        //---Actionイベント登録(ボタン入力)
        Attack.started += OnAttack;                     // 押された瞬間でコールバック

        //---入力の有効化
        PlayerActionAssets.Player.Enable();
    }

    private void OnDisable()
    {
        Attack.started -= OnAttack;

        //---入力の無効化
        PlayerActionAssets.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        //---武器オブジェクトをロード
        Weapon = (GameObject)Resources.Load("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //===================================================================
    //
    //  攻撃処理
    //  OnAttackとAttackでセット。OnAttackで攻撃フラグを立てて、
    //  Attackの中に攻撃の処理を書く
    //
    //===================================================================
    private void OnAttack(InputAction.CallbackContext obj)
    {
        Attacking();
    }
    private void Attacking()
    {
    //    //---スティック入力の際の処理
    //    GameData.PlayerPos = transform.position;        // 攻撃する瞬間のプレイヤー座標を取得
    //    AttackDirection += Attack.ReadValue<Vector2>(); // スティックの倒した値を取得

    //    //---攻撃分岐(左右・上・下)
    //    if(Mathf.Abs(AttackDirection.x) >= 1)           // 左右攻撃
    //    {
    //        Timer = StopTime;                           // タイマー設定
    //        PlayerData.animator.SetTrigger("Attack");   // アニメーション再生
    //    }

    //    if(AttackDirection.y >= 1)                      // 上攻撃
    //    {
    //        if(GroundNow == true)                       // 地面にいるとき
    //        {
    //            // ---少しあがる
    //            rb.AddForce(transform.up * 5.0f,ForceMode.Impulse);
    //            GroundNow = false;
    //        }
    //        //---アニメーション再生
    //        PlayerData.animator.SetTrigger("Attack_UP");
    //    }

    //    if(AttackDirection.y <= 1)                      // 下パリィ
    //    {
    //        if(GroundNow == true)
    //        {
    //            rb.AddForce(transform.up * 5.0f, ForceMode.Impulse);
    //            GroundNow = false;
    //        }
    //        //---アニメーション再生
    //        PlayerData.animator.SetTrigger("Attack_DOWN");
    //    }

    //    //---モデルの向きと反対方向に盾出したらモデル回転
    //    if ((AttackDirection.x > 0 && beforeDir.x < 0) || (AttackDirection.x < 0 && beforeDir.x > 0))
    //    {
    //        //方向を保存
    //        beforeDir.x = AttackDirection.x;
    //        //回転
    //        transform.rotation = Quaternion.LookRotation(AttackDirection);
    //        //スケールxを反転
    //        scale.x *= -1;
    //        transform.localScale = scale;
    //    }

    //    if (AttackDirection.x < 0 && beforeDir.x > 0)
    //    {
    //        beforeDir = AttackDirection;
    //        transform.rotation = Quaternion.LookRotation(AttackDirection);
    //    }

    }
}
