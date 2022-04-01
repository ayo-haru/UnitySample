//=============================================================================
//
// 反射板当たり判定
//
//
// 作成日:2022/03/27
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/27 作成
// 2022/03/29 盾の個数制限を付けた
//=============================================================================

//コメント追加
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    //プレイヤー
    GameObject Player;
    //プレイヤーのリジットボディ
    Rigidbody player_rb;
    //地面パリイした時のはね返り速度
    public float baunceGround = 2.0f;
    //シールドマネージャ
    ShieldManager shield_Manager;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameData.Player;
        Player = GameObject.Find("Rulaby 1(Clone)");
        player_rb = Player.GetComponent<Rigidbody>();
        shield_Manager = Player.GetComponent<ShieldManager>();
        //盾が最大数を超えていたら
        if (!shield_Manager.AddShield())
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        shield_Manager.DestroyShield();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            //方向
           // Vector3 dir = Player.transform.position - collision.transform.position;
            Vector3 dir = Player.transform.position - gameObject.transform.position;
            dir.Normalize();
            player_rb.velocity = Vector3.zero;
            //地面パリイ
            player_rb.AddForce(dir * baunceGround,ForceMode.Impulse);
        }

        //盾消去
        Destroy(gameObject);

        Debug.Log(collision.gameObject.name + "と当たった");
    }
}
