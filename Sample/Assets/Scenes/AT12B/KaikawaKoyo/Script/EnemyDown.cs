//==========================================================
//      雑魚敵の弾かれたとき
//      作成日　2022/03/20
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/20
//      2022/05/03  ヒットストップ演出追加-吉原
//      2022/05/18  画面で跳ね返った時の、UI振動の振動追加
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyDown : MonoBehaviour
{
    [SerializeField]
    private GameObject Item;
    private GameObject Player;
    private Camera maincam;
    private Vector3 Pos;
    private Vector3 vec;
    private Vector3 Pvec;           // ポーズ時の速度格納用
    private Vector3 CamRightTop;    // カメラの右上座標
    private Vector3 CamLeftBot;     // カメラの左下座標
    private Vector3 InAngle;        // 入射角
    private Vector3 ReAngle;        // 反射角
    private Vector3 inNormalU;      // 法線ベクトル上
    private Vector3 inNormalD;      // 法線ベクトル下
    private Vector3 inNormalR;      // 法線ベクトル右
    private Vector3 inNormalL;      // 法線ベクトル左
    private Animator animator;

    [SerializeField]
    private int DropRate;           // 回復アイテムのドロップ率

    [SerializeField]
    private int EnemyNumber;        // 敵識別
    private int Drop;
    private int reflect;
    private bool ItemDrop = false;
    public bool isAlive;
    private bool Reflect;
    private bool pause;
    float Timer;
    float DeadTime = 1.5f;
    private float bouncePower = 300.0f;
    private float speed;
    private float dis;
    private float CamZ;

    Rigidbody rb;

    //---ディゾルブ処理のための追記(2022/04/28.吉原)
    Dissolve _dissolve;
    private bool isCalledOnce = false;      // Update内で一回だけ処理を行いたいのでbool型の変数を用意
    private bool FinDissolve = false;       // Dissolveマテリアルに差し替える処理を終えたことを判定する

    //---ヒットストップ演出(2022/05/02.吉原)
    Player2 player2;
    public float Width = 0.1f;
    public int   RoundCnt = 4;
    public float Duration = 0.23f;

    //---UI振動用
    private HPManager ui;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
        _dissolve = this.GetComponent<Dissolve>();
        player2 = Player.GetComponent<Player2>();
        CamZ = Camera.main.transform.position.z;
        // 法線ベクトル定義
        inNormalU = transform.up;
        inNormalD = -transform.up;
        inNormalR = -transform.right;
        inNormalL = transform.right;

        //ui取得
        ui = GameObject.Find("HPSystem(2)(Clone)").GetComponent<HPManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            // ポーズ解除されたとき
            if (pause)
            {
                if(isCalledOnce)
                {
                    _dissolve.Invoke("Play", 0.0f);
                }
                rb.Resume(gameObject);
                rb.velocity = Pvec;
                pause = false;
            }

            // 生きてる時の処理
            if (isAlive)
            {
                // アニメーションは常に再生
                animator.speed = 1.0f;

                // 画面外の敵は消したい
                dis = Vector3.Distance(transform.position, Player.transform.position);
                if (dis >= 200.0f)
                {
                    Destroy(gameObject, 0.0f);
                }
            }

            // 時間で消える処理
            if (!isAlive)
            {
                // タイマー起動
                Timer += Time.deltaTime;
                // 敵の速度を取得
                speed = rb.velocity.magnitude;

                // カメラの端の座標取得
                CamRightTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, CamZ));
                CamLeftBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, CamZ));

                // 回転させる
                if (Player.transform.position.x < transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);
                }
                if (Player.transform.position.x > transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
                }

                // 画面端で跳ね返したい
                // 右端
                if (transform.position.x >= CamRightTop.x && !Reflect)
                {
                    reflect = 1;//Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalL);
                        rb.velocity = ReAngle;
                    }
                    else if(reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalL);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui振動
                    ui.Vibration();
                }
                else if(transform.position.x < CamRightTop.x && Reflect)
                {
                    Reflect = false;
                }
                // 左端
                if (transform.position.x <= CamLeftBot.x && !Reflect)
                {
                    reflect = Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalR);
                        rb.velocity = ReAngle;
                    }
                    else if (reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalR);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui振動
                    ui.Vibration();
                }
                else if (transform.position.x > CamLeftBot.x && Reflect)
                {
                    Reflect = false;
                }
                // 上端
                if (transform.position.y >= CamRightTop.y && !Reflect)
                {
                    reflect = Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalD);
                        rb.velocity = ReAngle;
                    }
                    else if (reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalD);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui振動
                    ui.Vibration();
                }
                else if (transform.position.y < CamRightTop.y && Reflect)
                {
                    Reflect = false;
                }
                // 下端
                if (transform.position.y <= CamLeftBot.y && !Reflect)
                {
                    reflect = Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalU);
                        rb.velocity = ReAngle;
                    }
                    else if (reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalU);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui振動
                    ui.Vibration();
                }
                else if (transform.position.y > CamLeftBot.y && Reflect)
                {
                    Reflect = false;
                }

                //---ディゾルマテリアルに変更
                if (!isCalledOnce)
                {
                    if (EnemyNumber == 1 || EnemyNumber == 0 || EnemyNumber == 4 || EnemyNumber == 3)
                    {
                        _dissolve.Invoke("Play", 0.2f);
                        isCalledOnce = true;
                        FinDissolve = true;
                    }
                }
                // 時間が経ったら消す。トマトはスピード無くなった時点で消しちゃいたい
                if (Timer > DeadTime || (EnemyNumber == 2 || EnemyNumber == 5) && speed <= 0.5f)
                {
                    Pos = transform.position;
                    Destroy(gameObject, 0.0f);
                    if (EnemyNumber == 2 || EnemyNumber == 5)
                    {
                        EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
                    }
                    EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_DEATH, Pos, 2.0f);
                }
            }
            Pvec = rb.velocity;
        }
        else
        {
            if (isCalledOnce)
            {
                _dissolve.Invoke("Stop", 0.0f);
            }
            rb.Pause(gameObject);
            animator.speed = 0.0f;
            pause = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!Pause.isPause)
        {
            // 弾かれたらベクトルを計算して関数を呼び出す
            if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
            {
                vec = (transform.position - Player.transform.position).normalized;

                // レイヤー変更
                if (EnemyNumber == 2)
                {
                    gameObject.layer = LayerMask.NameToLayer("TomatoDown");
                }
                gameObject.layer = LayerMask.NameToLayer("DownEnemy");

                //---ヒットストップ演出
                var seq = DOTween.Sequence();
                //---Enemyの振動演出
                seq.Append(transform.DOShakePosition(player2.HitStopTime,1f,100,fadeOut:false));

                //EnemyDead(vec , Player.transform.position.x);
                //---このタイミングでエネミーの死亡処理を呼び出す
                seq.AppendCallback(() => EnemyDead(vec));
                Shake(0.1f, 5, 0.23f);
            }

            // トマトがほかの敵に当たったら爆発する
            if (collision.gameObject.CompareTag("Enemy") && !isAlive && (EnemyNumber == 2 || EnemyNumber == 5))
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
            }
        }
    }

    // 死ぬ時の処理
    public void EnemyDead(Vector3 vec)
    {
        // アニメーションを止める
        animator.speed = 0;
        // 重力を消す
        rb.useGravity = false;
        // 空気抵抗をゼロに
        rb.angularDrag = 0.0f;

        // 回転軸を変更
        if (EnemyNumber == 1 || EnemyNumber == 4)
        {
            rb.centerOfMass = new Vector3(0.0f, 5.0f, 2.0f);
        }
        else if (EnemyNumber == 2 || EnemyNumber == 5)
        {
            rb.centerOfMass = new Vector3(0.0f, 0.3f, 0.0f);
        }
        else
        {
            rb.centerOfMass = new Vector3(0.0f, 0.0f, 0.0f);
        }

        // 回復アイテムを落とす
        Pos = transform.position;
        Drop = Random.Range(0, 100);
        if (Drop < DropRate && !ItemDrop)
        {
            Instantiate(Item, Pos, Quaternion.identity);
            ItemDrop = true;
        }

        //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
        //rb.AddForce(velocity * bouncePower, ForceMode.Force);
        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY;
        //プレイヤーと逆方向に跳ね返す
        rb.velocity = vec * bouncePower;

        SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);

        isAlive = false;
    }
    /// <summary>
    /// カメラ振動演出
    /// </summary>
    /// <param name="width"></param>    カメラの振れ幅
    /// <param name="cnt"></param>      往復回数
    /// <param name="duration"></param> 時間
    public void Shake(float width,int cnt,float duration)
    {
        var camera = Camera.main.transform;
        var seq = DOTween.Sequence();

        var partDuration = duration / cnt / 2f;

        var widthHalf = width / 2f;

        for(int i = 0; i < cnt - 1; i++)
        {
            seq.Append(camera.DOLocalRotate(new Vector3(-width,0f),partDuration));
            seq.Append(camera.DOLocalRotate(new Vector3( width,0f),partDuration));
        }

        seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf,0f),partDuration));
        seq.Append(camera.DOLocalRotate(Vector3.zero,partDuration));
    }
}
