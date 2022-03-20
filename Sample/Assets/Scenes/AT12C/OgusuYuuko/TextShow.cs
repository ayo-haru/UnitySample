//=============================================================================
//
// テキストを表示
//
//フェードみたいにだんだん表示させれます。
//パッと一瞬で表示させたいときはalphaSpeedに１を設定してください
//
// 作成日:2022/03/20
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/20 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    //使用フラグ
    bool useFlag;
    //テキスト
    Text text;
    //透明度
    float alpha;
    //色
    public float red = 0.0f;
    public float green = 0.0f;
    public float blue = 0.0f;
    //透明度更新速度
    public float alphaSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        useFlag = false;

        text = GetComponent<Text>();
        alpha = 0.0f;
        //透明に設定
        text.color = new Color(red,green,blue, alpha);
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
        text.color = new Color(red, green, blue, alpha);
    }
    public void Show()
    {
        useFlag = true;
    }

    public void Hide()
    {
        //表示途中だったらリターン
        if (alpha < 1.0f)
        {
            return;
        }
        useFlag = false;
        alpha = 0.0f;
        //透明に設定
        text.color = new Color(red, green, blue, alpha);
    }
}
