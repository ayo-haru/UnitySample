//=============================================================================
//
// セーブポイントによるセーブのUI管理
//
// 作成日:2022/04
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/04/19 セーブポイントによるセーブ実装
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Save_UI : MonoBehaviour
{
    // 表示するUIの変数
    [SerializeField]
    private GameObject savecharacter;
    private GameObject SaveCharacter;
    [SerializeField]
    private GameObject yescharacter;
    private GameObject YesCharacter;
    [SerializeField]
    private GameObject nocharacter;
    private GameObject NoCharacter;
    [SerializeField]
    private GameObject stick;
    private GameObject Stick;

    private Canvas canvas;  // このシーンのキャンバスを保存

    private int select; // 選択を保存

    private Game_pad UIActionAssets;                // InputActionのUIを扱う
    private InputAction LeftStickSelect;            // InputActionのselectを扱う
    private InputAction RightStickSelect;           // InputActionのselectを扱う


    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成

        select = 0; // 選択を初期化

        // キャンバスを指定
        canvas = GetComponent<Canvas>();

        // 実態化
        SaveCharacter = Instantiate(savecharacter);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);
        Stick = Instantiate(stick);

        // キャンバスの子にする
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);
        Stick.transform.SetParent(this.canvas.transform,false);

        // 通常は非表示
        SaveCharacter.GetComponent<UIBlink>().isHide = true;
        YesCharacter.GetComponent<UIBlink>().isHide = true;
        NoCharacter.GetComponent<UIBlink>().isHide = true;
        Stick.GetComponent<UIBlink>().isHide = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 入力初期化処理
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;


        if (!SaveManager.canSave)
        {
            // セーブ可能でないときはUIを隠す
            SaveCharacter.GetComponent<UIBlink>().isHide = true;
            YesCharacter.GetComponent<UIBlink>().isHide = true;
            NoCharacter.GetComponent<UIBlink>().isHide = true;
            YesCharacter.GetComponent<UIBlink>().isBlink = false;
            NoCharacter.GetComponent<UIBlink>().isBlink = false;
            Stick.GetComponent<UIBlink>().isHide = true;
            if (Player.isHitSavePoint)  // セーブポイントに当たったら操作方法を表示
            {
                Stick.GetComponent<UIBlink>().isHide = false;
            }

            return; // セーブできないときは以下の処理は必要ないので返す
        }
        else
        {
            // セーブ可能になったらUIを表示
            SaveCharacter.GetComponent<UIBlink>().isHide = false;
            YesCharacter.GetComponent<UIBlink>().isHide = false;
            NoCharacter.GetComponent<UIBlink>().isHide = false;
            Stick.GetComponent<UIBlink>().isHide = true;

            Pause.isPause = true;   // UI表示時はポーズ
        }


        // セーブ可能になったら選択させる
        if (keyboard.leftArrowKey.wasReleasedThisFrame)
        {
            SelectUp();
            return;
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                SelectUp();
                return;
            }
        }
        if (keyboard.rightArrowKey.wasReleasedThisFrame)
        {
            SelectDown();
            return;
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                SelectDown();
                return;
            }
        }


        // 選択しているものが何かで分岐
        if (select == 0)
        {
            YesCharacter.GetComponent<UIBlink>().isBlink = true;    // UIを点滅
            NoCharacter.GetComponent<UIBlink>().isBlink = false;    // 点滅を消す

            if (keyboard.enterKey.wasReleasedThisFrame) // 選択を確定
            {
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // 決定音
                Pause.isPause = false;  // ポーズやめる
                SaveManager.canSave = false;    // セーブ可能下す
                SaveManager.shouldSave = true;  // セーブするべきなのでフラグを立てる
                YesCharacter.GetComponent<UIBlink>().isHide = true; // UI表示を隠す
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // 決定音
                    Pause.isPause = false;  // ポーズやめる
                    SaveManager.canSave = false;    // セーブ可能下す
                    SaveManager.shouldSave = true;  // セーブするべきなのでフラグを立てる
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UI表示を隠す
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    YesCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
                }
            }
        }
        else
        {
            YesCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
            NoCharacter.GetComponent<UIBlink>().isBlink = true;     // UIを点滅

            if (keyboard.enterKey.wasReleasedThisFrame) // 選択を確定
            {
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // 決定音
                SaveManager.canSave = false;    // セーブ可能を下す
                Pause.isPause = false;  // ポーズをやめる
                YesCharacter.GetComponent<UIBlink>().isHide = true; // UIを消す
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;    // UIの点滅を消す
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // 決定音
                    SaveManager.canSave = false;    // セーブ可能を下す
                    Pause.isPause = false;  // ポーズをやめる
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UIを消す
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;    // UIの点滅を消す
                }
            }
        }
    }

    private void SelectUp() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown() {
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select > 1)
        {
            select = 0;
        }
    }

    private void OnEnable() {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Actionイベントを登録
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (!SaveManager.canSave)
        {
            return;
        }

        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doLeftStick.x > 0.0f)
        {
            SelectDown();
        }
        else if (doLeftStick.x < 0.0f)
        {
            SelectUp();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (!SaveManager.canSave)
        {
            return;
        }

        //---右ステックのステック入力を取得
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.x > 0.0f)
        {
            SelectDown();
        }
        else if (doRightStick.x < 0.0f)
        {
            SelectUp();
        }

    }

}
