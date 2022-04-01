using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attack : MonoBehaviour
{
    //ボスの攻撃の種類
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Idle,
    }
    static public bool RefrectFlg = false;                //プレイヤーがパリィに成功したかどうかの受け取り用
    static public bool OnlyFlg;
    static public Vector3 BossStartPoint;
    [SerializeField] public int RushDamage;
    [SerializeField] public int StrawberryDamage;
    [SerializeField] public int KnifeDamage;

    //突進用変数群
    //----------------------------------------------------------
    GameObject Forkobj;
    GameObject Fork;
    Vector3 RushStartPoint;
    Vector3 RushEndPoint;
    Vector3 RushPlayerPoint;
    Vector3 RushRefEndPoint;
    Vector3 RushMiddlePoint;
    bool OnlyRushFlg;
    [SerializeField] public float RushSpeed;
    bool RushRefFlg = false;
    float RushTime;
    float RushRefTime;
    bool BossReturnFlg;
    float BossReturnTime;
    bool RushEndFlg;
    float RushReturnSpeed;
    float ReturnDelay;
    //----------------------------------------------------------
    //イチゴ爆弾変数
    //----------------------------------------------------------
    GameObject obj;                                       //イチゴ生成用
    GameObject [] Strawberry;               //イチゴ生成後格納
    [SerializeField] public int Max_Strawberry;           //打ったイチゴの判断
    public static int PreMax_Strawberry;
    int StrawberryNum;
    int AliveStrawberry;
    Vector3 StrawberryPos;
    static bool[] StrawberryUseFlg;
    static public bool [] StrawberryRefFlg;
    [SerializeField] public float StrawberrySpeed;
    bool[] PlayerRefDir;
    Vector3 RefMiss;
    bool RefMissFlg;
    int [] SaveRef;
    int StrawBerryMany;
    bool[] StrawberryRefOnlyFlg;
    static public bool[] StrawberryColPlayer;

    //ベジエ曲線用
    Vector3  StartPoint;
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
    GameObject Knifeobj;
    GameObject Knife;
    Vector3 KnifeStartPoint;
    Vector3 KnifeEndPoint;
    Vector3 KnifePlayerPoint;
    float KnifeTime;
    [SerializeField] public float KnifeSpeed;
    bool KnifeRefFlg = false;
    float KnifeRefTime;
    //----------------------------------------------------------


    
    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("strawberryPre");
        Knifeobj = (GameObject)Resources.Load("knifePre");
        Forkobj = (GameObject)Resources.Load("forkPre");
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryUseFlg = new bool[Max_Strawberry];
        StrawberryRefFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        PlayerPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];
        Ref_FinishTime = new float[Max_Strawberry];
        PlayerMiddlePoint = new Vector3[Max_Strawberry];
        SaveRef = new int[Max_Strawberry];
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        PlayerRefDir = new bool[Max_Strawberry];
        RefMiss = GameObject.Find("StrawberryMiss").transform.position;
        StrawberryRefOnlyFlg = new bool[Max_Strawberry];
        StrawberryColPlayer = new bool[Max_Strawberry];
        PreMax_Strawberry = Max_Strawberry;

        for (int i= 0;i < Max_Strawberry;i++)
        {
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            MiddlePoint[i] = GameObject.Find("CubeEnd").transform.position; 
            EndPoint[i] = GameObject.Find("Cube").transform.position; 
            MiddlePoint[i].x -= (2.2f * i);
            EndPoint[i].x -= (4.4f * i);
        }
        OnlyFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
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
            Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_END;
        }
    }
    //それぞれの攻撃処理
    public void Boss1Fork()
    {
        //近距離(突進)
        if(!OnlyFlg)
        {
            OnlyFlg = true;
            Debug.Log("Pos : " + Boss1Manager.BossPos);
            RushStartPoint = Boss1Manager.BossPos;
            RushEndPoint = GameObject.Find("ForkEndPoint").transform.position;
            RushMiddlePoint = GameObject.Find("RushMiddle").transform.position;
            RushStartPoint.y -= 3.0f;
            Fork = Instantiate(Forkobj, RushStartPoint, Quaternion.Euler(0.0f,0.0f,90.0f));
            RushStartPoint.y +=3.0f;
            Fork.transform.parent = Boss1Manager.Boss.transform;
            SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
        }
        if(OnlyFlg)
        {
            if(BossReturnFlg)
            {
                RefrectFlg = false;
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                if (RushEndFlg)
                {
                    Boss1Manager.BossPos = Beziercurve.SecondCurve(RushEndPoint, RushMiddlePoint, BossStartPoint, BossReturnTime);
                }
                if (!RushEndFlg)
                {
                    Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);
                }

                if (BossReturnTime >= 1.0f)
                {
                    
                    Destroy(Fork);
                    ReturnDelay = 0;
                    RushEndFlg = false;
                    BossReturnFlg = false;
                    BossReturnTime = 0;
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if(HPgage.currentHp < 50)
                    {
                        Debug.Log("アイドルぅ！！！！！！！！！！！");
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    
                    OnlyFlg = false;
                }
                return;
            }
            if (RefrectFlg)
            {
                RushRefFlg = true;
                RushPlayerPoint.x = GameData.PlayerPos.x + 2.0f;
                RushPlayerPoint.y = GameData.PlayerPos.y + 3.0f;
                RushPlayerPoint.z = GameData.PlayerPos.z;
                RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                
                RefrectFlg = false; 
            }
            if (!RushRefFlg)
            {
                RushTime += Time.deltaTime * RushSpeed;
                Boss1Manager.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    ReturnDelay += Time.deltaTime;
                    if (ReturnDelay >= 1.0f)
                    {
                        RushReturnSpeed = 1.5f;
                        RushEndFlg = true;
                        BossReturnFlg = true;
                        RushTime = 0;
                        
                    }
                    return;
                }
            }
            if (RushRefFlg)
            {
                RushRefTime += Time.deltaTime * 2f;
                Boss1Manager.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    
                        RushReturnSpeed = 2;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        HPgage.damage = RushDamage;
                        HPgage.DelHP();
                        RushTime = 0;
                        RushRefTime = 0;
                    SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                    return;
                }
            }
        }
    }
    public void Boss1Strawberry()
    {
        //AliveStrawberryが終わっていなくて、最後のイチゴが排出された後でも続けて最後の弾が出ちゃう。
        if (AliveStrawberry >= Max_Strawberry)
        {
            
            AliveStrawberry = 0;
            StrawberryNum = 0;
            StrawBerryMany = 0;
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
        if (!StrawberryUseFlg[StrawberryNum] && StrawBerryMany < Max_Strawberry)
        {
            StartPoint.x = Boss1Manager.BossPos.x;
            StartPoint.y = Boss1Manager.BossPos.y + 4;
            StartPoint.z = Boss1Manager.BossPos.z;
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint, Quaternion.identity);
            Strawberry[StrawberryNum].name = "strawberry" + StrawberryNum;
            StrawberryUseFlg[StrawberryNum] = true;
            StrawBerryMany += 1;
            SoundManager.Play(SoundData.eSE.SE_BOOS1_STRAWBERRY, SoundData.GameAudioList);

        }
        for (int i = 0; i < Max_Strawberry; i++)
            {
            //イチゴ
            if (StrawberryUseFlg[i])
            {
                //弾かれたとき
                if (!StrawberryRefOnlyFlg[i] && StrawberryRefFlg[i])
                {
                    StrawberryRefOnlyFlg[i] = true;
                    Vector2 Dir = Strawberry[i].transform.position - GameData.PlayerPos;
                    float rad = Mathf.Atan2(Dir.y, Dir.x);
                    float degree = rad * Mathf.Rad2Deg;

                    Debug.Log("はいっちゃうぅぅぅ！！" + degree);
                    if (degree <= 80.0f && degree >= -90.0f)
                    {
                        Debug.Log("はいっちゃうぅぅぅ！！" + degree);
                        if (degree >= 45.0f)
                        {
                            PlayerPoint[i].x = GameData.PlayerPos.x;
                            PlayerPoint[i].y = GameData.PlayerPos.y + 2.0f;
                            PlayerPoint[i].z = GameData.PlayerPos.z;
                            PlayerMiddlePoint[i].x = GameData.PlayerPos.x + 3.0f;
                            PlayerMiddlePoint[i].y = GameData.PlayerPos.y + 3.0f;
                            PlayerMiddlePoint[i].z = GameData.PlayerPos.z;
                            RefEndPoint = Boss1Manager.BossPos;
                            PlayerRefDir[i] = true;
                        }
                        else
                        {
                            PlayerPoint[i].x = GameData.PlayerPos.x + 2.0f;
                            PlayerPoint[i].y = GameData.PlayerPos.y;
                            PlayerPoint[i].z = GameData.PlayerPos.z;
                            RefEndPoint = Boss1Manager.BossPos;
                        }
                    }
                    else if (degree >= 80.0f || degree <= -90.0f)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x - 2.0f;
                        PlayerPoint[i].y = GameData.PlayerPos.y;
                        PlayerPoint[i].z = GameData.PlayerPos.z;
                        RefEndPoint = RefMiss;
                        RefMissFlg = true;
                    }
                }
                //弾かれた後
                if (StrawberryRefFlg[i])
                {
                    Ref_FinishTime[i] += Time.deltaTime * 2f;
                   if(!PlayerRefDir[i])
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
                    if (Ref_FinishTime[i] >= 1.0f)
                    {
                        PlayerRefDir[i] = false;
                        if (!RefMissFlg)
                        {
                            HPgage.damage = StrawberryDamage;
                            HPgage.DelHP();
                            SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        }
                        RefMissFlg = false;
                        StrawberryUseFlg[i] = false ;
                        StrawberryRefFlg[i] = false ;
                        StrawberryRefOnlyFlg[i] = false; ;
                        Destroy(Strawberry[i]);
                        Ref_FinishTime[i] = 0;
                        FinishTime[i] = 0;
                        AliveStrawberry++;
                    }
                }
                //弾かれていたらこっちの処理しない
                if (!StrawberryRefFlg[i])
                {
                    Strawberry[i].transform.position = Beziercurve.SecondCurve(StartPoint, MiddlePoint[i], EndPoint[i], FinishTime[i]);
                    Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                }
                FinishTime[i] += Time.deltaTime * StrawberrySpeed;
                if (i < Max_Strawberry - 1)
                {
                    if (FinishTime[i] >= 0.5f && !StrawberryUseFlg[i + 1] && !StrawberryRefFlg[i])
                    {
                        StrawberryNum++;
                    }
                }
                //----------------------------------------------------------
                //弾が到着したら消す,攻撃をはじいていたら処理をしない
                if(StrawberryColPlayer[i])
                {
                    FinishTime[i] = 0;
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                    AliveStrawberry++;
                }
                if (FinishTime[i] >= 1.0f && !StrawberryRefFlg[i])
                {
                    FinishTime[i] = 0;
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                    AliveStrawberry++;
                }
            }
        }
    }
    
    public void Boss1Knife()
    {
        //ナイフ遠距離
        
        if(!OnlyFlg)
        {
            
            OnlyFlg = true;
            KnifeStartPoint.x = Boss1Manager.BossPos.x;
            KnifeStartPoint.y = Boss1Manager.BossPos.y + 4;
            KnifeStartPoint.z = Boss1Manager.BossPos.z;
            KnifeEndPoint = GameData.PlayerPos;

            Knife = Instantiate(Knifeobj, KnifeStartPoint, Quaternion.Euler(0.0f,0.0f,90.0f));

            Vector3 KnifeDir = GameData.PlayerPos - Knife.transform.position;
            // ターゲットの方向への回転
            Knife.transform.rotation = Quaternion.LookRotation(KnifeDir, Vector3.forward);

            SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
            
        }
        if (RefrectFlg)
        {
            KnifeRefFlg = true;
            KnifePlayerPoint.x = GameData.PlayerPos.x + 3.0f;
            KnifePlayerPoint.y = GameData.PlayerPos.y;
            KnifePlayerPoint.z = GameData.PlayerPos.z;
            RefrectFlg = false;
        }
        if (OnlyFlg && !KnifeRefFlg)
        {
            KnifeTime += Time.deltaTime * KnifeSpeed;
            Knife.transform.position = Vector3.Lerp(KnifeStartPoint, KnifeEndPoint, KnifeTime);
            if (KnifeTime >= 1.0f)
            {
                OnlyFlg = false;
                KnifeTime = 0;
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
        
        if(KnifeRefFlg)
        {
            KnifeRefTime += Time.deltaTime * 3;
            Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss1Manager.BossPos, KnifeRefTime);
            Debug.Log("Knife " + KnifePlayerPoint);
            if (KnifeRefTime >= 1.0f)
            {
                HPgage.damage = KnifeDamage;
                HPgage.DelHP();
                OnlyFlg = false;
                KnifeRefFlg = false;
                KnifeTime = 0;
                KnifeRefTime = 0;
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
