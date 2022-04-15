//=============================================================================
//
// 回復アイテム処理
//
// 作成日:2022/04/16
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/04/16   作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    //---変数宣言
    public GameObject prefab;
    Rigidbody rb;
    public float BounceSpeed = 10.0f;                   // 弾かれるスピード
    public float BounceVectorMultiple = 2.0f;           // 法線ベクトルに乗算する値
    public float BouncePower = 100.0f;                  // 弾かれる値
    private Vector3 Velocity;                           // 弾くベクトル
    private Vector3 TargetPos;                          // 武器を弾いた個所
    [System.NonSerialized] public bool isAlive;         // 生存フラグ

    // Start is called before the first frame update
    void Start()
    {
        // プレハブを複製
        
        //GameObject HealItem = Instantiate(prefab,
        //                                  new Vector3(0.0f,0.0f,0.0f),
        //                                  Quaternion.identity);
        rb = GetComponent<Rigidbody>();

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            // 衝突した面の接地点のベクトルを取得
            Vector3 normal = collision.contacts[0].normal; 
            
            // 衝突した速度ベクトルを単位ベクトルに置き換える
            Velocity = collision.rigidbody.velocity.normalized;

            // x,y,z方向に対して法線ベクトルを取得
            Velocity += new Vector3(normal.x * BounceVectorMultiple,
                                    normal.y * BounceVectorMultiple,
                                    normal.z * BounceVectorMultiple);

            // 逆方向に跳ね返す
            collision.rigidbody.AddForce(-Velocity * BounceSpeed,ForceMode.Impulse);

            
            isAlive = false;                                // 弾かれたら消す
            rb.useGravity = false;                          // 重力を消す
            rb.angularDrag = 0.0f;                          // 空気抵抗をゼロに
            rb.centerOfMass = new Vector3(0.0f,0.0f,0.0f);  // 回転軸を中央にする

            // 取得した法線ベクトルに跳ね返す早さをかけて、跳ね返す
            rb.AddForce(Velocity * BouncePower,ForceMode.Impulse);

            if(isAlive == false)
            {
                Destroy(prefab,0.0f);
            }

        }
    }
}
