//==========================================================
//      ニンジン雑魚亜種の攻撃
//      作成日　2022/04/16
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarrotEnemy2 : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 PP;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed;
    float MovingSpeed;
    private int AttackPattern;
    private float dis;
    private float Timer;
    private float RandomTime;
    private float rotTime;
    private float idlingTime = 0.5f;
    bool start = true;
    bool IdringFlg = true;
    private bool Attack;
    private bool Attack2;
    bool disFlg;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
        MovingSpeed = MoveSpeed / 2;
        transform.DOShakePosition(duration: idlingTime, strength: 5.0f);    // ぶるぶる震わせる
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                // アイドリングするよ
                if (IdringFlg)
                {
                    // 最初だけプレイヤーのほうを向いてプルプルする
                    if (start)
                    {
                        // プレイヤーのほうを向くよ
                        vec = (Player.transform.position - transform.position).normalized;
                        look = Quaternion.LookRotation(vec);
                        transform.localRotation = look;
                    }
                    else
                    {
                        // くるくる回転させる
                        rb.constraints = RigidbodyConstraints.FreezePosition |
                                       RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationY;
                        transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    }
                    // ディレイかけてから攻撃する
                    Timer += Time.deltaTime;
                    if (Timer >= idlingTime)
                    {
                        // プレイヤーの座標の取得
                        vec = (Player.transform.position - transform.position).normalized;
                        // 座標の固定
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                  RigidbodyConstraints.FreezeRotationX |
                                  RigidbodyConstraints.FreezeRotationY;
                        // 攻撃開始
                        Attack = true;
                        //AttackPattern = Random.Range(0, 2);
                        // サウンドフラグ切替
                        isCalledOnce = false;
                        // アイドリング終了
                        IdringFlg = false;
                        // タイマーのリセット
                        Timer = 0.0f;
                    }
                }
                if (Attack)
                {
                    start = false;
                    PP = Player.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
                    vec = (PP - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                    RandomTime = Random.Range(0.5f, 1.5f);
                    AttackPattern = 1;
                    Attack = false;
                    //case 2:
                    //    rb.velocity = new Vector3(0.0f, -MoveSpeed, 0.0f);
                    //    vec = ((transform.position -= new Vector3(0.0f, 15.0f, 0.0f)) - transform.position).normalized;
                    //    look = Quaternion.LookRotation(vec);
                    //    transform.localRotation = look;
                    //    Attack = true;
                    //    break;

                }

                if (AttackPattern == 1)
                {
                    Timer += Time.deltaTime;
                }

                if (Timer > RandomTime && AttackPattern == 1)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                    rotTime += Time.deltaTime;
                    transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    if (rotTime > 0.5f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                       RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationY;
                        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                        vec = (Player.transform.position - transform.position).normalized;
                        look = Quaternion.LookRotation(vec);
                        transform.localRotation = look;
                        rb.velocity = vec * MoveSpeed;
                        //AttackPattern = 3;
                    }
                }

                //if (AttackPattern == 2)
                //{
                //    if (rb.velocity.y < 0.0f)
                //    {
                //        rb.velocity += new Vector3(0.0f, 3.0f, 0.0f);
                //    }
                //    if (transform.position.x > Player.transform.position.x && rb.velocity.x > -MoveSpeed)
                //    {
                //        rb.velocity -= new Vector3(3.0f, 0.0f, 0.0f);
                //        if (rb.velocity.x <= -MoveSpeed)
                //        {
                //            vec = (Player.transform.position - transform.position).normalized;
                //            look = Quaternion.LookRotation(vec);
                //            transform.localRotation = look;
                //            rb.velocity = vec * MoveSpeed;
                //            AttackPattern = 3;
                //        }
                //    }
                //    if (transform.position.x < Player.transform.position.x && rb.velocity.x < MoveSpeed)
                //    {
                //        rb.velocity += new Vector3(3.0f, 0.0f, 0.0f);
                //        if (rb.velocity.x >= MoveSpeed)
                //        {
                //            vec = (Player.transform.position - transform.position).normalized;
                //            look = Quaternion.LookRotation(vec);
                //            transform.localRotation = look;
                //            rb.velocity = vec * MoveSpeed;
                //            AttackPattern = 3;
                //        }
                //    }
                //}
                
                //// プレイヤーとの距離を計算
                //dis = Vector3.Distance(transform.position, Player.transform.position);
                //// 一定距離離れたら再度アイドリング→攻撃
                //if (dis >= 70.0f && !disFlg)
                //{
                //    IdringFlg = true;
                //    disFlg = true;
                //}
                //else if (dis < 60.0f && disFlg)
                //{
                //    disFlg = false;
                //}
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
        if ((collision.gameObject.CompareTag("Default") || collision.gameObject.CompareTag("Ground")) && !IdringFlg)
        {
            // アイドリング開始
            IdringFlg = true;
        }
    }
}
