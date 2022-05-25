using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StrawBerry : MonoBehaviour
{
    Boss1Attack BossAttack;

    GameObject obj;                                         //イチゴ生成用
    GameObject[] Strawberry;                               //イチゴ生成後格納
    [SerializeField] public int Max_Strawberry;             //イチゴの最大数
    static public int PreMax_Strawberry;                    //最大数をほかの部分でも使えるように
    int StrawberryNum;                                      //現在の射出済みイチゴ計算用
    int AliveStrawberry;                                    //イチゴの生存確認用
    bool[] StrawberryUseFlg;                         //イチゴを使っているかどうかのフラグ
    public bool[] StrawberryRefFlg;                 //イチゴが弾かれたかどうかのフラグ
    [SerializeField] public float StrawberrySpeed;          //イチゴが飛んでいく速度
    bool[] PlayerRefDir;                                    //弾いたときの方向フラグ
    Vector3 RefMiss;                                        //弾くのに失敗したときの座標格納用
    bool RefMissFlg;                                        //弾くのに失敗したときに処理を一回だけする用
    int StrawBerryMany;                                     //イチゴを最大数以上出さないようにするための処理←たぶんいらない
    bool[] StrawberryRefOnlyFlg;                            //弾かれたもので一回だけ処理するもの用
    public bool[] StrawberryColPlayer;               //プレイヤーに当たった時用の処理
    GameObject StrawberryAimObj;
    GameObject[] StrawberryAim;
    Vector3[] StrawberryAimScale;
    bool[] StrawBerryLagFlg;
    Vector3 WeaponPos;

    //ベジエ曲線用
    Vector3[] StartPoint;
    Vector3[] MiddlePoint;
    Vector3[] EndPoint;
    float[] FinishTime;
    Vector3 RefEndPoint;
    float[] Ref_FinishTime;
    Vector3[] PlayerPoint;
    Vector3[] PlayerMiddlePoint;
    // Start is called before the first frame update
    void Start()
    {
        BossAttack = this.GetComponent<Boss1Attack>();
        obj = (GameObject)Resources.Load("strawberry");
        StrawberryAimObj = (GameObject)Resources.Load("StrawberryAim");
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
        PlayerRefDir = new bool[Max_Strawberry];
        StrawberryRefOnlyFlg = new bool[Max_Strawberry];
        StrawberryColPlayer = new bool[Max_Strawberry];
        PreMax_Strawberry = Max_Strawberry;
        for (int i = 0; i < Max_Strawberry; i++)
        {
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            MiddlePoint[i] = GameObject.Find("Strawberry").transform.position;
            EndPoint[i] = GameObject.Find("StrawberryEnd").transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
            for (int i = 0; i < Max_Strawberry; i++)
            {
                if (Strawberry[i] != null)
                {
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                }
            }
        }
    }
    public void Boss1Strawberry()
    {

        if (!BossAttack.OnlyFlg)
        {
            BossAttack.OnlyFlg = true;
            BossAttack.BossAnim.SetTrigger("Strawberry");
            BossAttack.BossAnim.Play("StrawBerry");

        }
        //イチゴの処理が全部終わったら一通り初期化
        if (AliveStrawberry >= Max_Strawberry)
        {
            AliveStrawberry = 0;
            StrawberryNum = 0;
            StrawBerryMany = 0;
            BossAttack.OnlyFlg = false;
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
                        if (!BossAttack.RFChange)
                        {
                            if (degree <= BossAttack.RefrectRotOver && degree >= BossAttack.RefrectRotUnder)
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
                            else if (degree <= BossAttack. RefrectRotOver || degree >= BossAttack.RefrectRotUnder)
                            {
                                PlayerPoint[i].x = WeaponPos.x;
                                PlayerPoint[i].y = WeaponPos.y;
                                PlayerPoint[i].z = WeaponPos.z;
                                RefMiss = GameObject.Find("StrawberryMiss").transform.position;
                                RefEndPoint = RefMiss;
                                RefMissFlg = true;
                            }
                        }
                        if (BossAttack.RFChange)
                        {
                            if (degree >= BossAttack.RefrectRotOver && degree >= BossAttack.RefrectRotUnder * -1)
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
                            else if (degree <= BossAttack.RefrectRotOver || degree >= BossAttack.RefrectRotUnder)
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
                            BossAttack.DamageColor.Invoke("Play", 0.0f);
                            EffectManager.Play(EffectData.eEFFECT.EF_BOSS_STRAWBERRY, Strawberry[i].transform.position);
                            PlayerRefDir[i] = false;
                            //弾い方がしっかりボスの方向だった時にだけダメージの処理する
                            if (!RefMissFlg)
                            {
                                BossAttack.HpScript.DelHP(BossAttack.StrawberryDamage);
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
                    if (i == 0)
                    {
                        StrawberrySpeed = 2;
                    }
                    else if (i == 1)
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
            
            if (BossAttack.RFChange)
            {
                MiddlePoint[StrawberryNum] = GameObject.Find("LeftStrawberry").transform.position;
                EndPoint[StrawberryNum] = GameObject.Find("LeftStrawberryEnd").transform.position;
                BossAttack.RFNum = 1;
            }
            if (!BossAttack.RFChange)
            {
                MiddlePoint[StrawberryNum] = GameObject.Find("Strawberry").transform.position;
                EndPoint[StrawberryNum] = GameObject.Find("StrawberryEnd").transform.position;
                BossAttack.RFNum = -1;
            }
            MiddlePoint[StrawberryNum].x += (11f * StrawberryNum) * BossAttack.RFNum;
            EndPoint[StrawberryNum].x += (22f * StrawberryNum) * BossAttack.RFNum;
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
}
