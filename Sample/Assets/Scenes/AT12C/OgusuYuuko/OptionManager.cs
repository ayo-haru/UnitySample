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
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionManager : MonoBehaviour
{
    //選択モード
    private enum SELECT_MODE { BGM, SE };
    private SELECT_MODE select;     //現在の選択
    private SELECT_MODE old_select; //前フレームの選択
    //選択用矢印のRectTransform
    public GameObject selectArrow;
    private RectTransform rt_selectArrow;
    //選択用矢印位置
    public Vector3 posBGM;
    public Vector3 posSE;

    //BGMスライダー
    public GameObject bgmSlider;
    //SEスライダー
    public GameObject seSlider;

    // InputActionのUIを扱う
    private Game_pad UIActionAssets;
    // InputActionのselectを扱う
    private InputAction LeftStickSelect;
    // InputActionのselectを扱う
    private InputAction RightStickSelect;

    void Awake()
    {
        // InputActionインスタンスを生成
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void Start()
    {
        //初めはbgmを選択
        select = SELECT_MODE.BGM;
        //コンポーネント取得
        rt_selectArrow = selectArrow.GetComponent<RectTransform>();
        //矢印位置設定
        Vector3 newPos = new Vector3(rt_selectArrow.transform.position.x, bgmSlider.transform.position.y, rt_selectArrow.transform.position.z);
        rt_selectArrow.transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        //前フレームの選択を保存
        old_select = select;
        //上矢印でBGM選択
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectBGM();
        }
        //下矢印でSE選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectSE();
        }
        OnLeftStick();
        OnRightStick();

        if (old_select == select)
        {
            //選択が変わってなかったらリターン
            return;
        }

        Vector3 newPos;
        switch (select)
        {
            case SELECT_MODE.BGM:
                //se選択解除
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //bgm選択
                bgmSlider.GetComponent<OptionBGM>().selectFlag = true;
                //矢印移動
                newPos = new Vector3(rt_selectArrow.transform.position.x, bgmSlider.transform.position.y, rt_selectArrow.transform.position.z);
                rt_selectArrow.transform.position = newPos;
                break;
            case SELECT_MODE.SE:
                //bgm選択解除
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se選択
                seSlider.GetComponent<OptionSE>().selectFlag = true;
                //矢印移動
                newPos = new Vector3(rt_selectArrow.transform.position.x, seSlider.transform.position.y, rt_selectArrow.transform.position.z);
                rt_selectArrow.transform.position = newPos;
                break;
        }
    }

    private void OnEnable()
    {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Actionイベントを登録
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }

    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void SelectBGM()
    {
        select = SELECT_MODE.BGM;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
    }

    private void SelectSE()
    {
        select = SELECT_MODE.SE;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
    }

    private void OnLeftStick()
    {
        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doLeftStick.y > 0.7f)
        {
            SelectBGM();
        }
        else if (doLeftStick.y < -0.7f)
        {
            SelectSE();
        }
    }

    private void OnRightStick()
    {
        //---右ステックのステック入力を取得
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.y > 0.7f)
        {
            SelectBGM();
        }
        else if (doRightStick.y < -0.7f)
        {
            SelectSE();
        }

    }

}
