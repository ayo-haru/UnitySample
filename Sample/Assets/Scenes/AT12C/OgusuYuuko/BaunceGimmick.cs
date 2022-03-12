using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaunceGimmick : MonoBehaviour
{
    public float bounceSpeed = 5.0f;
    public float bounceVectorMultiple = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            //衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = collision.contacts[0].normal;
            //衝突した速度ベクトルを単位ベクトルにする
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            //x,y,z方向に対して逆向きの法線ベクトルを取得
            velocity += new Vector3(-normal.x * bounceVectorMultiple, -normal.y * bounceVectorMultiple, -normal.z * bounceVectorMultiple);
            //取得した法線ベクトルに跳ね返す速さを掛けて、跳ね返す
            collision.rigidbody.AddForce(velocity * bounceSpeed, ForceMode.Impulse);
        }
    }
}
