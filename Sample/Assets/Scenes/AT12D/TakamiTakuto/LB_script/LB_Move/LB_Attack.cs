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
        CircleBullet,   //サークル弾(4)
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
    public int ArrowNum;                                    //アローの行動回数保存
    //抽選用の変数-------------------------------------------------------------------------------------------
    List<int> number = new List<int>();                     //抽選する最大数(List構造)
    private int random;                                     //抽選した番号
    private int Index;                                      //抽選する要素の指定
    //[[[[[BoundBollの処理で使う変数]]]]]====================================================================
    public GameObject CircleBullet;                         //GameObject:BoundBoll
    public GameObject CircleBulletobj;                      
    public GameObject firingPoint;                          //弾が発射される場所
    [SerializeField] public int BOundDamage = 10;           //弾の速度
    private float Angle;                                    //角度
    public Quaternion rotation = Quaternion.identity;       //Quaternion
    public int Circlenum;
    //[[[[WarpBulletの処理で使う変数]]]]]====================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    GameObject WarpBullets;                                 //WarpBulletで生成する弾
    [SerializeField] public int WarpBullet_MaxSpeed;        //弾の速度
    public int WarpCount = 0;                               //アローカウント
    [SerializeField] public int WapeDamage = 10;            //弾の速度
    public int WarpNum;
    //ランダム移動用の変数-----------------------------------------------------------------------------------
    private float time;                                     //移動までの時間
    private float vecX;                                     //座標指定用：X座標格納場所
    private float vecY;                                     //座標指定用：Y座標格納場所
    //[[[[[Bulletの処理で使う変数]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    GameObject TrakingBullet;                               //GameObject:TrakingBUllet
    [SerializeField] public int Bullet_MaxSpeed;            //弾の速度
    public bool BulletUseFlag = true;                       //バレットが使用中かどうか調べるフラグ（初期値：true)
    [SerializeField] public int BulletDamage = 10;          //弾の速度
    //-----------------------------------------------------------------------------------------------------
    private GameObject HpObject;                            //HPバー
    public LastHPGage HpScript;                             //HPgage       
    //=====================================================================================================
    [SerializeField]  public Animator LBossAnim;            //アニメーション
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool OneTimeFlg;
    //必殺技用フラグ(〇%以下になったとき一回)=================================================================
    bool UltFlg;                                            
    public bool OnlryFlg = true;
    //-----------------------------------------------------------------------------------------------------
    int IdleCount = 0;
    public GameObject Effect = null;

    // Start is called before the first frame update
    void Start()
    {
        Angle = 0;
        LBState = LB_State.Idle;
        firingPoint = GameObject.Find("LB_ShotPoint(Clone)");
        HpObject = GameObject.Find("HPGage");
        LBossAnim = this.gameObject.GetComponent<Animator>();
        HpScript = HpObject.GetComponent<LastHPGage>();
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
                        LBossAnim.SetTrigger("AttakTr");
                        EffectManager.Play(EffectData.eEFFECT.EF_LASTBOSS_ENERGYBALL, firingPoint.transform.position);      //effect生成
                        Effect = GameObject.Find("energyBall(Clone)");
                        

                    }
                    if (TrakingBullet != null)
                    {
                         Effect.transform.position = TrakingBullet.transform.position;                             //Effectをww
                    }
                    if (OneTimeFlg)
                    {
                        Destroy(Effect);
                        TrakingBullet = null;
                        OneTimeFlg = false;
                        OnlryFlg = true;
                        MoveSelect();
                    }

                }
                else if (LBState == LB_State.CircleBullet)
                {
                    Debug.Log("サークル個数" + Circlenum);
                    if (OnlryFlg == true)
                    {
                        Circlenum = 0;
                        OnlryFlg = false;
                        Debug.Log("Onlry" + OnlryFlg);
                        IdleCount = 0;
                        if (!AnimFlg)
                        {
                            LBossAnim.SetTrigger("CircleBulletTrigger");
                            AnimFlg = true;
                        }
                    }
                    if (CircleBulletobj != null)
                    {
                                                     //Effectをww
                    }
                    if (OneTimeFlg && Circlenum >= 20)
                    {
                        Circlenum = 0;
                        OnlryFlg = true;
                        AnimFlg = false;
                        OneTimeFlg = false;
                        MoveSelect();
                    }
                }
                else if (LBState == LB_State.WarpBullet)
                {
                    Debug.Log("ワープ座標" + this.transform.position);
                    Debug.Log("Onlry" + OnlryFlg);
                    Debug.Log("ワープ回数" + WarpNum);
                    if (OneTimeFlg && WarpNum >= 5)
                    {
                        WarpNum = 0;
                        OnlryFlg = true;
                        OneTimeFlg = false;
                        WarpCount = 0;
                        MoveSelect();
                        return;
                    }
                    if (OnlryFlg == true)
                    {
                        IdleCount = 0;
                        OnlryFlg = false;
                        if (WarpCount <= 5)
                        {
                            if (WarpBullets == null)
                            {
                                if (!AnimFlg)
                                {
                                    vecX = Random.Range(-180f, 300f);                          //座標ランダム位置決め(X)
                                    vecY = Random.Range(60, 230f);                            //座標ランダム位置決め(Y)
                                    this.transform.position = new Vector3(vecX, vecY, 120.0f); //ランダム移動
                                    LBossAnim.SetTrigger("WarpTrigger");
                                    AnimFlg = true;
                                }

                            }
                        }
                    }
                    
                }
                else if (LBState == LB_State.ArrowAttack)//もしボスの状態がナイフ投げの場合
                {
                    IdleCount = 0;
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
                   if (OneTimeFlg)
                   {
                       OneTimeFlg = false;
                        MoveSelect();
                    }
                }
                
            }
        }
    }
    //SetState============================================================================================
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
        else if (LBState == LB_State.CircleBullet)
        {
            Debug.Log("バウンド弾");
        }
        else if (LBState == LB_State.WarpBullet)
        {
            Debug.Log("ワープ弾");
        }
        else if (LBState == LB_State.ArrowAttack)
        {
            Debug.Log("アローアタック");
        }
    }
    //LB_State=============================================================================================
    public LB_State GetState()
    {
        return LBState;
    }
    //MoveSelect===========================================================================================
    private void MoveSelect()
    {
        //HPが30以下になった時→ULTへ-----------------------------------------------
        if (!UltFlg && LastHPGage.currentHp <= 30)
        {
            Debug.Log("Hp" + LastHPGage.currentHp);      //デバックログ
            Debug.Log("アローアタック");                  //デバックログ
            UltFlg = true;                              //
            SetState(LB_State.ArrowAttack);
            return;
        }
        //HPが51以上の場合--------------------------------------------------------
        if (LastHPGage.currentHp >= 51)
        {
            RandomNumbe = Random.Range(0, 4);            //攻撃パターンランダム化
            Debug.Log("ランダムナンバーは" + RandomNumbe);//デバックログ
        }
        //HPが50以下の場合---------------------------------------------------------
        else
        {
            RandomNumbe = Random.Range(1, 4);            //攻撃パターンランダム化
            Debug.Log("ランダムナンバーは" + RandomNumbe);//デバックログ
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

            case 2://サークルバレット弾
                SetState(LB_State.CircleBullet);
                break;//break文

            case 3://ワープバレット
                SetState(LB_State.WarpBullet);
                break;//break文
        }
    }
    //Idel==================================================================================================
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
                AttackCount = 0;                      //攻撃数カウントをゼロに
                Debug.Log("Idel");
            }
            //アイドルの時間経過が指定値を超えた時
            if (elapsedTimeOfIdleState >= timeToStayInIdle){
                elapsedTimeOfIdleState = 0f;          //idle状態の経過時間をoffにする
                MoveSelect();                         //ランダム行動
                Debug.Log("Idel");                    //デバックログ
                IdleCount++;                          //カウント増加
            }
        }
        //IdleCountが0以上の時に入る
        else{
            MoveSelect();                             //行動決め
        }
    }
    //RandomMove==============================================================================================
    private void RandomMove(){
        time -= Time.deltaTime;                                                      //時間減算
        if (time <= 0){                                                              //経過時間が0秒以上の場合
            vecX = Random.Range(20f, 40f);                                           //座標ランダム位置決め(X)                                     
            vecY = Random.Range(10f, 20f);                                           //座標ランダム位置決め(Y)
            LB.transform.position = new Vector3(vecX, vecY, 0.0f);                   //ランダムワープ実行
            time = 1.0f;                                                             //時間を1.0fに
        }
    }
    //BulletAttack============================================================================================
    private void BulletAttack(){
       Vector3 bulletPosition = firingPoint.transform.position;                       //発射位置をfiringPointに
       TrakingBullet = Instantiate(Bullet, bulletPosition, transform.rotation);       //上で取得した場所に
    }
    //WarpeBulletAttack========================================================================================
    private void WarpBulletAttack(){
        Vector3 bulletPosition = firingPoint.transform.position;                       //発射位置をfiringPointに
        WarpBullets = Instantiate(WarpBullet, bulletPosition, this.transform.rotation);//warpBullet生成
        WarpCount++;                                                                   //カウント増加               
    }
    //CircleBulletAttack=======================================================================================
    IEnumerator CircleBulletAttack(){
        for (int i = 0; i < 20; i++){                                                  //20回繰り返す
            Debug.Log("ゴミかすしねbaaaaaaaaaaaaaaaaaaaaaaaaakaaaaaaaaaaaaaa"+i);
            rotation.eulerAngles = new Vector3(0.0f,0.0f,Angle);                       //クォータニオン→オイラー角への変換
            yield return new WaitForSeconds(0.3f);                                     //0.3f待つ 
            EffectManager.Play(EffectData.eEFFECT.EF_LASTBOSS_ENERGYBALL, firingPoint.transform.position);      //effect生成
            Effect = GameObject.Find("energyBall(Clone)");
            CircleBulletobj =Instantiate(CircleBullet, firingPoint.transform.position, rotation);       //CircleBulletを生成
            CircleBulletobj.name = "Circle" + i;
            Effect.name = "EF_BALL" + i;

            GameObject.Find("EF_BALL" + i).transform.parent = GameObject.Find("EF_BALL" + i).transform;
            
            Angle += 30;                                                               //k(Z角度)を30ずつ増加
        }
    }
    private void StartCircle()
    {
        Debug.Log("Arrow_Upが動いた");                               //デバックログを表示  
        StartCoroutine("CircleBulletAttack");
        Debug.Log("Arrow_Upが動いた");                               //デバックログを表示  
    }
    //ArrowAttack==============================================================================================
    private void ArrowAttack(int selectnumber){
        Debug.Log("アローウゴキマスル");
        ArrowUseFlag = false;
        LBossAnim.SetInteger("UltCount", selectnumber);
        
        switch (selectnumber) {
            //Arrow_Rightを動かす-------------------------------------------------------------
            case 1:
                Vector3 Right_Pos;
                Right_Pos = new Vector3(-465,85, 0);
                GameObject ArrowRightobj = Instantiate(Arrow_Right, Right_Pos, transform.rotation);          //上で取得した
                LbRigidbody = ArrowRightobj.GetComponent<Rigidbody>();        //rigidbodyを取得
                Vector3 ForceArrowRight = new Vector3(80.0f, 0.0f, 0.0f);   //力を設定
                LbRigidbody.AddForce(ForceArrowRight * Arrow_MaxSpeed);     //力を加える
                Debug.Log("Arrow_Rightが動いた");                          //デバックログを表示
                LBossAnim.Play("ULT_Right");
                LBossAnim.SetBool("OnlyFlg", true);
                ArrowNum ++;
                break;                                                      //caseを抜ける
            //--------------------------------------------------------------------------------

            //Arrow_Leftを動かす---------------------------------------------------------------
            case 2:
                Vector3 Left_Pos;
                Left_Pos = new Vector3(630, 85, 0);
                GameObject ArrowLeftobj = Instantiate(Arrow_Left, Left_Pos, transform.rotation);          //上で取得した
                LbRigidbody = ArrowLeftobj.GetComponent<Rigidbody>();         //rigidbodyを取得
                Vector3 ForceArrowLeft = new Vector3(-80.0f, 0.0f, 0.0f);     //力を設定
                LbRigidbody.AddForce(ForceArrowLeft * Arrow_MaxSpeed);      //力を加える
                Debug.Log("Arrow_Leftが動いた");                           //デバックログを表示
                LBossAnim.Play("ULT_Left");
                LBossAnim.SetBool("OnlyFlg", true);
                ArrowNum++;
                break;                                                      //caseを抜ける
            //---------------------------------------------------------------------------------

            //Arrow_Upを動かす------------------------------------------------------------------
            case 3:
                Vector3 UP_Pos;
                UP_Pos = new Vector3(52, 430, 0);
                GameObject ArrowUpobj = Instantiate(Arrow_Up, UP_Pos, transform.rotation);          //上で取得した場所に
                LbRigidbody = ArrowUpobj.GetComponent<Rigidbody>();           //rigidbodyを取得
                Vector3 ForceArrowUp = new Vector3(0.0f, -80.0f, 0.0f);    //力を設定
                LbRigidbody.AddForce(ForceArrowUp * Arrow_MaxSpeed);        //力を加える
                Debug.Log("Arrow_Upが動いた");                               //デバックログを表示  
                LBossAnim.Play("ULT_Down");
                LBossAnim.SetBool("OnlyFlg", true);
                ArrowNum++;
                break;                                                      //caseを抜ける
            //-----------------------------------------------------------------------------------
            default:
                Debug.Log("<dolor = red>抽選失敗</color>");
            break;
        }
        number.RemoveAt(Index);//抽選で使用した値を要素から抜き出す
        if(ArrowNum >=3)
        {
            OneTimeFlg = true;
            
            LBossAnim.SetInteger("UltCount", 0);
            OnlryFlg = true;
        }
    }
}
//--------------------------------------------------------------------------------------------------------------






