using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanCake : MonoBehaviour {
    //Boss1Attack BossAttack;
    //突進用変数群
    //----------------------------------------------------------
    GameObject Forkobj;                                     //フォークのオブジェクト生成用
    GameObject Fork;                                        //フォークのオブジェクト格納用
    Vector3 RushStartPoint;                                 //突進開始地点
    Vector3 RushEndPoint;                                   //突進終了地点
    Vector3 RushPlayerPoint;                                //突進をはじいたときのプレイヤー座標格納用
    Vector3 RushRefEndPoint;                                //突進をはじいた後の敵の最終地点
    Vector3 RushMiddlePoint;                                //突進攻撃後戻ってくるための中間座標
    Vector3 ForkPos;
    private float RushSpeed = 0.3f;                //突進のスピード
    bool RushRefFlg = false;                                //突進をはじいた判定
    float RushTime;                                         //突進の経過時間
    float RushRefTime;                                      //弾いた後の時間経過
    bool BossReturnFlg = false;
    float BossReturnTime;                                   //突進後戻るまでの時間
    bool RushEndFlg;
    float RushReturnSpeed;
    bool ReturnDelay;                                      //戻ろうとするまでの時間
    Vector3 oldScale;
    private float RotateSpeed = 5.0f;
    Vector3 Rotate;

    // Boss1Attack1にあったやつ
    private bool AnimFlg;
    private bool MoveFlg;
    private Animator BossAnim;
    public static bool RefrectFlg;               //プレイヤーがパリィに成功したかどうかの受け取り用
    private Vector3 Scale;
    private bool RFChange;                                          //左右反転 true = 左から攻撃 false = 右から攻撃
    private bool WeaponAttackFlg;                            //武器使う攻撃変更 true = 雨攻撃 false = 突進orナイフ



    private bool onceUpdate = false;

    public static Vector3 pancakePos;
    public static bool isAlive;

    // Start is called before the first frame update
    void Start() {
        RushStartPoint = new Vector3(50.0f, 23.0f, 0.0f);
        RushEndPoint = new Vector3(-120.0f, 23.0f, 0.0f);

        this.Forkobj = (GameObject)Resources.Load("TutorialFork");
        this.Scale = this.transform.localScale;

        this.BossAnim = this.gameObject.GetComponent<Animator>();
        pancakePos = this.transform.position;

        isAlive = true;
    }

    // Update is called once per frame
    void Update() {
        if (!Pause.isPause)
        {
            BossAnim.speed = 1.0f;
            pancakePos = this.transform.position;
            if (!onceUpdate)
            {
                MoveFlg = true;
                onceUpdate = true;
            }
            ForkAction();
        }
        else
        {
            BossAnim.speed = 0.0f;
        }
    }

    private void ForkAction() {
        //アニメーション再生
        if (!this.AnimFlg)
        {
            AnimFlagOnOff();
            BossAnim.SetBool("IdleToTake", true);
        }
        //一回の処理が終わっていたら開始

        if (this.MoveFlg)
        {
            //ボスが突進終了後に変える処理
            if (BossReturnFlg)
            {
                RefrectFlg = false;
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                //最後まで攻撃し終わっていたら
                if (RushEndFlg)
                {
                    //方向が変わってたらスケールｘを反転
                    this.transform.localScale = this.Scale;
                    //回転の目標値
                    Quaternion target = new Quaternion();
                    //向きを設定
                    target = Quaternion.LookRotation(Rotate);
                    //ゆっくり回転させる
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target, RotateSpeed);
                }

                //途中で弾かれていたら
                if (!RushEndFlg)
                {
                    this.transform.position = Vector3.Lerp(RushRefEndPoint, RushStartPoint, BossReturnTime);
                }
                //開始地点まで戻ってきたときにもろもろ初期化
                if (BossReturnTime >= 1.0f)
                {
                    if (Fork != null)
                    {
                        Destroy(Fork);
                    }
                    if (RushEndFlg)
                    {
                        if (!this.RFChange)
                        {
                            this.RFChange = true;
                        }
                        else if (this.RFChange)
                        {
                            this.RFChange = false;
                        }
                    }
                    ReturnDelay = false;
                    RushEndFlg = false;
                    BossReturnFlg = false;
                    this.AnimFlagOnOff();
                    this.BossAnim.SetBool("IdleToTake", false);
                    this.BossAnim.SetBool("RushToJump", false);
                    MoveFlg = false;
                    BossReturnTime = 0;
                    this.BossAnim.speed = 1;
                    isAlive = false;
                    //Destroy(gameObject,0.5f);
                }
                return;
            }
            //弾かれたら一回だけ処理する部分
            if (RefrectFlg)
            {
                RushRefFlg = true;
                RushPlayerPoint = this.transform.position;
                this.BossAnim.SetBool("RushToJump", false);
                this.BossAnim.SetBool("Blow", true);
                RefrectFlg = false;
            }
            //弾かれていなかった場合の処理
            if (!RushRefFlg)
            {
                Debug.Log("<color=blue>シンプル移動</color>");
                RushTime += Time.deltaTime * RushSpeed;
                this.transform.position = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    this.Scale.x *= -1;
                    RushReturnSpeed = 1f;
                    RushEndFlg = true;
                    BossReturnFlg = true;
                    RushTime = 0;
                    return;
                }
            }
            //弾かれていた場合の処理
            if (RushRefFlg)
            {
                Debug.Log("<color=blue>はじかれた</color>");
                RushRefTime += Time.deltaTime * 2f;
                //壁にぶつけているように見せる
                this.transform.position = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    Destroy(Fork);
                    RushEndFlg = false;
                    this.BossAnim.SetBool("Blow", false);
                    this.BossAnim.SetTrigger("WallHit");
                    this.BossAnim.Play("WallHit");
                    this.BossAnim.speed = 0.3f;
                    if (ReturnDelay)
                    {
                        RushReturnSpeed = 3f;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        this.BossAnim.SetBool("IdleToTake", false);
                        this.BossAnim.SetBool("RushToJump", false);
                        RushTime = 0;
                        RushRefTime = 0;
                        SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        return;
                    }
                }
            }
        }
    }


    public void AnimFlagOnOff() {
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

    public void AnimMoveFlgOnOff() {
    }

    void ReturnGround() {
        ReturnDelay = true;
    }
    void BossRushAnim() {
        this.WeaponAttackFlg = false;
        //右から左
        if (!this.RFChange)
        {
            //BossAttack.OnlyFlg = true;
            ForkPos = GameObject.Find("ForkPos").transform.position;
            Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
            Fork.transform.parent = GameObject.Find("ForkPos").transform;
            RushRefEndPoint = new Vector3(45.2f, 60.0f, 0.0f);
            Rotate.x = 1;
            this.BossAnim.SetTrigger("TakeToRushTr");
            this.BossAnim.SetBool("RushToJump", true);
            this.BossAnim.SetBool("IdleToTake", false);
            SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
        }
    }
}
