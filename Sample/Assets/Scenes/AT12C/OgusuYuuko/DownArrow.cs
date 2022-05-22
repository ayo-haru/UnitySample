//=============================================================================
//
// マップの現在地を示す下矢印
//
//
// 作成日:2022/04/24
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/24 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownArrow : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //角度
    private float theta;
    //動かす速さ
    public float moveSpeed = 0.1f;
    //動かす幅
    public float moveWidth = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<RectTransform>();
        theta = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //theta更新
        theta += moveSpeed;
        //theta補正
        if(theta >= 180.0f)
        {
            theta -= 360.0f;
        }else if(theta <= -180.0f)
        {
            theta += 360.0f;
        }
        //画像位置更新
        image.transform.position += image.transform.up * moveWidth * (Mathf.Sin(theta));
    }
}
