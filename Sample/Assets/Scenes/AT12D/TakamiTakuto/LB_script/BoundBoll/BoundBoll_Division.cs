using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBoll_Division : MonoBehaviour
{
    //==============================================================================
    //---------------------処理内容：壁に当たったら弾を増殖させる----------------------
    //----------------------Prehub内のBoundBollに入っている--------------------------
    //==============================================================================
    public GameObject BoundBoll;                //ゲームオブジェクト：バウンドボール
    public int BoundBollCount;                  //バウンド弾が壁に当たった回数をカウント
    [SerializeField] public int Max_BoundBoll;  //バウンド弾の最大個数
    [SerializeField] public int Max_BoundCount; //バウンド弾の最大バウンド回数
    //------------------------------------------------------------------------------

    void Start()
    {
    }

    private void OnCollisionStay(Collision collision)//当たり判定処理
    {
        Debug.Log("衝突したオブジェクト：" + gameObject.name);              //デバックログ
        Debug.Log("衝突されたオブジェクト：" + collision.gameObject.name);  //デバックログ

        //--------------------------------------------------------------------------------------------------
        //もし当たったのがゲームオブジェクトの”ステージ”の場合
        //--------------------------------------------------------------------------------------------------
        if (collision.gameObject.tag == "Ground")
        {
            BoundBollCount++;                            //バウンドボールの跳ね返りカウントを１＋する
            Debug.Log("バウンドボールカウントが＋１された");//デバックログ
           

                if (Max_BoundBoll > BoundBollCount)
                {
                    GameObject instance = (GameObject)Instantiate(BoundBoll);  　 //BoundBollを生成
                    instance.transform.position = gameObject.transform.position;　//生成したBoundBollの位置を
                    GetComponent<Renderer>().material.color = Color.blue;      　 //テスト用いずれ消す
                    Debug.Log("弾を生成した");                                  　 //デバックログ
                }
            //BoundBollCountがMax_BoundCountよりも大きくなった場合
            if (BoundBollCount >= Max_BoundCount)
            {
                Destroy(BoundBoll);      //BoundBollを破壊
                Debug.Log("弾を破壊した");//デバックログを表示
            }
        }
        //-----------------------------------------------------------------------------------------------------
        
    }
}