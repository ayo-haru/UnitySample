using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial02UI : MonoBehaviour {
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

    private Canvas canvas;  // 表示するキャンバス

    private int UIcnt;  // UI表示順

    private Game_pad UIActionAssets;        // InputActionのUIを扱う
    private bool isDecision;                // 決定された


    private void Awake() {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
    }

    // Start is called before the first frame update
    void Start() {
        isDecision = false; // 決定を初期化

        UIcnt = 0;

        // キャンバスを指定
        canvas = GetComponent<Canvas>();

        //----- UI初期化 -----
        // 実態化
        CharacterBack = Instantiate(characterback);
        Chara2_1 = Instantiate(chara2_1);
        Chara2_2 = Instantiate(chara2_2);
        Chara2_3 = Instantiate(chara2_3);
        Chara2_4 = Instantiate(chara2_4);

        // キャンバスの子に指定
        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara2_1.transform.SetParent(this.canvas.transform, false);
        Chara2_2.transform.SetParent(this.canvas.transform, false);
        Chara2_3.transform.SetParent(this.canvas.transform, false);
        Chara2_4.transform.SetParent(this.canvas.transform, false);

        // 何も表示しない
        CharacterBack.GetComponent<ImageShow>().Hide();
        Chara2_1.GetComponent<ImageShow>().Hide();
        Chara2_2.GetComponent<ImageShow>().Hide();
        Chara2_3.GetComponent<ImageShow>().Hide();
        Chara2_4.GetComponent<ImageShow>().Hide();

    }

    // Update is called once per frame
    void Update() {
        if (UIcnt == 0)
        {
            //Debug.Log("<color=blue></color>"+UIcnt);
            Time.timeScale = 0.0f;
            CharacterBack.GetComponent<ImageShow>().Show();
            Chara2_1.GetComponent<ImageShow>().Show();
            if (isDecision)
            {
                //Debug.Log("<color=blue>決定</color>");

                UIcnt++;
                isDecision = false;
                return;
            }
        }
        else if(UIcnt == 1)
        {
            Chara2_1.GetComponent<ImageShow>().Clear();
            Chara2_2.GetComponent<ImageShow>().Show();
            if (isDecision)
            {
                UIcnt++;
                isDecision = false;
                Time.timeScale = 1.0f;
                return;
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

}