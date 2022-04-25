//==========================================================
//      雑魚敵の弾かれたとき
//      作成日　2022/03/20
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/20
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    [SerializeField]
    GameObject Item;
    GameObject Player;
    private float bouncePower = 200.0f;
    private Vector3 Pos;
    private Vector3 EnemyPos;
    private Vector3 velocity;
    private Vector3 vec;
    private Animator animator;

    [SerializeField]
    private int DropRate;           // 回復アイテムのドロップ率

    [SerializeField]
    private int EnemyNumber;        // 敵識別
    private int Drop;

    public bool isAlive;

    float DeadTime = 0.0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);
            if(isAlive)
            {
                animator.speed = 1;
            }
            // 時間で消える処理
            if (!isAlive)
            {
                DeadTime += Time.deltaTime;
            }

            if (DeadTime > 1.0f)
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMYDOWN, Pos);
            }
        }
        else
        {
            rb.Pause(gameObject);
            animator.speed = 0;
        }
 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);
            if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
            {
                //EnemyPos = transform.position + new Vector3(0.0f, 5.0f, 0.0f);
                //print(EnemyPos);
                vec = (Player.transform.position - transform.position).normalized;
                //プレイヤーを逆方向に跳ね返す
                collision.rigidbody.AddForce(vec * 5.0f, ForceMode.Impulse);
                // アニメーションを止める
                animator.speed = 0;
                // 重力を消す
                rb.useGravity = false;
                // 空気抵抗をゼロに
                rb.angularDrag = 0.0f;

                // 回転軸を変更
                if (EnemyNumber == 1)
                {
                    rb.centerOfMass = new Vector3(0.0f, 5.0f, 2.0f);
                }
                else if (EnemyNumber == 2)
                {
                    rb.centerOfMass = new Vector3(0.0f, 0.3f, 0.0f);
                }
                else
                {
                    rb.centerOfMass = new Vector3(0.0f, 0.0f, 0.0f);
                }

                // 回復アイテムを落とす
                Pos = transform.position;
                Drop = Random.Range(0, 100);
                if (Drop < DropRate)
                {
                    Instantiate(Item, Pos, Quaternion.identity);
                }

                //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
                //rb.AddForce(velocity * bouncePower, ForceMode.Force);
                rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY;
                //プレイヤーと逆方向に跳ね返す
                rb.velocity = -vec * bouncePower;
                
                // 回転させる
                if(Player.transform.position.x < transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);

                }
                if (Player.transform.position.x > transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
                }
                
                //SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
                
                //弾いたら消す
                isAlive = false;
            }

            //if (!isAlive)
            //{
            //    //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            //    rb.AddForce(velocity * bouncePower, ForceMode.Force);
            //    // 回転させる
            //    rb.AddTorque(0.0f, 0.0f, -300.0f);

            //}

            if (isAlive == false && collision.gameObject.CompareTag("Ground"))
            {
                rb.velocity = -vec * bouncePower;
                // 壁、床に当たったら消える
                //Destroy(gameObject, 0.0f);
            }
        }
        else
        {
            rb.Pause(gameObject);
        }
    }
}
