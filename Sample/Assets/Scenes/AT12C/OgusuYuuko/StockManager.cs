//=============================================================================
//
// 回復のストック
//
// 作成日:2022/04/06
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/06 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    //子オブジェクト
    public GameObject[] stock;
    //現在のストックの数
    private int nStock;
    // Start is called before the first frame update
    void Start()
    {
        //とりあえず０にしてるけど、ゲームデータクラスから回復のストックの数取得して入れる
        nStock = 0;
        //ストックの所持数分表示する
        for(int i = 0; i < nStock; ++i)
        {
            stock[i].GetComponent<ImageShow>().Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ストックが無かったら何もしない
        if(nStock <= 0)
        {
            return;
        }

        //プレイヤーのHPが減っていたら
        if(GameData.CurrentHP < 6)
        {
            //ストック消費
            --nStock;
            //ストック消す
            stock[nStock].GetComponent<ImageShow>().Hide();
            //プレイヤーのHP増やす
            ++GameData.CurrentHP;
            
            //ゲームデータ更新
            //セーブデータ更新
        }
    }

    public void AddStock()
    {
        //ストックが最大数だったらリターン
        if(nStock >= stock.Length)
        {
            return;
        }

        ++nStock;
        stock[nStock - 1].GetComponent<ImageShow>().Show();
        //ゲームデータ更新
        //セーブデータ更新
    }

    public bool IsAddStock()
    {
        if(nStock >= stock.Length)
        {
            return false;
        }

        return true;
        
    }
}
