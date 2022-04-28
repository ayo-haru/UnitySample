using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //----- 変数初期化 -----
    //--- フェードの変数
    float fadeOutSpeed = 0.01f;        //透明度が変わるスピードを管理
    float fadeInSpeed = 0.01f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    Image fadeImage;                //透明度を変更するパネルのイメージ

    void Start() {
        fadeImage = GetComponent<Image>();  // フェードの色関連
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;

        if (GameData.isFadeIn)
        {
            alfa = 1.0f;
        }
        if (GameData.isFadeOut)
        {
            alfa = 0.0f;
        }
    }

    void Update() {
        if (GameData.isFadeIn)
        {
            StartFadeIn();
        }

        if (GameData.isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn() {
        alfa -= fadeInSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            GameData.isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
            Pause.isPause = false;  // フェード中は恐らくポーズ中だからポーズをやめる
        }
    }

    void StartFadeOut() {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeOutSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            GameData.isFadeOut = false;
            GameData.isFadeIn = true;
            Pause.isPause = false;  // フェード中は恐らくポーズ中だからポーズをやめる
            Debug.Log("フェード終わりの解除");
        }
    }

    void SetAlpha() {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
