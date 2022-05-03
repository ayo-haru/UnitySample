using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //----- 変数初期化 -----
    //--- フェードの変数
    float fadeOutSpeed = 0.4f;        //透明度が変わるスピードを管理
    float fadeInSpeed = 0.4f;        //透明度が変わるスピードを管理
    float fadeDeltaTime = 0;                //フェードに使った時間

    [System.NonSerialized]
    public static bool onceFadeOut;
    [System.NonSerialized]
    public static bool onceFadeIn;
    //float red, green, blue, alfa;   //パネルの色、不透明度を管理

    Image fadeImage;                //透明度を変更するパネルのイメージ

    void Start() {
        onceFadeOut = true;
        onceFadeIn = true;
        fadeImage = GetComponent<Image>();  // フェードの色関連
        //red = fadeImage.color.r;
        //green = fadeImage.color.g;
        //blue = fadeImage.color.b;
        //alfa = fadeImage.color.a;

        //if (GameData.isFadeIn)
        //{
        //    alfa = 1.0f;
        //}
        //if (GameData.isFadeOut)
        //{
        //    alfa = 0.0f;
        //}
    }

    void Update()
    {
        //if (GameData.isFadeIn)
        //{ 
        //    FadeIn();
        //}

        //if (GameData.isFadeOut && onceFadeOut)
        //{
        //    FadeOut();
        //}
    }

    //void StartFadeIn() {
    //    alfa -= fadeInSpeed;                //a)不透明度を徐々に下げる
    //    SetAlpha();                      //b)変更した不透明度パネルに反映する
    //    if (alfa <= 0)
    //    {                    //c)完全に透明になったら処理を抜ける
    //        GameData.isFadeIn = false;
    //        fadeImage.enabled = false;    //d)パネルの表示をオフにする
    //        Pause.isPause = false;  // フェード中は恐らくポーズ中だからポーズをやめる
    //    }
    //}

    //void StartFadeOut() {
    //    fadeImage.enabled = true;  // a)パネルの表示をオンにする
    //    alfa += fadeOutSpeed;         // b)不透明度を徐々にあげる
    //    SetAlpha();               // c)変更した透明度をパネルに反映する
    //    if (alfa >= 1)
    //    {             // d)完全に不透明になったら処理を抜ける
    //        GameData.isFadeOut = false;
    //        GameData.isFadeIn = true;
    //        Pause.isPause = false;  // フェード中は恐らくポーズ中だからポーズをやめる
    //        Debug.Log("フェード終わりの解除");
    //    }
    //}

    //void SetAlpha() {
    //    fadeImage.color = new Color(red, green, blue, alfa);
    //}

    private IEnumerator FadeInCoroutine()
    {
        Debug.Log("フェード院");

        //float alpha = 1;                            //色の不透明度
        //Color color = new Color(0, 0, 0, alpha);    //Imageの色変更に使う
        this.fadeDeltaTime = 0;                     //初期化
        //this.fadeImage.color = color;                   //色の初期化(黒)
        while (this.fadeDeltaTime <= this.fadeInSpeed)
        {
            float fadeAlpha = Mathf.Lerp(1f, 0f, fadeDeltaTime / fadeInSpeed);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            fadeDeltaTime += Time.deltaTime;
            //yield return null;                      //次フレームで再開
            //this.fadeInSpeed += Time.deltaTime;       //時間の加算
            //alpha = Mathf.Lerp(0f, 1f, fadeDeltaTime / fadeInSpeed);   //透明度の決定
            if (fadeImage.color.a < 0)
            {
                Pause.isPause = false;
                GameData.isFadeIn = false;
            }
            yield return null;
            //color.a = alpha;                        //色の透明度の決定
            //this.fadeImage.color = color;               //色の代入
        }
        
    }

    private IEnumerator FadeOutCoroutine()
    {
        Debug.Log("フェードアウト");
        //float alpha = 0;                            //色の不透明度
        //Color color = new Color(0, 0, 0, alpha);    //Imageの色変更に使う
        this.fadeDeltaTime = 0;                     //初期化
        //this.fadeImage.color = color;                   //色の初期化
        while (this.fadeDeltaTime <= this.fadeOutSpeed)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, fadeDeltaTime / fadeOutSpeed);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            fadeDeltaTime += Time.deltaTime;
            //if (fadeImage.color.a >= 1)
            if (fadeAlpha >= 1)
            {
                //Pause.isPause = false;
                GameData.isFadeOut = false;
                Debug.Log("フェードアウト完了のポーズ解除");
                //GameData.isFadeIn = true;
            }
            yield return null; 
            //color.a = alpha;                        //色の透明度の決定
            //this.fadeImage.color = color;               //色の代入
        }
    }

    //外部から呼び出される
    public void FadeIn()
    {
        IEnumerator coroutine = FadeInCoroutine();
        StartCoroutine(coroutine);
    }
    public void FadeOut()
    {
        IEnumerator coroutine = FadeOutCoroutine();
        StartCoroutine(coroutine);
    }
}
