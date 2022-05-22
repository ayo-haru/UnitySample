using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour {
    private enum eSTATETITLE // タイトルシーンから行う動作
    {
        FROMBIGINING = 0,   // 初めから
        FROMCONTINUE,       // 続きから
        QUIT,               // やめる
        OPTION,             // オプション

        MAX_STATE
    }



    //---変数宣言
    private Game_pad UIActionAssets;                // InputActionのUIを扱う
    private InputAction LeftStickSelect;            // InputActionのselectを扱う
    private InputAction RightStickSelect;           // InputActionのselectを扱う


    public int select;                             // 選択されているモードの番号 
    private bool isDecision;


    private bool isPressButton = false;             // ボタンが押されたかの判定用
    //private bool oncePressButton = false;           // ボタンが押されたときに一回だけ処理をする用
    //private bool onceTiltStick = false;

    private GameObject TitleLogo;
    private GameObject PressAnyButton;
    private GameObject GameStart;
    private GameObject GameContinue;
    private GameObject GameEnd;
    private GameObject Option;
    private GameObject Optionmanager;
    private GameObject SelectFrame;

    private TitlePlayer titlePlayer;
    private bool weight = false;    //演出待ちかどうか
    private int timer = 0;

    private void Awake() {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
        select = (int)eSTATETITLE.FROMBIGINING;     // 初期値ははじめから
        isDecision = false;


        Application.targetFrameRate = 60;           // フレームレートを固定

        //----- シーン -----
        SaveManager.load();
        GameData.LoadData();
        GameData.CurrentMapNumber = (int)GameData.eSceneState.TITLE_SCENE;

        //----- サウンド -----
        for (int i = 0; i < SoundData.TitleAudioList.Length; ++i)
        {
            SoundData.TitleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager.Play(SoundData.eBGM.BGM_TITLE, SoundData.TitleAudioList);// 音鳴らす

        //----- UI -----
        TitleLogo = GameObject.Find("Titlelogo");               // UIを見つけて格納しておく
        PressAnyButton = GameObject.Find("PressAnyButton");
        GameStart = GameObject.Find("GameStart");
        GameContinue = GameObject.Find("GameContinue");
        GameEnd = GameObject.Find("GameEnd");
        Option = GameObject.Find("Option");
        Optionmanager = GameObject.Find("OptionManager");
        SelectFrame = GameObject.Find("SelectFrame");
        GameStart.GetComponent<UIBlink>().isHide = true;        // ゲームが始まった瞬間は要らないので消す
        //GameStart.GetComponent<Move2DTheta>().enabled = false;
        GameContinue.GetComponent<UIBlink>().isHide = true;
        //GameContinue.GetComponent<Move2DTheta>().enabled = false;
        GameEnd.GetComponent<UIBlink>().isHide = true;
        //GameEnd.GetComponent<Move2DTheta>().enabled = false;
        Option.GetComponent<UIBlink>().isHide = true;
        //Option.GetComponent<Move2DTheta>().enabled = false;
        Optionmanager.SetActive(false);
        SelectFrame.GetComponent<UIBlink>().isHide = true;
        SelectFrame.GetComponent<UIBlink>().isBlink = false;

        //---タイトル用プレイヤー取得
        titlePlayer = GameObject.Find("Rulaby").GetComponent<TitlePlayer>();

    }


    // Update is called once per frame
    void Update() {
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;

        // 何かボタンが押されたら
        if ((keyboard.anyKey.wasReleasedThisFrame || GamePadManager.ReleaseAnyButton(GamePadManager.eGamePadType.ALLTYPE)) && !isPressButton)
        {
            isPressButton = true;
            PressAnyButton.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            PressAnyButton.GetComponent<UIBlink>().isHide = true;   // ボタンが押されたら消す
            TitleLogo.GetComponent<UIBlink>().isHide = true;
            GameStart.GetComponent<UIBlink>().isHide = false;       // ボタンが押されたら表示する
            GameContinue.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;
            Option.GetComponent<UIBlink>().isHide = false;
            SelectFrame.GetComponent<UIBlink>().isHide = false;
            SelectFrame.GetComponent<RectTransform>().position = GameStart.GetComponent<RectTransform>().position;
            SelectFrame.GetComponent<UI_Parry>().enabled = true;
            GameStart.GetComponent<UI_Parry>().enabled = true;


            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

            titlePlayer.pressAnyButtonFlag = true;

            return;
        }


        // プレスボタンされていなかったら以下の処理をしない
        if (!isPressButton)
        {
            PressAnyButton.GetComponent<UIBlink>().isBlink = true;
            return;
        }


        // プレスボタンされたとき＝モードの選択になるので
        // 矢印キーで選択させる
        //オプション開いてるときは無効にする
        if (!Optionmanager.activeSelf)
        {
            if (keyboard.leftArrowKey.wasReleasedThisFrame)
            {
                SelectUp();
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
                {
                    SelectUp();
                }
            }

            if (keyboard.rightArrowKey.wasReleasedThisFrame)
            {
                SelectDown();
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
                {
                    SelectDown();
                }
            }
        }




        // 選択しているものが何かで分岐
        if (select == (int)eSTATETITLE.FROMBIGINING)
        {
            GameStart.GetComponent<UIBlink>().isBlink = true; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            Option.GetComponent<UIBlink>().isBlink = false;     //UIの点滅を消す

            SelectFrame.GetComponent<RectTransform>().position = GameStart.GetComponent<RectTransform>().position;  //選択枠の位置設定

            GameStart.GetComponent<UI_Parry>().enabled = true;  //上下移動無効
            GameContinue.GetComponent<UI_Parry>().enabled = false;  //上下移動有効
            GameEnd.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            Option.GetComponent<UI_Parry>().enabled = false;  //上下移動無効

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                // 選択枠を隠す
                //SelectFrame.GetComponent<UIBlink>().isHide = true;

                weight = true;
                titlePlayer.decisionFlag = true;
                isDecision = false;

            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
                // すべてのデータの初期化
                GameData.InitData();

                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial1;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }

        }
        else if (select == (int)eSTATETITLE.FROMCONTINUE)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = true; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            Option.GetComponent<UIBlink>().isBlink = false;     //UIの点滅を消す

            SelectFrame.GetComponent<RectTransform>().position = GameContinue.GetComponent<RectTransform>().position;  //選択枠の位置設定

            GameStart.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            GameContinue.GetComponent<UI_Parry>().enabled = true;  //上下移動有効
            GameEnd.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            Option.GetComponent<UI_Parry>().enabled = false;  //上下移動無効

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                titlePlayer.decisionFlag = true;
                isDecision = false;
                weight = true;
            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else if (select == (int)eSTATETITLE.QUIT)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = true; // UIの点滅を消す
            Option.GetComponent<UIBlink>().isBlink = false;     //UIの点滅を消す

            SelectFrame.GetComponent<RectTransform>().position = GameEnd.GetComponent<RectTransform>().position;  //選択枠の位置設定

            GameStart.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            GameContinue.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            GameEnd.GetComponent<UI_Parry>().enabled = true;  //上下移動有効
            Option.GetComponent<UI_Parry>().enabled = false;  //上下移動無効

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                isDecision = false;
                weight = true;
                titlePlayer.decisionFlag = true;
            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }



        }
        else if (select == (int)eSTATETITLE.OPTION)
        {
            Option.GetComponent<UIBlink>().isBlink = true;     //UIを点滅
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅を消す
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す

            SelectFrame.GetComponent<RectTransform>().position = Option.GetComponent<RectTransform>().position;  //選択枠の位置設定

            GameStart.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            GameContinue.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            GameEnd.GetComponent<UI_Parry>().enabled = false;  //上下移動無効
            Option.GetComponent<UI_Parry>().enabled = true;  //上下移動有効

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                titlePlayer.decisionFlag = true;
                isDecision = false;
                //演出待ち
                weight = true;

            }
            else if (weight && !titlePlayer.decisionFlag)
            {
                weight = false;
                //オプションマネージャーをアクティブにする
                Optionmanager.SetActive(true);
                Option.GetComponent<UI_Parry>().enabled = false;  //上下移動有効
                SelectFrame.GetComponent<UIBlink>().isHide = false;
                SelectFrame.GetComponent<UIParry>().enabled = true;
            }



        }
    }

    private void SelectUp() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);

        select++;
        if (select >= (int)eSTATETITLE.MAX_STATE)
        {
            //select = (int)eSTATETITLE.QUIT;
            select = (int)eSTATETITLE.OPTION;
        }
    }

    private void OnEnable() {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Actionイベントを登録
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }

        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doLeftStick.x < 0.0f)
        {
            SelectUp();
        }
        else if (doLeftStick.x > 0.0f)
        {
            SelectDown();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }

        //---右ステックのステック入力を取得
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.x < 0.0f)
        {
            SelectUp();
        }
        else if (doRightStick.x > 0.0f)
        {
            SelectDown();
        }

    }

    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj) {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }
        isDecision = true;
    }

}


