using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_Attack : MonoBehaviour
{
    //「状態推移:列挙型」===================　
    public enum LB_State
    {
        Idle,           //待機(0)
        RandomMove,     //ランダム移動(1)
        Damage,         //ダメージ(2)
        TrackingBullet, //追従弾(3)
        BoundBoll,      //バウンド弾(4)
        WarpBullet,     //ワープボール（5）
        ArrowAttack,    //アローアタック(6)
    }
    //------------------------------------

    //[[[[[idleの処理で使う変数]]]]]=========================================================================
    private Vector3 defaultPos;                             //ボスが登場した最初の位置
    static private LB_State LBState = LB_State.Idle;        //ボスの状態(初期値はidel)
    private int RandomNumbe = 0;                            //モーションのランダム抽選用の数
    [SerializeField] private float timeToStayInIdle;        //idle状態で留まる時間(フレーム指定)
    private float elapsedTimeOfIdleState = 0f;              //idle状態の経過時間
    [SerializeField] private int MaxAttack = 2;             //待機モーションなしの攻撃回数（HP50％以下のみ）
    public static int AttackCount = 0;                      //攻撃数カウント
    //[[[[[ArrowAttackの処理で使う変数]]]]]==================================================================
    public GameObject Arrow_Right;                          //GameObject:Arrow_Right
    public GameObject Arrow_Left;                           //GameObject:Arrow_Left
    public GameObject Arrow_Up;                             //GameObject:Arrow_Up
    public Rigidbody LbRigidbody;                           //リジットボディ
    public bool ArrowUseFlag = true;                        //アローが使用中かどうか調べるフラグ（初期値：true)
    public int ArrowCount = 0;                              //アローカウント
    [SerializeField] public int Arrow_MaxSpeed;             //アローの速さ調節用
    //抽選用の変数-------------------------------------------------------------------------------------------
    List<int> number = new List<int>();                     //抽選する最大数(List構造)
    private int random;                                     //抽選した番号
    private int Index;                                      //抽選する要素の指定
    //[[[[[BoundBollの処理で使う変数]]]]]====================================================================
    public GameObject BoundBoll;                            //GameObject:BoundBoll
    public GameObject firingPoint;                          //弾が発射される場所
    [SerializeField] public int Bound_MaxSpeed;             //BoundBollの速度
    //[[[[WarpBulletの処理で使う変数]]]]]====================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    GameObject WarpBullets;                                 //WarpBulletで生成する弾
    [SerializeField] public int WarpBullet_MaxSpeed;        //弾の速度
    public int WarpCount = 0;                               //アローカウント
    //ランダム移動用の変数-----------------------------------------------------------------------------------
    private float time;                                     //移動までの時間
    private float vecX;                                     //座標指定用：X座標格納場所
    private float vecY;                                     //座標指定用：Y座標格納場所
    //[[[[[Bulletの処理で使う変数]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    [SerializeField] public int Bullet_MaxSpeed;            //弾の速度
    public bool BulletUseFlag = true;                       //バレットが使用中かどうか調べるフラグ（初期値：true)
    //-----------------------------------------------------------------------------------------------------
    private GameObject HpObject;
    public HPgage HpScript;
    public Animator BossAnim;
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool MoveFlg;
    //必殺技用フラグ(〇%以下になったとき一回)
    bool UltFlg;


    // Start is called before the first frame update
    void Start()
    {
        LBState = LB_State.Idle;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();
        BossAnim = this.gameObject.GetComponent<Animator>();
        //---抽選する番号をリスト構造に格納
        //---抽選回数を指定(ここでは1~3)
        for (int i = 1; i <= 3; i++) {
            number.Add(i);
        }
    }
    //-----------------------------------------------

    // Update is called once per frame----------------------------------------------------
    void Update()
    {

        if (!Pause.isPause)
        {
            if (LB_Manager.LB_States == LB_Manager.LB_State.LB_BATTLE)
            {
                if (LBState == LB_State.Idle)       //もしボスの状態が待機の場合
                {
                    Idle();
                }
                else if (LBState == LB_State.RandomMove)//もしボスの状態がダメージの場合
                {

                }
                else if (LBState == LB_State.Damage)//もしボスの状態がダメージの場合
                {
                    //damage();
                }
                else if (LBState == LB_State.TrackingBullet)//もしボスの状態がナイフ投げの場合
                {
                    BulletAttack();
                }
                else if (LBState == LB_State.BoundBoll)//もしボスの状態がイチゴ爆弾の場合
                {
                    BoundBollAttack();
                }
                else if (LBState == LB_State.WarpBullet)//もしボスの状態が突進の場合
                {
                    if (WarpCount <= 5)
                    {
                        if (WarpBullets == null)
                        {
                            WarpBulletAttack();
                        }
                    }
                }
                else if (LBState == LB_State.ArrowAttack)//もしボスの状態がナイフ投げの場合
                {
                    //---矢印攻撃ならば------------
                    if (ArrowUseFlag)
                    {
                        //--最後のリストの要素数に達したら
                        if (number.Count <= 0)
                        {
                            return;
                        }

                        Index = Random.Range(0, number.Count);             // 0から要素の最大数までの範囲からランダムで抽選
                        random = number[Index];                            // 抽選した値で要素を指定する

                        ArrowAttack(random);
                    }
                }
            }
        }
    }

    static public void SetState(LB_State LbState, Transform playerTransform = null)
    {
        LBState = LbState;
        if (LBState == LB_State.Idle)
        {
            Debug.Log("アイドル");
        }
        else if (LBState == LB_State.RandomMove)
        {
            Debug.Log("ランダム移動");
        }
        else if (LBState == LB_State.Damage)
        {
            Debug.Log("ダメージ");
        }
        else if (LBState == LB_State.TrackingBullet)
        {
            Debug.Log("通常攻撃");
        }
        else if (LBState == LB_State.BoundBoll)
        {
            Debug.Log("衝撃波攻撃");
        }
        else if (LBState == LB_State.WarpBullet)
        {
            Debug.Log("チェイス");
        }
    }


    //--------------------------------------------------------------------------------------------------------

    public LB_State GetState()
    {
        return LBState;
    }

    public void AnimFlagOnOff()
    {
        if (!AnimFlg)
        {
            AnimFlg = true;
            return;
        }
        if (AnimFlg)
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

    //Idel=============================================================================
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);
        if (AttackCount == MaxAttack)
        {
            elapsedTimeOfIdleState = timeToStayInIdle;
            AttackCount = 0;          //攻撃数カウントをゼロに
            Debug.Log("Idel");
        }
        //　一定時間が経過したら各種攻撃状態にする
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle状態の経過時間をoffにする
            Debug.Log("AttackCount：" + AttackCount);

            //ランダム数の生成とswitch分岐をこの中へ
            if (!UltFlg && HPgage.currentHp <= 30)
            {
                Debug.Log("アローアタック");
                UltFlg = true;
                SetState(LB_State.ArrowAttack);
                return;
            }
            if (HPgage.currentHp >= 51)
            {
                RandomNumbe = Random.Range(1, 4);//攻撃パターンランダム化
                Debug.Log("Random" + RandomNumbe);
            }
            else
            {
                RandomNumbe = Random.Range(1, 5);//攻撃パターンランダム化
                Debug.Log("Random" + RandomNumbe);
            }
            switch (RandomNumbe)            //switch分岐
            {
                case 1://イチゴ爆弾へ
                    SetState(LB_State.TrackingBullet);
                    RandomNumbe = -1;
                    Debug.Log("イチゴ爆弾");
                    break;//break文

                case 2://突進へ
                    SetState(LB_State.BoundBoll);
                    RandomNumbe = -1;
                    Debug.Log("突進攻撃");
                    break;//break文

                case 3://ジャンプ
                    SetState(LB_State.WarpBullet);
                    RandomNumbe = -1;
                    Debug.Log("ジャンプ");
                    break;//break文

                case 4://ナイフ攻撃
                    SetState(LB_State.ArrowAttack);
                    RandomNumbe = -1;
                    Debug.Log("ナイフ攻撃");
                    break;
            }



        }
    }
    

//-------------------------------------------------------------------------------

//BulletAttack=====================================================================
private void BulletAttack()
    {
        if (Input.GetKeyDown("c"))
        {
            Vector3 bulletPosition = firingPoint.transform.position;
            GameObject newBullet = Instantiate(Bullet, bulletPosition, transform.rotation);// 上で取得した場所に、"bulle
        }
    }
    //---------------------------------------------------------------------------------

    private void WarpBulletAttack()
    {
        vecX = Random.Range(20f, 40f);
        vecY = Random.Range(10f, 20f);
        firingPoint.transform.position = new Vector3(vecX, vecY, 27.0f);
        Vector3 bulletPosition = firingPoint.transform.position;
        GameObject WarpBullets = Instantiate(WarpBullet, bulletPosition, transform.rotation);
        WarpCount++;                 
    }

//BoundBollAttack=====================================================================
private void BoundBollAttack()
    {
        if (Input.GetKeyDown("a"))
        {
            for (int i = 0; i <= 1; i++)
            {
              
                if (i == 0)
                {
                    Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);
                    Vector3 bulletPosition = firingPoint.transform.position;
                    GameObject newBall = Instantiate(BoundBoll, bulletPosition, transform.rotation);// 上で取得した場所に、"bullet"のPrefabを出現させる
                    float forceMagnitude = 10.0f;                     // 上の向きに加わる力の大きさを定義
                    Vector3 force = forceMagnitude * forceDirection;  // 向きと大きさからSphereに加わる力を計算する
                    Rigidbody rb = newBall.GetComponent<Rigidbody>(); // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
                    rb.AddForce(force, ForceMode.Impulse);            //力を加えるメソッド,ForceMode.Impulseは撃力
                }
                if(i == 1)
                {
                    Vector3 forceDirection = new Vector3(-1.0f, 1.0f, 0f);
                    Vector3 bulletPosition = firingPoint.transform.position;
                    GameObject newBall = Instantiate(BoundBoll, bulletPosition, transform.rotation);// 上で取得した場所に、"bullet"のPrefabを出現させる
                    float forceMagnitude = 10.0f;                     // 上の向きに加わる力の大きさを定義
                    Vector3 force = forceMagnitude * forceDirection;  // 向きと大きさからSphereに加わる力を計算する
                    Rigidbody rb = newBall.GetComponent<Rigidbody>(); // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
                    rb.AddForce(force, ForceMode.Impulse);            //力を加えるメソッド,ForceMode.Impulseは撃力
                }


            }
        }
    }
    //-------------------------------------------------------------------------------------
    private void ArrowAttack(int selectnumber)
    {
        ArrowUseFlag = false;

        switch (selectnumber)
        {
            //Arrow_Rightを動かす-------------------------------------------------------------
            case 1:
                LbRigidbody = Arrow_Right.GetComponent<Rigidbody>();        //rigidbodyを取得
                Vector3 ForceArrowRight = new Vector3(-8.0f, 0.0f, 0.0f);   //力を設定
                LbRigidbody.AddForce(ForceArrowRight * Arrow_MaxSpeed);     //力を加える
                //Debug.Log("Arrow_Rightが動いた");                          //デバックログを表示
                break;                                                      //caseを抜ける
            //--------------------------------------------------------------------------------

            //Arrow_Leftを動かす---------------------------------------------------------------
            case 2:
                LbRigidbody = Arrow_Left.GetComponent<Rigidbody>();         //rigidbodyを取得
                Vector3 ForceArrowLeft = new Vector3(8.0f, 0.0f, 0.0f);     //力を設定
                LbRigidbody.AddForce(ForceArrowLeft * Arrow_MaxSpeed);      //力を加える
                //Debug.Log("Arrow_Leftが動いた");                           //デバックログを表示
                break;                                                      //caseを抜ける
            //---------------------------------------------------------------------------------

            //Arrow_Upを動かす------------------------------------------------------------------
            case 3:
                LbRigidbody = Arrow_Up.GetComponent<Rigidbody>();           //rigidbodyを取得
                Vector3 ForceArrowUp = new Vector3(0.0f, -8.0f, 0.0f);      //力を設定
                LbRigidbody.AddForce(ForceArrowUp * Arrow_MaxSpeed);        //力を加える
                //Debug.Log("Arrow_Upが動いた");                             //デバックログを表示  
                break;                                                      //caseを抜ける
            //-----------------------------------------------------------------------------------
            default:
                Debug.Log("<dolor = red>抽選失敗</color>");
            break;
        }
        number.RemoveAt(Index);//抽選で使用した値を要素から抜き出す

    }
}






