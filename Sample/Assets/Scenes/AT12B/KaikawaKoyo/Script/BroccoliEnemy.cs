//==========================================================
//      ブロッコリー雑魚の攻撃
//      作成日　2022/03/18
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/18      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Quaternion rot;
    private Rigidbody rb;
    private EnemyDown ED;
    //private bool loop = false;

    [SerializeField]
    float MoveSpeed = 5.0f;
    
    bool InArea = false;
    private bool look = false;
    private bool isGround = false;

    private bool isCalledOnce = false;                             // 開始演出で使用。一回だけ処理をするために使う。

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        rb.centerOfMass = new Vector3(0, -1, 0);
        //transform.Rotate(new Vector3(0, 0, 15));
        transform.Rotate(0, -90, 0);
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            rot = transform.rotation;
            // プレイヤーを見つけたら攻撃開始
            if (InArea && ED.isAlive)
            {
                Vector3 pos = rb.position;

                // プレイヤーに向かって特攻する
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, Target.position, step);

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

                if(!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -0.5f, 0.0f);
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
        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }

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
