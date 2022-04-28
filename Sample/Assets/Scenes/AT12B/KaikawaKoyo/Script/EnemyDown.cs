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
    private GameObject Item;
    private GameObject Player;
    private ParticleSystem TomatoBom;
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
    private bool ItemDrop = false;
    public bool isAlive;
    float DeadTime = 0.0f;

    Rigidbody rb;

    //---ディゾルブ処理のための追記(2022/04/28.吉原)
    Dissolve _dissolve;
    private bool isCalledOnce = false;      // Update内で一回だけ処理を行いたいのでbool型の変数を用意
    private bool FinDissolve = false;       // Dissolveマテリアルに差し替える処理を終えたことを判定する

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
        _dissolve = this.GetComponent<Dissolve>();
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

                //---ディゾルマテリアルに変更
                if (!isCalledOnce)
                {
                    if (EnemyNumber == 1 || EnemyNumber == 0)
                    {
                        _dissolve.Invoke("Play", 0.2f);
                        isCalledOnce = true;
                        FinDissolve = true;
                    }
                }
            }

            if (DeadTime > 1.5f)
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                if (EnemyNumber == 2)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_TOMATOBOMB, transform.position, 0.9f);
                }
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMYDOWN, Pos, 2.0f);
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
            // 弾かれたらベクトルを計算して関数を呼び出す
            if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
            {
                vec = (transform.position -Player.transform.position).normalized;
                EnemyDead(vec , Player.transform.position.x);
            }
        }
        else
        {
            rb.Pause(gameObject);
        }
    }

    // 死ぬ時の処理
    public void EnemyDead(Vector3 vec , float x)
    {
        if (!Pause.isPause)
        {
            rb.Resume(gameObject);
            // アニメーションを止める
            animator.speed = 0;
            // 重力を消す
            rb.useGravity = false;
            // 空気抵抗をゼロに
            rb.angularDrag = 0.0f;
            // レイヤー変更
            gameObject.layer = LayerMask.NameToLayer("DownEnemy");

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
            if (Drop < DropRate && !ItemDrop)
            {
                Instantiate(Item, Pos, Quaternion.identity);
                ItemDrop = true;
            }

            //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            //rb.AddForce(velocity * bouncePower, ForceMode.Force);
            rb.constraints = RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY;
            //プレイヤーと逆方向に跳ね返す
            rb.velocity = vec * bouncePower;

            // 回転させる
            if (x < transform.position.x)
            {
                rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);

            }
            if (x > transform.position.x)
            {
                rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
            }

            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
            
            isAlive = false;
        }
        else
        {
            rb.Pause(gameObject);
        }
       
    }
}
