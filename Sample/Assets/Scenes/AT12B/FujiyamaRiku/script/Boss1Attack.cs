using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Boss1Attack : MonoBehaviour
{
    //ボスの攻撃の種類
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Attack4,
        Idle,
    }
    static public bool RefrectFlg = false;                  //プレイヤーがパリィに成功したかどうかの受け取り用
    static public bool OnlyFlg;                             //それぞれの処理の一回限定の処理用
    static public Vector3 BossStartPoint;                   //ボスの初期地点
    [SerializeField] public int RushDamage;                 //突進攻撃のダメージ
    [SerializeField] public int StrawberryDamage;           //イチゴ攻撃のダメージ
    [SerializeField] public int KnifeDamage;                //ナイフ攻撃のダメージ
    [SerializeField] public float RefrectRotOver;           //弾いた角度の上判定用
    [SerializeField] public float RefrectRotUnder;          //弾いた角度の上判定用
    bool LRSwitchFlg;
    private GameObject HpObject;
    HPgage HpScript;
    Animator BossAnim;
    bool AnimFlg;
    bool MoveFlg;
    BossMove.Boss_State BossTakeCase;
    bool RFChange;                                          //左右反転 true = 左から攻撃 false = 右から攻撃
    public bool WeaponAttackFlg;                            //武器使う攻撃変更 true = 雨攻撃 false = 突進orナイフ
    //実装するかわからない左右判定用
    //突進用変数群
    //----------------------------------------------------------
    GameObject Forkobj;                                     //フォークのオブジェクト生成用
    GameObject Fork;                                        //フォークのオブジェクト格納用
    Vector3 RushStartPoint;                                 //突進開始地点
    Vector3 RushEndPoint;                                   //突進終了地点
    Vector3 RushPlayerPoint;                                //突進をはじいたときのプレイヤー座標格納用
    Vector3 RushRefEndPoint;                                //突進をはじいた後の敵の最終地点
    Vector3 RushMiddlePoint;                                //突進攻撃後戻ってくるための中間座標
    Vector3 ForkPos;
    bool OnlyRushFlg;                                       //一回限定
    [SerializeField] public float RushSpeed;                //突進のスピード
    bool RushRefFlg = false;                                //突進をはじいた判定
    float RushTime;                                         //突進の経過時間
    float RushRefTime;                                      //弾いた後の時間経過
    bool BossReturnFlg;
    float BossReturnTime;                                   //突進後戻るまでの時間
    bool RushEndFlg;
    float RushReturnSpeed;
    bool ReturnDelay;                                      //戻ろうとするまでの時間
    Vector3 Scale;
    Vector3 oldScale;
    [SerializeField] public float RotateSpeed;
    Vector3 Rotate;
    int RFNum;
        
    //----------------------------------------------------------
    //イチゴ爆弾変数
    //----------------------------------------------------------
    GameObject obj;                                         //イチゴ生成用
    GameObject [] Strawberry;                               //イチゴ生成後格納
    [SerializeField] public int Max_Strawberry;             //イチゴの最大数
    static public int PreMax_Strawberry;                    //最大数をほかの部分でも使えるように
    int StrawberryNum;                                      //現在の射出済みイチゴ計算用
    int AliveStrawberry;                                    //イチゴの生存確認用
    static bool[] StrawberryUseFlg;                         //イチゴを使っているかどうかのフラグ
    static public bool [] StrawberryRefFlg;                 //イチゴが弾かれたかどうかのフラグ
    [SerializeField] public float StrawberrySpeed;          //イチゴが飛んでいく速度
    bool[] PlayerRefDir;                                    //弾いたときの方向フラグ
    Vector3 RefMiss;                                        //弾くのに失敗したときの座標格納用
    bool RefMissFlg;                                        //弾くのに失敗したときに処理を一回だけする用
    int StrawBerryMany;                                     //イチゴを最大数以上出さないようにするための処理←たぶんいらない
    bool[] StrawberryRefOnlyFlg;                            //弾かれたもので一回だけ処理するもの用
    static public bool[] StrawberryColPlayer;               //プレイヤーに当たった時用の処理
    GameObject StrawberryAimObj;
    GameObject [] StrawberryAim;
    Vector3 [] StrawberryAimScale;
    bool []StrawBerryLagFlg;
    Vector3 WeaponPos;
    

    //ベジエ曲線用
    Vector3  []StartPoint;
    Vector3 [] MiddlePoint;
    Vector3 [] EndPoint;
    float [] FinishTime;
    Vector3 RefEndPoint;
    float[] Ref_FinishTime;
     Vector3[] PlayerPoint;
    Vector3[] PlayerMiddlePoint;
    //----------------------------------------------------------
    //ナイフ投げ変数群
    //----------------------------------------------------------
    GameObject Knifeobj;                                    //ナイフ生成用
    GameObject Knife;                                       //ナイフ生成後格納
    Vector3 KnifeStartPoint;                                //ナイフのスタート座標
    Vector3 KnifeEndPoint;                                  //ナイフの終了地点
    Vector3 KnifePlayerPoint;                               
    float KnifeTime;
    [SerializeField] public float KnifeSpeed;               //ナイフの速度
    bool KnifeRefFlg = false;                               //ナイフが弾かれたかどうか
    float KnifeRefTime;
    Quaternion KnifeRotForward;                             //ナイフの角度変更用
    Quaternion KnifeRotDir;                                 //ナイフの角度変更用
    [SerializeField] Vector3 KnifeForward;                  //ナイフの前変更用
    Vector3 KnifePos;

    [SerializeField] public float KnifeThrowTime;
    float KnifeThrowNowTime;
    GameObject KnifeAimObj;
    GameObject KnifeAim;
    Vector3 KnifeAimPos;
    bool AimFlg;
    bool AimOnly;
    bool AimStart;
    //----------------------------------------------------------
    //ナイフフォーク大豪雨
    //----------------------------------------------------------
    public struct RainWeapon
    {
        public GameObject UseObj;
        public Vector3 StartPoint;
        public Vector3 EndPoint;
        public Vector3 ReturnPoint;
        public Vector3 ReturnMiddlePoint;
        public Vector3 ReturnEndPoint;
        public float RainMoveTime;
        public float RefMoveTime;
        public bool UseFlg;
        public bool RainRefrectFlg;
        public bool RainRefOnlyFlg;
        public Vector3 RollRand;
    };
    [SerializeField] public float RainTime;
    float RainNowTime;
    [SerializeField] public int MaxWeapon;
    public int PreMaxWeapon;
    Vector3 Range1;
    Vector3 Range2;
    Vector3 RainRand;
    int RainNum;
    int WeaponRand;

    public RainWeapon []g_Weapon;
    // Start is called before the first frame update
    void Start()
    {
        
        obj = (GameObject)Resources.Load("StrawBerry");
        Knifeobj = (GameObject)Resources.Load("Knife");
        Forkobj = (GameObject)Resources.Load("Fork");
        StrawberryAimObj = (GameObject)Resources.Load("StrawberryAim");
        KnifeAimObj = (GameObject)Resources.Load("KnifeAim");
        Range1 = GameObject.Find("Range1").transform.position;
        Range2 = GameObject.Find("Range2").transform.position;
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryUseFlg = new bool[Max_Strawberry];
        StrawberryRefFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        PlayerPoint = new Vector3[Max_Strawberry];
        StartPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];
        Ref_FinishTime = new float[Max_Strawberry];
        PlayerMiddlePoint = new Vector3[Max_Strawberry];
        StrawberryAim = new GameObject[Max_Strawberry];
        StrawberryAimScale = new Vector3[Max_Strawberry];
        StrawBerryLagFlg = new bool[Max_Strawberry];
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        PlayerRefDir = new bool[Max_Strawberry];
        StrawberryRefOnlyFlg = new bool[Max_Strawberry];
        StrawberryColPlayer = new bool[Max_Strawberry];
        PreMax_Strawberry = Max_Strawberry;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();
        BossAnim = this.gameObject.GetComponent<Animator>();
        Scale = Boss1Manager.Boss.transform.localScale;
        oldScale = Boss1Manager.Boss.transform.localScale;
        

        for (int i= 0;i < Max_Strawberry;i++)
        {
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            MiddlePoint[i] = GameObject.Find("Strawberry").transform.position; 
            EndPoint[i] = GameObject.Find("StrawberryEnd").transform.position; 
        }
        g_Weapon = new RainWeapon[MaxWeapon];
        PreMaxWeapon = MaxWeapon;
        for (int i = 0;i< MaxWeapon;i++)
        {
            g_Weapon[i].UseFlg = false;
            g_Weapon[i].RainRefrectFlg = false;
            g_Weapon[i].RainMoveTime = 0.0f;
        }
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
            if (Knife != null)
            {
                Destroy(Knife);
            }
            if (Fork != null)
            {
                Destroy(Fork);
            }
            for (int i = 0; i < Max_Strawberry; i++)
            {
                if(Strawberry[i] !=null)
                {
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                }
            }
            //ボスを倒した何かが起こる場面に移動
            Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_END;
        }
    }
    //それぞれの攻撃処理
    void AnimFlagOnOff()
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
    void BossTakeToCase()
    {
        if(BossTakeCase == BossMove.Boss_State.charge)
        {
            BossRushAnim();
        }
        
    }
    void AnimMoveFlgOnOff()
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
    //----------------------------------------------------------
    //近距離(突進)
    public void Boss1Fork()
    {
        //アニメーション再生
        if (!AnimFlg)
        {
            AnimFlagOnOff();
            BossAnim.SetBool("IdleToTake", true);
            BossTakeCase = BossMove.Boss_State.charge;
        }
        //一回の処理が終わっていたら開始

        if (OnlyFlg && MoveFlg)
        {
                //ボスが突進終了後に変える処理
                if (BossReturnFlg)
                {
                    RefrectFlg = false;
                    BossReturnTime += Time.deltaTime * RushReturnSpeed;
                    //最後まで攻撃し終わっていたら
                    if (RushEndFlg)
                    {
                    Debug.Log("あああああああああああああああああああああああ！！！");
                    //Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);
                    //方向が変わってたらスケールｘを反転
                    Boss1Manager.Boss.transform.localScale = Scale;
                    
                    //回転の目標値
                    Quaternion target = new Quaternion();
                    //向きを設定
                    target = Quaternion.LookRotation(Rotate);
                    //ゆっくり回転させる
                        Boss1Manager.Boss.transform.rotation = Quaternion.RotateTowards(Boss1Manager.Boss.transform.rotation, target, RotateSpeed);
                    }
                
                    //途中で弾かれていたら
                    if (!RushEndFlg)
                    {
                        Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, RushStartPoint, BossReturnTime);
                    }
                    //開始地点まで戻ってきたときにもろもろ初期化
                    if (BossReturnTime >= 1.0f)
                    {
                    if (Fork != null)
                    {
                        Destroy(Fork);
                    }
                    if (RushEndFlg)
                    {
                        if (!RFChange)
                        {
                            RFChange = true;
                        }
                        else if (RFChange)
                        {
                            RFChange = false;
                        }
                    }
                        ReturnDelay = false;
                        RushEndFlg = false;
                        BossReturnFlg = false;
                        AnimFlagOnOff();
                        BossAnim.SetBool("IdleToTake", false);
                        BossAnim.SetBool("RushToJump", false);
                        AnimMoveFlgOnOff();
                        BossReturnTime = 0;
                        BossAnim.speed = 1;
                    if (HPgage.currentHp >= 50)
                        {
                            BossMove.SetState(BossMove.Boss_State.idle);
                        }
                        if (HPgage.currentHp < 50)
                        {
                            Debug.Log("アイドルぅ！！！！！！！！！！！");
                            BossMove.AttackCount += 1;
                            BossMove.SetState(BossMove.Boss_State.idle);
                        }
                        OnlyFlg = false;
                    }
                    return;
                }
                //弾かれたら一回だけ処理する部分
                if (RefrectFlg)
                {
                    RushRefFlg = true;
                    RushPlayerPoint = Boss1Manager.Boss.transform.position;
                    BossAnim.SetBool("RushToJump", false);
                    BossAnim.SetBool("Blow", true);
                    RefrectFlg = false;
                }
                //弾かれていなかった場合の処理
                if (!RushRefFlg)
                {
                    RushTime += Time.deltaTime * RushSpeed;
                    Boss1Manager.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    Scale.x *= -1;
                    RushReturnSpeed = 1f;
                    RushEndFlg = true;
                    BossReturnFlg = true;
                    RushTime = 0;
                    return;
                }
            }
                //弾かれていた場合の処理
                if (RushRefFlg)
                {
                    RushRefTime += Time.deltaTime * 2f;
                    //壁にぶつけているように見せる
                    Boss1Manager.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                    if (RushRefTime >= 1.0f)
                    {
                        Destroy(Fork);
                    RushEndFlg = false;
                    BossAnim.SetBool("Blow", false);
                        BossAnim.SetTrigger("WallHit");
                        BossAnim.Play("WallHit");
                        BossAnim.speed = 0.3f;
                    if (ReturnDelay)
                    {
                        RushReturnSpeed = 3f;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        BossAnim.SetBool("IdleToTake", false);
                        BossAnim.SetBool("RushToJump", false);
                        HpScript.DelHP(RushDamage);
                        RushTime = 0;
                        RushRefTime = 0;
                        SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        return;
                    }
                    }
                }
            }
        
    }
    void ReturnGround()
    {
        ReturnDelay = true;
    }
    void BossRushAnim()
    {
        //突進攻撃を始めるために毎回一回だけ処理する部分
        if (!OnlyFlg)
        {
            WeaponAttackFlg = false;
            //右から左
            if (!RFChange)
            {
                OnlyFlg = true;
                Debug.Log("Pos : " + Boss1Manager.BossPos);
                RushStartPoint = GameObject.Find("BossPoint").transform.position;
                RushEndPoint = GameObject.Find("LeftBossPoint").transform.position;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                Rotate.x = 1;
                BossAnim.SetTrigger("TakeToRushTr");
                BossAnim.SetBool("RushToJump", true);
                BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
            //左から右
            else if(RFChange)
            {
                OnlyFlg = true;
                RushStartPoint = GameObject.Find("LeftBossPoint").transform.position;
                RushEndPoint = GameObject.Find("BossPoint").transform.position;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = GameObject.Find("LeftForkRefEndPoint").transform.position;
                Rotate.x = -1;
                BossAnim.SetTrigger("TakeToRushTr");
                BossAnim.SetBool("RushToJump", true);
                BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
        }
    }

    //----------------------------------------------------------
    //イチゴ攻撃
    public void Boss1Strawberry()
    {
        
        if (!OnlyFlg)
        {
            OnlyFlg = true;
            BossAnim.SetTrigger("Strawberry");
            BossAnim.Play("StrawBerry");
            
        }
        //イチゴの処理が全部終わったら一通り初期化
        if (AliveStrawberry >= Max_Strawberry)
        {
            AliveStrawberry = 0;
            StrawberryNum = 0;
            StrawBerryMany = 0;
            OnlyFlg = false;
            if (HPgage.currentHp >= 50)
            {
                BossMove.SetState(BossMove.Boss_State.idle);
            }
            if (HPgage.currentHp < 50)
            {
                BossMove.AttackCount += 1;
                BossMove.SetState(BossMove.Boss_State.idle);
            }
            return;
        }
        if (GameObject.Find("Weapon(Clone)"))
        {
            WeaponPos = GameObject.Find("Weapon(Clone)").transform.position;
        }
        //毎回使用しているイチゴの探索する
        for (int i = 0; i < Max_Strawberry; i++)
        {
            if (StrawBerryLagFlg[i])
            {
                if (StrawberryUseFlg[i])
                {
                    
                    if (StrawberryAimScale[i].x <= 2.5f)
                    {
                        StrawberryAim[i].transform.localScale = new Vector3(StrawberryAimScale[i].x, StrawberryAimScale[i].y, StrawberryAimScale[i].z);
                        StrawberryAimScale[i].x += 0.025f;
                        StrawberryAimScale[i].y += 0.025f;
                        StrawberryAimScale[i].z += 0.025f;
                    }
                    //弾かれたときに一回だけの処理
                    if (!StrawberryRefOnlyFlg[i] && StrawberryRefFlg[i])
                    {
                        StrawberryRefOnlyFlg[i] = true;
                        Vector2 Dir = Strawberry[i].transform.position - GameData.PlayerPos;
                        float rad = Mathf.Atan2(Dir.y, Dir.x);
                        float degree = rad * Mathf.Rad2Deg;
                        Debug.Log("はじいたぜぇぇぇぇぇぇぇぇぇ" + degree);
                        //弾いた角度が〇度だった時に飛んでく方向を変える処理。
                        //----------------------------------------------------------
                        if (!RFChange)
                        {
                            if (degree <= RefrectRotOver && degree >= RefrectRotUnder)
                            {
                                if (degree >= 45.0f)
                                {
                                    PlayerPoint[i].x = WeaponPos.x;
                                    PlayerPoint[i].y = WeaponPos.y + 2.0f;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    PlayerMiddlePoint[i].x = WeaponPos.x + 3.0f;
                                    PlayerMiddlePoint[i].y = WeaponPos.y + 3.0f;
                                    PlayerMiddlePoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                    PlayerRefDir[i] = true;
                                }
                                else
                                {
                                    PlayerPoint[i].x = WeaponPos.x + 2.0f;
                                    PlayerPoint[i].y = WeaponPos.y;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                }
                            }
                            else if (degree <= RefrectRotOver || degree >= RefrectRotUnder)
                            {
                                PlayerPoint[i].x = WeaponPos.x;
                                PlayerPoint[i].y = WeaponPos.y;
                                PlayerPoint[i].z = WeaponPos.z;
                                RefMiss = GameObject.Find("StrawberryMiss").transform.position;
                                RefEndPoint = RefMiss;
                                RefMissFlg = true;
                            }
                        }
                        if (RFChange)
                        {
                            if (degree >= RefrectRotOver && degree >= RefrectRotUnder * -1)
                            {
                                if (degree >= 135.0f)
                                {
                                    PlayerPoint[i].x = WeaponPos.x;
                                    PlayerPoint[i].y = WeaponPos.y - 2.0f;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    PlayerMiddlePoint[i].x = WeaponPos.x - 3.0f;
                                    PlayerMiddlePoint[i].y = WeaponPos.y - 3.0f;
                                    PlayerMiddlePoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                    PlayerRefDir[i] = true;
                                }
                                else
                                {
                                    PlayerPoint[i].x = WeaponPos.x - 2.0f;
                                    PlayerPoint[i].y = WeaponPos.y;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                }
                            }
                            else if (degree <= RefrectRotOver || degree >= RefrectRotUnder)
                            {
                                PlayerPoint[i].x = WeaponPos.x;
                                PlayerPoint[i].y = WeaponPos.y;
                                PlayerPoint[i].z = WeaponPos.z;
                                RefMiss = GameObject.Find("LeftStrawberryMiss").transform.position;
                                RefEndPoint = RefMiss;
                                RefMissFlg = true;
                            }
                        }
                        //----------------------------------------------------------
                    }
                    
                    //弾かれた後の処理
                    if (StrawberryRefFlg[i])
                    {
                        Ref_FinishTime[i] += Time.deltaTime * 2f;
                        //弾いた方向によって処理の種類を変える
                        //---------------------------------------------------------
                        if (!PlayerRefDir[i])
                        {
                            Strawberry[i].transform.position = Vector3.Lerp(PlayerPoint[i], RefEndPoint, Ref_FinishTime[i]);
                            Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                        }
                        if (PlayerRefDir[i])
                        {
                            Strawberry[i].transform.position = Beziercurve.SecondCurve(PlayerPoint[i], PlayerMiddlePoint[i],
                                                                                       RefEndPoint, Ref_FinishTime[i]);
                            Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                        }
                        //---------------------------------------------------------
                        //弾き終わったら弾いたイチゴを初期化
                        if (Ref_FinishTime[i] >= 1.0f)
                        {
                            PlayerRefDir[i] = false;
                            //弾い方がしっかりボスの方向だった時にだけダメージの処理する
                            if (!RefMissFlg)
                            {
                                HpScript.DelHP(StrawberryDamage);
                                SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                            }
                            RefMissFlg = false;
                            StrawberryUseFlg[i] = false;
                            StrawberryRefFlg[i] = false;
                            StrawberryRefOnlyFlg[i] = false;
                            StrawBerryLagFlg[i] = false;
                            Destroy(Strawberry[i]);
                            Destroy(StrawberryAim[i]);
                            Ref_FinishTime[i] = 0;
                            AliveStrawberry++;
                            FinishTime[i] = 0;
                        }
                    }
                    //弾かれていたらこっちの処理しない
                    if (!StrawberryRefFlg[i])
                    {
                        Strawberry[i].transform.position = Beziercurve.SecondCurve(StartPoint[i], MiddlePoint[i], EndPoint[i], FinishTime[i]);
                        Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                    }
                    if(i == 0)
                    {
                        StrawberrySpeed = 2;
                    }
                    else if(i == 1)
                    {
                        StrawberrySpeed = 1.5f;
                    }
                    else
                    {
                        StrawberrySpeed = 1;
                    }
                    FinishTime[i] += Time.deltaTime * StrawberrySpeed;
                    //----------------------------------------------------------
                    //プレイヤーにあったときに初期化する処理。
                    if (StrawberryColPlayer[i])
                    {
                        FinishTime[i] = 0;
                        StrawberryUseFlg[i] = false;
                        Destroy(Strawberry[i]);
                        Destroy(StrawberryAim[i]);
                        StrawBerryLagFlg[i] = false;
                        AliveStrawberry++;
                        
                    }
                    //弾が到着したら消す,攻撃をはじいていたら処理をしない
                    if (FinishTime[i] >= 1.0f && !StrawberryRefFlg[i])
                    {
                        FinishTime[i] = 0;
                        StrawberryUseFlg[i] = false;
                        Destroy(Strawberry[i]);
                        Destroy(StrawberryAim[i]);
                        StrawBerryLagFlg[i] = false;
                        AliveStrawberry++;
                        
                    }
                }
            }
        }
    }

    //イチゴをひとつづつ生成する処理、最大数より多く出ないように。
    void StrawBerryCreate()
        {
        if (!StrawberryUseFlg[StrawberryNum] && StrawberryNum < Max_Strawberry)
        {
            if (RFChange)
            {
                MiddlePoint[StrawberryNum] = GameObject.Find("LeftStrawberry").transform.position;
                EndPoint[StrawberryNum] = GameObject.Find("LeftStrawberryEnd").transform.position;
                RFNum = 1;
            }
            if(!RFChange)
            {
                MiddlePoint[StrawberryNum] = GameObject.Find("Strawberry").transform.position;
                EndPoint[StrawberryNum] = GameObject.Find("StrawberryEnd").transform.position;
                RFNum = -1;
            }
            MiddlePoint[StrawberryNum].x += (11f * StrawberryNum) * RFNum;
            EndPoint[StrawberryNum].x += (22f * StrawberryNum) * RFNum;
            //イチゴの座標指定
            if (StrawberryNum % 2 == 0)
            {
                StartPoint[StrawberryNum].x = GameObject.Find("middle2_R").transform.position.x;
                StartPoint[StrawberryNum].y = GameObject.Find("middle2_R").transform.position.y;
                StartPoint[StrawberryNum].z = GameObject.Find("middle2_R").transform.position.z;
            }
            if (StrawberryNum % 2 != 0)
            {
                StartPoint[StrawberryNum].x = GameObject.Find("middle2_L").transform.position.x;
                StartPoint[StrawberryNum].y = GameObject.Find("middle2_L").transform.position.y;
                StartPoint[StrawberryNum].z = GameObject.Find("middle2_L").transform.position.z;
            }
            //イチゴの生成後それぞれの名前変更
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint[StrawberryNum], Quaternion.identity);
            if (StrawberryNum % 2 == 0)
            {
                Strawberry[StrawberryNum].transform.parent = GameObject.Find("middle2_R").transform;
            }
            if (StrawberryNum % 2 != 0)
            {
                Strawberry[StrawberryNum].transform.parent = GameObject.Find("middle2_L").transform;
            }
            Strawberry[StrawberryNum].name = "strawberry" + StrawberryNum;
            StrawberryUseFlg[StrawberryNum] = true;
            //イチゴの使用状況変更
            StrawberryAim[StrawberryNum] = Instantiate(StrawberryAimObj, EndPoint[StrawberryNum], Quaternion.Euler(-7.952f, 0f, 0f));
            StrawberryAim[StrawberryNum].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            StrawberryAimScale[StrawberryNum] = new Vector3(1.0f, 1.0f, 1.0f);
            StrawberryNum++;
            SoundManager.Play(SoundData.eSE.SE_BOOS1_STRAWBERRY, SoundData.GameAudioList);

        }
        
    }
    void ShotStrawberry()
    {
        if (StrawBerryMany % 2 == 0)
        {
            
            StartPoint[StrawBerryMany].x = GameObject.Find("middle2_R").transform.position.x;
            StartPoint[StrawBerryMany].y = GameObject.Find("middle2_R").transform.position.y;
            StartPoint[StrawBerryMany].z = GameObject.Find("middle2_R").transform.position.z;
            Strawberry[StrawBerryMany].transform.parent.DetachChildren();

        }
        else if (StrawBerryMany % 2 == 1)
        {
            
            StartPoint[StrawBerryMany].x = GameObject.Find("middle2_L").transform.position.x;
            StartPoint[StrawBerryMany].y = GameObject.Find("middle2_L").transform.position.y;
            StartPoint[StrawBerryMany].z = GameObject.Find("middle2_L").transform.position.z;
            Strawberry[StrawBerryMany].transform.parent.DetachChildren();

        }
        StrawBerryLagFlg[StrawBerryMany] = true;
        
        StrawBerryMany++;
    }
    //----------------------------------------------------------
    public void Boss1Knife()
    {
        if (!AnimFlg)
        {
            AnimFlagOnOff();
            BossAnim.SetBool("TakeToKnife",true);
        }
        //ナイフ遠距離
        
            if (!AimFlg)
            {
                if (!AimOnly)
                {
                    KnifeAim = Instantiate(KnifeAimObj, GameData.PlayerPos, Quaternion.Euler(90f, 0f, 0f));
                    AimOnly = true;
                }
                KnifeAimPos.x = GameData.PlayerPos.x;
                KnifeAimPos.y = GameData.PlayerPos.y;
                KnifeAimPos.z = GameData.PlayerPos.z + 3.0f;

                if (KnifeThrowTime >= KnifeThrowNowTime)
                {
                    KnifeAim.transform.position = KnifeAimPos;
                    KnifeThrowNowTime += Time.deltaTime;
                }
                else
                {
                    AimFlg = true;
                    AimOnly = false;
                    KnifeThrowNowTime = 0;
                }
            }

        if (AimStart)
        {
            if (!OnlyFlg)
            {
                OnlyFlg = true;
                GameObject.Find("knife(Clone)").transform.parent.DetachChildren();
                Vector3 KnifeDir = GameData.PlayerPos - Knife.transform.position;
                // ターゲットの方向への回転
                KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
                KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
            }
            if (RefrectFlg)
            {
                KnifeRefFlg = true;
                KnifePlayerPoint = KnifePos;
                Vector3 KnifeDir = Boss1Manager.Boss.gameObject.transform.position - Knife.transform.position;
                // ターゲットの方向への回転
                KnifeRotDir = Quaternion.LookRotation(KnifeDir , Vector3.back);
                KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
                RefrectFlg = false;
            }
            if (OnlyFlg && !KnifeRefFlg)
            {
                KnifeTime += Time.deltaTime * KnifeSpeed;
                Knife.transform.position = Vector3.Lerp(KnifeStartPoint, KnifeEndPoint, KnifeTime);
                KnifePos = GameObject.Find("knife(Clone)").transform.position;
                if (KnifeTime >= 1.0f)
                {
                    OnlyFlg = false;
                    AimFlg = false;
                    AnimFlagOnOff();
                    Debug.Log("Aパターンが通ったよー");
                    BossAnim.SetBool("TakeToKnife", false);
                    KnifeTime = 0;
                    AimOnly = false;
                    AimStart = false;
                    Destroy(Knife);
                    Destroy(KnifeAim);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }

            if (KnifeRefFlg)
            {
                KnifeRefTime += Time.deltaTime * 3;
                Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss1Manager.BossPos, KnifeRefTime);
                Debug.Log("Knife " + KnifePlayerPoint);
                if (KnifeRefTime >= 1.0f)
                {
                    HpScript.DelHP(KnifeDamage);
                    OnlyFlg = false;
                    KnifeRefFlg = false;
                    AimFlg = false;
                    BossAnim.SetBool("??ToDamage", true);
                    BossAnim.Play("Damage");
                    BossAnim.SetBool("??ToDamage", false);
                    BossAnim.SetBool("TakeToKnife", false);
                    AnimFlagOnOff();
                    AimOnly = false;
                    AimStart = false;
                    KnifeTime = 0;
                    KnifeRefTime = 0;
                    Destroy(KnifeAim);
                    SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);

                    Destroy(Knife);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }
        }
            
    }
    void StartShotAnim()
    {
        AimStart = true;
        WeaponAttackFlg = false;
        KnifeStartPoint = GameObject.Find("KnifePos").transform.position;
        KnifeEndPoint = GameData.PlayerPos;
        KnifePos = GameObject.Find("KnifePos").transform.position;
        Knife = Instantiate(Knifeobj, KnifePos, Quaternion.identity);
        Knife.transform.parent = GameObject.Find("KnifePos").transform;
        SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
    }
    //大豪雨
    public void Boss1Rain()
    {
        WeaponAttackFlg = true;
        if (!g_Weapon[MaxWeapon - 1].UseFlg)
        {
            RainNowTime = RainNowTime + (1.0f / 60.0f);
            
            if (RainNowTime >= RainTime / MaxWeapon)
            {
                //生成
                if(RFChange)
                {
                    Range1 = GameObject.Find("LeftRange1").transform.position;
                    Range2 = GameObject.Find("LeftRange2").transform.position;
                }
                else
                {
                    Range1 = GameObject.Find("Range1").transform.position;
                    Range2 = GameObject.Find("Range2").transform.position;
                }
                RainRand.x = Random.Range(Range1.x, Range2.x);
                RainRand.y = Range1.y;
                RainRand.z = Range1.z;
                WeaponRand = Random.Range(1, 3);
                if (WeaponRand == 1)
                {
                    g_Weapon[RainNum].UseObj = Instantiate(Knifeobj, RainRand, Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f)));
                }
                if (WeaponRand == 2)
                {
                    g_Weapon[RainNum].UseObj = Instantiate(Forkobj, RainRand, Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f)));
                }
                g_Weapon[RainNum].UseObj.name = "BossWeapon" + RainNum;
                g_Weapon[RainNum].UseFlg = true;
                g_Weapon[RainNum].StartPoint = RainRand;
                g_Weapon[RainNum].EndPoint = RainRand;
                g_Weapon[RainNum].EndPoint.y = EndPoint[0].y;
                RainNum++;
                RainNowTime = 0;
            }
        }
        for (int i = 0; i < MaxWeapon;i++)
        {
            if (g_Weapon[i].UseFlg)
            {
                if (g_Weapon[i].RainRefOnlyFlg)
                {
                    g_Weapon[i].RainMoveTime = 0;
                    g_Weapon[i].RainRefOnlyFlg = false;
                    g_Weapon[i].ReturnPoint = g_Weapon[i].UseObj.gameObject.transform.position;
                    g_Weapon[i].ReturnMiddlePoint = GameObject.Find("RainRefMid").transform.position;
                    g_Weapon[i].ReturnEndPoint = GameObject.Find("RainRefEnd").transform.position;
                    g_Weapon[i].ReturnMiddlePoint.x = g_Weapon[i].StartPoint.x;
                    g_Weapon[i].ReturnEndPoint.x = g_Weapon[i].StartPoint.x;
                    g_Weapon[i].RollRand.x = 10 * (Random.Range(0, 2) * 2 - 1);
                    g_Weapon[i].RollRand.y = 10 * (Random.Range(0, 2) * 2 - 1);
                    g_Weapon[i].RollRand.z = 10 * (Random.Range(0, 2) * 2 - 1);
                }
                g_Weapon[i].RainMoveTime += Time.deltaTime * 1.25f;
                if (!g_Weapon[i].RainRefrectFlg)
                {
                    if(g_Weapon[i].UseObj != null)
                    g_Weapon[i].UseObj.transform.position = Vector3.Lerp(g_Weapon[i].StartPoint, g_Weapon[i].EndPoint, g_Weapon[i].RainMoveTime);
                }
                if (g_Weapon[i].RainRefrectFlg)
                {
                    if (g_Weapon[i].UseObj != null)
                    {
                        g_Weapon[i].UseObj.transform.position = Beziercurve.SecondCurve(g_Weapon[i].ReturnPoint, g_Weapon[i].ReturnMiddlePoint,
                                                                                        g_Weapon[i].ReturnEndPoint, g_Weapon[i].RainMoveTime);
                        g_Weapon[i].UseObj.transform.Rotate(new Vector3(g_Weapon[i].RollRand.x, g_Weapon[i].RollRand.y, g_Weapon[i].RollRand.z));
                    }
                }
                if (g_Weapon[i].RainMoveTime >= 1.0f)
                {
                    
                    if (g_Weapon[MaxWeapon - 1].RainMoveTime >= 1.0f)
                    {
                        
                        //ここ書き換えないと
                        
                            if (g_Weapon[MaxWeapon - 1].UseObj != null)
                            {
                                Destroy(g_Weapon[MaxWeapon - 1].UseObj);
                            }
                        
                        for (int j = 0; j < MaxWeapon; j++)
                        {
                            if(g_Weapon[j].RainRefrectFlg && g_Weapon[j].RainMoveTime >= 1.0f)
                            {
                                    if (g_Weapon[j].UseObj != null)
                                    {
                                        Destroy(g_Weapon[j].UseObj);
                                    }
                                    g_Weapon[j].UseFlg = false;
                                    g_Weapon[j].RainRefrectFlg = false;
                                    g_Weapon[j].RainMoveTime = 0;
                                
                            }
                            if (g_Weapon[j].RainRefrectFlg && g_Weapon[j].RainMoveTime <= 1.0f)
                            {
                                Debug.Log("なんだこいつぅぅぅ" + j);
                                return;
                            }
                        }
                        RainNum = 0;
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    g_Weapon[i].RainRefrectFlg = false;

                    
                        g_Weapon[i].UseFlg = false;
                        if (g_Weapon[i].UseObj != null)
                        {
                            Destroy(g_Weapon[i].UseObj);
                        }
                        g_Weapon[i].RainMoveTime = 0;
                    
                    
                }
            }
        }
    }
    //大豪雨アニメーションから用
    public void StartRain()
    {
        if (!AnimFlg)
        {
            AnimFlagOnOff();
            //BossAnim.SetBool("TakeToKnife", true);
        }
    }
}
