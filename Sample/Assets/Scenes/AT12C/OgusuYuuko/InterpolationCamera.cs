//=============================================================================
//
// カメラ切り替え補間用カメラ
//
//
// 作成日:2022/03/24
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/24 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolationCamera : MonoBehaviour
{
    //移動時間
    public int MoveTime = 60;
    //方向
    Vector3 Dir;
    //タイマー
    int Timer;
    //使用フラグ
    bool useFlag;
    // Start is called before the first frame update
    void Start()
    {
        //補間用カメラ設定
        CameraSwitch.InterpolationCamera = gameObject;
        useFlag = false;
    }

    private void OnDestroy()
    {
        //補間用カメラ破棄
        CameraSwitch.InterpolationCamera = null;
    }

    // Update is called once per frame
    void Update()
    {
        //使ってなかったらリターン
        if (!useFlag)
        {
            return;
        }

        if (Timer < MoveTime)
        {
            //タイマー更新
            ++Timer;
            //位置更新
            transform.position += Dir;
        }
        else
        {
            CameraSwitch.FinishSwitching();
            useFlag = false;
        }
    }
    
    //引数 : 位置、方向
    public void StartInterpolation(Vector3 pos,Vector3 dir)
    {
        //初期位置設定
        gameObject.transform.position = pos;
        //方向設定
        Dir = dir;
        Dir /= MoveTime;
        //タイマー設定
        Timer = 0;
        //使用フラグ立てる
        useFlag = true;
    }
}
