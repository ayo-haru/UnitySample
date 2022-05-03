using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //状態推移、列挙型　
    public enum Boss_State
    {
        idle,                  //待機(0)
        damage,               //ダメージ(1)
        strawberryBomb,      //イチゴ爆弾(3)
        charge,             //突進(4)
        KnifeThrower,      //ナイフ投げ(5)
        Rain,             //武器雨(6)
    }

    private Animator animator;
    //ボスが登場した最初の位置
    private Vector3 defaultPos;
    //ボスの状態
    static private Boss_State BossState = Boss_State.idle;
    //idle状態の経過時間
    private float elapsedTimeOfIdleState = 0f;
    //idle状態で留まる時間
    [SerializeField]
    private float timeToStayInIdle = 24f;
    //モーションのランダム抽選用の数
    private int RandomNumbe = 0;

    //攻撃数カウント
    public static int AttackCount = 0;
    //待機モーションなしの攻撃回数（HP50％以下のみ）
    [SerializeField]
    private int MaxAttack = 2;
    //必殺技用フラグ(〇%以下になったとき一回)
    private bool UltFlg;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();アニメーション追加した場合
        defaultPos = transform.position;//デフォルトの出現位置をtransform.positionから取得
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            if (Boss1Manager.BossState == Boss1Manager.Boss1State.BOSS1_BATTLE)
            {
                if (BossState == Boss_State.idle)       //もしボスの状態が待機の場合
                {
                    Idle();
                }
                else if (BossState == Boss_State.damage)//もしボスの状態がダメージの場合
                {
                    //damage();
                }
                else if (BossState == Boss_State.strawberryBomb)//もしボスの状態がイチゴ爆弾の場合
                {
                    //strawberryBomb();
                    this.GetComponent<Boss1Attack>().BossStrawberry.Boss1Strawberry();
                }
                else if (BossState == Boss_State.charge)//もしボスの状態が突進の場合
                {
                    //charge();
                    Boss1Attack BossAttack;
                    BossAttack = this.GetComponent<Boss1Attack>();
                    BossAttack.BossRush.Boss1Fork();
                    //this.GetComponent<Boss1Attack>().BossRush.Boss1Fork();
                }
                else if (BossState == Boss_State.KnifeThrower)//もしボスの状態がナイフ投げの場合
                {
                    //KnifeThrower();
                    this.GetComponent<Boss1Attack>().BossKnife.Boss1Knife();
                }
                else if(BossState == Boss_State.Rain)
                {
                    this.GetComponent<Boss1Attack>().BossRain.BossRain();
                }
            }
        }
    }

    //ボスの状態推移
    static public void SetState(Boss_State bossState, Transform playerTransform = null)
    {
        BossState = bossState;
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

    //　状態取得
    public Boss_State GetState()
    {
        return BossState;
    }

    //待機モーション
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);
        if (AttackCount == MaxAttack)
        {
            //SetState(Boss_State.idle);//待機モーション
            elapsedTimeOfIdleState = timeToStayInIdle;
            AttackCount = 0;          //攻撃数カウントをゼロに
            Debug.Log("アイドル??");
        }
        //　一定時間が経過したら各種攻撃状態にする
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle状態の経過時間をoffにする
            Debug.Log("AttackCount：" + AttackCount);

            //ランダム数の生成とswitch分岐をこの中へ
            if(!UltFlg && HPgage.currentHp <= 30)
            {
                UltFlg = true;
                SetState(Boss_State.Rain);
                return;
            }
            if (HPgage.currentHp >= 51)
            {
                RandomNumbe = Random.Range(1, 3);//攻撃パターンランダム化
                Debug.Log("Random" + RandomNumbe);
            }
            else
            {
                RandomNumbe = Random.Range(1, 4);//攻撃パターンランダム化
                Debug.Log("Random" + RandomNumbe);
            }
            switch (RandomNumbe)            //switch分岐
                {
                    case 1://イチゴ爆弾へ
                        SetState(Boss_State.strawberryBomb);
                        RandomNumbe = -1;
                        Debug.Log("イチゴ爆弾");
                        break;//break文

                    case 2://突進へ
                        SetState(Boss_State.charge);
                        RandomNumbe = -1;
                        Debug.Log("突進攻撃");
                        break;//break文

                    case 3://ナイフ攻撃
                            SetState(Boss_State.KnifeThrower);
                            RandomNumbe = -1;
                        Debug.Log("ナイフ攻撃");
                        break;//break文

                    case 4://雨攻撃
                    SetState(Boss_State.Rain);
                    RandomNumbe = -1;
                    Debug.Log("雨攻撃");
                    break;

                }
            
            
        }
    }
}

