//=============================================================================
//
// ゲームオーバー演出
//
// 作成日:2022/03/16
// 作成者:小楠裕子
//
// canvasにこのスクリプト入れる
//
// <開発履歴>
// 2022/03/16 作成
// 2022/03/20 演出時はサブカメラに切り替えるようにした
// 2022/03/24 CameraSwitchくらすを使ったカメラ切り替えに変更
// 2022/03/28 prefabから読み込んで表示するようにした
// 2022/03/30 リトライとタイトルに戻るを追加
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private enum SELECT {RETRY,BACKTITLE};
    //ゲームオーバーで使うオブジェクト
    //ゲームオーバー画像
    private GameObject GameOverImage;
    //リトライ画像
    private GameObject RetryImage;
    //タイトルに戻る画像
    private GameObject BackTitleImage;
    //プれハブ
    private GameObject prefab;
    //キャンバス
    Canvas canvas;
    //選択
    SELECT select;
    //使用フラグ
    public bool GameOverFlag;
    //ImageShowコンポーネント
    ImageShow imageShow_GameOver;
    ImageShow imageShow_Retry;
    ImageShow imageShow_BackTitle;
    //メインカメラ
    //public GameObject mainCam;
    //サブカメラ
    //public GameObject subCam;

    private bool isCalledOnce = false;                              // 開始演出で使用。一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Start()
    {
        //キャンバス取得
        canvas = GetComponent<Canvas>();
        //ゲームオーバー画像取得
        prefab = (GameObject)Resources.Load("GameOverImage");
        GameOverImage = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        //リトライ画像取得
        prefab = (GameObject)Resources.Load("RetryImage");
        RetryImage = Instantiate(prefab, new Vector3(-150.0f, -200.0f, 0.0f), Quaternion.identity);
        //タイトルに戻る画像取得
        prefab = (GameObject)Resources.Load("BackTitleImage");
        BackTitleImage = Instantiate(prefab, new Vector3(150.0f, -200.0f, 0.0f), Quaternion.identity);

        //canvasの子に設定
        GameOverImage.transform.SetParent(this.canvas.transform, false);
        RetryImage.transform.SetParent(this.canvas.transform, false);
        BackTitleImage.transform.SetParent(this.canvas.transform, false);

        //ImageShowコンポーネント取得
        imageShow_GameOver = GameOverImage.GetComponent<ImageShow>();
        imageShow_Retry = RetryImage.GetComponent<ImageShow>();
        imageShow_BackTitle = BackTitleImage.GetComponent<ImageShow>();

        //使用フラグ設定
        GameOverFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //未使用だったらリターン
        if (!GameOverFlag)
        {
            return;
        }

        // ゲームオーバーになったら一回だけ処理を行う
        if (!isCalledOnce)
        {
            SoundManager.Play(SoundData.eSE.SE_GAMEOVER, SoundData.IndelibleAudioList);
            isCalledOnce = true;
            Pause.PauseStart();
        }
        
        //f4キーが押されたら表示終了
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameOverHide();
        }

        //f5が押されたらリトライ（スティック左に倒したら）
        if (Input.GetKeyDown(KeyCode.F5))
        {
            select = SELECT.RETRY;
            //リトライ画像　黄色
            imageShow_Retry.SetColor(1.0f,1.0f,0.0f);
            //タイトルに戻る画像　白
            imageShow_BackTitle.SetColor(1.0f, 1.0f, 1.0f);
            Debug.Log("リトライ選択中");
        }
        //f6が押されたらタイトルに戻る（スティック右に倒したら）
        if (Input.GetKeyDown(KeyCode.F6))
        {
            select = SELECT.BACKTITLE;
            //タイトルに戻る画像　黄色
            imageShow_BackTitle.SetColor(1.0f, 1.0f, 0.0f);
            // リトライ画像　白
            imageShow_Retry.SetColor(1.0f, 1.0f, 1.0f);
            Debug.Log("タイトルに戻る選択中");
        }
        //f7が押されたら決定
        if (Input.GetKeyDown(KeyCode.F7))
        {
            switch (select)
            {
                case SELECT.RETRY:
                    //ゲームに戻る
                    //シーン遷移
                    GameData.Init();
                    Debug.Log("リトライを押した");
                    break;
                case SELECT.BACKTITLE:
                    //タイトルに戻る
                    //シーン遷
                    GameData.NextMapNumber = (int)GameData.eSceneState.TITLE_SCENE;
                    SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);
                    Debug.Log("タイトルに戻るを押した");
                    break;
            }
            //ゲームオーバー表示終了
            GameOverHide();
        }

    }
    public void GameOverShow()
    {
        //色初期化
        imageShow_Retry.SetColor(1.0f, 1.0f, 0.0f);
        imageShow_BackTitle.SetColor(1.0f, 1.0f, 1.0f);
        //select初期化
        select = SELECT.RETRY;
        //画像表示
        imageShow_GameOver.Show();
        imageShow_Retry.Show();
        imageShow_BackTitle.Show();
        //カメラ切り替え　メインカメラ→サブカメラ　補間あり
        //CameraSwitch.StartSwitching(mainCam, subCam, true);
        ////メインカメラオフ
        //mainCam.SetActive(false);
        ////サブカメラオン
        //subCam.SetActive(true);
        //使用フラグ立てる
        GameOverFlag = true;
    }
    public void GameOverHide()
    {
        //画像消去
        imageShow_GameOver.Hide();
        imageShow_Retry.Hide();
        imageShow_BackTitle.Hide();
        //カメラ切り替え サブカメラ→メインカメラ　補間なし
        //CameraSwitch.StartSwitching(subCam, mainCam, false);
        ////サブカメラオフ
        //subCam.SetActive(false);
        ////メインカメラオン
        //mainCam.SetActive(true);
        //使用フラグ下す
        GameOverFlag = false;

    }
}
