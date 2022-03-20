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
// 2022/03/20 使用するオブジェクトをunity側で変えれるようにした
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    //ゲームクリアで使うオブジェクト
    //クリア画像
    public GameObject Image;
    //テキスト
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //f3キーが押されたら表示終了
        if (Input.GetKey(KeyCode.F3))
        {
            GameClearHide();
        }
    }

    public void GameClearShow()
    {
        //画像表示
        Image.GetComponent<ImageShow>().Show();
        //テキスト表示
        text.GetComponent<TextShow>().Show();
    }

    public void GameClearHide()
    {
        //画像消去
        Image.GetComponent<ImageShow>().Hide();
        //テキスト消去
        text.GetComponent<TextShow>().Hide();
    }
}
