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
    public float bounceSpeed = 5.0f;
    public float bounceVectorMultiple = 2f;
    private float bouncePower = 1000.0f;

    public bool isAlive;

    float DeadTime;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            DeadTime += Time.deltaTime;
        }

        if (DeadTime > 1.0f)
        {
            Destroy(gameObject, 0.0f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            
            //衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = collision.contacts[0].normal;
            //衝突した速度ベクトルを単位ベクトルにする
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            //x,y,z方向に対して法線ベクトルを取得
            velocity += new Vector3(normal.x * bounceVectorMultiple, normal.y * bounceVectorMultiple, normal.z * bounceVectorMultiple);
            //プレイヤーを逆方向に跳ね返す
            collision.rigidbody.AddForce(-velocity * bounceSpeed, ForceMode.Impulse);
            //弾いたら消す
            isAlive = false;
            // 重力を消す
            rb.useGravity = false;
            // 空気抵抗をゼロに
            rb.angularDrag = 0.0f;
            // 回転軸を中央に
            rb.centerOfMass = new Vector3(0, 0, 0);
            
            //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            rb.AddForce(velocity * bouncePower, ForceMode.Force);
            // 回転させる
            rb.AddTorque(-100.0f, -100.0f, -100.0f);
        }

        if (isAlive == false && collision.gameObject.CompareTag("Ground"))
        {
            // 壁、床に当たったら消える
            //Destroy(gameObject, 0.0f);
        }

    }


}
