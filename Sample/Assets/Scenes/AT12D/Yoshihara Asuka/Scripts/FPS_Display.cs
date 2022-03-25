//=============================================================================
//
// FPS表示
//
// 作成日:2022/03/26
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/26   作成
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Display : MonoBehaviour
{
    //---変数宣言
    private int FrameCount;                             // フレームが書き出された回数をカウント
    private float PrevTime;                             // 経過時間
    private float FPS;                                  // FPS値
    // Start is called before the first frame update
    void Start()
    {
        //---初期化処理
        FrameCount = 0;
        PrevTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FrameCount++;                                  // Updateの最初にカウントする。(フレームが書き出された回数となる)
        float time = Time.realtimeSinceStartup - PrevTime;
        
        
        if(time >= 0.5f)                               // 0.5秒ごとにFPS値を算出
        {
            FPS = FrameCount / time;                   // 毎フレーム、time(0.5秒)を割ることで、FPSの近似値を算出
            Debug.Log("FPS値:"+ FPS);

            FrameCount = 0;
            PrevTime = Time.realtimeSinceStartup;
        }
    }

    private void OnGUI()
    {
        // FPSの値をUI表示
        GUILayout.Label(FPS.ToString());
    }
}
