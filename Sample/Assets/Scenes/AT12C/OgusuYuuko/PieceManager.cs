//=============================================================================
//
// 回復のかけら
//
// 作成日:2022/04/06
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/06 作成
// 2022/04/19 アイテム取得時の処理追加
// 2022/04/20 hpの制御をHPManagerに移動
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour
{
    //子オブジェクト
    public GameObject[] piece;
    //現在のかけらの数
    private int nPiece;
    //かけら所持枠
    private int PieceGrade;
    //所持枠最大値
    private int MaxPieceGrade;
    //一回だけ実行する用のフラグ
    private bool onceFlag;
    //エフェクト
    //public GameObject effect;
    //ランタン画像
    //public GameObject PieceImage;
    //UVScrollコンポーネント
    //private UVScroll pieceUV;
    //回復のストックマネージャー
   // private StockManager stockManager;
    // Start is called before the first frame update
    void Start()
    {
        onceFlag = true;

        //コンポーネント取得
        //pieceUV = PieceImage.GetComponent<UVScroll>();

       // stockManager = GameObject.Find("StockHPManager").GetComponent<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onceFlag)
        {
            return;
        }
        // 所持枠最大値設定
        MaxPieceGrade = piece.Length;
        //ゲームデータクラスから回復のかけらの数取得して入れる
        nPiece = GameData.CurrentPiece;
        //ゲームデータクラスから取得していれる
        PieceGrade = GameData.CurrentPieceGrade;

        //現在のかけらの数に応じてＵＶ座標決定
        //pieceUV.SetFrame(nPiece);
        //PieceGradeの数だけランタンを出す
        for(int i = 0; i < PieceGrade; ++i)
        {
            piece[i].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
        }
        //nPieceの数だけランタンを光らせる
        for(int i = 0; i < nPiece; ++i)
        {
            piece[i].GetComponent<UVScroll>().SetFrame(1);
        }

        //for (int i = 0; i < nPiece; ++i)
        //{
        //    //かけらの所持数分黄色にして表示
        //    piece[i].GetComponent<ImageShow>().Show();
        //}

        //for (int i = nPiece; i < PieceGrade; ++i)
        //{
        //    //かけらを黒色にして表示
        //    piece[i].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
        //    piece[i].GetComponent<ImageShow>().Show();
        //}

        onceFlag = false;
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
            //所持枠より多かったらリターン
            if (nPiece >= PieceGrade)
            {
                return;
            }

            //かけら増やす
            ++nPiece;
        //pieceUV.SetFrame(nPiece);
        Debug.Log("nPiece" + nPiece);
        //ランタンを光らせる
        piece[nPiece - 1].GetComponent<UVScroll>().SetFrame(1);

        //表示
        //piece[nPiece - 1].GetComponent<ImageShow>().SetColor(1.0f,1.0f,1.0f);
        //    //エフェクト発生
        //    Instantiate(effect, piece[nPiece - 1].GetComponent<RectTransform>().position, Quaternion.identity);
            //ゲームデータ更新
            ++GameData.CurrentPiece;
            //保存
            //SaveManager.saveCurrentPiece(GameData.CurrentPiece);

    }

    public bool DelPiece()
    {
        //かけらが無かったら
        if(nPiece <= 0)
        {
            //HP減らす
            //--GameData.CurrentHP;
            return false;
        }
        else
        {
            //かけら減らす
            --nPiece;
            //pieceUV.SetFrame(nPiece);
            //ランタンの光を消す
            piece[nPiece].GetComponent<UVScroll>().SetFrame(0);

            ////エフェクト発生
            //GameObject Effect = Instantiate(effect, piece[nPiece].GetComponent<RectTransform>().position, Quaternion.identity);
            ////エフェクトの色を黒にする
            //Effect.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            ////かけらの色を黒にする
            //piece[nPiece].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            //ゲームデータ更新
            --GameData.CurrentPiece;
            //保存
            //SaveManager.saveCurrentPiece(GameData.CurrentPiece);

            return true;
        }
    }

    public void GetItem()
    {
        //既に上限値がマックスだったらリターン
        if(PieceGrade >= MaxPieceGrade)
        {
            return;
        }

        //かけら枠増やせるアイテム数
        int PieceUpGradeNum = 0;
        for(int i = 0; i <= PieceGrade; ++i)
        {
            PieceUpGradeNum += i;
        }
        //累計アイテム数
        int TotalItem = 0;
        for(int i = 0; i < 10; ++i)
        {
         for(int j = 0; j < 10; ++j)
            {
                if (GameData.isStarGet[j, i])
                {
                    ++TotalItem;
                }
            }   
        }
        Debug.Log(TotalItem);
        if(TotalItem >= PieceUpGradeNum + PieceGrade)   //今ゲットしたアイテムの情報がまだ配列に入ってないためイコール付けてる
        {
            //かけら所持枠を増やす
            ++PieceGrade;
            //ランタンを一つ増やす
            piece[PieceGrade - 1].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
            //ゲームデータ更新
            ++GameData.CurrentPieceGrade;
            //保存
            //SaveManager.savePieceGrade(GameData.CurrentPieceGrade);
            ////表示
            //piece[PieceGrade - 1].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            //piece[PieceGrade - 1].GetComponent<ImageShow>().Show();
            ////エフェクト発生
            //Instantiate(effect, piece[PieceGrade - 1].GetComponent<RectTransform>().position, Quaternion.identity);
        }
    }

    public void Vibration()
    {
        //PieceImage.transform.parent.gameObject.GetComponent<pieceMove>().vibration();

        for (int i = 0; i < PieceGrade; ++i)
        {
            piece[i].transform.parent.gameObject.GetComponent<pieceMove>().vibration();
        }
    }
}
