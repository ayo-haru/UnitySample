//=============================================================================
//
// 跳ね返すギミック
//
// 作成日:2022/03
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03    作成
// 2022/03/17 一定の距離跳ね返されるように変更
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaunceGimmick : MonoBehaviour
{
    //跳ね返す速度
    public float bounceSpeed = 5.0f;
    //public float bounceVectorMultiple = 2f;
    //プレイヤー
    GameObject Player;
    //プレイヤーのリジットボディ
    Rigidbody player_rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        player_rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            ////衝突した面の、接触した点における法線ベクトルを取得
            //Vector3 normal = collision.contacts[0].normal;
            ////衝突した速度ベクトルを単位ベクトルにする
            //Vector3 velocity = collision.rigidbody.velocity.normalized;
            ////x,y,z方向に対して逆向きの法線ベクトルを取得
            //velocity += new Vector3(-normal.x * bounceVectorMultiple, -normal.y * bounceVectorMultiple, -normal.z * bounceVectorMultiple);
            ////取得した法線ベクトルに跳ね返す速さを掛けて、跳ね返す
            //collision.rigidbody.AddForce(velocity * bounceSpeed, ForceMode.Impulse);

            //方向取得
            Vector3 dir =  Player.transform.position - gameObject.transform.position;
            //z軸移動しないように０にしておく
            dir.z = 0;
            dir.Normalize();
            //プレイヤーのリジットボディ無効
            player_rb.velocity = Vector3.zero;
            //跳ね返す
            player_rb.AddForce(dir * bounceSpeed, ForceMode.Impulse);
            //盾消去
            Destroy(collision.gameObject);

        }
    }
}
