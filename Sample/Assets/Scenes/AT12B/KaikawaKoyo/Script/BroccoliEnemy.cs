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
    private int count = 0;

    [SerializeField]
    float MoveSpeed = 5.0f;
    int DetecDist = 8;
    bool InArea = false;

    private bool isCalledOnce = false;                             // 開始演出で使用。一回だけ処理をするために使う。

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        rb.centerOfMass = new Vector3(0, -1, 0);
        transform.Rotate(new Vector3(0, 0, 15));
    }

    private void Update()
    {
        rot = transform.rotation;
        print(rot.z);
        // プレイヤーを見つけたら攻撃開始
        if (InArea && ED.isAlive)
        {
            Vector3 pos = rb.position;
            // プレイヤーに向かって特攻する
            float step = MoveSpeed * Time.deltaTime;
            rb.position = Vector3.MoveTowards(pos, Target.position, step);

            //if (rot.z < 0.15f)
            //{
            //    transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
            //}
            //if (rot.z > -0.15f)
            //{
            //    transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
            //}

            // SEの処理
            if (!isCalledOnce)     // 一回だけ呼ぶ
            {
                SoundManager.Play(SoundData.eSE.SE_BUROKORI,SoundData.GameAudioList);
                isCalledOnce = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    Destroy(gameObject, 0.0f);
        //}
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
