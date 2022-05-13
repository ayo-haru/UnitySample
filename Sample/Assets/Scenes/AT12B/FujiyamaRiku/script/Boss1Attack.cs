using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Boss1Attack : MonoBehaviour
{
    public Boss1Rush BossRush;
    public Boss1StrawBerry BossStrawberry;
    public Boss1KnifeThrow BossKnife;
    public Boss1Rain BossRain;
    public Boss1Jamp BossJump;
    //ボスの攻撃の種類
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Attack4,
        Idle,
    }
    [Header("突進攻撃のダメージ")]
    [SerializeField] public int RushDamage;                 //突進攻撃のダメージ
    [SerializeField] public int StrawberryDamage;           //イチゴ攻撃のダメージ
    [SerializeField] public int KnifeDamage;                //ナイフ攻撃のダメージ
    [SerializeField] public float RefrectRotOver;           //弾いた角度の上判定用
    [SerializeField] public float RefrectRotUnder;          //弾いた角度の上判定用
    [System.NonSerialized] public bool RefrectFlg = false;                  //プレイヤーがパリィに成功したかどうかの受け取り用
    [System.NonSerialized] public bool OnlyFlg;                             //それぞれの処理の一回限定の処理用
    [System.NonSerialized]  public bool LRSwitchFlg;
    static public Vector3 BossStartPoint;                   //ボスの初期地点
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool MoveFlg;
    private GameObject HpObject;
    public HPgage HpScript;
    public Animator BossAnim;
    [System.NonSerialized]  public Vector3 Scale;
    [System.NonSerialized] public bool WeaponAttackFlg;                            //武器使う攻撃変更 true = 雨攻撃 false = 突進orナイフ
    [System.NonSerialized] public bool RFChange;                                          //左右反転 true = 左から攻撃 false = 右から攻撃
    BossMove.Boss_State BossTakeCase;
    [System.NonSerialized] public bool JampFlg;
    //実装するかわからない左右判定用
    [System.NonSerialized] public int RFNum;
    GameObject Forkobj;                                     //フォークのオブジェクト生成用
    GameObject Knifeobj;                                    //ナイフ生成用
    
    // Start is called before the first frame update
    void Start()
    {
        BossRush = this.GetComponent<Boss1Rush>();
        BossStrawberry = this.GetComponent<Boss1StrawBerry>();
        BossKnife = this.GetComponent<Boss1KnifeThrow>();
        BossRain = this.GetComponent<Boss1Rain>();
        BossJump = this.GetComponent<Boss1Jamp>();
        Scale = Boss1Manager.Boss.transform.localScale;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();
        BossAnim = this.gameObject.GetComponent<Animator>();
        
        OnlyFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Pause.isPause)
        {
            BossAnim.speed = 0.0f;  // アニメーションポーズ
        }
        else
        {
            /*
             雑にアニメーション再開
             */
            BossAnim.speed = 1.0f;
        }

        //ボスが死んだら処理をやめる
        if (!GameData.isAliveBoss1)
        {
            //それぞれの初期化をかける
            BossAnim.speed = 0f;
            //ボスを倒した何かが起こる場面に移動
            Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_END;
        }
    }
    //それぞれの攻撃処理
    public void AnimFlagOnOff()
    {
        if (!AnimFlg)
        {
            AnimFlg = true;
            return;
        }
        if(AnimFlg)
        {
            AnimFlg = false;
            return;
        }
        
    }
    public void AnimMoveFlgOnOff()
    {
        if (!MoveFlg)
        {
            MoveFlg = true;
            return;
        }
        if (MoveFlg)
        {
            MoveFlg = false;
            return;
        }
    }
   
    
}
