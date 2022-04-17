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
    //private float distance;
    private EnemyDown ED;
    //private Vector3 aim;
    private bool look;              // プレイヤーのほうを見るフラグ
    private bool isGround;          // 接地フラグ
    private float delay;            // ジャンプのディレイ

    [SerializeField]
    private float JumpPower;        // ジャンプ力

    [SerializeField]
    float MoveSpeed = 2.0f;          // 移動速度
    //int DetecDist = 8;
    bool InArea = false;                

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        //distance = 1.0f;
        ED = GetComponent<EnemyDown>();
        transform.Rotate(0, -90, 0);     
        look = false;
        rb.velocity += new Vector3(0.0f, -0.5f, 0.0f);
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            print(rb.velocity.y);

            // プレイヤーを見つけたら攻撃開始
            if (InArea && ED.isAlive)
            {
                Vector3 pos = rb.position;
                if (!isGround)
                {
                    // プレイヤーに向かって特攻する
                    float step = MoveSpeed * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(pos, Target.position, step);
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
                if (isGround)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
                    {
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        //SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                }

                // 上昇速度&落下速度調整
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -0.8f, 0.0f);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 接地判定
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

        // プレイヤーに当たったら消える
        if (collision.gameObject.CompareTag("Player"))
        {
            //SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            Destroy(gameObject, 0.0f);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    接地判定
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGround = false;
    //    }
    //}

    public void OnTriggerEnter(Collider other)    // コライダーでプレイヤーを索敵したい
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InArea = false;
        }
    }
}
