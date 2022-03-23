using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //状態推移、列挙型　
    public enum Boss_State
    {
        idle,                  //待機(0)
        damage,               //ダメージ(1)
        strawberryBomb,      //イチゴ爆弾(3)
        charge,             //突進(4)
        KnifeThrower,      //ナイフ投げ(5)
    }

    private Animator animator;
    //ボスが登場した最初の位置
    private Vector3 defaultPos;
    //ボスの状態
    static　private Boss_State BossState = Boss_State.idle;
    //idle状態の経過時間
    private float elapsedTimeOfIdleState = 0f;
    //idle状態で留まる時間
    [SerializeField]
    private float timeToStayInIdle = 24f;
    //モーションのランダム抽選用の数
    private int RandomNumbe = 0;


    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();アニメーション追加した場合
        defaultPos = transform.position;//デフォルトの出現位置をtransform.positionから取得
    }

    // Update is called once per frame
    void Update()
    {
        if (BossState == Boss_State.idle)       //もしボスの状態が待機の場合
        {
            //Idle();
        }
        else if (BossState == Boss_State.damage)//もしボスの状態がダメージの場合
        {
            //damage();
        }
        else if (BossState == Boss_State.strawberryBomb)//もしボスの状態がイチゴ爆弾の場合
        {
            //strawberryBomb();
        }
        else if (BossState == Boss_State.charge)//もしボスの状態が突進の場合
        {
            //charge();
        }
        else if (BossState == Boss_State.KnifeThrower)//もしボスの状態がナイフ投げの場合
        {
            //KnifeThrower();
        }
    }

    //ボスの状態推移
    static　public void SetState(Boss_State tmpState, Transform playerTransform = null)
    {
        BossState = tmpState;
        if (BossState == Boss_State.idle)
        {
            Debug.Log("アイドル");
        }
        else if (BossState == Boss_State.damage)
        {
            Debug.Log("ダメージ");
        }
        else if (BossState == Boss_State.strawberryBomb)
        {
            Debug.Log("通常攻撃");
        }
        else if (BossState == Boss_State.charge)
        {
            Debug.Log("衝撃波攻撃");
        }
        else if (BossState == Boss_State.KnifeThrower)
        {
            Debug.Log("チェイス");
        }
    }
    //　状態取得メソッド
    public Boss_State GetState()
    {
        return BossState;
    }

    //待機モーション
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //　一定時間が経過したらpatrol状態にする
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;

            RandomNumbe = Random.Range(3, 5);
            switch (RandomNumbe)
            {
                case 3://処理１
                    SetState(Boss_State.strawberryBomb);
                    Debug.Log("イチゴ爆弾");
                    break;//break文
                case 4://処理２
                    SetState(Boss_State.charge);
                    Debug.Log("突進攻撃");
                    break;//break文
                case 5://処理３
                    //if(HPゲージが半分の場合)
                    SetState(Boss_State.KnifeThrower);
                    Debug.Log("ナイフ攻撃");
                    break;//break文
            }
        }
    }
}


