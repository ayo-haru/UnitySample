//==========================================================
//      ハチ雑魚の攻撃
//      作成日　2022/04/04
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/04      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    GameObject Aim;
    GameObject Item;
    private Rigidbody rb;
    private Vector3 Enemypos;
    private Vector3 velocity;
    public float bounceVectorMultiple = 2f;
    private float bouncePower = 1000.0f;
    public bool isAlive;
    public bool InArea;
    float DeadTime = 0.0f;
    float A = 2.0f;
    float B = 5.0f;
    float Width = 6.0f;     // 飛び回る横幅
    float Vertical = 0.7f;  // 飛び回る縦幅

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。

    [SerializeField]
    private GameObject FiringPoint;

    [SerializeField]
    private GameObject BeeBullet;

    [SerializeField]
    private float speed = 30.0f;

    //[SerializeField]
    private float TimeOut = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Aim = GameObject.Find("BeeAim");
        Item = (GameObject)Resources.Load("HealItem");
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        isAlive = true;
        Enemypos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            // 時間で消える処理
            if (!isAlive)
            {
                DeadTime += Time.deltaTime;

            }
            if (DeadTime > 1.0f)
            {
                Enemypos = transform.position;
                Destroy(gameObject, 0.0f);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMYDOWN, Enemypos);
            }

            // 生きている間行う処理
            if (isAlive)
            {
                // 飛び回る処理
                transform.position = new Vector3(Mathf.Sin(A * Time.time) * Width + Enemypos.x,
                    Mathf.Cos(B * Time.time) * Vertical + Enemypos.y, Enemypos.z);

                if (InArea)
                {
                    //PoisonShot();
                    TimeOut += Time.deltaTime;

                    if (TimeOut >= 3.0f)
                    {
                        GameObject newBall = Instantiate(BeeBullet, Aim.transform.position, Aim.transform.rotation);
                        Vector3 direction = newBall.transform.forward;
                        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
                        TimeOut = 0.0f;
                    }
                }

                // サウンド処理
                /*
                if (!isCalledOnce)     // 一回だけ呼ぶ
                {
                    SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
                */
            }

        }

    }

    public void OnTriggerEnter(Collider other)    // コライダーでプレイヤーを索敵したい
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 弾かれた弾に当たったら死ぬ
        if (collision.gameObject.name == "PoisonBullet(Clone)")
        {
            //衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = collision.contacts[0].normal;
            //衝突した速度ベクトルを単位ベクトルにする
            velocity = collision.rigidbody.velocity.normalized;
            //x,y,z方向に対して法線ベクトルを取得
            velocity += new Vector3(normal.x * bounceVectorMultiple, normal.y * bounceVectorMultiple, normal.z * bounceVectorMultiple);

            //弾いたら消す
            isAlive = false;
            // 空気抵抗をゼロに
            rb.angularDrag = 0.0f;

            // 回復アイテムを落とす
            Enemypos = transform.position;
            Instantiate(Item, Enemypos, Quaternion.identity);

            //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            rb.AddForce(velocity * bouncePower, ForceMode.Force);
        }
    }

}
