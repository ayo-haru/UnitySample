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

public class CarrotEnemy2 : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 PlayerPos;
    private Vector3 EnemyPos;
    private Vector3 PP;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    private int AttackPattern = 1;
    private float dis;
    private float AttackTime;
    private float RandomTime;
    private float rotTime;
    private bool InArea;
    private bool Look;
    private bool Attack = false;
    private bool Attack2 = false;
    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        Look = false;
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                PlayerPos = Player.transform.position;
                EnemyPos = transform.position;
                dis = Vector3.Distance(EnemyPos, PlayerPos);
                if (dis >= 200.0f)
                {
                    Destroy(gameObject, 0.0f);
                }
                if (!Attack)
                {
                    switch (AttackPattern)
                    {
                        case 0:
                            vec = (Player.transform.position - transform.position).normalized;
                            look = Quaternion.LookRotation(vec);
                            transform.localRotation = look;
                            rb.velocity = vec * MoveSpeed;
                            Attack = true;
                            break;
                        case 1:
                            PP = Player.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
                            vec = (PP - transform.position).normalized;
                            look = Quaternion.LookRotation(vec);
                            transform.localRotation = look;
                            rb.velocity = vec * MoveSpeed;
                            RandomTime = Random.Range(0.5f, 1.5f);
                            Attack = true;
                            break;
                        //case 2:
                        //    rb.velocity = new Vector3(0.0f, -MoveSpeed, 0.0f);
                        //    vec = ((transform.position -= new Vector3(0.0f, 15.0f, 0.0f)) - transform.position).normalized;
                        //    look = Quaternion.LookRotation(vec);
                        //    transform.localRotation = look;
                        //    Attack = true;
                        //    break;
                    }
                }

                if (AttackPattern == 1)
                {
                    AttackTime += Time.deltaTime;
                }

                if (AttackTime > RandomTime && AttackPattern == 1)
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

                if (!isCalledOnce)     // 一回だけ呼ぶ
                {
                    SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
                if (!InArea)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition |
                                 RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY;
                    AttackTime += Time.deltaTime;
                    transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    if (AttackTime > 0.5f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY;
                        Attack = false;
                        InArea = true;
                        AttackTime = 0.0f;
                    }
                }
            }
        }
        else
        {
            rb.Pause(gameObject);
            //pause = true;
        }
    }

    public void OnTriggerEnter(Collider other)    // コライダーでプレイヤーを索敵したい
    {
        if (other.CompareTag("Player") && Look == false)
        {
            InArea = true;
            Look = true;
            //AttackPattern = Random.Range(0, 4);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Pause.isPause)
        {
            if (other.CompareTag("Player"))
            {
                InArea = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Attack = false;
            //AttackPattern = Random.Range(0, 4);
        }
    }
}
