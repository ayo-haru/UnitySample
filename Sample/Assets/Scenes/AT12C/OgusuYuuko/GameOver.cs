//=============================================================================
//
// ゲームオーバー演出
//
// 作成日:2022/03/16
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/16 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //ゲームオーバーで使うオブジェクト
    //画像
    GameObject Image;

    // Start is called before the first frame update
    void Start()
    {
        Image = GameObject.Find("GameOverImage");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOverShow()
    {
        //画像表示
        Image.SendMessage("Show");
    }
}
