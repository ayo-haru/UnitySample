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
    private Vector3 PlayerPosX;
    private Vector3 TomatoPosX;
    private bool look = true;       // プレイヤーのほうを見るフラグ
    private bool isGround;          // 接地フラグ
    private bool TomatoDead;
    private bool Attack;
    private bool Plunge;
    private bool AttackEnd;
    private bool Explosion;
    private float delay;            // ジャンプのディレイ
    private float dis;
    private float Distance = 80.0f;
    private int AttackPattern;

    [SerializeField]
    private float JumpPower;        // ジャンプ力

    [SerializeField]
    float MoveSpeed = 2.0f;          // 移動速度
    float AttackSpeed;            

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
        AttackSpeed = MoveSpeed + 10.0f;
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            Vector3 pos = rb.position;
            // プレイヤーとのX軸間の距離を求める
            PlayerPosX = Player.transform.position - new Vector3(0.0f, Player.transform.position.y, Player.transform.position.z);
            TomatoPosX = transform.position - new Vector3(0.0f, transform.position.y, transform.position.z);
            dis = Vector3.Distance(PlayerPosX, TomatoPosX);
            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive && !TomatoDead)
            {
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

                // 派生攻撃
                if(Plunge)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
                    {
                        // ジャンプする
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, 100.0f, 0.0f);
                        // 座標計算
                        TargetPos = Target.position - new Vector3(0.0f, 15.0f, 0.0f);
                        
                        isGround = false;
                        TomatoDead = true;
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
                    transform.rotation = Quaternion.LookRotation(new Vector3(-180, 0, 0));
                    look = true;
                }
                if (Target.position.x > transform.position.x && look && !Attack)  // プレイヤーのほうを向く処理
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(180, 0, 0));
                    look = false;
                }

                // 上昇速度&落下速度調整
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }
            }

            if(AttackEnd)
            {
                // 回転させる
                if (look)
                {
                    transform.Rotate(new Vector3(-20.0f, 0.0f, 0.0f), Space.Self);
                }
                if (!look)
                {
                    transform.Rotate(new Vector3(20.0f, 0.0f, 0.0f), Space.Self);
                }
            }

            if(TomatoDead)
            {
                float step = 100.0f * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, TargetPos, step);
         
                // 上昇速度&落下速度調整
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -2.0f, 0.0f);
                }
                Explosion = true;
            }
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
            if (dis <= Distance && !Attack)
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
            if (AttackEnd)
            {
                Plunge = true;
            }
            if(Explosion)
            {
                SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
                EffectManager.Play(EffectData.eEFFECT.EF_TOMATOBOMB, transform.position, 0.9f);
                Destroy(gameObject, 0.0f);
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
}
