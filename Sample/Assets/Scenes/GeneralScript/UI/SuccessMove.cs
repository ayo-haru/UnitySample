using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessMove : MonoBehaviour {
    public enum eSccessState {  // 表示の変更をかけるステート
        NONE,       // 何もしない
        EXPANSION,  // 拡大
        UP,         // 上にひゅってする
        FIN         // 終了
    }

    private const float maxSizeRate = 1.2f;      // 拡大の最大値
    private const float minSizeRate = 0.5f;      // 拡大の最小値（この値から拡大を始める）
    private const float MaxWidth = 600.0f * maxSizeRate;
    private const float MaxHeight = 200.0f * maxSizeRate;
    private const float upforce = 10.0f;        // 上に飛ばす力
    private const float expantionSpeed = 3.0f;   // 拡大スピード

    //------ 変数定義 -----
    public eSccessState mode;       // 表示の変更のステート

    private float width;             // 足す大きさ
    private float height;             // 足す大きさ

    private RectTransform rectTransform;  // このUIのRectTransform

    private bool onceCoroutine = true;

    // Start is called before the first frame update
    void Start() {
        mode = eSccessState.NONE;   // 最初は何もしない
        width = 0.0f;     // 最初は少し小さい状態から始めていく
        height = 0.0f;     // 最初は少し小さい状態から始めていく
        rectTransform = this.GetComponent<RectTransform>(); // このUIのRectTransformを格納
        this.GetComponent<Image>().enabled = false; // 表示なし
    }

    // Update is called once per frame
    void Update() {
        if (mode == eSccessState.EXPANSION)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + width, rectTransform.sizeDelta.y + height);
            width += Time.deltaTime * expantionSpeed * 3;    //3:1の割合だから３かける
            height += Time.deltaTime * expantionSpeed;
            if (rectTransform.sizeDelta.x > MaxWidth)
            {
                mode = eSccessState.UP;
            }
        }
        if (mode == eSccessState.UP)
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x,rectTransform.localPosition.y+upforce,rectTransform.localPosition.z);
            if (rectTransform.localPosition.y > 50.0f && onceCoroutine)
            {
                StartCoroutine("WaitFin");
                onceCoroutine = false;
            }
        }
    }

    public void StartSccess() {
        mode = eSccessState.EXPANSION;
        rectTransform.sizeDelta = new Vector2(0, 0);
        width = 0.0f;     // 最初は少し小さい状態から始めていく
        height = 0.0f;     // 最初は少し小さい状態から始めていく
        rectTransform.localPosition = new Vector3(0.0f,0.0f,0.0f);
        this.GetComponent<Image>().enabled = true;
        onceCoroutine = true;
    }

    public void FinSuccess() {
        mode = eSccessState.NONE;
        this.GetComponent<Image>().enabled = false;
    }

    private IEnumerator WaitFin() {
        yield return new WaitForSeconds(1.5f);
        mode = eSccessState.FIN;

    }
}

