using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private enum eSTATEPAUSE    // ポーズから行う操作
    {
        RETURNGAME = 0, // ゲームに戻る
        RETURNTITLE,    // タイトルに戻る
        QUITGAME,       // げーむをやめる

        MAX_STATE
    }

    [SerializeField]
    private GameObject pausecharacter;
    private GameObject PauseCharacter;
    [SerializeField]
    private GameObject backgame;
    private GameObject BackGame;
    [SerializeField]
    private GameObject gameend;
    private GameObject GameEnd;
    [SerializeField]
    private GameObject backtitle;
    private GameObject BackTitle;
    [SerializeField]
    private GameObject panel;
    private GameObject Panel;


    Canvas canvas;

    private bool isDecision;

    private Game_pad UIActionAssets;                // InputActionのUIを扱う
    private InputAction LeftStickSelect;            // InputActionのselectを扱う
    private InputAction RightStickSelect;           // InputActionのselectを扱う


    private int select;
    private bool isCalledOncce = false;

    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成

        select = (int)eSTATEPAUSE.RETURNGAME;

        // キャンバスを指定
        canvas = GetComponent<Canvas>();

        // 実態化
        PauseCharacter = Instantiate(pausecharacter);
        BackGame = Instantiate(backgame);
        GameEnd = Instantiate(gameend);
        BackTitle = Instantiate(backtitle);
        Panel = Instantiate(panel);
        

        // キャンバスの子にする
        Panel.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);
        BackGame.transform.SetParent(this.canvas.transform, false);
        GameEnd.transform.SetParent(this.canvas.transform, false);
        BackTitle.transform.SetParent(this.canvas.transform, false);

        // ゲームスタート時は非表示
        PauseCharacter.GetComponent<UIBlink>().isHide = true;
        BackGame.GetComponent<UIBlink>().isHide = true;
        GameEnd.GetComponent<UIBlink>().isHide = true;
        BackTitle.GetComponent<UIBlink>().isHide = true;
        Panel.GetComponent<Image>().enabled = false;
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

        if (!Pause.isPause/* && !GameData.isFadeIn && !GameData.isFadeOut && !SaveManager.canSave && !Warp.shouldWarp*/)
        {
            PauseCharacter.GetComponent<UIBlink>().isHide = true;
            BackGame.GetComponent<UIBlink>().isHide = true;
            GameEnd.GetComponent<UIBlink>().isHide = true;
            BackTitle.GetComponent<UIBlink>().isHide = true;
            Panel.GetComponent<Image>().enabled = false;
            isCalledOncce = false;

            return;
        }
        else if (Pause.isPause  && !isCalledOncce)
        {
            // ポーズ中になったら表示
            PauseCharacter.GetComponent<UIBlink>().isHide = false;
            BackGame.GetComponent<UIBlink>().isHide = false;
            GameEnd.GetComponent<UIBlink>().isHide = false;
            BackTitle.GetComponent<UIBlink>().isHide = false;
            Panel.GetComponent<Image>().enabled = true;

            isCalledOncce = true;
        }

        // ポーズになったら選択させる
        if (keyboard.upArrowKey.wasReleasedThisFrame)
        {
            SelectUp();
            return;
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
            {
                SelectUp();
                return;
            }
        }

        if (keyboard.downArrowKey.wasReleasedThisFrame)
        {
            SelectDown();
            return;
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                SelectDown();
                return;
            }
        }

        // 選択しているものが何かで分岐
        if (select == (int)eSTATEPAUSE.RETURNGAME)
        {
            BackGame.GetComponent<UIBlink>().isBlink = true; // UIを点滅
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す

            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                Pause.isPause = false;
                backgame.GetComponent<UIBlink>().isHide = true;
                BackGame.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.RETURNTITLE)
        {
            BackGame.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            BackTitle.GetComponent<UIBlink>().isBlink = true; // UIを点滅
            GameEnd.GetComponent<UIBlink>().isBlink = false;  // UIの点滅を消す

            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                SceneManager.LoadScene(nextSceneName);
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            BackGame.GetComponent<UIBlink>().isBlink = false;  // UIの点滅を消す
            BackTitle.GetComponent<UIBlink>().isBlink = false; // UIの点滅を消す
            GameEnd.GetComponent<UIBlink>().isBlink = true;    // UIを点滅

            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                isDecision = false;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

            }

        }
    }

    private void SelectUp() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select >= (int)eSTATEPAUSE.MAX_STATE)
        {
            select = (int)eSTATEPAUSE.QUITGAME;
        }
    }

    private void OnEnable() {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Actionイベントを登録
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        UIActionAssets.UI.Decision.started += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    /// <summary>
    /// 左スティック
    /// </summary>
    /// <param name="obj"></param>
    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!Pause.isPause)
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

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!Pause.isPause)
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
    private void OnDecision(InputAction.CallbackContext obj) {
        if (Pause.isPause)
        {
            isDecision = true;
        }
    }

}