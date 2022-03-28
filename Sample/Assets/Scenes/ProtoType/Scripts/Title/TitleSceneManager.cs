using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class TitleSceneManager : MonoBehaviour {
   
    //---変数宣言
    private Game_pad UIActionAssets;                                // InputActionのUIを扱う
    private InputAction LeftStickSelect;                                 // InputActionのselectを扱う
    private InputAction RightStickSelect;                                 // InputActionのselectを扱う

    private Vector2 doLeftStick = Vector2.zero;
    private Vector2 doRightStick = Vector2.zero;
    //private AudioSource[] audioSourceList = new AudioSource[5];   // 一回に同時にならせる数

    private void Awake()
    {
        UIActionAssets = new Game_pad();                            // InputActionインスタンスを生成
    }

    private void OnEnable()
    {
        //---スティックの値を取るための設定
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Actionイベントを登録
        UIActionAssets.UI.LeftStickSelect.performed += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.performed += OnRightStick;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable()
    {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }
    // Start is called before the first frame update
    void Start() {
        Application.targetFrameRate = 60;           // フレームレートを固定
        SaveManager.load();
        GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
        GameData.CurrentMapNumber = (int)GameData.eSceneState.TITLE_SCENE;
        for (int i = 0; i < SoundData.TitleAudioList.Length; ++i)
        {
            SoundData.TitleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }

        // 音鳴らす
        SoundManager.Play(SoundData.eBGM.BGM_TITLE, SoundData.TitleAudioList);

    }

    // Update is called once per frame
    void Update() {

        

        if (Input.GetKeyDown(KeyCode.Return))   // はじめから
        {
            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }

        if (Input.GetKeyDown(KeyCode.RightShift))    // つづきから
        {
            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }

    }

    private void OnLeftStick(InputAction.CallbackContext obj)
    {
        //---左ステックのステック入力を取得
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if(doLeftStick.x >=0.1f || doLeftStick.y >= 0.1f )
        {
            Debug.Log("左スティックが倒された");
            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);

        }
    }

    private void OnRightStick(InputAction.CallbackContext obj)
    {
        //---右ステックのステック入力を取得
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---少しでも倒されたら処理に入る
        if (doRightStick.x >= 0.1f || doRightStick.y >= 0.1f)
        {
            Debug.Log("右スティックが倒された");

            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);

        }
    }

    private void OnGUI()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        //---ゲームパッドとつながっている時に表示される。
        GUILayout.Label($"LeftStick:{Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"RightStick:{Gamepad.current.rightStick.ReadValue()}");
        GUILayout.Label($"ButtonNorth:{Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"ButtonSouth:{Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"ButtonEast:{Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"ButtonWast:{Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"LeftShoulder:{Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"LeftTrigger:{Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"RightShoulder:{Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"RighetTrigger:{Gamepad.current.rightTrigger.ReadValue()}");
        GUILayout.Label($"LeftStickUp:{Gamepad.current.leftStick.up.ReadValue()}");
        GUILayout.Label($"Space:{Keyboard.current.spaceKey.ReadValue()}");
        GUILayout.Label($"LeftStick:{doLeftStick}");

    }

}



