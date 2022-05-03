//==========================================================
//      トマト雑魚亜種の攻撃
//      作成日　2022/04/29
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/29      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy2 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private Vector3 TargetPos;
    private Vector3 vec;
    private bool look = true;       // プレイヤーのほうを見るフラグ
    private bool isGround;          // 接地フラグ
    private bool Invincible;
    private bool Attack;
    private bool Plunge;
    private bool AttackEnd;
    private float delay;            // ジャンプのディレイ
    private float InvincibleTime;

    [SerializeField]
    private float JumpPower;        // ジャンプ力

    [SerializeField]
    float MoveSpeed = 2.0f;          // 移動速度
    float AttackSpeed;
    //int DetecDist = 8;
    //bool InArea = false;                

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        //distance = 1.0f;
        ED = GetComponent<EnemyDown>();
        AttackSpeed = MoveSpeed + 10.0f;
        transform.Rotate(0, -90, 0);
        rb.velocity += new Vector3(0.0f, -0.5f, 0.0f);
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            rb.Resume(gameObject);

            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                Vector3 pos = rb.position;
                if (!isGround)
                {
                    // プレイヤーに向かって特攻する
                    if(!Attack && !Plunge)
                    {
                        float a = Player.transform.position.y - transform.position.y;
                        TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);
                        float step = MoveSpeed * Time.deltaTime;
                        rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                    }
                    if(Attack && !Plunge)
                    {
                        float step = AttackSpeed * Time.deltaTime;
                        rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                        AttackEnd = true;
                    }
                }

                if(Plunge)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
                    {
                        vec = (Player.transform.position - transform.position).normalized;
                        rb.velocity = vec * 80;
                        transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    }
                }

                // 跳ねる処理
                if (isGround && !Plunge)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f && !Attack)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                    if(delay > 0.5f && Attack)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, 3.0f * JumpPower, 0.0f);
                        SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                }
                
                if (Target.position.x < transform.position.x && !look && !Attack)   // プレイヤーのほうを向く処理
                {
                    transform.Rotate(0, -180, 0);
                    look = true;
                }
                if (Target.position.x > transform.position.x && look && !Attack)  // プレイヤーのほうを向く処理
                {
                    transform.Rotate(0, 180, 0);
                    look = false;
                }

                // 上昇速度&落下速度調整
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }
            }
        }
        else
        {
            rb.Pause(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY;
            if (AttackEnd)
            {
                Plunge = true;
                Attack = false;
            }
        }

        // プレイヤーに当たったら消える
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            EffectManager.Play(EffectData.eEFFECT.EF_TOMATOBOMB, transform.position, 0.9f);
            Destroy(gameObject, 0.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!Attack && isGround)
            {
                if (look)
                {
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(20.0f, a, 0.0f);
                }
                if (!look)
                {
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(-20.0f, a, 0.0f);
                }
                Attack = true;
            }
            
        }
    }
}
