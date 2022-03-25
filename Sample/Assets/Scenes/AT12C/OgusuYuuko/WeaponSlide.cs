//=============================================================================
//
// 反射板スライド
//
//
// 作成日:2022/03/16
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/16 作成
// 2022/03/17 スライドを全方向できるようにした
// 2022/03/24 プレイヤーと盾がくっつかないようにした
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum Dir_Attack { NONE,RIGHT, LEFT, UP, DOWN };  //攻撃の方向
public class WeaponSlide : MonoBehaviour
{
    //スライドの方向
    Vector3 dir;
    //スライドの速度
    public float slideSpeed = 0.1f;
    //リジットボディ
    Rigidbody rb;
    //プレイヤー位置
    Vector3 PlayerPos;
    //プレイヤーと盾の最小距離
    public float minDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Player = GameObject.Find("Player");
        PlayerPos = GameData.PlayerPos;
        dir = gameObject.transform.position - PlayerPos;
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //反射板位置
        //Vector3 pos = rb.position;
        //プレイヤー位置
        PlayerPos = GameData.PlayerPos;

        //switch (nDir)
        //{
        //    case Dir_Attack.RIGHT:
        //        //反射板右に移動
        //        pos.x += slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.LEFT:
        //        //反射板左に移動
        //        pos.x -= slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.UP:
        //        //反射板上に移動
        //        pos.y += slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.DOWN:
        //        //反射板下に移動
        //        pos.y -= slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.NONE:
        //        break;
        //}

        //プレイヤーから反射板の方向を取得
        //Vector3 dir = playerPos - pos;
        //dir.Normalize();

        //取得した方向に反射板移動
        rb.position += dir * slideSpeed;

        //プレイヤーと盾の距離が近い場合は離す
        float dis = Vector3.Distance(PlayerPos, rb.position);
        if (dis < minDistance)
        {
            rb.position += dir * (minDistance - dis);
        }

    }

    //方向設定する
    //public void SetDir(Dir_Attack dir) {
    //    nDir = dir;
    //}
}
