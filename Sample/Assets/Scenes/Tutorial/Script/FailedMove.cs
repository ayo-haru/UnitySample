using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailedMove : MonoBehaviour
{
    public enum eFailedState {  // 表示の変更をかけるステート
        NONE,       // 何もしない
        EXPANSION,  // 拡大
        TILT,       // 傾ける
        FIN         // 終了
    }

    private const float maxSizeRate = 1.2f;      // 拡大の最大値
    private const float minSizeRate = 0.5f;      // 拡大の最小値（この値から拡大を始める）
    private const float MaxWidth = 600.0f * maxSizeRate;
    private const float MaxHeight = 200.0f * maxSizeRate;
    private const float maxAngle = -45.0f;        // 傾ける角度の最大
    private const float angle = 1.0f;        // 傾ける角度
    //private const float expantionSpeed = 1.5f;   // 拡大スピード
    private const float expantionSpeed = 3.0f;   // 拡大スピード
    private const float tiltSpeed = 0.5f;        // 傾けるスピード

    //------ 変数定義 -----
    public eFailedState mode;       // 表示の変更のステート

    private float width;             // 足す大きさ
    private float height;             // 足す大きさ

    private RectTransform rectTransform;  // このUIのRectTransform

    private bool onceCoroutine = true;

    // Start is called before the first frame update
    void Start()
    {
        mode = eFailedState.NONE;   // 最初は何もしない
        width = 0.0f;     // 最初は少し小さい状態から始めていく
        height = 0.0f;     // 最初は少し小さい状態から始めていく
        rectTransform = this.GetComponent<RectTransform>(); // このUIのRectTransformを格納
        this.GetComponent<Image>().enabled = false; // 表示なし
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == eFailedState.EXPANSION)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + width, rectTransform.sizeDelta.y + height);
            width += Time.deltaTime * expantionSpeed *3;    //3:1の割合だから３かける
            height += Time.deltaTime * expantionSpeed;
            if (rectTransform.sizeDelta.x > MaxWidth)
            {
                mode = eFailedState.TILT;
                //mode = eFailedState.FIN;
            }
        }
        if (mode == eFailedState.TILT)
        {
            if (onceCoroutine)
            {
                StartCoroutine("DelayTilt");
                StartCoroutine("WaitFin");
                onceCoroutine = false;
            }
        }
    }

    public void StartFailed() {
        mode = eFailedState.EXPANSION;
        rectTransform.sizeDelta = new Vector2(0, 0);
        //rectTransform.sizeDelta = new Vector2(600, 200);
        width = 0.0f;     // 最初は少し小さい状態から始めていく
        height = 0.0f;     // 最初は少し小さい状態から始めていく
        rectTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        this.GetComponent<Image>().enabled = true;
        onceCoroutine = true;
    }

    public  void FinFailed() {
        mode = eFailedState.NONE;
        this.GetComponent<Image>().enabled = false;
    }


    private IEnumerator DelayTilt() {
        yield return new WaitForSeconds(0.5f);
        rectTransform.eulerAngles = new Vector3(0.0f, 0.0f, -10.0f);
    }

    private IEnumerator WaitFin() {
        yield return new WaitForSeconds(1.5f);
        mode = eFailedState.FIN;

    }
}
