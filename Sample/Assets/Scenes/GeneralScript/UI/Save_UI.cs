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
    private GameObject abutton;
    private GameObject AButton;

    [SerializeField]
    private GameObject selectbox;
    private GameObject SelectBox;

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
        SelectBox = Instantiate(selectbox);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);
        AButton = Instantiate(abutton);
        WarpCharacter = Instantiate(warpcharacter);
        SusumuCharacter = Instantiate(susumucharacter);

        // キャンバスの子にする
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        SelectBox.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);
        AButton.transform.SetParent(this.canvas.transform,false);
        WarpCharacter.transform.SetParent(this.canvas.transform, false);
        SusumuCharacter.transform.SetParent(this.canvas.transform, false);

        // 通常は非表示        
        SelectBox.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
        SelectBox.GetComponent<UIBlink>().isBlink = true;
        SelectBox.GetComponent<UIBlink>().isHide = true;
        SelectBox.GetComponent<Image>().enabled = false;
        SaveCharacter.GetComponent<Image>().enabled = false;
        YesCharacter.GetComponent<Image>().enabled = false;
        NoCharacter.GetComponent<Image>().enabled = false;
        AButton.GetComponent<UIBlink>().isHide = true;
        WarpCharacter.GetComponent<Image>().enabled = false;
        SusumuCharacter.GetComponent<Image>().enabled = false;

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
                // すべて非表示
                SelectBox.GetComponent<UIBlink>().isBlink = true;
                SelectBox.GetComponent<UIBlink>().isHide = true;
                SelectBox.GetComponent<Image>().enabled = false;
                SaveCharacter.GetComponent<Image>().enabled = false;
                YesCharacter.GetComponent<Image>().enabled = false;
                NoCharacter.GetComponent<Image>().enabled = false;
                AButton.GetComponent<Image>().enabled = false;
                WarpCharacter.GetComponent<Image>().enabled = false;
                SusumuCharacter.GetComponent<Image>().enabled = false;

                if (Player.isHitSavePoint)  // セーブポイントに当たったら操作方法を表示
                {
                    AButton.GetComponent<UIBlink>().isHide = false;
                }

                return; // セーブできないときは以下の処理は必要ないので返す
            }
            else
            {
                //GamePadManager.onceTiltStick = false;
                SelectBox.GetComponent<Image>().enabled = true;
                // セーブは非表示
                SaveCharacter.GetComponent<Image>().enabled = false;
                YesCharacter.GetComponent<Image>().enabled = false;
                AButton.GetComponent<UIBlink>().isHide = true;
                // ワープは表示
                WarpCharacter.GetComponent<Image>().enabled = true;
                SusumuCharacter.GetComponent<Image>().enabled = true;
                NoCharacter.GetComponent<Image>().enabled = true;

                SelectBoxPosUpdete();   // 選択枠の更新
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
            if (select == 0)    // ワープはい
            {
                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // 決定音
                    Pause.isPause = false;  // ポーズやめる
                    Warp.canWarp = false;    // ワープ可能下す
                    Warp.shouldWarp = true;  // ワープするべきなのでフラグを立てる
                    isDecision = false;
                    
                }
            }
            else
            {
                if (isDecision) // ワープいいえ
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // 決定音
                    Warp.canWarp = false;    // ワープ可能下す
                    Pause.isPause = false;  // ポーズをやめる
                    isDecision = false;
                }
            }

        }
        else
        {
            //----- セーブ -----
            if (!SaveManager.canSave)
            {
                // すべて非表示
                SelectBox.GetComponent<UIBlink>().isBlink = true;
                SelectBox.GetComponent<UIBlink>().isHide = true;
                SelectBox.GetComponent<Image>().enabled = false;
                SaveCharacter.GetComponent<Image>().enabled = false;
                YesCharacter.GetComponent<Image>().enabled = false;
                NoCharacter.GetComponent<Image>().enabled = false;
                AButton.GetComponent<UIBlink>().isHide = true;
                WarpCharacter.GetComponent<Image>().enabled = false;
                SusumuCharacter.GetComponent<Image>().enabled = false;

                if (Player.isHitSavePoint)  // セーブポイントに当たったら操作方法を表示
                {
                    AButton.GetComponent<UIBlink>().isHide = false;
                }

                return; // セーブできないときは以下の処理は必要ないので返す
            }
            else
            {
                //GamePadManager.onceTiltStick = false;
                Pause.isPause = true;

                // セーブ可能になったらUIを表示
                SelectBox.GetComponent<Image>().enabled = true;
                SaveCharacter.GetComponent<Image>().enabled = true;
                YesCharacter.GetComponent<Image>().enabled = true;
                NoCharacter.GetComponent<Image>().enabled = true;
                AButton.GetComponent<UIBlink>().isHide = true;
                // ワープは非表示
                WarpCharacter.GetComponent<Image>().enabled = false;
                SusumuCharacter.GetComponent<Image>().enabled = false;

                SelectBoxPosUpdete();   // 選択枠の更新
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
                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);   // 決定音
                    SaveManager.canSave = false;    // セーブ可能下す
                    SaveManager.shouldSave = true;  // セーブするべきなのでフラグを立てる
                    Pause.isPause = false;
                    isDecision = false;
                }
            }
            else
            {
                if (isDecision)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList); // 決定音
                    SaveManager.canSave = false;    // セーブ可能を下す
                    Pause.isPause = false;
                    isDecision = false;
                }
            }
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
        }else if(Player.isHitSavePoint && !SaveManager.canSave)
        {
            SaveManager.canSave = true;
        }
    }

    private void SelectUp() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        select--;
        if (select < 0)
        {
            select = 0;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    private void SelectDown() {
        // 音
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        select++;
        if (select > 1)
        {
            select = 0;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 選択枠の座標更新
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * 選択枠の位置の更新
         * それぞれの文字のRectTransformと合わせることで同じ位置に表示ができる
         */
        if (select == 0)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = YesCharacter.GetComponent<RectTransform>().localPosition;
        }
        else if (select == 1)
        {
            SelectBox.GetComponent<RectTransform>().localPosition = NoCharacter.GetComponent<RectTransform>().localPosition;
        }
    }

}
