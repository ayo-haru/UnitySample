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
    //ランタンを下すタイミング(アイテム数)を格納　
    private int[] rantanDown = { 1, 2, 4, 7 };
    //ランタンのα値格納
    private float[] rantanAlpha = { 1.0f,0.5f,1.0f,0.3f,0.6f,1.0f,0.2f,0.4f,0.6f,1.0f};
    // Start is called before the first frame update
    void Start()
    {
        onceFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onceFlag)
        {
            return;
        }
        onceFlag = false;

        // 所持枠最大値設定
        MaxPieceGrade = piece.Length;
        //ゲームデータクラスから回復のかけらの数取得して入れる
        nPiece = GameData.CurrentPiece;
        //ゲームデータクラスから取得していれる
        PieceGrade = GameData.CurrentPieceGrade;
        //ランタンを下す
        lanthanumDown();
        //α値調整
        lanthanumAlpha();
        //nPieceの数だけランタンを光らせる
        for(int i = 0; i < nPiece; ++i)
        {
            piece[i].GetComponent<UVScroll>().SetFrame(1);
        }
    }

    public void GetPiece()
    {
        //所持枠より多かったらリターン
        if (nPiece >= PieceGrade)
        {
            return;
        }

        //かけら増やす
        ++nPiece;
        //ランタンを光らせる
        piece[nPiece - 1].GetComponent<UVScroll>().SetFrame(1);
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
            return false;
        }
        else
        {
            //かけら減らす
            --nPiece;
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
        int TotalItem = GetTotalItem();

        Debug.Log(TotalItem + "とーたるあいてむ");
        
        if(TotalItem > PieceUpGradeNum + PieceGrade) 
        {
            //かけら所持枠を増やす
            ++PieceGrade;
            //ランタンを一つ増やす
            //piece[PieceGrade - 1].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
            //エフェクトループ解除
            //if(TotalItem > 1)
            //{
            //    //バグ出てるからコメントアウト
            //    //effectManager.effectFinish();
            //}
            
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
        //ランタン下す
        lanthanumDown();
        //α値調整
        lanthanumAlpha();
    }

    public void lanthanumDown()
    {
        //現在のアイテム取得数
        int totalItem = GetTotalItem();

        for(int i = 0; i < rantanDown.Length; ++i)
        {
            if(totalItem >= rantanDown[i])
            {
                //ランタンを下す
                piece[i].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
                //透明度初期値設定
                piece[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Debug.Log("ランタン下した");
            }
        }

    }

    public void lanthanumAlpha()
    {
        // 現在のアイテム取得数
        int totalItem = GetTotalItem();
        //アイテム取得してなかったらリターン
        if(totalItem <= 0)
        {
            return;
        }
        //α値調整するオブジェクトのコンポーネント取得
        Image alphaSetImage;
        if (rantanAlpha[totalItem - 1] < 1.0f)
        {
            alphaSetImage = piece[PieceGrade].GetComponent<Image>();
        }
        else
        {
            alphaSetImage = piece[PieceGrade - 1].GetComponent<Image>();
        }
        
        //α値設定
        alphaSetImage.color = new Color(1.0f, 1.0f, 1.0f, rantanAlpha[totalItem - 1]);
    }

    public int GetTotalItem()
    {
        int _totalItem = 0;
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                if (GameData.isStarGet[j, i])
                {
                    ++_totalItem;
                }
            }
        }

        return _totalItem;
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
