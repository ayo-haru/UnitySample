//=============================================================================
//
// マップ表示
//
//
// 作成日:2022/04/23
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/16 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MAP_UI : MonoBehaviour {
    public GameObject DisplayMAP;
    private Game_pad UIActionAssets;        // InputActionのUIを扱う
    private bool isMapButton;
    // Start is called before the first frame update
    void Awake() {
        UIActionAssets = new Game_pad();            // InputActionインスタンスを生成
        isMapButton = false;

        //---MAP表示
        GameObject canvas;
        canvas = GameObject.Find("Canvas2");
        if (!canvas)
        {
            canvas = GameObject.Find("Canvas");
        }

        DisplayMAP = Instantiate(DisplayMAP);
        DisplayMAP.transform.SetParent(canvas.transform, false);
    }

    private void Start() {
        DisplayMAP.SetActive(false);
        //HPが表示されていたら
        //HPの手前に表示する
        GameObject hpUI = GameObject.Find("HPSystem(2)(Clone)");
        if (hpUI)
        {
            int hpUIIndex = hpUI.transform.GetSiblingIndex();
            DisplayMAP.transform.SetSiblingIndex(hpUIIndex + 1);
        }
    }

    // Update is called once per frame
    void Update() {
        ////ｆ１押したらマップ表示
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    DisplayMAP.SetActive(true);
        //}
        ////ｆ２押したらマップ非表示
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    DisplayMAP.SetActive(false);
        //}
        //Mキーで表示非表示切り替え
        //if (Input.GetKeyDown(KeyCode.M))
        if (isMapButton)
        {
            DisplayMAP.SetActive(!DisplayMAP.activeSelf);
        }
        isMapButton = false;
    }
    private void OnEnable() {
        UIActionAssets.UI.Map.canceled += OnMap;

        //---InputActionの有効化
        UIActionAssets.UI.Enable();

    }

    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }


    private void OnMap(InputAction.CallbackContext obj) {
        isMapButton = true;
    }
}