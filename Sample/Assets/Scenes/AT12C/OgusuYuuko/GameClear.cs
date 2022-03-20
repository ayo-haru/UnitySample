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
// 2022/03/19 何かのキーを押したら終了する処理を追加
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
    //テキスト
    GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        Image = GameObject.Find("GameClearImage");
        text = GameObject.Find("GameClearText");
    }

    // Update is called once per frame
    void Update()
    {
        //何かしらのキーが押されたら表示終了
        if (Input.anyKey)
        {
            GameClearHide();
        }
    }

    public void GameClearShow()
    {
        //画像表示
        Image.SendMessage("Show");
        //テキスト表示
        text.SendMessage("Show");
    }

    public void GameClearHide()
    {
        //画像消去
        Image.SendMessage("Hide");
        //テキスト消去
        text.SendMessage("Hide");
    }
}
