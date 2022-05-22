//==========================================================
//      トマト雑魚の攻撃
//      作成日　2022/03/17
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/17      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private Vector3 TargetPos;
    private bool isGround;          // 接地フラグ
    private float delay;            // ジャンプのディレイ

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
        if(!Pause.isPause)
        {
            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                Vector3 pos = rb.position;
                if (!isGround)
                {
                    // プレイヤーに向かって特攻する
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);
                    float step = MoveSpeed * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                }

                if (Target.position.x < transform.position.x)   // プレイヤーのほうを向く処理
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(-180, 0, 0));
                }
                if (Target.position.x > transform.position.x)  // プレイヤーのほうを向く処理
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(180, 0, 0));
                }

                // 跳ねる処理
                if (isGround)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
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
        }

        // プレイヤーに当たったら消える
        if (collision.gameObject.CompareTag("Player") && ED.isAlive)
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
            Destroy(gameObject, 0.0f);
        }
    }
}
