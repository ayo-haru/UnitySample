using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_WarpBullet : MonoBehaviour
{
    public GameObject WarpBullet;
    public static bool flag = true;
    public LB_Attack Attack;

    // Start is called before the first frame update
    void Start()
    {
        Attack = GameObject.Find("LastBoss(Clone)").GetComponent<LB_Attack>();
    }

    private void OnCollisionStay(Collision collision)//当たり判定処理
    {
        if (collision.gameObject.name == "Rulaby"|| collision.gameObject.name == "LastBoss(Clone)")        //もし当たったモノにGroundタグが付いていた場合
        {
            Attack.AnimFlg = false;
            Attack.OnlryFlg = true;
            Attack.WarpNum++;
            Attack.OneTimeFlg = true;
            Destroy(WarpBullet);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
            flag = true;

        }
    }
}
