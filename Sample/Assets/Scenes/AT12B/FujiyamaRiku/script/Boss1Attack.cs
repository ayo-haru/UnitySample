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
    int StrawberryNum;
    int AliveStrawberry;
    Vector3 StrawberryPos;
    bool [] StrawberryUseFlg;
    bool [] StrawberryRefFlg;
    [SerializeField] public float StrawberrySpeed;
    bool[] PlayerRefDir;
    Vector3 RefMiss;
    bool RefMissFlg;

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
        obj = (GameObject)Resources.Load("strawberry");
        Knifeobj = (GameObject)Resources.Load("Knife");
        Forkobj = (GameObject)Resources.Load("Fork");
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
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        PlayerRefDir = new bool[Max_Strawberry];
        RefMiss = GameObject.Find("StrawberryMiss").transform.position;

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

    }
    //それぞれの攻撃処理
    public void Boss1Fork()
    {
        //近距離(突進)
        if(!OnlyFlg)
        {
            OnlyFlg = true;
            Debug.Log("Pos : " + Boss.BossPos);
            RushStartPoint = Boss.BossPos;
            RushEndPoint = GameObject.Find("ForkEndPoint").transform.position;
            RushMiddlePoint = GameObject.Find("RushMiddle").transform.position;

            Fork = Instantiate(Forkobj, RushStartPoint, Quaternion.Euler(0.0f,0.0f,90.0f));
            Fork.transform.parent = Boss.Bossobj.transform;
        }
        if(OnlyFlg)
        {
            if(BossReturnFlg)
            {
                
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                if (RushEndFlg)
                {
                    Boss.BossPos = Beziercurve.SecondCurve(RushEndPoint, RushMiddlePoint, BossStartPoint, BossReturnTime);
                }
                if (!RushEndFlg)
                {
                    Boss.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);
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
                RushPlayerPoint.x = GameData.PlayerPos.x + 3.0f;
                RushPlayerPoint.y = GameData.PlayerPos.y;
                RushPlayerPoint.z = GameData.PlayerPos.z;
                RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                
                RefrectFlg = false; 
            }
            if (!RushRefFlg)
            {
                RushTime += Time.deltaTime * RushSpeed;
                Boss.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
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
                RushRefTime += Time.deltaTime * 2;
                Boss.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    
                        RushReturnSpeed = 2;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        HPgage.damage = 10;
                        HPgage.DelHP();
                        RushTime = 0;
                        RushRefTime = 0;
                    
                    return;
                }
            }
        }
    }
    public void Boss1Strawberry()
    {
        if(AliveStrawberry>= Max_Strawberry)
        {
            
            AliveStrawberry = 0;
            StrawberryNum = 0;
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
            return;
        }
        //Debug.Log("StrNum:" + StrawberryUseFlg[0]);
        if (!StrawberryUseFlg[StrawberryNum])
        {
            //Debug.Log("Strawberry");
            StartPoint.x = Boss.BossPos.x;
            StartPoint.y = Boss.BossPos.y + 4;
            StartPoint.z = Boss.BossPos.z;
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint, Quaternion.identity);
            StrawberryUseFlg[StrawberryNum] = true;
            
        }
        for (int i = 0; i < Max_Strawberry; i++)
            {
            //イチゴ
            if (StrawberryUseFlg[i])
            {
                //弾かれたとき
                if (RefrectFlg && !StrawberryRefFlg[i])
                {
                    StrawberryRefFlg[i] = true;
                    if (Strawberry[i].transform.position.y >= GameObject.Find("polySurface1").transform.position.y + 2.0f)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x;
                        PlayerPoint[i].y = GameData.PlayerPos.y + 2.0f;
                        PlayerPoint[i].z = GameData.PlayerPos.z;
                        PlayerMiddlePoint[i].x += GameObject.Find("polySurface1").transform.position.x + 3.0f;
                        PlayerMiddlePoint[i].y += GameObject.Find("polySurface1").transform.position.y + 3.0f;
                        PlayerMiddlePoint[i].z += GameObject.Find("polySurface1").transform.position.z;
                        RefEndPoint = Boss.BossPos;
                        PlayerRefDir[i] = true;
                    }
                    else if (Strawberry[i].transform.position.x >= GameObject.Find("star4").transform.position.x)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x + 2.0f;
                        PlayerPoint[i].y = GameData.PlayerPos.y;
                        PlayerPoint[i].z = GameData.PlayerPos.z;
                        RefEndPoint = Boss.BossPos;


                    }
                    else if (Strawberry[i].transform.position.x <= GameObject.Find("polySurface1").transform.position.x)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x - 2.0f;
                        PlayerPoint[i].y = GameData.PlayerPos.y;
                        PlayerPoint[i].z = GameData.PlayerPos.z;
                        RefEndPoint = RefMiss;
                        RefMissFlg = true;
                    }
                    


                    RefrectFlg = false;
                }
                //弾かれた後
                if (StrawberryRefFlg[i])
                {
                    
                    Ref_FinishTime[i] += Time.deltaTime * 2;
                   if(!PlayerRefDir[i])
                    {
                        Strawberry[i].transform.position = Vector3.Lerp(PlayerPoint[i], RefEndPoint, Ref_FinishTime[i]);
                    }
                    if (PlayerRefDir[i])
                    {
                        Strawberry[i].transform.position = Beziercurve.SecondCurve(PlayerPoint[i], PlayerMiddlePoint[i],
                                                                                   RefEndPoint, Ref_FinishTime[i]);
                    }
                    if (Ref_FinishTime[i] >= 1.0f && StrawberryUseFlg[i])
                    {
                        PlayerRefDir[i] = false;
                        if (!RefMissFlg)
                        {
                            HPgage.damage = 5;
                            HPgage.DelHP();
                        }
                        RefMissFlg = false;
                        StrawberryUseFlg[i] = false ;
                        StrawberryRefFlg[i] = false ;
                        Destroy(Strawberry[i]);
                        Ref_FinishTime[i] = 0;
                        FinishTime[i] = 0;
                        AliveStrawberry++;
                    }
                }
                //イチゴ消えたら初期化するんだぞby私
                //弾かれていたらこっちの処理しない
                if (!StrawberryRefFlg[i])
                {
                    Strawberry[i].transform.position = Beziercurve.SecondCurve(StartPoint, MiddlePoint[i], EndPoint[i], FinishTime[i]);
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
            KnifeStartPoint.x = Boss.BossPos.x;
            KnifeStartPoint.y = Boss.BossPos.y + 4;
            KnifeStartPoint.z = Boss.BossPos.z;
            KnifeEndPoint = GameData.PlayerPos;
            Knife = Instantiate(Knifeobj, KnifeStartPoint, Quaternion.identity);
            
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
            Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss.BossPos, KnifeRefTime);
            Debug.Log("Knife " + KnifePlayerPoint);
            if (KnifeRefTime >= 1.0f)
            {
                HPgage.damage = 10;
                HPgage.DelHP();
                OnlyFlg = false;
                KnifeRefFlg = false;
                KnifeTime = 0;
                KnifeRefTime = 0;
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
