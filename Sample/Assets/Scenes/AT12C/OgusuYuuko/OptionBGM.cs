//=============================================================================
//
// BGM設定
//
// 作成日:2022/04/26
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/26    作成

//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class OptionBGM : MonoBehaviour
{
    //スライダー
    private Slider BgmSlider;
    //選択用フラグ
    public bool selectFlag = true;
    //移動量
    public float moveSpeed = 0.005f;
    // InputActionのUIを扱う
    private Game_pad UIActionAssets;               
    // InputActionのselectを扱う
    private InputAction LeftStickSelect;            
    // InputActionのselectを扱う
    private InputAction RightStickSelect;           

    // Start is called before the first frame update
    void Awake()
    {
        // InputActionインスタンスを生成
        UIActionAssets = new Game_pad();

        //コンポーネント取得
        BgmSlider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        //Awakeだとまだデータがロードされてないためstartで実行
        //サウンドマネージャーからbgmの音量を取得
        BgmSlider.value = SoundManager.bgmVolume;
    }

    // Update is called once per frame
    void Update()
    {
        //選択されてなかったらリターン
        if (!selectFlag)
        {
            return;
        }

        //右矢印キーで音量プラス
        if (Input.GetKey(KeyCode.RightArrow))
        {
            VolUp();
        }
        OnRightStick();
        //左矢印キーで音量－
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            VolDown();
        }
        OnLeftStick();


    }
    private void OnEnable()
    {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Actionイベントを登録
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }

    //設定画面閉じたときに音量保存
    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();

        //SaveManager.saveBGMVolume(SoundManager.bgmVolume);
    }

    // ボリューム上げる
    private void VolUp()
    {
        BgmSlider.value += moveSpeed;
        //音量設定
        SoundManager.bgmVolume = BgmSlider.value;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.setVolume(SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.setVolume(SoundData.GameAudioList);
        }

    }

    // ボリューム下げる
    private void VolDown()
    {
        BgmSlider.value -= moveSpeed;
        //音量設定
        SoundManager.bgmVolume = BgmSlider.value;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.setVolume(SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.setVolume(SoundData.GameAudioList);
        }

    }

    private void OnLeftStick()
    {
        if (!selectFlag)
        {
            return;
        }

        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doLeftStick.x > 0.5f)
        {
            VolUp();
        }
        else if (doLeftStick.x < -0.5f)
        {
            VolDown();
        }
    }

    private void OnRightStick()
    {
        if (!selectFlag)
        {
            return;
        }

        //---右ステックのステック入力を取得
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.x > 0.5f)
        {
            VolUp();
        }
        else if (doRightStick.x < -0.5f)
        {
            VolDown();
        }

    }

}
