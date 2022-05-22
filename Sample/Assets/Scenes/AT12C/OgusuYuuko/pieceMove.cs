//=============================================================================
//
// UI移動用
//
// 作成日:2022/05/17
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/17 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceMove : MonoBehaviour
{
    private float theta;
    private RectTransform rt;
    private Vector3 startPos;           //初期位置
    private bool ReturnFlag;            //折り返し用フラグ
    private bool FinishFlag;            //終了フラグ
    private bool VibrationFlag;         //振動用フラグ
    public float moveSpeed = 0.5f;     //移動速度
    public float vibrationSpeed = 10.0f;        //振動速度
    public float moveWidth = 10.0f;     //移動幅
    public float vibrationWidth = 10.0f;        //振動幅
    public bool startFlag = true;                 //移動開始用フラグ
    private Vector3 defaultPos;             //規定位置
    

    // Start is called before the first frame update
    void Start()
    {
        theta = 0.0f;
        rt = GetComponent<RectTransform>();
        startPos = rt.position;
        defaultPos = new Vector3(0.0f, 0.0f, 0.0f);
        ReturnFlag = false;
        FinishFlag = false;
        VibrationFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //振動処理
        if (VibrationFlag)
        {

            //theta更新
            theta += vibrationSpeed;

            rt.position += rt.right * Mathf.Sin(theta) * vibrationWidth;

            if(theta >= 90 || theta <= -90)
            {
                rt.position = defaultPos;
                VibrationFlag = false;
            }

            //終わったらリターン
            return;

        }
        if (FinishFlag||!startFlag)
        {
            //終わってたら何もせずにリターン
            return;
        }

        //theta更新
        theta += moveSpeed;
        if(theta >= 90.0f && !ReturnFlag)
        {
            moveSpeed *= -1;
            ReturnFlag = true;
        }
        rt.position = new Vector3(rt.position.x,startPos.y - (Mathf.Sin(Mathf.Deg2Rad * theta) * moveWidth),rt.position.z);
        if(ReturnFlag && theta <= 75.0f)
        {
            defaultPos = rt.position;
            FinishFlag = true;
        }
    }

    public void vibration()
    {
        //移動中だったらリターン
        if (!FinishFlag || VibrationFlag)
        {
            return;
        }


        VibrationFlag = true;
        theta = 0.0f;

    }
}
