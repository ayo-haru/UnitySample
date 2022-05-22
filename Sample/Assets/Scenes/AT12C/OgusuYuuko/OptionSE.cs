//=============================================================================
//
// SE設定
//
// 作成日:2022/04/26
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/26    作成
// 2022/05/10    パッド完全対応
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OptionSE : MonoBehaviour
{
    //スライダー
    private Slider SeSlider;
    //選択用フラグ
    public bool selectFlag = false;
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
        SeSlider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        //Awakeだとまだデータがロードされてないためstartで実行
        //サウンドマネージャーからSEの音量を取得
        SeSlider.value = SoundManager.seVolume;
    }

    // Update is called once per frame
    void Update()
    {
        //選択されてなかったらリターン
        if (!selectFlag)
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


        //右矢印キーで音量プラス
        if (Input.GetKey(KeyCode.RightArrow))
        {
            VolUp();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.right.isPressed)
            {
                VolUp();
            }
        }
        OnRightStick();

        //左矢印キーで音量－
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            VolDown();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.left.isPressed)
            {
                VolDown();
            }
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

        //SaveManager.saveSEVolume(SoundManager.seVolume);
    }

    // ボリュームあげる
    private void VolUp()
    {
        SeSlider.value += moveSpeed;
        //音量設定
        SoundManager.seVolume = SeSlider.value;

    }

    // ボリューム下げる
    private void VolDown()
    {
        SeSlider.value -= moveSpeed;
        //音量設定
        SoundManager.seVolume = SeSlider.value;

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
