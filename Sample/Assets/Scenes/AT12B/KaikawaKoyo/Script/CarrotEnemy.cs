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
using DG.Tweening;

public class CarrotEnemy : MonoBehaviour
{
    GameObject Player;
    private GameObject Jet;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 EffectPos;
    private EnemyDown ED;
    private Quaternion look;
    private ParticleSystem effect;

    [SerializeField]
    float MoveSpeed;
    float MovingSpeed;
    private float Timer;
    private float idlingTime = 0.5f;
    private float AttackTime = 0.5f;
    bool start = true;
    bool IdringFlg = true;
    bool InArea = true;
    bool Attack;
    //bool pause = false;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Jet = transform.Find("JetPos").gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        MovingSpeed = MoveSpeed / 2;
        transform.DOShakePosition(duration: idlingTime, strength: 5.0f);    // ぶるぶる震わせる
        //effect = Instantiate(EffectData.EF[0]);
        //effect.gameObject.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        //effect.Play();
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);

            // ジェットのエフェクト出すよ
            //effect.transform.position = Jet.transform.position;

            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                // アイドリングするよ
                if(IdringFlg)
                {
                    Idling();
                }
                // 攻撃処理
                if (Attack)
                {
                    start = false;
                    // プレイヤーのほうを向く
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;

                    // サウンド処理
                    if (!isCalledOnce)     // 一回だけ呼ぶ
                    {
                        SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                        isCalledOnce = true;
                    }

                    // 加速
                    rb.velocity = vec * MovingSpeed;
                    if(MoveSpeed >= MovingSpeed)
                    {
                        MovingSpeed += 3.0f;
                    }
                    else
                    {
                        Attack = false;

                    }
                }
                //if (pause)
                //{
                //    rb.velocity = vec * MoveSpeed;
                //    pause = false;
                //}

                // 一定以上離れたら回転してから再度攻撃する
                if (!InArea)
                {
                    // くるくる回転させる
                    rb.constraints = RigidbodyConstraints.FreezePosition |
                        RigidbodyConstraints.FreezeRotationX |
                        RigidbodyConstraints.FreezeRotationY;
                    Timer += Time.deltaTime;
                    transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    if (Timer > 0.5f)
                    {
                        // プレイヤーの座標の取得
                        vec = (Player.transform.position - transform.position).normalized;
                        // 座標の固定
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                  RigidbodyConstraints.FreezeRotationX |
                                  RigidbodyConstraints.FreezeRotationY;
                        // 攻撃開始
                        Attack = true;
                        InArea = true;
                        // タイマーのリセット
                        Timer = 0.0f;
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

    // アイドリング処理
    private void Idling()
    {
        if(start)
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
            // サウンドフラグ切替
            isCalledOnce = false;
            // アイドリング終了
            IdringFlg = false;
            // タイマーのリセット
            Timer = 0.0f;
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
            //Destroy(effect.gameObject, 0.0f);
            Destroy(gameObject, 0.0f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            // プレイヤーの座標の取得
            vec = (Player.transform.position - transform.position).normalized;
            // アイドリング開始
            IdringFlg = true;
        }
    }

}