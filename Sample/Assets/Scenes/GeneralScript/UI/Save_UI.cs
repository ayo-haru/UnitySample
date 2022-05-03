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
    private GameObject warpcharacter;
    private GameObject WarpCharacter;
    [SerializeField]
    private GameObject susumucharacter;
    private GameObject SusumuCharacter;

    [SerializeField]
    private GameObject stick;
    private GameObject Stick;

    private Canvas canvas;                  // このシーンのキャンバスを保存

    private int select;                     // 選択を保存
    private bool isDecision;                // 決定された

    private Game_pad UIActionAssets;        // InputActionのUIを扱う
    private InputAction LeftStickSelect;    // InputActionのselectを扱う
    private InputAction RightStickSelect;   // InputActionのselectを扱う


    // Start is called before the first frame update
    void Awake()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成

        select = 0; // 選択を初期化
        isDecision = false; // 決定を初期化

        // キャンバスを指定
        canvas = GetComponent<Canvas>();    // キャンバスを保存

        // 実態化
        SaveCharacter = Instantiate(savecharacter);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);
        Stick = Instantiate(stick);
        WarpCharacter = Instantiate(warpcharacter);
        SusumuCharacter = Instantiate(susumucharacter);

        // キャンバスの子にする
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);
        Stick.transform.SetParent(this.canvas.transform,false);
        WarpCharacter.transform.SetParent(this.canvas.transform, false);
        SusumuCharacter.transform.SetParent(this.canvas.transform, false);

        // 通常は非表示
        SaveCharacter.GetComponent<UIBlink>().isHide = true;
        YesCharacter.GetComponent<UIBlink>().isHide = true;
        NoCharacter.GetComponent<UIBlink>().isHide = true;
        Stick.GetComponent<UIBlink>().isHide = true;
        WarpCharacter.GetComponent<UIBlink>().isHide = true;
        SusumuCharacter.GetComponent<UIBlink>().isHide = true;

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

        if (Player.HitSavePointColorisRed)
        {
            //----- ワープ -----
            if (!Warp.canWarp)
            {
                // セーブは非表示
                SaveCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ワープは隠す
                WarpCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isBlink = false;

                if (Player.isHitSavePoint)  // セーブポイントに当たったら操作方法を表示
                {
                    Stick.GetComponent<UIBlink>().isHide = false;
                }

                return; // セーブできないときは以下の処理は必要ないので返す
            }
            else
            {
                GamePadManager.onceTiltStick = false;
                // セーブは非表示
                SaveCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ワープは表示
                WarpCharacter.GetComponent<UIBlink>().isHide = false;
                SusumuCharacter.GetComponent<UIBlink>().isHide = false;
                NoCharacter.GetComponent<RectTransform>().localPosition = new Vector3(327.0f,115.0f,0.0f);
                NoCharacter.GetComponent<UIBlink>().isHide = false;


                //Pause.isPause = true;   // UI表示時はポーズ
                Debug.Log("UI表示時ポーズ");
            }
            // ワープ可能になったら選択させる
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

            // 選択の決定
            if (select == 0)
            {
                SusumuCharacter.GetComponent<UIBlink>().isBlink = true;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // 決定音
                    Pause.isPause = false;  // ポーズやめる
                    Debug.Log("ワープするの解除");
                    Warp.canWarp = false;    // ワープ可能下す
                    Warp.shouldWarp = true;  // ワープするべきなのでフラグを立てる
                    SusumuCharacter.GetComponent<UIBlink>().isHide = true; // UI表示を隠す
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    SusumuCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
                    isDecision = false;
                    
                }
            }
            else
            {
                SusumuCharacter.GetComponent<UIBlink>().isBlink = false;
                NoCharacter.GetComponent<UIBlink>().isBlink = true;

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // 決定音
                    Warp.canWarp = false;    // ワープ可能下す
                    Pause.isPause = false;  // ポーズをやめる
                    Debug.Log("ワープしないの解除");
                    SusumuCharacter.GetComponent<UIBlink>().isHide = true; // UI表示を隠す
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
                    isDecision = false;
                }
            }

        }
        else
        {
            //----- セーブ -----
            if (!SaveManager.canSave)
            {
                // セーブ可能でないときはUIを隠す
                SaveCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ワープは隠す
                WarpCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isHide = true;

                if (Player.isHitSavePoint)  // セーブポイントに当たったら操作方法を表示
                {
                    Stick.GetComponent<UIBlink>().isHide = false;
                }

                return; // セーブできないときは以下の処理は必要ないので返す
            }
            else
            {
                GamePadManager.onceTiltStick = false;

                // セーブ可能になったらUIを表示
                SaveCharacter.GetComponent<UIBlink>().isHide = false;
                YesCharacter.GetComponent<UIBlink>().isHide = false;
                NoCharacter.GetComponent<RectTransform>().localPosition = new Vector3(146.0f, 115.0f, 0.0f);
                NoCharacter.GetComponent<UIBlink>().isHide = false;
                Stick.GetComponent<UIBlink>().isHide = true;
                // ワープは非表示
                WarpCharacter.GetComponent<UIBlink>().isHide = true;
                SusumuCharacter.GetComponent<UIBlink>().isHide = true;


                //Pause.isPause = true;   // UI表示時はポーズ
                Debug.Log("セーブUI表示時のポーズ");
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

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // 決定音
                    Pause.isPause = false;  // ポーズやめる
                    Debug.Log("セーブするのポーズ");
                    SaveManager.canSave = false;    // セーブ可能下す
                    SaveManager.shouldSave = true;  // セーブするべきなのでフラグを立てる
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UI表示を隠す
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    YesCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
                    isDecision = false;
                }
            }
            else
            {
                YesCharacter.GetComponent<UIBlink>().isBlink = false;   // 点滅を消す
                NoCharacter.GetComponent<UIBlink>().isBlink = true;     // UIを点滅

                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // 決定音
                    Pause.isPause = false;  // ポーズをやめる
                    Debug.Log("セーブしないのポーズ");
                    SaveManager.canSave = false;    // セーブ可能を下す
                    YesCharacter.GetComponent<UIBlink>().isHide = true; // UIを消す
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;    // UIの点滅を消す
                    isDecision = false;
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
        UIActionAssets.UI.Decision.started += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj) {
        if (SaveManager.canSave || Warp.canWarp)
        {
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
    }

    private void OnRightStick(InputAction.CallbackContext obj) {
        if (SaveManager.canSave || Warp.canWarp)
        {
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

    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if (SaveManager.canSave || Warp.canWarp)
        {
            isDecision = true;
        }
    }
}
