//==========================================================
//      ニンジン雑魚の攻撃
//      作成日　2022/03/16
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 PlayerPos;
    private Vector3 EnemyPos;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    private float dis;
    private float InvincibleTime;
    private float AttackTime;
    bool InArea = true;
    bool Attack = false;
    bool pause = false;
    private bool Invincible = false;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);
            //print(InArea);
            //print(rb.velocity);
            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                PlayerPos = Player.transform.position;
                EnemyPos = transform.position;
                dis = Vector3.Distance(EnemyPos, PlayerPos);
                if(dis >= 100.0f)
                {
                    //Destroy(gameObject, 0.0f);
                }
                if (Invincible)
                {
                    gameObject.layer = LayerMask.NameToLayer("Invincible");
                    InvincibleTime += Time.deltaTime;
                }
                if (InvincibleTime > 0.3f)
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    InvincibleTime = 0.0f;
                    Invincible = false;
                }
                if (!Attack)
                {
                    vec = (Player.transform.position - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                    Attack = true;
                }
                //if (pause)
                //{
                //    rb.velocity = vec * MoveSpeed;
                //    pause = false;
                //}

                if (!isCalledOnce)     // 一回だけ呼ぶ
                {
                    //SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
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

    private void OnTriggerExit(Collider other)
    {
        if (!Pause.isPause)
        {
            if (other.CompareTag("Player"))
            {
                //InArea = false;
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
        }
    }

}