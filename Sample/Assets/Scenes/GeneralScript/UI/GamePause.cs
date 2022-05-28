using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    //----- 定数定義 -----
    private enum eSTATEPAUSE    // ポーズから行う操作
    {
        RETURNGAME = 0, // ゲームに戻る
        RETURNTITLE,    // タイトルに戻る
        QUITGAME,       // げーむをやめる
        OPTION,         // オプション
        QUITQUESTION,   // 本当にやめますか

        MAX_STATE
    }
    private enum eQuitState {
        NONE,
        YES,
        NO
    }

    //----- 変数定義 -----
    //---表示するUI
    [SerializeField]
    private GameObject pauseframe;
    private GameObject PauseFrame;
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
    private GameObject selectbox_1;
    private GameObject SelectBox_1; // 通常の選択で使用
    [SerializeField]
    private GameObject selectbox_2;
    private GameObject SelectBox_2; // 最終確認で使用
    [SerializeField]
    private GameObject decision;
    private GameObject Decision;

    [SerializeField]
    private GameObject quitquestion;
    private GameObject QuitQuestion;
    [SerializeField]
    private GameObject quityes;
    private GameObject QuitYes;
    [SerializeField]
    private GameObject quitno;
    private GameObject QuitNo;

    Canvas canvas;                          // 表示に使用するCanvas
    Canvas canvas2;

    private bool isDecision;                // 決定
    private bool isConfirm;                   // ファイナルアンサー？

    private Game_pad UIActionAssets;        // InputActionのUIを扱う
    private InputAction LeftStickSelect;    // InputActionのselectを扱う
    private InputAction RightStickSelect;   // InputActionのselectを扱う

    private int pauseSelect;                // ポーズ選択
    private int quitSelect;                 // 最終確認選択
    private int returnSelect;               // 戻らなきゃいけない選択肢
    private bool isCalledOncce = false;     // ポーズ中一回しか呼ばない
       
    private float UIBasePosx;               // UI表示の基準位置
    private float UIMoveSpeed = 1.5f;       // UI表示を動かすときのスピード

    private bool notShowPause;              // ポーズではないときにUIを表示しないようにするための変数

    private GameObject hpUI;                // ポーズに入ったらUIを消したいから
    private GameObject minimapUI;    




    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
    }




    private void Start() {
        quitSelect = (int)eQuitState.YES;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // 選択のモードの初期化
        returnSelect = pauseSelect;                      // 選択のモードの初期化

        UIBasePosx = 0.0f;  // UIの表示位置の基準位置初期化

        notShowPause = false;   // trueが表示しないとき

        isConfirm = false;         // 本当にやめるかが確定されたらtrue


        //---探して格納
        hpUI = GameObject.Find("HPSystem(2)(Clone)");   
        minimapUI = GameObject.Find("MiniMapFrame");

        //---キャンバスを指定
        canvas = GetComponent<Canvas>();
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        }

        //---実態化
        PauseFrame = Instantiate(pauseframe);           // ポーズ枠
        PauseCharacter = Instantiate(pausecharacter);   // ポーズの文字
        BackGame = Instantiate(backgame);               // ゲームに戻るの文字
        GameEnd = Instantiate(gameend);                 // ゲームやめるの文字
        BackTitle = Instantiate(backtitle);             // タイトルもドルの文字
        Option = Instantiate(option);                   // オプションの文字
        Optionmanager = Instantiate(optionmanager);     // オプションの画面
        SelectBox_1 = Instantiate(selectbox_1);         // 選択枠
        SelectBox_2 = Instantiate(selectbox_2);
        Decision = Instantiate(decision);               // 決定操作説明文字
        QuitQuestion = Instantiate(quitquestion);       // 本当にやめますか文字
        QuitYes = Instantiate(quityes);                 // 本当にやめますかイエス
        QuitNo = Instantiate(quitno);                   // 本当にやめますかのー

        //---キャンバスの子にする
        PauseFrame.transform.SetParent(this.transform, false);              // ポーズ枠
        SelectBox_1.transform.SetParent(this.canvas.transform, false);      // 選択枠
        SelectBox_2.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);   // ポーズの文字
        BackGame.transform.SetParent(this.canvas.transform, false);         // ゲームに戻るの文字
        GameEnd.transform.SetParent(this.canvas.transform, false);          // ゲームやめるの文字
        BackTitle.transform.SetParent(this.canvas.transform, false);        // タイトルもドルの文字
        Option.transform.SetParent(this.canvas.transform, false);           // オプションの文字
        Decision.transform.SetParent(this.canvas.transform, false);         // 決定操作説明文字
        QuitQuestion.transform.SetParent(this.canvas.transform, false);     // 本当にやめますか文字
        QuitYes.transform.SetParent(this.canvas.transform, false);          // 本当にやめますかイエス
        QuitNo.transform.SetParent(this.canvas.transform, false);           // 本当にやめますかのー
        // オプション画面
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Optionmanager.transform.SetParent(this.canvas2.transform, false);
        }
        else
        {
            Optionmanager.transform.SetParent(this.canvas.transform, false);
        }


        //---ゲームスタート時は非表示
        // 選択枠
        SelectBox_1.GetComponent<UIBlink>().isBlink = true;
        SelectBox_1.GetComponent<UIBlink>().isHide = true;
        SelectBox_1.GetComponent<Image>().enabled = false;
        SelectBox_1.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
        SelectBox_2.GetComponent<UIBlink>().isBlink = true;
        SelectBox_2.GetComponent<UIBlink>().isHide = true;
        SelectBox_2.GetComponent<Image>().enabled = false;
        SelectBox_2.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
        // ポーズ枠
        PauseFrame.GetComponent<Image>().enabled = false;
        // ポーズって文字
        PauseCharacter.GetComponent<Image>().enabled = false;
        // ゲームに戻るの文字
        BackGame.GetComponent<Image>().enabled = false;
        // ゲーム終わるの文字
        GameEnd.GetComponent<Image>().enabled = false;
        // タイトルもドルの文字
        BackTitle.GetComponent<Image>().enabled = false;
        // オプションの文字
        Option.GetComponent<Image>().enabled = false;
        // オプションの画面
        Optionmanager.SetActive(false);
        // 決定の操作
        Decision.GetComponent<Image>().enabled = false;
        // 本当によろしいですか
        QuitQuestion.GetComponent<Image>().enabled = false;
        // 本当によろしいですかのイエス
        QuitYes.GetComponent<Image>().enabled = false;
        // 本当によろしいですかののー
        QuitNo.GetComponent<Image>().enabled = false;
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

            // 非表示
            PauseFrame.GetComponent<Image>().enabled = false;
            SelectBox_1.GetComponent<Image>().enabled = false;
            SelectBox_2.GetComponent<Image>().enabled = false;
            PauseCharacter.GetComponent<Image>().enabled = false;
            BackGame.GetComponent<Image>().enabled = false;
            GameEnd.GetComponent<Image>().enabled = false;
            BackTitle.GetComponent<Image>().enabled = false;
            Option.GetComponent<Image>().enabled = false;
            isCalledOncce = false;
            hpUI.SetActive(true);
            minimapUI.SetActive(true);
            Decision.GetComponent<Image>().enabled = false;
            QuitQuestion.GetComponent<Image>().enabled = false;
            QuitYes.GetComponent<Image>().enabled = false;
            QuitNo.GetComponent<Image>().enabled = false;



            if (UIBasePosx > 0.0f)
            //if (UIPosx > -1390.0f)
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
                PauseFrame.GetComponent<Image>().enabled = true;
                SelectBox_1.GetComponent<Image>().enabled = true;
                PauseCharacter.GetComponent<Image>().enabled = true;
                BackGame.GetComponent<Image>().enabled = true;
                GameEnd.GetComponent<Image>().enabled = true;
                BackTitle.GetComponent<Image>().enabled = true;
                Option.GetComponent<Image>().enabled = true;
                hpUI.SetActive(false);
                minimapUI.SetActive(false);


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
        //---選択
        //オプションが開いてる間は無効にする
        if (!Optionmanager.activeSelf)
        {
            // オプション中じゃないときに操作説明を表示
            Decision.GetComponent<RectTransform>().localPosition = new Vector3(-460, -500, 0);
            Decision.GetComponent<Image>().enabled = true;

            // ポーズになったら選択させる
            if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
            {
                if (keyboard.leftArrowKey.wasReleasedThisFrame)
                {
                    SelectLeft();
                    return;
                }
                else if (isSetGamePad)
                {
                    if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
                    {
                        SelectLeft();
                        return;
                    }
                }

                if (keyboard.rightArrowKey.wasReleasedThisFrame)
                {
                    SelectRight();
                    return;
                }
                else if (isSetGamePad)
                {
                    if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
                    {
                        SelectRight();
                        return;
                    }
                }

            }
            else
            {
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
        }
        else
        {
            // オプション中はオプション中の操作説明があるのでけす
            Decision.GetComponent<Image>().enabled = false;
        }


        //---選択しているものが何かで分岐
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME)
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
        else if(pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {
            //----- 最終確認 -----
            SelectBox_2.GetComponent<Image>().enabled = true;

            //---本当にいいかの確認を出すための決定
            if (isDecision)
            {
                pauseSelect = returnSelect;

                isConfirm = true;
                isDecision = false;
            }
        }
        else if (pauseSelect == (int)eSTATEPAUSE.RETURNTITLE)
        {
            //----- タイトルに戻る -----
            //---本当にいいかの確認を出すための決定
            if (isDecision)
            {
                //---本当にいいかUI
                QuitQuestion.GetComponent<Image>().enabled = true;
                QuitYes.GetComponent<Image>().enabled = true;
                QuitNo.GetComponent<Image>().enabled = true;

                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                returnSelect = (int)eSTATEPAUSE.RETURNTITLE;
                pauseSelect = (int)eSTATEPAUSE.QUITQUESTION;  // 選択を本当にやめるかモードにする
                quitSelect = (int)eQuitState.YES;
                SelectBoxPosUpdete();
                isDecision = false;
            }
            //---本当にいいかを確定
            if (isConfirm)
            {
                if (quitSelect == (int)eQuitState.YES)
                {
                    //---本当にいいかUI
                    QuitQuestion.GetComponent<Image>().enabled = false;
                    QuitYes.GetComponent<Image>().enabled = false;
                    QuitNo.GetComponent<Image>().enabled = false;

                    // シーン関連
                    GameData.OldMapNumber = GameData.CurrentMapNumber;
                    string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                    SceneManager.LoadScene(nextSceneName);
                }
                else if (quitSelect == (int)eQuitState.NO)
                {
                    //---本当にいいかUI
                    QuitQuestion.GetComponent<Image>().enabled = false;
                    QuitYes.GetComponent<Image>().enabled = false;
                    QuitNo.GetComponent<Image>().enabled = false;
                    SelectBox_2.GetComponent<Image>().enabled = false;
                }
                isConfirm = false;
            }
            else
            {
                isConfirm = false;
            }
        }
        else if (pauseSelect == (int)eSTATEPAUSE.QUITGAME)
        {
            //----- ゲームをやめる -----
            if (isDecision)
            {
                //---本当にいいかUI
                QuitQuestion.GetComponent<Image>().enabled = true;
                QuitYes.GetComponent<Image>().enabled = true;
                QuitNo.GetComponent<Image>().enabled = true;

                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                returnSelect = (int)eSTATEPAUSE.QUITGAME;
                pauseSelect = (int)eSTATEPAUSE.QUITQUESTION;  // 選択を本当にやめるかモードにする
                quitSelect = (int)eQuitState.YES;
                SelectBoxPosUpdete();
                isDecision = false;
            }

            //---本当にいいかを確定
            if (isConfirm)
            {
                if (quitSelect == (int)eQuitState.YES)
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
                else if (quitSelect == (int)eQuitState.NO)
                {
                    //---本当にいいかUI
                    QuitQuestion.GetComponent<Image>().enabled = false;
                    QuitYes.GetComponent<Image>().enabled = false;
                    QuitNo.GetComponent<Image>().enabled = false;
                    SelectBox_2.GetComponent<Image>().enabled = false;
                }
                isConfirm = false;
            }
            else
            {
                isConfirm = false;
            }
    }
        else if(pauseSelect == (int)eSTATEPAUSE.OPTION)
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
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {
            //---少しでも倒されたら処理に入る
            if (doLeftStick.x > 0.0f)
            {
                SelectRight();
            }
            else if (doLeftStick.x < 0.0f)
            {
                SelectLeft();
            }
        }
        else
        {
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
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {
            //---少しでも倒されたら処理に入る
            if (doRightStick.x > 0.0f)
            {
                SelectRight();
            }
            else if (doRightStick.x < 0.0f)
            {
                SelectLeft();
            }
        }
        else
        {
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
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = BackGame.GetComponent<RectTransform>().localPosition;
        }
        else if (pauseSelect == (int)eSTATEPAUSE.RETURNTITLE)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = BackTitle.GetComponent<RectTransform>().localPosition;
        }
        else if (pauseSelect == (int)eSTATEPAUSE.QUITGAME)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = GameEnd.GetComponent<RectTransform>().localPosition;
        }
        else if (pauseSelect == (int)eSTATEPAUSE.OPTION)
        {
            SelectBox_1.GetComponent<RectTransform>().localPosition = Option.GetComponent<RectTransform>().localPosition;
        }
        else if(pauseSelect == (int)eSTATEPAUSE.QUITQUESTION)
        {

            if (quitSelect == (int)eQuitState.YES)
            {
                SelectBox_2.GetComponent<RectTransform>().localPosition = QuitYes.GetComponent<RectTransform>().localPosition;
            }
            else if(quitSelect == (int)eQuitState.NO)
            {
                SelectBox_2.GetComponent<RectTransform>().localPosition = QuitNo.GetComponent<RectTransform>().localPosition;
            }
        }
    }

    /// <summary>
    /// 上方向選択
    /// </summary>
    private void SelectUp() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        pauseSelect--;
        if (pauseSelect < 0) // 例外処理
        {
            pauseSelect = 0;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 下方向選択
    /// </summary>
    private void SelectDown() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        pauseSelect++;
        if (pauseSelect >= (int)eSTATEPAUSE.MAX_STATE)   // 例外処理
        {
            pauseSelect = (int)eSTATEPAUSE.OPTION;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }


    /// <summary>
    /// 選択右
    /// </summary>
    private void SelectRight() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect++;
        if (quitSelect > (int)eQuitState.NO)
        {
            quitSelect = (int)eQuitState.NO;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 選択左
    /// </summary>
    private void SelectLeft() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect--;
        if (quitSelect < (int)eQuitState.YES)
        {
            quitSelect = (int)eQuitState.YES;
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