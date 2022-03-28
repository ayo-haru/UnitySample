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
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameData.Player;
        Player = GameObject.Find("Player(Clone)");
        player_rb = Player.GetComponent<Rigidbody>();
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
            //地面パリイ
            player_rb.AddForce(dir * baunceGround,ForceMode.Impulse);
        }

        //盾消去
        Destroy(gameObject);
    }
}
