//=============================================================================
//
// 設定画面
//
// 作成日:2022/04/26
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/26    作成
// 2022/04/27   SE付けた
// 2022/05/10    パッド完全対応
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionManager : MonoBehaviour
{
    //選択モード
    private enum SELECT_MODE { BGM, SE,BACK,MAX_MODE };
    private int select;     //現在の選択
    private int old_select; //前フレームの選択
    //選択用枠のRectTransform
    public GameObject selectFrame;
    private RectTransform rt_selectFrame;

    //BGMスライダー
    public GameObject bgmSlider;
    //SEスライダー
    public GameObject seSlider;
    //戻る画像
    public GameObject BackImage;

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
        //初めはbgmを選択
        select = (int)SELECT_MODE.BGM;
        //コンポーネント取得
        rt_selectFrame = selectFrame.GetComponent<RectTransform>();
        //矢印位置設定
        Vector3 newPos = new Vector3(rt_selectFrame.position.x, bgmSlider.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
        rt_selectFrame.position = newPos;
        // 初期化最初は決定じゃない
        isDecision = false;
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

        //前フレームの選択を保存
        old_select = select;
        Debug.Log(select);

        //上矢印
        if (Input.GetKeyDown(KeyCode.UpArrow))
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

        //下矢印
        if (Input.GetKeyDown(KeyCode.DownArrow))
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


        //戻るが選択された状態でエンターキー押されたらオプションを閉じる
        if (select == (int)SELECT_MODE.BACK)
        {
            if (isDecision)
            {
                //決定音
                if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
                }
                else
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.GameAudioList);
                    if(GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE)
                    {
                        GameObject bgmObject = GameObject.Find("BGMObject(Clone)");
                        if (bgmObject)
                        {
                            bgmObject.GetComponent<AudioSource>().volume = SoundManager.bgmVolume;
                        }
                    }
                }
                //音量保存
                SaveManager.saveSEVolume(SoundManager.seVolume);
                SaveManager.saveBGMVolume(SoundManager.bgmVolume);
                gameObject.SetActive(false);
                isDecision = false;
            }
        }

        /*
         アップデート内でSelectUp、Downを呼ばれたときは下のリターンはスルーするが、
         スティックで選択した場合は下のいふぶんでひっかかる
         */

        //if (old_select == select)
        //{
        //    //選択が変わってなかったらリターン
        //    return;
        //}

        Vector3 newPos;
        switch (select)
        {
            case (int)SELECT_MODE.BGM:
                //se選択解除
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //bgm選択
                bgmSlider.GetComponent<OptionBGM>().selectFlag = true;
                //選択フレーム移動
                newPos = new Vector3(rt_selectFrame.position.x, bgmSlider.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
                rt_selectFrame.position = newPos;
                break;
            case (int)SELECT_MODE.SE:
                //bgm選択解除
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se選択
                seSlider.GetComponent<OptionSE>().selectFlag = true;
                //選択フレーム移動
                newPos = new Vector3(rt_selectFrame.position.x, seSlider.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
                rt_selectFrame.position = newPos;
                break;
            case (int)SELECT_MODE.BACK:
                //bgm選択解除
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se選択解除
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //選択フレーム移動
                newPos = new Vector3(rt_selectFrame.position.x, BackImage.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
                rt_selectFrame.position = newPos;
                break;
        }
    }

    private void OnEnable()
    {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Actionイベントを登録
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }

    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj)
    {
        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doLeftStick.y > 0.05f)
        {
            SelectUp();
        }
        else if (doLeftStick.y < -0.05f)
        {
            SelectDown();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj)
    {
        //---右ステックのステック入力を取得
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.y > 0.05f)
        {
            //SelectBGM();
            SelectUp();
        }
        else if (doRightStick.y < -0.05f)
        {
            //SelectSE();
            SelectDown();
        }

    }
    private void OnDecision(InputAction.CallbackContext obj) {
        if (select != (int)SELECT_MODE.BACK)
        {
            return;
        }
        isDecision = true;
    }

    private void SelectUp()
    {
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        //矢印移動した時の音鳴らす
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown()
    {
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        //矢印移動した時の音鳴らす
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }

        select++;
        if (select >= (int)SELECT_MODE.MAX_MODE)
        {
            select = (int)SELECT_MODE.BACK;
        }
    }

}
