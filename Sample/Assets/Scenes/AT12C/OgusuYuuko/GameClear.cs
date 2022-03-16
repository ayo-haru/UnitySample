//=============================================================================
//
// ゲームクリア演出
//
// 作成日:2022/03/15
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/15 作成
// 2022/03/16 画像表示の機能をImageShow.csに移動
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    //ゲームクリアで使うオブジェクト
    //クリア画像
    GameObject Image;

    // Start is called before the first frame update
    void Start()
    {
        Image = GameObject.Find("GameClearImage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameClearShow()
    {
        //画像表示
        Image.SendMessage("Show");
    }
}
