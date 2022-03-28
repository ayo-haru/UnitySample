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
    private float distance;
    private EnemyDown ED;
    //private Vector3 aim;
    private bool look;

    [SerializeField]
    float MoveSpeed = 2.0f;
    int DetecDist = 8;
    bool InArea = false;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        Target = Player.transform;                    // プレイヤーの座標取得
        rb = gameObject.GetComponent<Rigidbody>();
        distance = 1.0f;
        ED = GetComponent<EnemyDown>();
        look = true;
    }

    private void Update()
    {
        if (InArea && ED.isAlive)
        {
            Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
            Ray ray = new Ray(rayPosition, Vector3.down);
            bool isGround = Physics.Raycast(ray, distance); // 接地判定
            Vector3 pos = rb.position;
            // プレイヤーを見つけたら攻撃開始
            if (isGround == false)
            {
                // プレイヤーに向かって特攻する
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, Target.position, step);
            }

            if (Target.position.x < transform.position.x && look)
            {
                transform.Rotate(0, -90, 0);
                look = false;
            }

            if (Target.position.x > transform.position.x && !look)
            {
                transform.Rotate(0, 90, 0);
                look = true;
            }

            // 跳ねる処理
            if (isGround)
            {
                rb.AddForce(transform.up * 60.0f, ForceMode.Force);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たったら消える
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
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
