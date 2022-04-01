//=============================================================================
//
// ズームイン
//
//ゲームオーバー用に作った
//指定されたターゲットを追従する
//
// 作成日:2022/03/20
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/20 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    //ターゲットオブジェクト
    public GameObject TargetObject;
    //ターゲットから見たカメラ位置
    public Vector3 cameraPos = new Vector3(0.0f, 0.0f, -2.0f);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = TargetObject.transform.position + cameraPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = TargetObject.transform.position + cameraPos;
    }
}
