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
        OPTION,         //オプション

        MAX_STATE
    }

    //---表示するUI
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
    private GameObject option;
    private GameObject Option;
    [SerializeField]
    private GameObject optionmanager;
    private GameObject Optionmanager;
    [SerializeField]
    private GameObject panel;
    private GameObject Panel;
    [SerializeField]
    private GameObject selectbox;
    private GameObject SelectBox;

    //---表示に使用するCanvas
    Canvas canvas;
    Canvas canvas2;

    private bool isDecision;                // 決定

    private Game_pad UIActionAssets;        // InputActionのUIを扱う
    private InputAction LeftStickSelect;    // InputActionのselectを扱う
    private InputAction RightStickSelect;   // InputActionのselectを扱う


    private int select;                     // 選択中
    private bool isCalledOncce = false;     // ポーズ中一回しか呼ばない
       
    private float UIBasePosx;               // UI表示の基準位置
    private float UIMoveSpeed = 1.5f;       // UI表示を動かすときのスピード

    private bool notShowPause;

    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成

        select = (int)eSTATEPAUSE.RETURNGAME;   // 選択のモードの初期化

        UIBasePosx = 0.0f;  // UIの表示位置の基準位置初期化

        notShowPause = false;   // trueが表示しないとき

        // キャンバスを指定
        canvas = GetComponent<Canvas>();
        if(GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        }
        // 実態化
        PauseCharacter = Instantiate(pausecharacter);
        BackGame = Instantiate(backgame);
        GameEnd = Instantiate(gameend);
        BackTitle = Instantiate(backtitle);
        Option = Instantiate(option);
        Optionmanager = Instantiate(optionmanager);
        Panel = Instantiate(panel);
        SelectBox = Instantiate(selectbox);


        // キャンバスの子にする
        Panel.transform.SetParent(this.canvas.transform, false);
        SelectBox.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);
        BackGame.transform.SetParent(this.canvas.transform, false);
        GameEnd.transform.SetParent(this.canvas.transform, false);
        BackTitle.transform.SetParent(this.canvas.transform, false);
        Option.transform.SetParent(this.canvas.transform, false);
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Optionmanager.transform.SetParent(this.canvas2.transform, false);
        }
        else
        {
            Optionmanager.transform.SetParent(this.canvas.transform, false);
        }


        // ゲームスタート時は非表示
        SelectBox.GetComponent<UIBlink>().isBlink = true;
        SelectBox.GetComponent<UIBlink>().isHide = true;
        SelectBox.GetComponent<Image>().enabled = false;
        SelectBox.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
        PauseCharacter.GetComponent<Image>().enabled = false;
        BackGame.GetComponent<Image>().enabled = false;
        GameEnd.GetComponent<Image>().enabled = false;
        BackTitle.GetComponent<Image>().enabled = false;
        Option.GetComponent<Image>().enabled = false;
        BackGame.GetComponent<Image>().enabled = false;
        GameEnd.GetComponent<Image>().enabled = false;
        BackTitle.GetComponent<Image>().enabled = false;
        Option.GetComponent<Image>().enabled = false;
        Panel.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Panel.GetComponent<Image>().enabled = false;
        Optionmanager.SetActive(false);


    }

    // Update is called once per frame
    void Update() {
        //----- コントローラー初期化 -----
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;

        //----- ポーズのUIを出さない為のif(下の条件式が長くなっていやだったから別で書いちゃった) -----
        notShowPause = false; // trueが表示しないとき
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE ||
            GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial1 ||
            GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial2 ||
            GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial3)
        {
            notShowPause = true;
        }

        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || notShowPause)
        {
            //----- ポーズ中ではない時の処理 -----
            //音楽再生
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE)
            {
                GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                if (kitchenBgmObject)
                {
                    kitchenBgmObject.GetComponent<AudioSource>().UnPause();
                }
            }

            SelectBox.GetComponent<Image>().enabled = false;
            PauseCharacter.GetComponent<Image>().enabled = false;
            BackGame.GetComponent<Image>().enabled = false;
            GameEnd.GetComponent<Image>().enabled = false;
            BackTitle.GetComponent<Image>().enabled = false;
            Option.GetComponent<Image>().enabled = false;
            Panel.GetComponent<Image>().enabled = false;
            isCalledOncce = false;

            if (UIBasePosx > 0.0f)
            {
                FinUIMove();
            }

            return;
        }
        else if (Pause.isPause)
        {
            //----- ポーズ中の処理 -----
            if (!isCalledOncce)
            {
                //----- ポーズに入ったら一回のみする -----
                // ポーズ中になったら表示
                SelectBox.GetComponent<Image>().enabled = true;
                PauseCharacter.GetComponent<Image>().enabled = true;
                BackGame.GetComponent<Image>().enabled = true;
                GameEnd.GetComponent<Image>().enabled = true;
                BackTitle.GetComponent<Image>().enabled = true;
                Option.GetComponent<Image>().enabled = true;
                Panel.GetComponent<Image>().enabled = true;

                //音楽停止
                if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE)
                {
                    GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                    if (kitchenBgmObject)
                    {
                        kitchenBgmObject.GetComponent<AudioSource>().Pause();
                    }
                }
            }
            isCalledOncce = true;   // もう上のif文に入らないようにフラグを反転

            if (UIBasePosx < 0.3125f)
            {
                StartUIMove();
                SelectBoxPosUpdete();   // 選択の枠の表示位置の更新
            }
        }


        //----- ポーズ中の処理 -----
        //オプションが開いてる間は無効にする
        if (!Optionmanager.activeSelf)
        {
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
        }
        

        // 選択しているものが何かで分岐
        if (select == (int)eSTATEPAUSE.RETURNGAME)
        {
            //----- ゲームに戻る -----
            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // ポーズ解除
                Pause.isPause = false;
                // 決定解除
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.RETURNTITLE)
        {
            //----- タイトルに戻る -----
            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // シーン関連
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                SceneManager.LoadScene(nextSceneName);
                // 決定解除
                isDecision = false;
            }
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            //----- ゲームをやめる -----
            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // 決定解除
                isDecision = false;

                /*
                 * Unityエディタ上とexeとでゲームの終了のさせ方がちがうので分岐
                 */
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

        }
        else if(select == (int)eSTATEPAUSE.OPTION)
        {
            //----- オプション -----
            if (isDecision) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // オプションマネージャーをアクティブにする
                Optionmanager.SetActive(true);
                // 決定解除
                isDecision = false;
            }
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

    /// <summary>
    /// 左スティック
    /// </summary>
    /// <param name="obj"></param>
    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause)
        {
            // ポーズ中ではないときははじく
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
        if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause)
        {
            // ポーズ中ではないときははじく
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
        if (Pause.isPause || !SaveManager.canSave || !Warp.shouldWarp || !GameData.isFadeIn || !GameData.isFadeOut || !GameOver.GameOverFlag || notShowPause)
        {
            // ポーズ中ではないときははじく
            isDecision = true;
        }
    }

    /// <summary>
    /// 選択枠の座標更新
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * 選択枠の位置の更新
         * それぞれの文字のRectTransformと合わせることで同じ位置に表示ができる
         */
        if (select == (int)eSTATEPAUSE.RETURNGAME)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = BackGame.GetComponent<RectTransform>().localPosition;
        }
        else if (select == (int)eSTATEPAUSE.RETURNTITLE)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = BackTitle.GetComponent<RectTransform>().localPosition;
        }
        else if (select == (int)eSTATEPAUSE.QUITGAME)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = GameEnd.GetComponent<RectTransform>().localPosition;
        }
        else if (select == (int)eSTATEPAUSE.OPTION)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = Option.GetComponent<RectTransform>().localPosition;
        }
    }

    /// <summary>
    /// 上方向選択
    /// </summary>
    private void SelectUp() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0) // 例外処理
        {
            select = 0;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 下方向選択
    /// </summary>
    private void SelectDown() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select >= (int)eSTATEPAUSE.MAX_STATE)   // 例外処理
        {
            select = (int)eSTATEPAUSE.OPTION;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }


    /// <summary>
    /// ポーズに入ったらUIを動かす
    /// </summary>
    private void StartUIMove() {
        Camera camera = Camera.main; // メインカメラを指定

        // タイムで画面の表示位置の割合を変える
        UIBasePosx += Time.deltaTime * UIMoveSpeed;
        if (UIBasePosx > 0.3125f)      // 例外処理
        {
            // 0.3125(600/1920)はUIの横幅をカメラの座標にあわせたやつ
            UIBasePosx = 0.3125f;
        }

        camera.rect = new Rect(UIBasePosx, 0.0f, 1.0f - UIBasePosx, 1.0f);
        //BackGame.GetComponent<RectTransform>().localPosition.x = ;
    }

    /// <summary>
    /// ポーズ終わったらUIを戻す
    /// </summary>
    private void FinUIMove() {
        Camera camera = Camera.main; // メインカメラを指定

        // タイムで画面の表示位置の割合を変える
        UIBasePosx -= Time.deltaTime * UIMoveSpeed;
        if (UIBasePosx < 0.0f)  // 例外処理
        {
            UIBasePosx = 0.0f;
        }
        camera.rect = new Rect(UIBasePosx, 0.0f, 1.0f - UIBasePosx, 1.0f);
    }

}