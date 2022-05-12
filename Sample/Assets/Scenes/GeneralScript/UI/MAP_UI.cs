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

public class MAP_UI : MonoBehaviour
{
    public GameObject DisplayMAP;
    // Start is called before the first frame update
    void Awake()
    {
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

    private void Start()
    {
        DisplayMAP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            DisplayMAP.SetActive(!DisplayMAP.activeSelf);
        }

    }
}
