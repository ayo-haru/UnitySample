using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_Attack : MonoBehaviour
{
    //「状態推移:列挙型」===================　
    public enum Boss_State
    {
        Idle,           //待機(0)
        Damage,         //ダメージ(1)
        TrackingBullet, //追従弾(2)
        BoundBoll,      //バウンド弾(3)
        WarpBullet,     //ワープボール（4）
        ArrowAttack,    //アローアタック(5)
    }
    //------------------------------------

    //[[[[[idleの処理で使う変数]]]]]=========================================================================
    private Vector3 defaultPos;                             //ボスが登場した最初の位置
    static private Boss_State BossState = Boss_State.Idle;  //ボスの状態(初期値はidel)
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
    public GameObject firingPoint;                          //
    [SerializeField] public int Bound_MaxSpeed;             //BoundBollの速度
    //[[[[WarpBulletの処理で使う変数]]]]]=======================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    public Destroy_WarpBullet destroywarpbullet;
    [SerializeField] public int WarpBullet_MaxSpeed;        //弾の速度
    public bool WarpBulletUseFlag = true;                  //バレットが使用中かどうか調べるフラグ（初期値：true)
    public int WarpCount = 0;                              //アローカウント
    private float time;
    private float vecX;
    private float vecY;
    //[[[[[Bulletの処理で使う変数]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    [SerializeField] public int Bullet_MaxSpeed;           //弾の速度
    public bool BulletUseFlag = true;                       //バレットが使用中かどうか調べるフラグ（初期値：true)
    //-----------------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
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
            if (LB_Manager.BossState == LB_Manager.LBState.LB_BATTLE)
            {
                if (BossState == Boss_State.Idle)       //もしボスの状態が待機の場合
                {
                    Idle();
                }
                else if (BossState == Boss_State.Damage)//もしボスの状態がダメージの場合
                {
                    //damage();
                }
                else if (BossState == Boss_State.TrackingBullet)//もしボスの状態がナイフ投げの場合
                {
                    BulletAttack();
                }
                else if (BossState == Boss_State.BoundBoll)//もしボスの状態がイチゴ爆弾の場合
                {
                    BoundBollAttack();
                }
                else if (BossState == Boss_State.WarpBullet)//もしボスの状態が突進の場合
                {
                    if (WarpCount <= 5)
                    {
                        if (WarpBulletUseFlag)
                        {
                            WarpBulletAttack();
                        }
                    }
                }
                else if (BossState == Boss_State.ArrowAttack)//もしボスの状態がナイフ投げの場合
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

    //--------------------------------------------------------------------------------------------------------

    //Idel=============================================================================
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);

        //アタックカウントが最大攻撃回数と同等なら
        if (AttackCount == MaxAttack)
        {
            //SetState(Boss_State.idle);                //待機モーション
            elapsedTimeOfIdleState = timeToStayInIdle;
            AttackCount = 0;                            //攻撃数カウントをゼロに
            Debug.Log("アイドル");                       //デバックログ
        }
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {    //一定時間が経過したら各種攻撃状態にする
            elapsedTimeOfIdleState = 0f;                 //idle状態の経過時間をoffにする
            Debug.Log("AttackCount：" + AttackCount);    //デバックログ

            //ランダム数の生成とswitch分岐をこの中へ
            if (HPgage.currentHp >= 51)
            {   //HPが半分以上の場合
                RandomNumbe = Random.Range(1, 3);        //攻撃パターンランダム化
                Debug.Log("Random" + RandomNumbe);       //デバックログ
            }
            else
            {   //HPが半分以下の場合
                RandomNumbe = Random.Range(1, 4);        //攻撃パターンランダム化
                Debug.Log("Random" + RandomNumbe);       //デバックログ
            }

            //ランダムナンバーによるswitch分岐
            switch (RandomNumbe)
            {
                case 1://イチゴ爆弾へ
                    Debug.Log("イチゴ爆弾");
                    break;

                case 2://突進へ
                    Debug.Log("突進攻撃");
                    break;

                case 3://ナイフ攻撃
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
        WarpBulletUseFlag = false;                   
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






