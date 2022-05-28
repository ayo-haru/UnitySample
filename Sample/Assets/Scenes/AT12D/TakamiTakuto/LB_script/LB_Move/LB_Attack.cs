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

    public GameObject LB;
    //[[[[[idleの処理で使う変数]]]]]=========================================================================
    private Vector3 defaultPos;                             //ボスが登場した最初の位置
    static private LB_State LBState = LB_State.Idle;        //ボスの状態(初期値はidel)
    private int RandomNumbe = 0;                            //モーションのランダム抽選用の数
    [SerializeField] private float timeToStayInIdle=2.4f;    //idle状態で留まる時間(フレーム指定)
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
    public Arrow_Disappear Arrow_End;
    //抽選用の変数-------------------------------------------------------------------------------------------
    List<int> number = new List<int>();                     //抽選する最大数(List構造)
    private int random;                                     //抽選した番号
    private int Index;                                      //抽選する要素の指定
    //[[[[[BoundBollの処理で使う変数]]]]]====================================================================
    public GameObject BoundBoll;                            //GameObject:BoundBoll
    public GameObject BoundBolls;                            //GameObject:BoundBoll
    public GameObject firingPoint;                          //弾が発射される場所
    [SerializeField] public int BOundDamage = 10;           //弾の速度
    public BoundBoll_Division BoundBollEnd;
    private float k;
    public Quaternion rotation = Quaternion.identity;
    //[[[[WarpBulletの処理で使う変数]]]]]====================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    GameObject WarpBullets;                                 //WarpBulletで生成する弾
    [SerializeField] public int WarpBullet_MaxSpeed;        //弾の速度
    public int WarpCount = 0;                               //アローカウント
    [SerializeField] public int WapeDamage = 10;            //弾の速度
    public Destroy_WarpBullet WarpEnd;
    //ランダム移動用の変数-----------------------------------------------------------------------------------
    private float time;                                     //移動までの時間
    private float vecX;                                     //座標指定用：X座標格納場所
    private float vecY;                                     //座標指定用：Y座標格納場所
    //[[[[[Bulletの処理で使う変数]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    GameObject TrakingBullet;
    [SerializeField] public int Bullet_MaxSpeed;            //弾の速度
    public bool BulletUseFlag = true;                       //バレットが使用中かどうか調べるフラグ（初期値：true)
    [SerializeField] public int BulletDamage = 10;            //弾の速度
    public DestroyBullet BulletEnd;
    //-----------------------------------------------------------------------------------------------------
    private GameObject HpObject;
    public LastHPGage HpScript;
    //=====================================================================================================
    public Animator BossAnim;
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool MoveFlg;
    //=====================================================================================================
    //必殺技用フラグ(〇%以下になったとき一回)
    bool UltFlg;
    bool End;
    bool OnlryFlg = true;
    //-----------------------------------------------------------------------------------------------------
    int IdleCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        k = 0;
        LBState = LB_State.Idle;
        LB = GameObject.Find("LB(Clone)");
        firingPoint = GameObject.Find("LB_ShotPoint(Clone)");
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<LastHPGage>();
        Arrow_End = GetComponent<Arrow_Disappear>();
        BoundBollEnd = GetComponent<BoundBoll_Division>();
        BulletEnd = GetComponent<DestroyBullet>();
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
                     Debug.Log("Onlry" + OnlryFlg);
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
                    if (OnlryFlg == true)
                    {
                        OnlryFlg = false;
                        IdleCount = 0;
                        BulletAttack();
                        
                    }
                    if (TrakingBullet==null)
                    {
                        OnlryFlg = true;
                        MoveSelect();
                    }

                }
                else if (LBState == LB_State.BoundBoll)//もしボスの状態がイチゴ爆弾の場合
                {
                    if (OnlryFlg == true)
                    {
                        OnlryFlg = false;
                        Debug.Log("Onlry" + OnlryFlg);
                        IdleCount = 0;
                        StartCoroutine("BallSet");
                    }
                    if (BoundBoll == null)
                    {
                        OnlryFlg = true;
                        MoveSelect();
                    }

                    else if (LBState == LB_State.WarpBullet)//もしボスの状態が突進の場合
                    {

                        if (OnlryFlg == true)
                        {
                            IdleCount = 0;
                            OnlryFlg = false;
                            if (WarpCount <= 5)
                            {
                                if (WarpBullets == null)
                                {
                                    WarpBulletAttack();
                                }
                            }
                        }
                        if (WarpBullets == null)
                        {
                            OnlryFlg = true;
                            MoveSelect();
                        }
                    }
                    else if (LBState == LB_State.ArrowAttack)//もしボスの状態がナイフ投げの場合
                    {
                        IdleCount = 0;
                        if (OnlryFlg == true)
                        {
                            Debug.Log("Onlry" + OnlryFlg);
                            OnlryFlg = false;
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
                            if (End == true)
                            {
                                OnlryFlg = true;
                                MoveSelect();
                            }
                        }
                    }
                }
            }
        }
    }
    private void DelayMethod()
    {
        Debug.Log("Invoke");
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
            Debug.Log("追従弾");
        }
        else if (LBState == LB_State.BoundBoll)
        {
            Debug.Log("バウンド弾");
        }
        else if (LBState == LB_State.WarpBullet)
        {
            Debug.Log("ワープ弾");
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

    private void MoveSelect()
    {
        //HPが30以下になった時→ULTへ----------------------------------
        if (!UltFlg && LastHPGage.currentHp <= 30)
        {
            Debug.Log("Hp" + LastHPGage.currentHp);
            Debug.Log("アローアタック");
            UltFlg = true;
            SetState(LB_State.ArrowAttack);
            return;
        }
        //HPが51以上の場合---------------------------------------------
        if (LastHPGage.currentHp >= 51)
        {
            RandomNumbe = Random.Range(0, 3);//攻撃パターンランダム化
            Debug.Log("ランダムナンバーは" + RandomNumbe);
        }
        //HPが30以下の場合----------------------------------------------
        else
        {
            RandomNumbe = Random.Range(0, 4);//攻撃パターンランダム化
            Debug.Log("ランダムナンバーは" + RandomNumbe);
        }
        //switch(ランダムナンバー)
        switch (RandomNumbe)//switch分岐
        {
            case 0://アイドル
                SetState(LB_State.Idle);
                break;

            case 1://追従弾
                SetState(LB_State.TrackingBullet);
                break;

            case 2://バウンド弾
                SetState(LB_State.BoundBoll);
                break;//break文

            case 3://ワープバレット
                SetState(LB_State.WarpBullet);
                break;//break文

            case 4://アローアタック
                SetState(LB_State.ArrowAttack);
                break;
        }
    }

    //Idel=============================================================================
    private void Idle()
    {
        //Idleを二回連続で行わないようにするIF
        if (IdleCount == 0)
        {
            elapsedTimeOfIdleState += Time.deltaTime;　//Idle経過時間
            Debug.Log("Time" + elapsedTimeOfIdleState);//デバックログ
            if (AttackCount == MaxAttack)
            {
                elapsedTimeOfIdleState = timeToStayInIdle;
                AttackCount = 0;          //攻撃数カウントをゼロに
                Debug.Log("Idel");
            }
            //アイドルの時間経過が指定値を超えた時
            if (elapsedTimeOfIdleState >= timeToStayInIdle)
            {
                elapsedTimeOfIdleState = 0f;//idle状態の経過時間をoffにする
                MoveSelect();               //ランダム行動
                Debug.Log("Idel");          //デバックログ
                IdleCount++;                //カウント増加
            }
        }
        else
        {
            MoveSelect();//ランダム行動
        }
    }
    //RandomMove=========================================================================
    private void RandomMove()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            vecX = Random.Range(20f, 40f);
            vecY = Random.Range(10f, 20f);
            LB.transform.position = new Vector3(vecX, vecY, 0.0f);
            time = 1.0f;
        }
    }
    //BulletAttack=====================================================================
    private void BulletAttack()
    {
       DestroyBullet.Flg = false;
       Vector3 bulletPosition = firingPoint.transform.position;
       TrakingBullet = Instantiate(Bullet, bulletPosition, transform.rotation);// 上で取得した場所に、"bulle
    }
    //---------------------------------------------------------------------------------

    private void WarpBulletAttack()
    {
        vecX = Random.Range(-180f, 3000f);
        vecY = Random.Range(0f,230f);
        LB.transform.position = new Vector3(vecX, vecY);
        Vector3 bulletPosition = firingPoint.transform.position;
        WarpBullets = Instantiate(WarpBullet, bulletPosition, transform.rotation);
        WarpCount++;                 
    }
    //BoundBollAttack=====================================================================
    IEnumerator BallSet()
    {
        //Vector3 bulletPosition = firingPoint.transform.position;
        //BoundBolls = Instantiate(BoundBoll, bulletPosition, transform.rotation);// 上で取得した場所に、"bullet"のPrefabを出現させる
        //Vector3 direction = BoundBolls.transform.up;
        //// 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
        //BoundBolls.GetComponent<Rigidbody>().AddForce(direction * 100, ForceMode.Impulse);
        //Rigidbody rb = BoundBolls.GetComponent<Rigidbody>(); // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("BallSet");
            rotation.eulerAngles = new Vector3(0, k, 0);
            yield return new WaitForSeconds(3.0f);
            Instantiate(BoundBoll, firingPoint.transform.position, rotation);
            k += 30;
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






