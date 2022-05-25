//=============================================================================
//
// 星アイテム取得時のエフェクト処理
//
// 作成日:2022/05/25
// 作成者:小楠裕子
//
//  pieceManagerにいれる
//
// <開発履歴>
// 2022/05/25    作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStarItemEffect : MonoBehaviour
{
    //エフェクト
    public GameObject effect;
    //生成したエフェクト格納用
    private GameObject instanceEffect;
    //エフェクトのテクスチャアニメーションコンポーネント
    private TextureAnimation effectAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //バグ出てるからコメントアウト
       // StartEffect(GetTotalItem());
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void StartEffect(int _totalItem)
    {
        //バグ出てるからコメントアウト
        ////エフェクトが無かったら生成
        //if (_totalItem == 2 || _totalItem == 4 || _totalItem == 7)
        //{
        //    //RectTransform rt = piece[PieceGrade - 1].GetComponent<RectTransform>();
        //    instanceEffect = Instantiate(effect, /*rt.position*/new Vector3(100.0f,100.0f,0.0f), Quaternion.identity);
        //    effectAnimation = instanceEffect.transform.GetChild(0).gameObject.GetComponent<TextureAnimation>();
        //}

        //if (_totalItem == 2 || _totalItem == 5 || _totalItem == 9)
        //{

        //    effectAnimation.finishFrame = 7;
        //}

        //if (_totalItem == 4)
        //{
        //    effectAnimation.finishFrame = 4;
        //}

        //if (_totalItem == 7)
        //{
        //    effectAnimation.finishFrame = 2;
        //}

        //if (_totalItem == 8)
        //{
        //    effectAnimation.finishFrame = 5;
        //}
    }

    public void effectFinish()
    {
        //バグ出てるからコメントアウト
        //effectAnimation.finishFrame = 14;   //テクスチャの最終枠設定
        //effectAnimation.loop = false;       //ループ解除
    }
}
