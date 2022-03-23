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
    static GameObject Forkobj;
    static GameObject Fork;
    static public Vector3 RushStartPoint;
    static public Vector3 RushEndPoint;
    static public Vector3 RushPlayerPoint;
    static public Vector3 RushRefEndPoint;
    static public bool OnlyRushFlg;
    [SerializeField] public int SFRushSpeed;
    static public int RushSpeed;
    static public bool RushRefFlg = false;
    static public float RushTime;
    static public float RushRefTime;
    static public bool BossReturnFlg;
    static public float BossReturnTime;
    //----------------------------------------------------------
    //イチゴ爆弾変数
    //----------------------------------------------------------
    static GameObject obj;                                       //イチゴ生成用
    static public GameObject [] Strawberry;               //イチゴ生成後格納
    [SerializeField] public int SFMax_Strawberry;           //打ったイチゴの判断
    static public int Max_Strawberry;
    static public int StrawberryNum;
    static public int AliveStrawberry;
    static public Vector3 StrawberryPos;
    static public bool [] StrawberryUseFlg;
    static public bool [] StrawberryRefFlg;
    [SerializeField] public int SFStrawberrySpeed;
    static public int StrawberrySpeed;

    //ベジエ曲線用
    static public Vector3  StartPoint;
    static public Vector3 [] MiddlePoint;
    static public Vector3 [] EndPoint;
    [SerializeField] public Vector3 FirstMiddlePoint;
    [SerializeField] public Vector3 FirstEndPoint;
    static public float [] FinishTime;
    static public float[] Ref_FinishTime;
    static public  Vector3[] PlayerPoint;
    //----------------------------------------------------------
    //ナイフ投げ変数群
    //----------------------------------------------------------
    static GameObject Knifeobj;
    static GameObject Knife;
    static public Vector3 KnifeStartPoint;
    static public Vector3 KnifeEndPoint;
    static public Vector3 KnifePlayerPoint;
    static public float KnifeTime;
    [SerializeField] public int SFKnifeSpeed;
    static public int KnifeSpeed;

    static public bool KnifeRefFlg = false;
    static public float KnifeRefTime;
    //----------------------------------------------------------


    private BossAttack Boss1AttackState = BossAttack.Idle;
    // Start is called before the first frame update
    void Start()
    {

        Max_Strawberry = SFMax_Strawberry;
        StrawberrySpeed = SFStrawberrySpeed;
        RushSpeed = SFRushSpeed;
        KnifeSpeed = SFKnifeSpeed;

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
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        

        for (int i= 0;i < Max_Strawberry;i++)
        {
            
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            MiddlePoint[i] = FirstMiddlePoint;
            EndPoint[i] = FirstEndPoint;
            MiddlePoint[i].x -= (1.7f * i);
            EndPoint[i].x -= (4f * i);
        }
        OnlyFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Boss1AttackState = BossAttack.Attack1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Boss1AttackState = BossAttack.Attack2;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Boss1AttackState = BossAttack.Attack3;
        }
        

        //攻撃によって処理を変える処理
        //switch (Boss1AttackState)
        //{
        //        //突進
        //    case BossAttack.Attack1:
        //        {
        //            Boss1Attack1();
        //            break;
        //        }
        //        //イチゴ
        //    case BossAttack.Attack2:
        //        {
        //            Boss1Attack2();
        //            break;
        //        }
        //        //ナイフ投げ
        //    case BossAttack.Attack3:
        //        {
        //            Boss1Attack3();
        //            break;
        //        }
        //        //疑似通常状態(仮)
        //    case BossAttack.Idle:
        //        {
        //            break;
        //        }
        //}
    }
    //それぞれの攻撃処理
    public static void Boss1Attack1()
    {
        //近距離(突進)
        if(!OnlyFlg)
        {
            OnlyFlg = true;
            Debug.Log("Pos : " + Boss.BossPos);
            RushStartPoint = Boss.BossPos;
            RushEndPoint = GameObject.Find("ForkEndPoint").transform.position;
            Fork = Instantiate(Forkobj, RushStartPoint, Quaternion.identity);
            Fork.transform.parent = Boss.Bossobj.transform;
        }
        if(OnlyFlg)
        {
            if(BossReturnFlg)
            {
                
                BossReturnTime += Time.deltaTime * 2;
                Boss.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);

                if(BossReturnTime >= 1.0f)
                {
                    BossReturnFlg = false;
                    BossReturnTime = 0;
                    BossMove.SetState(BossMove.Boss_State.idle);
                    //Boss1AttackState = BossAttack.Idle;
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
                RushTime += Time.deltaTime / 2;
                Boss.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    OnlyFlg = false;
                    RushTime = 0;
                    Destroy(Fork);
                    BossMove.SetState(BossMove.Boss_State.idle);
                    //Boss1AttackState = BossAttack.Idle;
                    return;
                }
            }
            if (RushRefFlg)
            {
                RushRefTime += Time.deltaTime * 2;
                Boss.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    
                    BossReturnFlg = true;
                    HPgage.damage = 10;
                    HPgage.DelHP();
                    RushRefFlg = false;
                    RushTime = 0;
                    RushRefTime = 0;
                    Destroy(Fork);
                   
                    return;
                }
            }
        }
        
    }
    public static void Boss1Attack2()
    {
        if(AliveStrawberry>= Max_Strawberry)
        {
            
            AliveStrawberry = 0;
            StrawberryNum = 0;
            BossMove.SetState(BossMove.Boss_State.idle);
            return;
        }
        //Debug.Log("StrNum:" + StrawberryUseFlg[0]);
        if (!StrawberryUseFlg[StrawberryNum])
        {
            //Debug.Log("Strawberry");
            StartPoint.x = Boss.BossPos.x;
            StartPoint.y = Boss.BossPos.y + 2;
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
                    
                    PlayerPoint[i].x = GameData.PlayerPos.x + 3.0f;
                    PlayerPoint[i].y = GameData.PlayerPos.y;
                    PlayerPoint[i].z = GameData.PlayerPos.z;
                    RefrectFlg = false;
                    
                }
                //弾かれた後
                if (StrawberryRefFlg[i])
                {
                    
                    Ref_FinishTime[i] += Time.deltaTime * 3;
                    Strawberry[i].transform.position = Vector3.Lerp(PlayerPoint[i], Boss.BossPos, Ref_FinishTime[i]);
                    if (Ref_FinishTime[i] >= 1.0f && StrawberryUseFlg[i])
                    {
                        HPgage.damage = 5;
                        HPgage.DelHP();
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
    
    static public void Boss1Attack3()
    {
        //ナイフ遠距離
        
        if(!OnlyFlg)
        {
            
            OnlyFlg = true;
            KnifeStartPoint.x = Boss.BossPos.x;
            KnifeStartPoint.y = Boss.BossPos.y + 2;
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
                BossMove.SetState(BossMove.Boss_State.idle);
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
                BossMove.SetState(BossMove.Boss_State.idle);
                return;
            }
        }
            
    }
}
