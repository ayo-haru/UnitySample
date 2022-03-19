//=============================================================================
//
// 画像を表示
//
//フェードみたいにだんだん表示させれます。
//パッと一瞬で表示させたいときはalphaSpeedに１を設定してください
//
// 作成日:2022/03/16
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/16 作成
// 2022/03/19 表示を終了する関数を追加
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour
{
    //使用フラグ
    bool useFlag;
    //画像
    Image Image;
    //画像透明度
    float alpha;
    //透明度更新速度
    public float alphaSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        useFlag = false;

        Image = GetComponent<Image>();
        alpha = 0.0f;
        //透明に設定
        Image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //使ってなかったらリターン
        if (!useFlag)
        {
            return;
        }

        //透明度更新
        alpha += alphaSpeed;
        if (alpha > 1.0f)
        {
            alpha = 1.0f;
        }
        //色設定
        Image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
    public void Show()
    {
        useFlag = true;
    }
    
    public void Hide()
    {
        //表示途中だったらリターン
        if(alpha < 1.0f)
        {
            return;
        }
        useFlag = false;
        alpha = 0.0f;
        //透明に設定
        Image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
