using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attack : MonoBehaviour
{
    //�{�X�̍U���̎��
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Idle,
    }
    static public bool RefrectFlg = false;                //�v���C���[���p���B�ɐ����������ǂ����̎󂯎��p
    static public bool OnlyFlg;
    static public Vector3 BossStartPoint;
    //�ːi�p�ϐ��Q
    //----------------------------------------------------------
    static GameObject Forkobj;
    static GameObject Fork;
    static public Vector3 RushStartPoint;
    static public Vector3 RushEndPoint;
    static public Vector3 RushPlayerPoint;
    static public Vector3 RushRefEndPoint;
    static public Vector3 RushMiddlePoint;
    static public bool OnlyRushFlg;
    [SerializeField] public float SFRushSpeed;
    static public float RushSpeed;
    static public bool RushRefFlg = false;
    static public float RushTime;
    static public float RushRefTime;
    static public bool BossReturnFlg;
    static public float BossReturnTime;
    static public bool RushEndFlg;
    static public float RushReturnSpeed;
    //----------------------------------------------------------
    //�C�`�S���e�ϐ�
    //----------------------------------------------------------
    static GameObject obj;                                       //�C�`�S�����p
    static public GameObject [] Strawberry;               //�C�`�S������i�[
    [SerializeField] public int SFMax_Strawberry;           //�ł����C�`�S�̔��f
    static public int Max_Strawberry;
    static public int StrawberryNum;
    static public int AliveStrawberry;
    static public Vector3 StrawberryPos;
    static public bool [] StrawberryUseFlg;
    static public bool [] StrawberryRefFlg;
    [SerializeField] public float SFStrawberrySpeed;
    static public float StrawberrySpeed;
    static public bool[] PlayerRefDir;

    //�x�W�G�Ȑ��p
    static public Vector3  StartPoint;
    static public Vector3 [] MiddlePoint;
    static public Vector3 [] EndPoint;
    [SerializeField] public Vector3 FirstMiddlePoint;
    [SerializeField] public Vector3 FirstEndPoint;
    static public float [] FinishTime;
    static public float[] Ref_FinishTime;
    static public  Vector3[] PlayerPoint;
    static public Vector3[] PlayerMiddlePoint;
    //----------------------------------------------------------
    //�i�C�t�����ϐ��Q
    //----------------------------------------------------------
    static GameObject Knifeobj;
    static GameObject Knife;
    static public Vector3 KnifeStartPoint;
    static public Vector3 KnifeEndPoint;
    static public Vector3 KnifePlayerPoint;
    static public float KnifeTime;
    [SerializeField] public float SFKnifeSpeed;
    static public float KnifeSpeed;

    static public bool KnifeRefFlg = false;
    static public float KnifeRefTime;
    //----------------------------------------------------------


    
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
        PlayerMiddlePoint = new Vector3[Max_Strawberry];
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        PlayerRefDir = new bool[Max_Strawberry];

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

    }
    //���ꂼ��̍U������
    public static void Boss1Fork()
    {
        //�ߋ���(�ːi)
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
                
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                if (RushEndFlg)
                {
                    Boss.BossPos = Beziercurve.SecondCurve(RushEndPoint, RushMiddlePoint, BossStartPoint, BossReturnTime);
                }
                if(!RushEndFlg)
                {
                    Boss.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);
                }

                if(BossReturnTime >= 1.0f)
                {

                    RushEndFlg = false;
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
                RushTime += Time.deltaTime * RushSpeed;
                Boss.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    RushReturnSpeed = 1;
                    RushEndFlg = true;
                    BossReturnFlg = true;
                    OnlyFlg = false;
                    RushMiddlePoint = GameObject.Find("RushMiddle").transform.position;
                    RushTime = 0;
                    Destroy(Fork);
                    
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
                    RushReturnSpeed = 2;
                    RushRefFlg = false;
                    BossReturnFlg = true;
                    HPgage.damage = 10;
                    HPgage.DelHP();
                    RushTime = 0;
                    RushRefTime = 0;
                    Destroy(Fork);
                   
                    return;
                }
            }
        }
        
    }
    public static void Boss1Strawberry()
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
            StartPoint.y = Boss.BossPos.y + 4;
            StartPoint.z = Boss.BossPos.z;
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint, Quaternion.identity);
            StrawberryUseFlg[StrawberryNum] = true;
            
        }
        for (int i = 0; i < Max_Strawberry; i++)
            {
            //�C�`�S
            if (StrawberryUseFlg[i])
            {
                //�e���ꂽ�Ƃ�
                if (RefrectFlg && !StrawberryRefFlg[i])
                {
                    StrawberryRefFlg[i] = true;
                    if (Strawberry[i].transform.position.y >= GameObject.Find("polySurface1").transform.position.y)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x;
                        PlayerPoint[i].y = GameData.PlayerPos.y + 2.0f;
                        PlayerPoint[i].z = GameData.PlayerPos.z;
                        PlayerMiddlePoint[i].x += GameObject.Find("polySurface1").transform.position.x + 3.0f;
                        PlayerMiddlePoint[i].y += GameObject.Find("polySurface1").transform.position.y + 3.0f;
                        PlayerMiddlePoint[i].z += GameObject.Find("polySurface1").transform.position.z;
                        PlayerRefDir[i] = true;
                    }
                    else if (Strawberry[i].transform.position.x >= GameObject.Find("star4").transform.position.x)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x + 2.0f;
                        PlayerPoint[i].y = GameData.PlayerPos.y;
                        PlayerPoint[i].z = GameData.PlayerPos.z;

                        
                    }

                    else if (Strawberry[i].transform.position.x <= GameObject.Find("polySurface1").transform.position.x)
                    {
                        PlayerPoint[i].x = GameData.PlayerPos.x - 2.0f;
                        PlayerPoint[i].y = GameData.PlayerPos.y;
                        PlayerPoint[i].z = GameData.PlayerPos.z;
                        PlayerMiddlePoint[i].x += GameObject.Find("polySurface1").transform.position.x - 3.0f;
                        PlayerMiddlePoint[i].y += GameObject.Find("polySurface1").transform.position.y + 3.0f;
                        PlayerMiddlePoint[i].z += GameObject.Find("polySurface1").transform.position.z;
                        PlayerRefDir[i] = true;
                    }
                    


                    RefrectFlg = false;
                }
                //�e���ꂽ��
                if (StrawberryRefFlg[i])
                {
                    
                    Ref_FinishTime[i] += Time.deltaTime * 3;
                   if(!PlayerRefDir[i])
                    {
                        Strawberry[i].transform.position = Vector3.Lerp(PlayerPoint[i], Boss.BossPos, Ref_FinishTime[i]);
                    }
                    if (PlayerRefDir[i])
                    {
                        Strawberry[i].transform.position = Beziercurve.SecondCurve(PlayerPoint[i], PlayerMiddlePoint[i], 
                                                                                   Boss.BossPos, Ref_FinishTime[i]);
                    }
                    if (Ref_FinishTime[i] >= 1.0f && StrawberryUseFlg[i])
                    {
                        PlayerRefDir[i] = false;
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
                //�C�`�S�������珉��������񂾂�by��
                //�e����Ă����炱�����̏������Ȃ�
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
                //�e���������������,�U�����͂����Ă����珈�������Ȃ�
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
    
    static public void Boss1Knife()
    {
        //�i�C�t������
        
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
