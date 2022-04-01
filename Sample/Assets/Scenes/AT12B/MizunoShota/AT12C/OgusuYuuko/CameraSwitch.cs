//=============================================================================
//
// カメラ切り替え用
//
// StartSwitchingでカメラの切り替えができます
// 引数には現在のカメラ、次のカメラ、切り替えの間を補間するか（する場合はtrue）を指定してください
// 補間をする場合はシーンに補間用のカメラを1つ作って、それにInterpolationCamera.csを追加して下さい。
// ↑これやらないと補間してくれないです
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

public static class CameraSwitch
{
    private static GameObject NextCamera = null;
    public static GameObject InterpolationCamera = null;
    //カメラ切り替え
    //引数 : 今のカメラ、次のカメラ、間を補完するか
    public static void StartSwitching(GameObject Nowcam, GameObject Nextcam, bool b_interpolation)
    {
        //カメラ保存
        NextCamera = Nextcam;

        //今のカメラオフ
        Nowcam.GetComponent<Camera>().enabled = false;
        //補間しない場合と補間用カメラが無い場合はカメラ切り替えてリターン
        if (!b_interpolation || InterpolationCamera == null)
        {
            //次のカメラオン
            Nextcam.GetComponent<Camera>().enabled = true;
            return;
        }

        //方向の計算
        Vector3 dir = Nextcam.transform.position - Nowcam.transform.position;
        //補間用カメラに変数をセット
        InterpolationCamera.GetComponent<InterpolationCamera>().StartInterpolation(Nowcam.transform.position, dir);
        //補間用カメラ有効化
        InterpolationCamera.GetComponent<Camera>().enabled = true;
    }

    public static void FinishSwitching()
    {
        //補間用カメラオフ
        InterpolationCamera.GetComponent<Camera>().enabled = false;
        //次のカメラオン
        NextCamera.GetComponent<Camera>().enabled = true;

        NextCamera = null;
    }



}
