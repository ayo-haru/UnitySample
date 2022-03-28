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
    private Rigidbody rb;
    private EnemyDown ED;
    private int count = 0;

    [SerializeField]
    float MoveSpeed = 5.0f;
    int DetecDist = 8;
    bool InArea = false;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        transform.Rotate(new Vector3(0, 0, 15));
        rb.centerOfMass = new Vector3(0, -1, 0);
    }

    private void Update()
    {
        // プレイヤーを見つけたら攻撃開始
        if (InArea && ED.isAlive)
        {
            Vector3 pos = rb.position;
            // プレイヤーに向かって特攻する
            float step = MoveSpeed * Time.deltaTime;
            rb.position = Vector3.MoveTowards(pos, Target.position, step);
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
