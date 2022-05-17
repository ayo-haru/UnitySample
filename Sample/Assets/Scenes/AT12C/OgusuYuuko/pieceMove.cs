//ランタン移動用
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
    public float moveSpeed = 0.5f;     //移動速度
    public float moveWidth = 10.0f;     //移動幅
    public bool startFlag;                 //移動開始用フラグ

    // Start is called before the first frame update
    void Start()
    {
        theta = 0.0f;
        rt = GetComponent<RectTransform>();
        startPos = rt.position;
        ReturnFlag = false;
        FinishFlag = false;
        //startFlag = false;
        startFlag = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if(ReturnFlag && theta <= 75.0f)
        {
            FinishFlag = true;
        }
        rt.position = new Vector3(rt.position.x,startPos.y - (Mathf.Sin(Mathf.Deg2Rad * theta) * moveWidth),rt.position.z);
        Debug.Log("サイン"+Mathf.Sin(Mathf.Deg2Rad * theta));
    }
}
