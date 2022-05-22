//=============================================================================
//
// 雲移動
//
// 作成日:2022/05/12
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/12 実装
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    //移動速度
    private float moveSpeed;
    //移動範囲
    private float moveWidth;
    //角度
    private float theta;
    // Start is called before the first frame update
    void Start()
    {
        //ランダムで移動速度と移動を決定
        moveSpeed = Random.Range(1, 5) / 1000.0f;
        moveWidth = Random.Range(10, 20) / 100.0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!Pause.isPause)
        {
            theta += moveSpeed;
            if (theta >= 180.0f)
            {
                theta -= 360.0f;
            }
            //移動
            gameObject.transform.position += gameObject.transform.right * moveWidth * Mathf.Sin(theta);
        }
    }
}
