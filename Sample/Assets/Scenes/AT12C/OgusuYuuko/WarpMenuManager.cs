using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WarpMenuManager : MonoBehaviour
{
    public int nWarpNumber; //マップマネージャで値設定する
    //はい
    public GameObject YesButton;
    //いいえ
    public GameObject NoButton;
    //選択用フレーム
    public GameObject SelectFrame;
    //現在の選択
    private enum ESELECT { YES,NO};
    private ESELECT nSelect;
    // InputActionのUIを扱う
    private Game_pad UIActionAssets;
    // InputActionのselectを扱う
    private InputAction LeftStickSelect;
    // InputActionのselectを扱う
    private InputAction RightStickSelect;
    // 決定されたフラグ
    private bool isDecision;

    void Awake()
    {
        // InputActionインスタンスを生成
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Actionイベントを登録
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();

        //はいを選択
        nSelect = ESELECT.YES;
        //フレームの位置設定
        SelectFrame.GetComponent<RectTransform>().transform.position = YesButton.GetComponent<RectTransform>().transform.position;
    }

    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラー初期化
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }

        //左矢印
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectLeft();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                SelectLeft();
            }
        }

        //右矢印
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectRight();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                SelectRight();
            }
        }

        //決定ボタン押されたら
        if (isDecision)
        {
            isDecision = false;
            if(nSelect == ESELECT.YES)
            {
                Debug.Log(nWarpNumber + "に移動");
                //指定されたシーンに遷移

                //キッチンステージから、EXステージ　又は　EXステージからキッチンステージの移動の場合BGMが変わるのでBGMオブジェクトを削除しておく
                if(GameData.CurrentMapNumber < (int)GameData.eSceneState.BossStage001 && nWarpNumber + 1 > (int)GameData.eSceneState.KitchenStage006)
                {
                    GameObject bgmObject = GameObject.Find("BGMObject(Clone)");
                    if (bgmObject)
                    {
                        Destroy(bgmObject);
                    }
                }

                if(GameData.CurrentMapNumber > (int)GameData.eSceneState.KitchenStage006 && nWarpNumber + 1 < (int)GameData.eSceneState.BossStage001)
                {
                    GameObject bgmObject = GameObject.Find("BGMObject_EX(Clone)");
                    if (bgmObject)
                    {
                        Destroy(bgmObject);
                    }

                }


                // シーン関連
                switch (nWarpNumber)
                {
                    case 0:
                        GameData.CurrentMapNumber = -1; //０にすると初期化されてしまうので-1にしてる
                        break;
                    case 1:
                    case 3:
                    case 6:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage001;
                        break;
                    case 2:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage006;
                        break;
                    case 4:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage004;
                        break;
                    case 5:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage005;
                        break;
                    case 7:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.BossStage001;
                        break;
                    case 8:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.BossStage002;
                        break;
                }
                GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001 + nWarpNumber;
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }

    }

    private void OnDecision(InputAction.CallbackContext obj)
    {
        //音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        isDecision = true;
    }

    private void SelectLeft()
    {
        //矢印移動した時の音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if(nSelect == ESELECT.YES)
        {
            return;
        }

        //はいを選択
        nSelect = ESELECT.YES;
        //フレーム移動
        SelectFrame.GetComponent<RectTransform>().transform.position = YesButton.GetComponent<RectTransform>().transform.position;
    }


    private void SelectRight()
    {
        //矢印移動した時の音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (nSelect == ESELECT.NO)
        {
            return;
        }

        //いいえを選択
        nSelect = ESELECT.NO;
        //フレーム移動
        SelectFrame.GetComponent<RectTransform>().transform.position = NoButton.GetComponent<RectTransform>().transform.position;
    }
}
