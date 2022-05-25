using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tutorial02UI : MonoBehaviour {
    //----- 表示するUI -----
    [SerializeField]
    private GameObject characterback;
    private GameObject CharacterBack;
    [SerializeField]
    private GameObject chara2_1;
    private GameObject Chara2_1;
    [SerializeField]
    private GameObject chara2_2;
    private GameObject Chara2_2;
    [SerializeField]
    private GameObject chara2_3;
    private GameObject Chara2_3;
    [SerializeField]
    private GameObject chara2_4;
    private GameObject Chara2_4;
    [SerializeField]
    private GameObject panel;
    private GameObject Panel;
    [SerializeField]
    private GameObject nexticon;
    private GameObject NextIcon;
    [SerializeField]
    private GameObject successchara;
    private GameObject SuccessChara;
    [SerializeField]
    private GameObject failedchara;
    private GameObject FailedChara;


    private Canvas canvas;  // 表示するキャンバス

    private int UIcnt;  // UI表示順

    private Game_pad UIActionAssets;        // InputActionのUIを扱う
    private bool isDecision;                // 決定された

    private Tutorial02Manager scenemanager; // 02シーンのマネージャー

    private DelayFollowCamera _delayfollowcamera;   // ディレイフォローカメラを保存しておく

    private BoxCollider wallcollider;   // はじくチュートリアルが終わるまでの壁

    private bool tutorialPause; // チュートリアル中のポーズ

    private void Awake() {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
    }

    // Start is called before the first frame update
    void Start() {
        //----- 各種変数初期化 -----
        isDecision = false; // 決定を初期化
        UIcnt = 0;  // UIを表示する順番を管理する
        canvas = GetComponent<Canvas>();    // キャンバスを指定
        scenemanager = GameObject.Find("SceneManager").GetComponent<Tutorial02Manager>();   // シーンマネージャーをほそん
        _delayfollowcamera = Camera.main.GetComponent<DelayFollowCamera>(); // ディレイフォローカメラを保存
        wallcollider = GameObject.Find("wallcollider004").GetComponent<BoxCollider>();  // 壁を保存
        tutorialPause = false;  // 初期はポーズしないのでfalse

        //----- UI初期化 -----
        // 実態化
        CharacterBack = Instantiate(characterback);
        Chara2_1 = Instantiate(chara2_1);
        Chara2_2 = Instantiate(chara2_2);
        Chara2_3 = Instantiate(chara2_3);
        Chara2_4 = Instantiate(chara2_4);
        Panel = Instantiate(panel);
        NextIcon = Instantiate(nexticon);
        SuccessChara = Instantiate(successchara);
        FailedChara = Instantiate(failedchara);

        // キャンバスの子に指定
        Panel.transform.SetParent(this.canvas.transform, false);
        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara2_1.transform.SetParent(this.canvas.transform, false);
        Chara2_2.transform.SetParent(this.canvas.transform, false);
        Chara2_3.transform.SetParent(this.canvas.transform, false);
        Chara2_4.transform.SetParent(this.canvas.transform, false);
        NextIcon.transform.SetParent(this.canvas.transform, false);
        SuccessChara.transform.SetParent(this.canvas.transform, false);
        FailedChara.transform.SetParent(this.canvas.transform, false);

        // 初期表示調整
        CharacterBack.GetComponent<ImageShow>().Hide();
        Chara2_1.GetComponent<ImageShow>().Hide();
        Chara2_2.GetComponent<ImageShow>().Hide();
        Chara2_3.GetComponent<ImageShow>().Hide();
        Chara2_4.GetComponent<ImageShow>().Hide();
        NextIcon.transform.localPosition = new Vector3(500.0f, -380.0f, 0.0f);
        NextIcon.GetComponent<ImageShow>().Hide();
        Panel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        Panel.GetComponent<Image>().enabled = false;
        SuccessChara.GetComponent<ImageShow>().Hide();
        
    }

    void Update() {
        if (UIcnt == 0)
        {
            //---敵が現れました
            Panel.GetComponent<Image>().enabled = true;

            //Time.timeScale = 0.0f;
            Pause.isPause = true;
            CharacterBack.GetComponent<ImageShow>().Show();
            Chara2_1.GetComponent<ImageShow>().Show();
            NextIcon.GetComponent<ImageShow>().Show();
            if (isDecision)
            {
                UIcnt++;
                isDecision = false;
                return;
            }
        }
        else if(UIcnt == 1)
        {
            //---タイミングよく弾きましょう
            Chara2_1.GetComponent<ImageShow>().Clear();
            Chara2_2.GetComponent<ImageShow>().Show();
            NextIcon.transform.localPosition = new Vector3(850.0f, -380.0f, 0.0f);
            NextIcon.GetComponent<Image>().enabled = true;

            if (isDecision)
            {
                NextIcon.GetComponent<Image>().enabled = false;
                Panel.GetComponent<Image>().enabled = false;
                UIcnt++;
                isDecision = false;
                //Time.timeScale = 1.0f;
                Pause.isPause = false;
                return;
            }
        }
        else if (UIcnt == 2)
        {
            //---はじけたか分岐
            if (!TutorialPanCake.isAlive)
            {
                //SuccessChara.GetComponent<ImageShow>().Show(30);
                if(SuccessChara.GetComponent<SuccessMove>().mode == SuccessMove.eSccessState.NONE)
                {
                    SuccessChara.GetComponent<SuccessMove>().StartSccess();
                }
                StartCoroutine("DelayDestroyPancake");     // すぐに消すのではなく遅延させて消す
                Chara2_2.GetComponent<ImageShow>().Clear();
                Chara2_3.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }

            if (GameData.PlayerPos.x > TutorialPanCake.pancakePos.x)
            {
                //FailedChara.GetComponent<ImageShow>().Show(30);
                if(FailedChara.GetComponent<FailedMove>().mode == FailedMove.eFailedState.NONE)
                {
                    FailedChara.GetComponent<FailedMove>().StartFailed();
                    Pause.isPause = true;
                    //Time.timeScale = 0.0f;              // タイムは止める
                }
                if (FailedChara.GetComponent<FailedMove>().mode == FailedMove.eFailedState.FIN)
                {
                    FailedChara.GetComponent<FailedMove>().FinFailed();
                    scenemanager.PancakeDestroy();      // パンケーキ消す  
                    scenemanager.PancakeAppearance();   // パンケーき出現
                    scenemanager.PlayerDestroy();       // プレイヤー消す
                    scenemanager.PlayerAppearance();    // プレイヤー出現
                    UIcnt = 1;                          // UIを一個前のやつに戻す
                }
            }
        }
        else if(UIcnt == 3 && Chara2_3.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
        {
            //---たいみんぐよくはじく
            Chara2_4.GetComponent<ImageShow>().Show(3);
            UIcnt++;
        }
        else if(UIcnt == 4 && Chara2_4.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
        {
            //---これがこの世界の戦い方
            CharacterBack.GetComponent<ImageShow>().FadeOut();
            _delayfollowcamera.enabled = true;
            wallcollider.enabled = false;
        }
        else if(UIcnt == 5)
        {
            // 多分いらんけどエラー防止
            if (SuccessChara.GetComponent<SuccessMove>().mode == SuccessMove.eSccessState.FIN)
            {
                SuccessChara.GetComponent<SuccessMove>().FinSuccess();
            }
        }
    }

    private void OnEnable() {
        //---Actionイベントを登録
        UIActionAssets.UI.Decision.started += OnDecision;

        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }



    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj) {
        isDecision = true;
    }

    private IEnumerator DelayDestroyPancake() {
        yield return new WaitForSeconds(0.5f);
        scenemanager.PancakeDestroy();
    }

}