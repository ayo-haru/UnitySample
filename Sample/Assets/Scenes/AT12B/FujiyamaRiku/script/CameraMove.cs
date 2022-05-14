using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラ移動させる
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera camera; //カメラ格納用(指定しなくても一応動く)
    private float g_MoveNowTime;            //現在の移動量格納用
    private float g_MoveTime;               //移動時間の格納用
    private float g_DelayTime;              //ディレイ処理の時間格納用
    private Vector3 g_CameraPos;            //カメラの座標格納用
    private Vector3 g_CameraStartPos;       //カメラの初期位置格納用
    private Vector3 g_CameraEndPos;         //カメラの到着地点格納用
    private bool ReturnFlg;                 //戻る処理をするためのフラグ
    private bool DelayFlg;                  //ディレイをかける時用のフラグ
    private bool InitFlg;                   //処理の初めに一度だけ処理する時用のフラグ
    // Start is called before the first frame update
    void Start()
    {
        //カメラを指定しない限りはメインのカメラを動かす
        camera = Camera.main;
        DelayFlg = false;
        g_MoveNowTime = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetCamera(Camera Usecamera)
    {
        camera = Usecamera;
    }

    //今現在の座標から指定した座標まで行く処理
    //ベジエ曲線で作るため第〇次ベジエ曲線対応
    //==========================================================
    //一次ベジエ曲線
    //時間指定バージョン
    //最後のboolは元の位置に同じ速度で戻せる処理を使うかどうか。
    //最後のを使わずに二度宣言して速度を変えても良い
    /// <summary>
    /// 時間を指定して、指定した座標に移動する処理(直線)
    /// <para>戻り値：処理してる間はtrue、処理の終了時にfalse</para>
    /// <para>CameraStartPos：開始地点の座標</para>
    /// <para>CameraEndPos：終了地点の座標</para>
    /// <para>MoveTime：到着までの時間</para>
    /// </summary>
    /// <param name="CameraStartPos">開始地点の座標</param>
    /// <param name="CameraEndPos">終了地点の座標</param>
    /// <param name="MoveTime">到着までの時間</param>
    /// <returns></returns>
    public bool MoveCameraTime(Vector3 CameraStartPos,Vector3 CameraEndPos, float MoveTime) //処理してる間はtrue 処理の終了時にfalseを呼ぶ
    {
        g_MoveNowTime += 1.0f / (MoveTime * 60.0f); //ベジエ曲線が1までの距離を動いてくれるので
                                              //時間指定するためにフレーム数をかけてから1を割っている
                                              //それを足すことで〇秒を実現してる(1 / 動かす時間 * １秒間に処理するフレーム数)
                            
        g_CameraPos = Vector3.Lerp(CameraStartPos, CameraEndPos, g_MoveNowTime);
        //カメラの座標更新処理
        camera.transform.position = g_CameraPos;
        if (g_MoveNowTime >=1.0f) //ベジエ曲線が1に到達していた場合(エンドポイントに到着していた場合)
        {
            return false;
        }
        return true;
    }

    //==========================================================
    /// <summary>
    /// 時間指定で指定した座標へ移動し、その後元に戻ってくる処理(直線)
    /// <para>戻り値：処理してる間は０、目的の座標についた瞬間１、元の位置に戻ってきたとき２</para>
    /// <para>CameraStartPos：開始地点の座標</para>
    /// <para>CameraEndPos：終了地点の座標</para>
    /// <para>MoveTime：到着までの時間</para>
    /// <para>ReturnMoveTIme：戻ってくるまでの時間</para>
    /// <para>ReturnDelayTime：ディレイの時間</para>
    /// </summary>
    /// <param name="CameraStartPos">開始地点の座標</param>
    /// <param name="CameraEndPos">終了地点の座標</param>
    /// <param name="MoveTime">到着までの時間</param>
    /// <param name="ReturnMoveTime">戻ってくるまでの時間</param>
    /// <param name="ReturnDelayTime">ディレイの時間</param>
    /// <returns></returns>
    //戻ってくる時間を指定した場合元の座標に指定した速度で戻ってくる
    public int MoveCameraTime(Vector3 CameraStartPos, Vector3 CameraEndPos, 
                              float MoveTime, float ReturnMoveTime , float ReturnDelayTime) //処理してる間は0 一度目的の座標についた瞬間１ 戻ってきた瞬間２
    {
        //最初に一回だけ処理する部分
        if (!InitFlg)
        {
            g_MoveTime = MoveTime;              //最初に到着するまでの時間を格納
            g_CameraStartPos = CameraStartPos;  //初期地点を格納
            g_CameraEndPos = CameraEndPos;      //終了地点を格納
            InitFlg = true;                     
        }

        //戻るまでのディレイ処理
        if (DelayFlg)
        {
            //一フレームごとに足し算していく(Updateが基本フレームごとに更新してくれるから動く)
            g_DelayTime += ((1.0f / 60.0f));
            //〇秒経過したらディレイの処理をやめて戻る処理に移行する
            if (g_DelayTime >= ReturnDelayTime)
            {
                DelayFlg = false;
            }
        }

        //ディレイ処理がされていない時(カメラが動いている処理をしているとき)
        else
        {
            g_MoveNowTime += 1 / (MoveTime * 60); //ベジエ曲線が1までの距離を動いてくれるので
                                                  //時間指定するためにフレーム数をかけてから1を割っている
                                                  //それを足すことで〇秒を実現してる(1 / 動かす時間 * １秒間に処理するフレーム数)

            //スタートとエンドの座標を切り替えても問題ないよう変数で処理を入れている
            g_CameraPos = Vector3.Lerp(g_CameraStartPos, g_CameraEndPos, g_MoveNowTime);
            //カメラの座標更新
            camera.transform.position = g_CameraPos;
        }

        if (g_MoveNowTime >= 1.0f) //ベジエ曲線が1に到達していた場合
        {
            Debug.Log("きゃめら" + ReturnFlg);
            if (ReturnFlg)//元の位置に戻ってきたときにこの処理が終わったことにする
            {
                g_MoveNowTime = 0.0f;
                InitFlg = false;
                ReturnFlg = false;
                return 2;
            }
            
            //一回目の処理が終わったときに戻る処理に変更する
            g_MoveNowTime = 0.0f;               //変数一つで時間を再利用
            MoveTime = ReturnMoveTime;          //帰りの時間を代入
            g_CameraStartPos = CameraEndPos;    //帰りの時の出発地点の座標を変更
            g_CameraEndPos = CameraStartPos;    //帰りの時の到着地点の座標を変更
            ReturnFlg = true;                   //ここで元の位置に戻る処理にする
            DelayFlg = true;                    //ディレイをかける処理用フラグon
            return 1;
        }
        return 0;
    }

    //==========================================================
    /// <summary>
    /// 速度を指定して、指定した座標に移動する処理(直線)
    /// <para>戻り値：処理してる間はtrue、処理の終了時にfalse</para>
    /// <para>CameraStartPos：開始地点の座標</para>
    /// <para>CameraEndPos：終了地点の座標</para>
    /// <para>MoveSpeed：動く速度</para>
    /// </summary>
    /// <param name="CameraStartPos">開始地点の座標</param>
    /// <param name="CameraEndPos">終了地点の座標</param>
    /// <param name="MoveSpeed">動く速度</param>
    /// <returns></returns>
    //スピード指定バージョン
    public bool MoveCameraSpeed(Vector3 CameraStartPos, Vector3 CameraEndPos, float MoveSpeed) //処理してる間はtrue 処理の終了時にfalseを呼ぶ
    {
        g_MoveNowTime += (1.0f / 60.0f) * MoveSpeed; //ベジエ曲線が1までの距離を動いてくれるので
                                               //速度を指定して１フレームで動く距離を変えている
                                               //初期値の〇倍の速度などができる。
                                               
        g_CameraPos = Vector3.Lerp(CameraStartPos, CameraEndPos, g_MoveNowTime);
        //カメラの座標更新
        camera.transform.position = g_CameraPos;

        if (g_MoveNowTime >= 1.0f) //ベジエ曲線が1に到達していた場合(エンドポイントに到着していた場合)
        {
            g_MoveNowTime = 0.0f;
            return false;
        }
        return true;
    }

    //==========================================================
    /// <summary>
    /// 速度指定で指定した座標へ移動し、その後元の位置に戻ってくる処理(直線)
    /// <para>戻り値：処理してる間は０、目的の座標についた瞬間１、元の位置に戻ってきたとき２</para>
    /// <para>CameraStartPos：開始地点の座標</para>
    /// <para>CameraEndPos：終了地点の座標</para>
    /// <para>MoveSpeed：動く速度</para>
    /// <para>ReturnMoveSpeed：戻ってくるときの速度</para>
    /// <para>ReturnDelayTime：ディレイの時間</para>
    /// </summary>
    /// <param name="CameraStartPos">開始地点の座標</param> 
    /// <param name="CameraEndPos">終了地点の座標</param>
    /// <param name="MoveSpeed"> 動く速度</param>
    /// <param name="ReturnMoveSpeed">戻ってくるときの速度</param>
    /// <param name="ReturnDelayTime">ディレイの時間</param>
    /// <returns></returns>
    //スピード指定で元の場所に戻るバージョン
    public int MoveCameraSpeed(Vector3 CameraStartPos, Vector3 CameraEndPos,
                              float MoveSpeed, float ReturnMoveSpeed, float ReturnDelayTime) //処理してる間は0 一度目的の座標についた瞬間１ 戻ってきた瞬間２
    {
        //最初に一回だけ処理する部分
        if (!InitFlg)
        {
            g_MoveTime = MoveSpeed;              //最初に到着するまでの時間を格納
            g_CameraStartPos = CameraStartPos;  //初期地点を格納
            g_CameraEndPos = CameraEndPos;      //終了地点を格納
            InitFlg = true;
        }

        //戻るまでのディレイ処理
        if (DelayFlg)
        {
            //一フレームごとに足し算していく(Updateが基本フレームごとに更新してくれるから動く)
            g_DelayTime += ((1.0f / 60.0f));
            //〇秒経過したらディレイの処理をやめて戻る処理に移行する
            if (g_DelayTime >= ReturnDelayTime)
            {
                DelayFlg = false;
            }
        }

        //ディレイ処理がされていない時(カメラが動いている処理をしているとき)
        else
        {
            g_MoveNowTime += (1.0f / 60.0f) * MoveSpeed; //ベジエ曲線が1までの距離を動いてくれるので
                                                         //速度指定のため１フレームで動く分を
                                                         //実質的にスピード分のフレーム動かすことで速度の変化が起こっているように見せる
                                                         //それを足すことで(基本の速度 * 指定した速度)

            //スタートとエンドの座標を切り替えても問題ないよう変数で処理を入れている
            g_CameraPos = Vector3.Lerp(g_CameraStartPos, g_CameraEndPos, g_MoveNowTime);
            //カメラの座標更新
            camera.transform.position = g_CameraPos;
        }

        if (g_MoveNowTime >= 1.0f) //ベジエ曲線が1に到達していた場合
        {
            Debug.Log("きゃめら" + ReturnFlg);
            if (ReturnFlg)//元の位置に戻ってきたときにこの処理が終わったことにする
            {
                g_MoveNowTime = 0.0f;
                InitFlg = false;
                ReturnFlg = false;
                return 2;
            }

            //一回目の処理が終わったときに戻る処理に変更する
            g_MoveNowTime = 0.0f;               //変数一つで時間を再利用
            MoveSpeed = ReturnMoveSpeed;          //帰りの速度を代入
            g_CameraStartPos = CameraEndPos;    //帰りの時の出発地点の座標を変更
            g_CameraEndPos = CameraStartPos;    //帰りの時の到着地点の座標を変更
            ReturnFlg = true;                   //ここで元の位置に戻る処理にする
            DelayFlg = true;                    //ディレイをかける処理用フラグon
            return 1;
        }
        return 0;
    }

    //==========================================================
    //二次ベジエ曲線


}
