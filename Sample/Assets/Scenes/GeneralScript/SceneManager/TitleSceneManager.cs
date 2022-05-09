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


    private int select;                             // 選択されているモードの番号 
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

    private void Awake() {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
        select = (int)eSTATETITLE.FROMBIGINING;     // 初期値ははじめから
        isDecision = false;


        Application.targetFrameRate = 60;           // フレームレートを固定

        //----- シーン -----
        SaveManager.load();
        GameData.LoadData();
        //GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
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
        GameStart.GetComponent<UIBlink>().isHide = true;        // ゲームが始まった瞬間は要らないので消す
        GameContinue.GetComponent<UIBlink>().isHide = true;
        GameEnd.GetComponent<UIBlink>().isHide = true;
        Option.GetComponent<UIBlink>().isHide = true;
        Optionmanager.SetActive(false);

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


            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

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
            if (keyboard.upArrowKey.wasReleasedThisFrame)
            {
                SelectUp();
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
                {
                    SelectUp();
                }
            }

            if (keyboard.downArrowKey.wasReleasedThisFrame)
            {
                SelectDown();
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
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

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                GameData.InitData();

                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
                isDecision = false;

            }
        }
        else if (select == (int)eSTATETITLE.FROMCONTINUE)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = true; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            Option.GetComponent<UIBlink>().isBlink = false;     //UIの点滅を消す

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
                isDecision = false;
            }
        }
        else if (select == (int)eSTATETITLE.QUIT)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = true; // UIの点滅を消す
            Option.GetComponent<UIBlink>().isBlink = false;     //UIの点滅を消す

            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
                isDecision = false;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

            }
        }
        else if(select == (int)eSTATETITLE.OPTION)
        {
            Option.GetComponent<UIBlink>().isBlink = true;     //UIを点滅
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅を消す
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            if (isDecision)
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
                //オプションマネージャーをアクティブにする
                Optionmanager.SetActive(true);
                isDecision = false;
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

    private void OnLeftStick(InputAction.CallbackContext obj)
    {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }

        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doLeftStick.y > 0.0f)
        {
            SelectUp();
        }
        else if (doLeftStick.y < 0.0f)
        {
            SelectDown();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj)
    {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }

        //---右ステックのステック入力を取得
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.y > 0.0f)
        {
            SelectUp();
        }
        else if (doRightStick.y < 0.0f)
        {
            SelectDown();
        }
        
    }
    
    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if (!isPressButton || Optionmanager.activeSelf)
        {
            return;
        }
        isDecision = true;
    }

    private void OnGUI()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        //---ゲームパッドとつながっている時に表示される。
        GUILayout.Label($"LeftStick:{Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"RightStick:{Gamepad.current.rightStick.ReadValue()}");
        GUILayout.Label($"ButtonNorth:{Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"ButtonSouth:{Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"ButtonEast:{Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"ButtonWast:{Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"LeftShoulder:{Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"LeftTrigger:{Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"RightShoulder:{Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"RighetTrigger:{Gamepad.current.rightTrigger.ReadValue()}");
        GUILayout.Label($"LeftStickUp:{Gamepad.current.leftStick.up.ReadValue()}");
        GUILayout.Label($"Space:{Keyboard.current.spaceKey.ReadValue()}");

    }
}



