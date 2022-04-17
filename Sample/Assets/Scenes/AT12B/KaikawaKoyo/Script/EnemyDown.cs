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
    private float bouncePower = 10000.0f;
    private Vector3 Pos;
    private Vector3 velocity;
    private Vector3 vec;

    [SerializeField]
    private int DropRate;           // 回復アイテムのドロップ率
    private int Drop;

    public bool isAlive;

    float DeadTime = 0.0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            vec = (Player.transform.position - transform.position).normalized;
            

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
 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!Pause.isPause)
        {
            if (collision.gameObject.name == "Weapon(Clone)")
            {
                //プレイヤーを逆方向に跳ね返す
                collision.rigidbody.AddForce(vec * 5.0f, ForceMode.Impulse);

                //弾いたら消す
                isAlive = false;
                // 重力を消す
                rb.useGravity = false;
                // 空気抵抗をゼロに
                rb.angularDrag = 0.0f;
                // 回転軸を中央に
                rb.centerOfMass = new Vector3(0, 0, 0);

                // 回復アイテムを落とす
                Pos = transform.position;
                Drop = Random.Range(0, 100);
                if (Drop < DropRate)
                {
                    Instantiate(Item, Pos, Quaternion.identity);
                }

                //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
                //rb.AddForce(velocity * bouncePower, ForceMode.Force);

                //プレイヤーと逆方向に跳ね返す
                rb.AddForce(-vec * bouncePower, ForceMode.Force);

                // 回転させる
                rb.AddTorque(0.0f, 0.0f, -10000.0f);

                //SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
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
                // 壁、床に当たったら消える
                //Destroy(gameObject, 0.0f);
            }

        }
    }
}
