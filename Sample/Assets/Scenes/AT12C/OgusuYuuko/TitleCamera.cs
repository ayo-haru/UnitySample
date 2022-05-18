//=============================================================================
//
// タイトルのカメラ用
//
// 作成日:2022/05/18
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/18 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 middlePos;
    public Vector3 endPos;
    public bool startFlag;
    private float time;
    public float rotSpeed = 1.0f;
    public float rotTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        startFlag = false;
        time = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startFlag)
        {
            time += Time.deltaTime * 2;
            transform.position = Beziercurve.SecondCurve(startPos, middlePos, endPos, time);
            if(time <= rotTime)
            {
                transform.Rotate(new Vector3(0.0f, -rotSpeed, 0.0f));
            }
        }
    }
}
