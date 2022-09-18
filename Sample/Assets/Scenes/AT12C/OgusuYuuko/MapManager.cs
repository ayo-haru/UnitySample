//=============================================================================
//
// キッチンマップマネージャー
//
// 作成日:2022/04/22
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/22 作成
// 2022/04/24 魔法陣追加
// 2022/09/15 コントローラ対応
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //プレイヤーアイコン
    public GameObject[] PlayerIcon;
    //MapGround
    public GameObject[] MapGround;
    //魔法陣
    public GameObject[] MagicCircle;
    //ワープ用の矢印
    public GameObject[] WarpArrow;
    //一回だけ呼び出す用のフラグ
    private bool OnceFlag = true;
    //ワープしますか
    public GameObject WarpMenu;

    // InputActionのUIを扱う
    private Game_pad UIActionAssets;
    // InputActionのselectを扱う
    private InputAction LeftStickSelect;
    // InputActionのselectを扱う
    private InputAction RightStickSelect;
    // 決定されたフラグ
    private bool isDecision;
    //現在の選択
    private int nSelect;
    //選択の遷移先
    private int[,,] MapTransition = { 
        { {6,0,0 },{1,0,0 },{0,0,0 },{2,3,0 } }, 
        { {-1 ,0,0},{0,0,0 },{0,0,0 },{3,2,0 } },
        { {5,0,0 },{1,0,0 },{-2,0,0 },{3 ,0,0} },
        { {-1,0,0 },{1,0,0 },{-3,-2,0 },{2,0,0 } },
        { {-1,0,0 },{0,0,0 },{-3,0,0 },{1,0,0 } },
        { {3,0,0 },{0,0,0 },{-3,-2,-1 },{0,0,0 } },
        { {0,0,0 },{-6,0,0 },{0,0,0 },{1,0,0 } },
        { {0,0,0 },{-5,0,0 },{-1,0,0 },{1,0,0 } },
        { {0,0,0 },{-3,0,0 },{-1,0,0 },{0,0,0 } },
    };

    void Awake()
    {
        // InputActionインスタンスを生成
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Actionイベントを登録
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();

        //プレイヤーアイコンを非表示
        //マップの背景を白に設定
        for (int i = 0; i < PlayerIcon.Length; ++i)
        {
            PlayerIcon[i].SetActive(false);
            if (GameData.isWentMap[i + 1])
            {
                MapGround[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }else
            {
                MapGround[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            
            WarpArrow[i].SetActive(false);
        }
        //初めはワープメニュー非表示
        WarpMenu.SetActive(false);
        // 初期化最初は決定じゃない
        isDecision = false;
        OnceFlag = true;
        //現在いるマップを選択する
        nSelect = GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001;
    }

    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //ワープメニューが表示されてたらリターン
        if (WarpMenu.activeSelf)
        {
            return;
        }
        // コントローラー初期化
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }

        //コントローラーでワープ先選択
        //上矢印
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectUp();
        }else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
            {
                SelectUp();
            }
        }

        //下矢印
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectDown();
        }else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                SelectDown();
            }
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
            //ワープメニュー表示
            WarpMenu.SetActive(true);
            WarpMenu.GetComponent<WarpMenuManager>().nWarpNumber = nSelect;
            isDecision = false;
            //Debug.Log(nSelect + "に移動");
            ////指定されたシーンに遷移
            //// シーン関連
            //switch (nSelect)
            //{
            //    case 0:
            //        GameData.OldMapNumber = 0;
            //        break;
            //    case 1:
            //    case 3:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage001;
            //        break;
            //    case 2:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage006;
            //        break;
            //    case 4:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage004;
            //        break;
            //    case 5:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage005;
            //        break;
            //}
            //GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001 + nSelect;
        }



        //魔法陣表示
        for (int i = 0; i < MagicCircle.Length; ++i)
        {
            MagicCircle[i].GetComponent<ImageShow>().Show(180);
        }

      

       
        if (!OnceFlag)
        {
            return;
        }

        //シーン名で分岐　CurrentMapNumber分かったらそっちに変える予定
        //switch (SceneManager.GetActiveScene().name)
        //{
        //    case "KitchenStage001":
        //        PlayerIcon[0].SetActive(true);
        //        break;
        //    case "KitchenStage002":
        //        PlayerIcon[1].SetActive(true);
        //        break;
        //    case "KitchenStage003":
        //        PlayerIcon[2].SetActive(true);
        //        break;
        //    case "KitchenStage004":
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //    case "KitchenStage005":
        //        PlayerIcon[4].SetActive(true);
        //        break;
        //    case "KitchenStage006":
        //        PlayerIcon[5].SetActive(true);
        //        break;
        //    default:
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //}

        //CurrentMapNumberを元に表示するプレイヤーアイコン設定
        //eSceneStateが変更されても、連番なら[]の中変更しなくていいようにした
        PlayerIcon[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].SetActive(true);
        MapGround[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].GetComponent<Image>().color = new Color(0.1f, 1.0f, 1.0f, 1.0f);
        WarpArrow[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].SetActive(true);

        OnceFlag = false;
    }

   
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        isDecision = true;
    }

    private void SelectUp()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //矢印移動した時の音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if(MapTransition[nSelect,0,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 0,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 0,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
        }
    }

    private void SelectDown()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //矢印移動した時の音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (MapTransition[nSelect, 1,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 1,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 1,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
            
        }
    }

    private void SelectLeft()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //矢印移動した時の音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (MapTransition[nSelect, 2,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 2,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 2,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
            
        }
        
    }


    private void SelectRight()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //矢印移動した時の音鳴らす
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (MapTransition[nSelect, 3,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 3,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 3,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
            
        }
    }

}

