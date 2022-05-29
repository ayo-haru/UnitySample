//==========================================================
//      エンディング
//      作成日　2022/05/29
//      作成者　小楠裕子
//
//      このスクリプトが入ったプレハブ(EndingManager)をラスボスシーンのCanvas2に入れる
//      必ずプレイヤーのHPの手前に置く
//      
//      <開発履歴>
//      2022/05/29  作成      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public bool startFlag;              //エンディング開始フラグ
    private GameObject BackGroundImage;  //背景
    private GameObject TitleLogoImage;   //タイトルロゴ
    private GameObject FinImage;          //Finの文字
    private GameObject BackTitleImage;   //タイトルに戻る
    private GameObject AButtonImage;     //Aボタン
    private GameObject FadeImage;        //フェード用画像

    public int StopTime = 180;                //停止時間

    private int step;

    // InputActionのUIを扱う
    private Game_pad UIActionAssets;
    private bool isDecision;

    void Awake()
    {
        // InputActionインスタンスを生成
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void Start()
    {
        startFlag = false;
        step = 0;
        isDecision = false;

        BackGroundImage = GameObject.Find("EndingBGImage");
        TitleLogoImage = GameObject.Find("TitleLogo");
        FinImage = GameObject.Find("FinImage");
        AButtonImage = GameObject.Find("AbuttonImage");
        BackTitleImage = GameObject.Find("BackTitleImage");
        FadeImage = GameObject.Find("FadeImage");

        //でバック用
        if(GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            GameData.CurrentMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ラスボス倒されたフラグ立ってたらスタートする
        //if (GameData.isAlivelastBoss)
        //{
        //    startFlag = true;
        //}
        //フラグ立ってなかったら何もしない
        if (!startFlag)
        {
            return;
        }

        switch (step)
        {
            case 0:
                //演出が始まったら背景とタイトルロゴを表示
                BackGroundImage.GetComponent<ImageShow>().Show();
                TitleLogoImage.GetComponent<ImageShow>().Show();
                ++step;
                break;
            case 1:
                //背景とタイトルロゴが完全に表示されたらfinを表示
                if (BackGroundImage.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE && TitleLogoImage.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
                {
                    FinImage.GetComponent<ImageShow>().Show();
                    ++step;
                }
                break;
            case 2:
                //finが表示されたらStopTime待機
                --StopTime;
                if(StopTime <= 0)
                {
                    ++step;
                }
                break;
            case 3:
                //待機終了後タイトルに戻るとAボタン画像を表示
                BackTitleImage.GetComponent<ImageShow>().Show();
                AButtonImage.GetComponent<ImageShow>().Show();
                ++step;
                break;
            case 4:
                //タイトルに戻るが表示されてからAボタンが押されたらタイトルに戻る
                if (isDecision)
                {
                    isDecision = false;
                    // 決定音
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.GameAudioList);
                    ++step;
                }
                break;
            case 5:
                //フェード表示
                FadeImage.GetComponent<ImageShow>().Show();
                ++step;
                break;
            case 6:
                //完全にフェードアウトしたらシーン切り替え
                if(FadeImage.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
                {
                    // シーン関連
                    GameData.OldMapNumber = GameData.CurrentMapNumber;
                    string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                    SceneManager.LoadScene(nextSceneName);
                    ++step;
                }
                break;
        }
    }

    private void OnEnable()
    {
        //---Actionイベントを登録

        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if(step != 4)
        {
            return;
        }
        isDecision = true;
    }

}
