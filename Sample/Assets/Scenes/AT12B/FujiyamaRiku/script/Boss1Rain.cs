using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rain : MonoBehaviour
{
    Boss1Attack BossAttack;
    //BossWeapon Weapon;
    GameObject Forkobj;                                     //フォークのオブジェクト生成用
    GameObject Knifeobj;                                    //ナイフ生成用

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
        public bool FadeFlg;
        public float DelTime;
        public bool delFlg;
        public Vector3 RollRand;
        public BossWeapon Weapon;
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
    bool StartFlg;
    public int LoopSave = 0;
    public int LoopSaver = 0;

    public RainWeapon[] g_Weapon;
    // Start is called before the first frame update
    void Start()
    {
        Knifeobj = (GameObject)Resources.Load("Knife");
        Forkobj = (GameObject)Resources.Load("Fork");
        BossAttack = this.GetComponent<Boss1Attack>();
        
        Range1 = GameObject.Find("Range1").transform.position;
        Range2 = GameObject.Find("Range2").transform.position;
        g_Weapon = new RainWeapon[MaxWeapon];
         
        PreMaxWeapon = MaxWeapon;
        for (int i = 0; i < MaxWeapon; i++)
        {
            g_Weapon[i].UseFlg = false;
            g_Weapon[i].RainRefrectFlg = false;
            g_Weapon[i].RainMoveTime = 0.0f;
            g_Weapon[i].DelTime = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BossRain()
    {
        BossAttack.WeaponAttackFlg = true;
        if (!BossAttack.AnimFlg)
        {
            BossAttack.AnimFlagOnOff();
            BossAttack.BossAnim.SetTrigger("RainThrow");
        }
        if (StartFlg)
        {
            if (!g_Weapon[MaxWeapon - 1].UseFlg)
            {
                RainNowTime = RainNowTime + (1.0f / 60.0f);

                if (RainNowTime >= RainTime / MaxWeapon)
                {
                    //生成
                    if (BossAttack.RFChange)
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
                    g_Weapon[RainNum].Weapon = g_Weapon[RainNum].UseObj.GetComponent<BossWeapon>();
                    g_Weapon[RainNum].UseFlg = true;
                    g_Weapon[RainNum].StartPoint = RainRand;
                    g_Weapon[RainNum].EndPoint = RainRand;
                    g_Weapon[RainNum].EndPoint.y = GameObject.Find("StrawberryEnd").transform.position.y;
                    RainNum++;
                    RainNowTime = 0;
                }
            }
            for (int i = 0; i < MaxWeapon; i++)
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
                        if (g_Weapon[i].UseObj != null)
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

                        if (!g_Weapon[i].FadeFlg)
                        {
                            g_Weapon[i].FadeFlg = true;
                            g_Weapon[i].Weapon.Invoke("Play", 0.1f);

                        }
                        
                            g_Weapon[i].DelTime += Time.deltaTime;
                        
                        if (g_Weapon[MaxWeapon - 1].RainMoveTime >= 1.0f)
                        {
                            //ここ書き換えないと
                            if (g_Weapon[MaxWeapon - 1].DelTime >= 0.6f)
                            {
                                if (g_Weapon[MaxWeapon - 1].UseObj != null)
                                {
                                    Destroy(g_Weapon[MaxWeapon - 1].UseObj);
                                }
                            }
                            
                                if (g_Weapon[i].RainRefrectFlg && g_Weapon[i].RainMoveTime >= 1.0f)
                                {
                                    
                                        if (g_Weapon[i].UseObj != null)
                                        {
                                            Destroy(g_Weapon[i].UseObj);
                                        }
                                        g_Weapon[i].UseFlg = false;
                                        g_Weapon[i].RainRefrectFlg = false;
                                        g_Weapon[i].RainMoveTime = 0;

                                    
                                }
                                if(!g_Weapon[i].delFlg && g_Weapon[i].DelTime >= 0.6f)
                                {

                                    g_Weapon[i].delFlg = true;
                                    g_Weapon[i].RainRefrectFlg = false;
                                    g_Weapon[i].UseFlg = false;
                                    if (g_Weapon[i].UseObj != null)
                                    {
                                        Destroy(g_Weapon[i].UseObj);
                                    }
                                    g_Weapon[i].RainMoveTime = 0;
                                    g_Weapon[i].DelTime = 0;
                                }
                                
                                if (g_Weapon[i].RainRefrectFlg && g_Weapon[i].RainMoveTime <= 1.0f)
                                {
                                    Debug.Log("なんだこいつぅぅぅ" + i);
                                    return;
                                }
                                if (g_Weapon[i].DelTime <= 0.6f)
                                {
                                Debug.Log("ああああああああああああああああああああああああああああああああああああああ" + i);
                                return;
                                }
                            
                            RainNum = 0;
                            BossAttack.BossAnim.SetBool("Tired", false);
                            BossAttack.AnimFlagOnOff();
                            BossMove.SetState(BossMove.Boss_State.idle);
                            return;
                        }
                        if (!g_Weapon[i].delFlg && g_Weapon[i].DelTime >= 0.6f)
                        {
                            g_Weapon[i].delFlg = true;
                            g_Weapon[i].RainRefrectFlg = false;
                            g_Weapon[i].UseFlg = false;
                            if (g_Weapon[i].UseObj != null)
                            {
                                Destroy(g_Weapon[i].UseObj);
                            }
                            g_Weapon[i].RainMoveTime = 0;
                            g_Weapon[i].DelTime = 0;
                        }


                    }
                }
            }
        }
    }
    //大豪雨アニメーションから用
    public void StartRain()
    {
        BossAttack.BossAnim.SetBool("Tired", true);
        StartFlg = true;
    }
}
