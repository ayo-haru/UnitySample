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
// 2022/03/23 消す秒数を指定できるようにした
// 2022/03/30 色を変更できるようにした
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour
{
    enum ImageMode {NONE,SHOW,HIDE,TIMER };   //更新なし、表示中、隠し中,タイマー更新
    //使用しているモード
    ImageMode mode;
    //画像
    Image Image;
    //画像透明度
    float alpha;
    //画像色
    float red;
    float green;
    float blue;
    //透明度更新速度
    public float ShowAlphaSpeed = 0.005f;
    public float HideAlphaSpeed = 0.005f;
    //表示時間
    int Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        mode = ImageMode.NONE;
        Timer = 0;
        //useFlag = false;

        Image = GetComponent<Image>();
        red = Image.color.r;
        green = Image.color.g;
        blue = Image.color.b;
        alpha = 0.0f;
        //透明に設定
        Image.color = new Color(red, green, blue, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //使ってなかったらリターン
        if (mode == ImageMode.NONE)
        {
            return;
        }

        
        if (mode == ImageMode.SHOW)
        {
            //透明度更新
            alpha += ShowAlphaSpeed;
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                if (Timer > 0)
                {
                    mode = ImageMode.TIMER;
                }
                else
                {
                    mode = ImageMode.NONE;
                }
                
            }
            //色設定
            Image.color = new Color(red, green, blue, alpha);
            return;
        }

        if (mode == ImageMode.HIDE)
        {
            //透明度更新
            alpha -= HideAlphaSpeed;
            if (alpha <= 0.0f)
            {
                alpha = 0.0f;
                mode = ImageMode.NONE;
            }
            //色設定
            Image.color = new Color(red, green, blue, alpha);
            return;
        }

        if(mode == ImageMode.TIMER)
        {
            //タイマー更新
            --Timer;
            if (Timer <= 0)
            {
                mode = ImageMode.HIDE;
                Timer = 0;
            }
        }
        
       


    }
    public void Show()
    {
        mode = ImageMode.SHOW;
    }

    public void Show(int second)
    {
        if (mode != ImageMode.NONE)
        {
            return;
        }
        mode = ImageMode.SHOW;
        Timer = second;
    }

    public void Hide()
    {
        //表示途中だったらリターン
        if(alpha < 1.0f)
        {
            return;
        }
        mode = ImageMode.NONE;
        alpha = 0.0f;
        //透明に設定
        Image.color = new Color(red, green, blue, alpha);
    }

    public void SetColor(float r ,float g,float b)
    {
        red = r;
        green = g;
        blue = b;
        //色設定
        Image.color = new Color(red, green, blue, alpha);
    }
}
