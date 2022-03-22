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
    //突進用変数群
    //----------------------------------------------------------
    GameObject Forkobj;
    //----------------------------------------------------------
    //イチゴ爆弾変数
    //----------------------------------------------------------
    GameObject obj;                                       //イチゴ生成用
    static public GameObject [] Strawberry;               //イチゴ生成後格納
    [SerializeField] public int Max_Strawberry;           //打ったイチゴの判断
    private int StrawberryNum;
    static public int AliveStrawberry;
    private Vector3 StrawberryPos;
    static public bool[] StrawberryUseFlg;
    static public bool [] StrawberryRefFlg;
    [SerializeField] public int StrawberrySpeed;

    //ベジエ曲線用
    private Vector3  StartPoint;
    private Vector3 [] MiddlePoint;
    private Vector3 [] EndPoint;
    [SerializeField] private Vector3 FirstMiddlePoint;
    [SerializeField] private Vector3 FirstEndPoint;
    static public float [] FinishTime;
    static public float[] Ref_FinishTime;
    public static Vector3[] PlayerPoint;
    //----------------------------------------------------------
    //ナイフ投げ変数群
    //----------------------------------------------------------
    GameObject Knifeobj;
    GameObject Knife;
    private Vector3 KnifeStartPoint;
    private Vector3 KnifeEndPoint;
    private Vector3 KnifePlayerPoint;
    private float KnifeTime;
    private bool OnlyFlg;
    [SerializeField] public int KnifeSpeed;
    static public bool KnifeRefFlg = false;
    private float KnifeRefTime;
    //----------------------------------------------------------



    private BossAttack Boss1AttackState = BossAttack.Attack1;
    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("strawberry");
        Knifeobj = (GameObject)Resources.Load("Knife");
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryUseFlg = new bool[Max_Strawberry];
        StrawberryRefFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        PlayerPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];
        Ref_FinishTime = new float[Max_Strawberry];
        
        for (int i=0;i < Max_Strawberry;i++)
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
        switch (Boss1AttackState)
        {
                //突進
            case BossAttack.Attack1:
                {
                    Boss1Attack1();
                    break;
                }
                //イチゴ
            case BossAttack.Attack2:
                {
                    Boss1Attack2();
                    break;
                }
                //ナイフ投げ
            case BossAttack.Attack3:
                {
                    Boss1Attack3();
                    break;
                }
                //疑似通常状態(仮)
            case BossAttack.Idle:
                {
                    break;
                }
        }
    }
    //それぞれの攻撃処理
    private void Boss1Attack1()
    {
        //近距離(突進)

    }
    private void Boss1Attack2()
    {
        if(AliveStrawberry>=Max_Strawberry)
        {
            AliveStrawberry = 0;
            StrawberryNum = 0;
            Boss1AttackState = BossAttack.Idle;
            return;
        }
        if (!StrawberryUseFlg[StrawberryNum])
        {
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
    
    private void Boss1Attack3()
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
        if (OnlyFlg && !KnifeRefFlg)
        {
            KnifeTime += Time.deltaTime * KnifeSpeed;
            Knife.transform.position = Vector3.Lerp(KnifeStartPoint, KnifeEndPoint, KnifeTime);
            if (KnifeTime >= 1.0f)
            {
                OnlyFlg = false;
                KnifeTime = 0;
                Destroy(Knife);
                Boss1AttackState = BossAttack.Idle;
                return;
            }
        }
        if(KnifeRefFlg)
        {
            KnifeRefTime += Time.deltaTime * 3;
            Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss.BossPos, KnifeRefTime);
            
            if(KnifeRefTime >= 1.0f)
            {
                HPgage.damage = 10;
                HPgage.DelHP();
                OnlyFlg = false;
                KnifeRefFlg = false;
                KnifeTime = 0;
                KnifeRefTime = 0;
                Destroy(Knife);
                Boss1AttackState = BossAttack.Idle;
                return;
            }
        }
            
    }
}
