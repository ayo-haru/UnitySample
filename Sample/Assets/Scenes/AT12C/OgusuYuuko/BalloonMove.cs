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
    private Vector3 startPos;
    //移動速度
    public float moveSpeed = 0.1f;
    //上限位置
    public float finPos = 100.0f;
    //移動方向
    private float moveDir;  //１が上昇－１が下降
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        moveDir = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //風船上下
        gameObject.transform.position += gameObject.transform.up * moveSpeed * moveDir;

        //上昇、下降切り替え
        if(transform.position.y >= finPos　||　transform.position.y <= startPos.y)
        {
            moveDir *= -1;
        }
    }
}
