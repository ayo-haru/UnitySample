using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanCakeMove : MonoBehaviour
{
    //状態推移、列挙型　
    public enum Boss_State {
        idle,                  //待機(0)
        damage,               //ダメージ(1)
        strawberryBomb,      //イチゴ爆弾(3)
        charge,             //突進(4)
        Jump,              //ジャンプ
        KnifeThrower,     //ナイフ投げ(5)
        Rain,            //武器雨(6)

    }

    private Boss1Attack BossAttack;

    private Animator animator;
    //ボスが登場した最初の位置
    private Vector3 defaultPos;
    //ボスの状態
    static private Boss_State BossState;
    //idle状態の経過時間
    private float elapsedTimeOfIdleState = 0f;
    //idle状態で留まる時間
    [SerializeField]
    private float timeToStayInIdle = 24f;


    // Start is called before the first frame update
    void Start() {
        //animator = GetComponent<Animator>();アニメーション追加した場合
        defaultPos = transform.position;//デフォルトの出現位置をtransform.positionから取得
        BossAttack = this.GetComponent<Boss1Attack>();
        BossState = Boss_State.idle;
    }

    // Update is called once per frame
    void Update() {
        if (!Pause.isPause)
        {
            if (Boss1Manager.BossState == Boss1Manager.Boss1State.BOSS1_BATTLE)
            {
                if (BossState == Boss_State.idle)       //もしボスの状態が待機の場合
                {
                    Idle();
                }
                else if (BossState == Boss_State.charge)//もしボスの状態が突進の場合
                {
                    //charge();

                    BossAttack.BossRush.Boss1Fork();
                }
            }
        }
    }

    //ボスの状態推移
    static public void SetState(Boss_State bossState, Transform playerTransform = null) {
        BossState = bossState;
        if (BossState == Boss_State.idle)
        {
            Debug.Log("アイドル");
        }
        else if (BossState == Boss_State.charge)
        {
            Debug.Log("衝撃波攻撃");
        }
    }

    //　状態取得
    public Boss_State GetState() {
        return BossState;
    }

    //待機モーション
    private void Idle() {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);
        //　一定時間が経過したら各種攻撃状態にする
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle状態の経過時間をoffにする

            //ランダム数の生成とswitch分岐をこの中へ
            //if (!UltFlg && HPgage.currentHp <= 30)
            //{
            //    Debug.Log("うるとだよ");
            //    UltFlg = true;
            //    SetState(Boss_State.Rain);
            //    return;
            //}
            //if (HPgage.currentHp >= 51)
            //{

            //    RandomNumbe = Random.Range(1, 4);//攻撃パターンランダム化
            //    Debug.Log("Random" + RandomNumbe);
            //    if (BossAttack.JampFlg)
            //    {
            //        RandomNumbe = Random.Range(1, 3);//攻撃パターンランダム化
            //        BossAttack.JampFlg = false;
            //    }
            //}
            //else
            //{
            //    RandomNumbe = Random.Range(1, 5);//攻撃パターンランダム化
            //    if (BossAttack.JampFlg)
            //    {
            //        if (RandomNumbe == 3)
            //        {
            //            RandomNumbe = 4;
            //            BossAttack.JampFlg = false;
            //        }
            //    }
            //}
            //switch (RandomNumbe)            //switch分岐
            //{
            //    case 1://イチゴ爆弾へ
            //        SetState(Boss_State.strawberryBomb);
            //        RandomNumbe = -1;
            //        Debug.Log("イチゴ爆弾");
            //        break;//break文

            //    case 2://突進へ
            //        SetState(Boss_State.charge);
            //        RandomNumbe = -1;
            //        Debug.Log("突進攻撃");
            //        break;//break文

            //    case 3://ジャンプ
            //        SetState(Boss_State.Jump);
            //        RandomNumbe = -1;
            //        Debug.Log("ジャンプ");
            //        break;//break文

            //    case 4://ナイフ攻撃
            //        SetState(Boss_State.KnifeThrower);
            //        RandomNumbe = -1;
            //        Debug.Log("ナイフ攻撃");
            //        break;

            //    case 5://雨攻撃
            //        SetState(Boss_State.Rain);
            //        RandomNumbe = -1;
            //        Debug.Log("雨攻撃");
            //        break;

            //}


        }
    }
}
