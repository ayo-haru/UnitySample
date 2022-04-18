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
        PlayerActionAssets.Player.Attack.started += OnAttack;
    }



    // Start is called before the first frame update
    void Start()
    {
        
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
        //---スティック入力の際の処理
        GameData.PlayerPos = transform.position;        // 攻撃する瞬間のプレイヤー座標を取得
        AttackDirection += Attack.ReadValue<Vector2>(); // スティックの倒した値を取得
    }

}
