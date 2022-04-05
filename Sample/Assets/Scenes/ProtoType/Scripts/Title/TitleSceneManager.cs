using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour {
   private enum eSTATETITLE // タイトルシーンから行う動作
    {
        FROMBIGINING = 0,   // 初めから
        FROMCONTINUE,       // 続きから
        QUIT,               // やめる
       
        MAX_STATE
    }



    //---変数宣言
    private Game_pad UIActionAssets;                // InputActionのUIを扱う
    private InputAction LeftStickSelect;            // InputActionのselectを扱う
    private InputAction RightStickSelect;           // InputActionのselectを扱う

    private Vector2 doLeftStick = Vector2.zero;
    private Vector2 doRightStick = Vector2.zero;

    private int select;                             // 選択されているモードの番号 

    private bool isPressButton = false;             // ボタンが押されたかの判定用
    private bool oncePressButton = false;           // ボタンが押されたときに一回だけ処理をする用

    private GameObject TitleLogo;
    private GameObject PressAnyButton;
    private GameObject GameStart;
    private GameObject GameContinue;
    private GameObject GameEnd;

    private void Awake()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
        select = (int)eSTATETITLE.FROMBIGINING;     // 初期値ははじめから      

        Application.targetFrameRate = 60;           // フレームレートを固定
        
        //----- シーン -----
        SaveManager.load();
        GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
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
        GameStart.GetComponent<UIBlink>().isHide = true;        // ゲームが始まった瞬間は要らないので消す
        GameContinue.GetComponent<UIBlink>().isHide = true;
        GameEnd.GetComponent<UIBlink>().isHide = true;

    }


    // Update is called once per frame
    void Update() {
        // 何かボタンが押されたら
        if (Input.anyKeyDown && !isPressButton)
        {
            isPressButton = true;
            PressAnyButton.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            PressAnyButton.GetComponent<UIBlink>().isHide = true;   // ボタンが押されたら消す
            TitleLogo.GetComponent<UIBlink>().isHide = true;
            GameStart.GetComponent<UIBlink>().isHide = false;       // ボタンが押されたら表示する
            GameContinue.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;


            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
        }


        // プレスボタンされていなかったら以下の処理をしない
        if (!isPressButton)
        {
            PressAnyButton.GetComponent<UIBlink>().isBlink = true;
            return;
        }


        // プレスボタンされたとき＝モードの選択になるので
        // 矢印キーで選択させる
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
            select--;
            if (select < 0)
            {
                select = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);

            select++;
            if (select >= (int)eSTATETITLE.MAX_STATE)
            {
                select = (int)eSTATETITLE.QUIT;
            }
        }


        // 選択しているものが何かで分岐
        if (select == (int)eSTATETITLE.FROMBIGINING)
        {
            GameStart.GetComponent<UIBlink>().isBlink = true; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す

            if (Input.GetKeyUp(KeyCode.Return)) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

                GameData.InitData();

                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else if (select == (int)eSTATETITLE.FROMCONTINUE)
        {
            GameStart.GetComponent<UIBlink>().isBlink = false; // UIを点滅
            GameContinue.GetComponent<UIBlink>().isBlink = true; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す

            if (Input.GetKeyUp(KeyCode.Return)) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

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

            if (Input.GetKeyUp(KeyCode.Return)) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

        }
    }

    private void OnEnable() {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Actionイベントを登録
        UIActionAssets.UI.LeftStickSelect.performed += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.performed += OnRightStick;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj)
    {
        //---左ステックのステック入力を取得
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if(doLeftStick.x >=0.1f || doLeftStick.y >= 0.1f )
        {
            Debug.Log("左スティックが倒された");
            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);

        }
    }

    private void OnRightStick(InputAction.CallbackContext obj)
    {
        //---右ステックのステック入力を取得
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.x >= 0.1f || doRightStick.y >= 0.1f)
        {
            Debug.Log("右スティックが倒された");

            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);

        }
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
        GUILayout.Label($"LeftStick:{doLeftStick}");

    }

}



