//==========================================================
//      トマト雑魚亜種の攻撃2
//      作成日　2022/05/04
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/05/04      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy3 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private Vector3 TargetPos;
    private Vector3 PlayerPosX;
    private Vector3 TomatoPosX;
    private bool look;              // プレイヤーのほうを見るフラグ
    private bool isGround;          // 接地フラグ
    private bool Attack;
    private bool AttackEnd;
    private bool Plunge;
    private bool TomatoDead;
    private bool Explosion;
    private float delay;            // ジャンプのディレイ
    private float dis;
    private float Distance = 80.0f;
    
    [SerializeField]
    private float JumpPower;        // ジャンプ力

    [SerializeField]
    float MoveSpeed = 2.0f;          // 移動速度       

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            rb.Resume(gameObject);

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
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);
                    float step = MoveSpeed * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                    if(Attack)
                    {
                        // 回転させる
                        if (look)
                        {
                            transform.Rotate(new Vector3(-8.2f, 0.0f, 0.0f), Space.Self);
                        }
                        if (!look)
                        {
                            transform.Rotate(new Vector3(8.2f, 0.0f, 0.0f), Space.Self);
                        }
                        AttackEnd = true;
                    }
                }

                // 派生攻撃
                if (Plunge)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.7f)
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

                if (Target.position.x < transform.position.x && look)   // プレイヤーのほうを向く処理
                {
                    transform.Rotate(0, -180, 0);
                    look = false;
                }
                if (Target.position.x > transform.position.x && !look)  // プレイヤーのほうを向く処理
                {
                    transform.Rotate(0, 180, 0);
                    look = true;
                }

                // 跳ねる処理
                if (isGround && !Plunge)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                }

                // 上昇速度&落下速度調整
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }
            }

            if (TomatoDead)
            {
                float step = 100.0f * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                // 回転させる
                if (look)
                {
                    transform.Rotate(new Vector3(-20.0f, 0.0f, 0.0f), Space.Self);
                }
                if (!look)
                {
                    transform.Rotate(new Vector3(20.0f, 0.0f, 0.0f), Space.Self);
                }
                // 上昇速度&落下速度調整
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -2.0f, 0.0f);
                }
                Explosion = true;
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
            if (dis <= Distance)
            {
                Attack = true;
            }
            if (AttackEnd)
            {
                Plunge = true;
            }
            if (Explosion)
            {
                SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
                Destroy(gameObject, 0.0f);
            }
        }

        // プレイヤーに当たったら消える
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
            Destroy(gameObject, 0.0f);
        }
    }
}
