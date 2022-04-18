using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Save_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject savecharacter;
    private GameObject SaveCharacter;
    [SerializeField]
    private GameObject yescharacter;
    private GameObject YesCharacter;
    [SerializeField]
    private GameObject nocharacter;
    private GameObject NoCharacter;

    private Canvas canvas;

    private int select;

    private Game_pad UIActionAssets;                // InputActionのUIを扱う
    private InputAction LeftStickSelect;            // InputActionのselectを扱う
    private InputAction RightStickSelect;           // InputActionのselectを扱う


    // Start is called before the first frame update
    void Start()
    {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成

        select = 0;

        // キャンバスを指定
        canvas = GetComponent<Canvas>();

        // 実態化
        SaveCharacter = Instantiate(savecharacter);
        YesCharacter = Instantiate(yescharacter);
        NoCharacter = Instantiate(nocharacter);

        // キャンバスの子にする
        SaveCharacter.transform.SetParent(this.canvas.transform, false);
        YesCharacter.transform.SetParent(this.canvas.transform, false);
        NoCharacter.transform.SetParent(this.canvas.transform, false);

        // 通常は非表示
        SaveCharacter.GetComponent<UIBlink>().isHide = true;
        YesCharacter.GetComponent<UIBlink>().isHide = true;
        NoCharacter.GetComponent<UIBlink>().isHide = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }
        Keyboard keyboard = Keyboard.current;

        if (!SaveManager.canSave)
        {
            SaveCharacter.GetComponent<UIBlink>().isHide = true;
            YesCharacter.GetComponent<UIBlink>().isHide = true;
            NoCharacter.GetComponent<UIBlink>().isHide = true;

            return;
        }
        else
        {
            SaveCharacter.GetComponent<UIBlink>().isHide = false;
            YesCharacter.GetComponent<UIBlink>().isHide = false;
            NoCharacter.GetComponent<UIBlink>().isHide = false;
            Pause.isPause = true;
        }

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
            YesCharacter.GetComponent<UIBlink>().isBlink = true;
            NoCharacter.GetComponent<UIBlink>().isBlink = false;

            if (keyboard.enterKey.wasReleasedThisFrame) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                Pause.isPause = false;
                SaveManager.canSave = false;
                SaveManager.shouldSave = true;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                YesCharacter.GetComponent<UIBlink>().isBlink = false;
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
                {
                    // 決定音
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                    Pause.isPause = false;
                    SaveManager.canSave = false;
                    SaveManager.shouldSave = true;
                    YesCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    YesCharacter.GetComponent<UIBlink>().isBlink = false;

                }
            }
        }
        else
        {
            YesCharacter.GetComponent<UIBlink>().isBlink = false;
            NoCharacter.GetComponent<UIBlink>().isBlink = true;

            if (keyboard.enterKey.wasReleasedThisFrame) // 選択を確定
            {
                // 決定音
                SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                SaveManager.canSave = false;
                Pause.isPause = false;
                GameData.Player.GetComponent<Player2>().UnderParryNow = false;
                YesCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isHide = true;
                NoCharacter.GetComponent<UIBlink>().isBlink = false;
            }
            else if (isSetGamePad)
            {
                if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
                {
                    // 決定音
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                    SaveManager.canSave = false;
                    Pause.isPause = false;
                    GameData.Player.GetComponent<Player2>().UnderParryNow = false;
                    YesCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isHide = true;
                    NoCharacter.GetComponent<UIBlink>().isBlink = false;

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

}
