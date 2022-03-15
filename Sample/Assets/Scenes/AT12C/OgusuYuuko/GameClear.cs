//=============================================================================
//
// ゲームクリア演出
//
// 作成日:2022/03/15
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/15 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    //ゲームクリア画像
    Image clearImage;

    // Start is called before the first frame update
    void Start()
    {
        clearImage = GetComponent<Image>();
        //透明に設定
        clearImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        //画像表示
        clearImage.color = new Color(1.0f, 1.0f, 1.0f, 255.0f);
    }
}
