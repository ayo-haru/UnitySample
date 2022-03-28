//=============================================================================
//
// ゲームオーバー演出
//
// 作成日:2022/03/16
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/16 作成
// 2022/03/20 演出時はサブカメラに切り替えるようにした
// 2022/03/24 CameraSwitchくらすを使ったカメラ切り替えに変更
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //ゲームオーバーで使うオブジェクト
    //画像
    public GameObject Image;
    //メインカメラ
    //public GameObject mainCam;
    //サブカメラ
    //public GameObject subCam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //f4キーが押されたら表示終了
        if (Input.GetKey(KeyCode.F4))
        {
            GameOverHide();
        }
    }
    public void GameOverShow()
    {
        //画像表示
        Image.GetComponent<ImageShow>().Show();
        //カメラ切り替え　メインカメラ→サブカメラ　補間あり
        //CameraSwitch.StartSwitching(mainCam, subCam, true);
        ////メインカメラオフ
        //mainCam.SetActive(false);
        ////サブカメラオン
        //subCam.SetActive(true);
    }
    public void GameOverHide()
    {
        //画像消去
        Image.GetComponent<ImageShow>().Hide();
        //カメラ切り替え サブカメラ→メインカメラ　補間なし
        //CameraSwitch.StartSwitching(subCam, mainCam, false);
        ////サブカメラオフ
        //subCam.SetActive(false);
        ////メインカメラオン
        //mainCam.SetActive(true);
    }
}
