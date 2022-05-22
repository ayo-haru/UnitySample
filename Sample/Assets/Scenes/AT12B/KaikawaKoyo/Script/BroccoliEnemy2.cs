//==========================================================
//      ブロッコリー雑魚亜種の攻撃
//      作成日　2022/04/15
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/15      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliEnemy2 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Vector3 TargetPos;
    private Rigidbody rb;
    private EnemyDown ED;

    [SerializeField]
    private float MoveSpeed = 5.0f;
    [SerializeField]
    private float JumpPower;
    private float PosY;
    private bool jump = false;
    private float delay;
    private float InvincibleTime = 2.0f;
    private float DamageTime;
    private bool isGround = false;
    private bool Invincible = false;

    private bool isCalledOnce = false;                             // 開始演出で使用。一回だけ処理をするために使う。

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
            if (Invincible)
            {
                gameObject.layer = LayerMask.NameToLayer("Invincible");
                transform.Rotate(0, 0, 0);
                DamageTime += Time.deltaTime;
                if (DamageTime > InvincibleTime)
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    DamageTime = 0.0f;
                    Invincible = false;
                }
            }

            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                Vector3 pos = rb.position;

                // プレイヤーに向かって特攻する
                float a = Player.transform.position.y - transform.position.y;
                TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);

                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, TargetPos, step);

                // プレイヤーがジャンプしたらジャンプする
                PosY = transform.position.y + 10.0f;
                if (Target.position.y > PosY && !jump)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f)
                    {
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        jump = true;
                        delay = 0.0f;
                    }
                }

                if (Target.position.x < transform.position.x)   // プレイヤーのほうを向く処理
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(-180, 0, 0));
                }
                if (Target.position.x > transform.position.x)  // プレイヤーのほうを向く処理
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(180, 0, 0));
                }

                // 落下処理
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }

                // SEの処理
                if (!isCalledOnce)     // 一回だけ呼ぶ
                {
                    SoundManager.Play(SoundData.eSE.SE_BUROKORI, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 接触判定
        if (collision.gameObject.CompareTag("Player"))
        {
            Invincible = true;
        }

        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            jump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}