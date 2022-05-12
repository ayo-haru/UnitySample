//=============================================================================
//
// 風船移動
//
// 作成日:2022/05/11
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/11 実装
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMove : MonoBehaviour
{
    //初期位置
    //private Vector3 startPos;
    //移動速度
    public float moveSpeed = 0.01f;
    //上限位置
    public float finPos = 10.0f;
    //角度
    private float theta;
    // Start is called before the first frame update
    void Start()
    {
        //startPos = gameObject.transform.position;
        theta = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        theta += moveSpeed;
        if (theta >= 180.0f)
        {
            theta -= 360.0f;
        }
        //風船上下
        gameObject.transform.position += gameObject.transform.up * finPos * Mathf.Sin(theta) * 0.01f;

        //上昇、下降切り替え
        //if(transform.position.y >= finPos　||　transform.position.y <= startPos.y)
        //{
        //    moveDir *= -1;
        //}
    }
}
