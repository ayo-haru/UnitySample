//=============================================================================
//
// SE設定
//
// 作成日:2022/04/26
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/26    作成

//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSE : MonoBehaviour
{
    //スライダー
    private Slider SeSlider;
    //選択用フラグ
    public bool selectFlag = false;
    //移動量
    public float moveSpeed = 0.005f;
    // Start is called before the first frame update
    void Awake()
    {
        //コンポーネント取得
        SeSlider = gameObject.GetComponent<Slider>();

        //サウンドマネージャーからSEの音量を取得
        SeSlider.value = SoundManager.seVolume;

    }

    // Update is called once per frame
    void Update()
    {
        //選択されてなかったらリターン
        if (!selectFlag)
        {
            return;
        }

        //右矢印キーで音量プラス
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SeSlider.value += moveSpeed;
            //音量設定
            SoundManager.seVolume = SeSlider.value;
        }
        //左矢印キーで音量－
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SeSlider.value -= moveSpeed;
            //音量設定
            SoundManager.seVolume = SeSlider.value;
        }


    }
}
