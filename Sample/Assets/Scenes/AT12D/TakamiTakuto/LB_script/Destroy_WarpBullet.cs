using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_WarpBullet : MonoBehaviour
{
    public GameObject WarpBullet;
    public static bool flag = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)//当たり判定処理
    {
        if (collision.gameObject.tag == "Ground")        //もし当たったモノにGroundタグが付いていた場合
        {
            Destroy(WarpBullet);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
            flag = true;

        }
        if (collision.gameObject.tag == "Player")        //もし当たったモノにGroundタグが付いていた場合
        {
            Destroy(WarpBullet);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
            flag = true;
        }
    }
}
