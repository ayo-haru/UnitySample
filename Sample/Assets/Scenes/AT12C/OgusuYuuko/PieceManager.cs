//=============================================================================
//
// 回復のかけら
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

public class PieceManager : MonoBehaviour
{
    //子オブジェクト
    public GameObject[] piece;
    //現在のかけらの数
    private int nPiece;
    //回復のストックマネージャー
   // private StockManager stockManager;
    // Start is called before the first frame update
    void Start()
    {
        //とりあえず０にしてるけど、ゲームデータクラスから回復のかけらの数取得して入れる
        nPiece = GameData.CurrentPiece;
        for(int i = 0; i < nPiece; ++i)
        {
            //かけらの所持数分黄色にして表示
            piece[i].GetComponent<ImageShow>().Show();
        }
        for(int i = nPiece; i < piece.Length; ++i)
        {
            //かけらを黒色にして表示
            piece[i].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            piece[i].GetComponent<ImageShow>().Show();
        }

       // stockManager = GameObject.Find("StockHPManager").GetComponent<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //かけらが５個ある時
        //if(nPiece == piece.Length)
        //{
        //    if (stockManager.IsAddStock())
        //    {
        //        //ストック増やせるなら
        //        //ストック増やす
        //        stockManager.AddStock();
        //        //かけら消す
        //        nPiece = 0;
        //        for (int i = 0; i < piece.Length; ++i)
        //        {
        //            piece[i].GetComponent<ImageShow>().Hide();
        //        }
        //        //ゲームデータ更新
        //        //セーブデータ更新
        //    }

        //    //ストック増やせないならそのまま
        //}
    }

    public void GetPiece()
    {
        //かけらの数更新
        //++nPiece;
        ////かけらが5個集まったら回復のストックを増やす
        //if(nPiece >= piece.Length)
        //{
        //    if (stockManager.IsAddStock())
        //    {
        //        //回復のストック増やす
        //        //増やせたら
        //        stockManager.AddStock();
        //        //かけら消す
        //        nPiece = 0;
        //        for (int i = 0; i < piece.Length; ++i)
        //        {
        //            piece[i].GetComponent<ImageShow>().Hide();
        //        }
        //    }else
        //    {
        //        //増やせなかったら
        //        nPiece = piece.Length;
        //        piece[nPiece - 1].GetComponent<ImageShow>().Show();
        //    }
        //}
        //else
        //{
        //    piece[nPiece - 1].GetComponent<ImageShow>().Show();
        //}

        //HPがＭＡＸだったらかけらを増やす
        if(GameData.CurrentHP >= 5)
        {
            if (nPiece >= piece.Length)
            {
                return;
            }
            //かけら増やす
            ++nPiece;
            piece[nPiece - 1].GetComponent<ImageShow>().SetColor(1.0f,1.0f,1.0f);
            //ゲームデータ更新
            ++GameData.CurrentPiece;
        }
        else
        {
            //HPが減っていたらＨＰを1回復
            ++GameData.CurrentHP;
        } 
    }

    public void DelPiece()
    {
        //かけらが無かったら
        if(nPiece <= 0)
        {
            //HP減らす
            --GameData.CurrentHP;
        }
        else
        {
            //かけら減らす
            --nPiece;
            //かけらの色を黒にする
            piece[nPiece].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            //ゲームデータ更新
            --GameData.CurrentPiece;
        }
    }
}
