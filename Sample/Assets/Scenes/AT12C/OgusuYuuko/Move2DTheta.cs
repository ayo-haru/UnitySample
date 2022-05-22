//=============================================================================
//
// 2D移動用
//
//
// 作成日:2022/04/24
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/24 作成
// 2022/05/19 下に弾かれる処理を追加
// 2022/05/20 弾かれた時の処理を別スクリプトに移動
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2DTheta : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //動かす方向
    public Vector2 dir = new Vector2(0.0f,0.0f);
    //移動速度
    public float moveSpeed = 0.1f;
    //動かす幅
    public float moveWidth = 0.5f;
    //角度
    private float theta;
    //初期位置
    private Vector3 startPos;
    //一回だけ処理するよう
    private bool onceFlag;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<RectTransform>();
        startPos = image.position;
        theta = 0.0f;
        onceFlag = true;
    }

    private void OnDisable()
    {
        theta = 0.0f;
        onceFlag = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onceFlag)
        {
            image.position = startPos;
            onceFlag = false;
        }

        //角度更新
        theta += moveSpeed;
        //角度補正
        if (theta >= 180.0f)
        {
            theta -= 360.0f;
        }
        else if (theta <= -180.0f)
        {
            theta += 360.0f;
        }

        //画像位置更新
        image.transform.position += image.transform.up * dir.y * moveWidth * (Mathf.Sin(theta));
        image.transform.position += image.transform.right * dir.x * moveWidth * (Mathf.Sin(theta));
    }
}
