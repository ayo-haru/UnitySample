//=============================================================================
//
// 設定画面表示
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

public class OptionOpen : MonoBehaviour
{
    public GameObject Option;

    // Start is called before the first frame update
    void Awake()
    {
        //---設定画面表示
        GameObject canvas = GameObject.Find("Canvas");
        Option = Instantiate(Option);
        Option.transform.SetParent(canvas.transform, false);
    }

    private void Start()
    {
        //初めは非表示
        Option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //f3で表示
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Option.SetActive(true);
            return;
        }
        //f４で非表示
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Option.SetActive(false);
            return;
        }
    }
}
