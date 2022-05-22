//=============================================================================
//
// UIパリイ
//
// 作成日:2022/05/20
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/20    作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parry : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //弾かれた時の速さ
    public float ParrySpeed = 10.0f;
    //一回だけ処理するよう
    private bool onceFlag;
    //初期位置
    private Vector3 startPos;
    //弾かれたか
    public bool underParryFlag;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RectTransform>();
        startPos = image.position;
        onceFlag = true;
        underParryFlag = false;
    }

    private void OnDisable()
    {
        onceFlag = true;
        underParryFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onceFlag)
        {
            image.position = startPos;
            onceFlag = false;
        }

        if(underParryFlag)
        image.transform.position += image.transform.up * -ParrySpeed;
    }
}
