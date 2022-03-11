//=============================================================================
//
// プレイヤーの追従カメラ[FollowCamera]
//
// 作成日:2022/03/08
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/08 作成
// 2022/03/10 カメラ追従の値を変更、インスペクターで変更できるようにした。	
// 2022/03/10 マップ遷移を追加
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraProto : MonoBehaviour
{
    //---変数宣言
    public Vector3 FollowCameraPos = new Vector3(0.0f,1.5f,-6.0f);     // 追従するカメラの高さ(x,y,z)
    public int RightScreenOut = 40;          // 右の画面外設定
    public int LeftScreenOut = 0;           // 左の画面外設定
    public float MovePoint;             // マップ遷移するための地点
    bool MoveFlg;                       // マップ遷移のフラグ

    // Start is called before the first frame update
    void Start()
    {
        //---追従するオブジェクト名を設定
        this.MoveFlg = false;    
    }

    // Update is called once per frame
    void Update()
    {
        //---プレイヤーに追従する
        Vector3 PlayerPos = GameData.PlayerPos;

        // *****座標*****
        //transform.position = new Vector3(PlayerPos.x,0.7f, PlayerPos.z - 4.0f);
        transform.position = new Vector3(PlayerPos.x, 
                                         PlayerPos.y + FollowCameraPos.y, 
                                         FollowCameraPos.z);                       // ジャンプ追従

        //---画面外設定(x = 45.0fの地点に到達したらカメラの移動を停止)
        if (this.MoveFlg == false && PlayerPos.x > RightScreenOut)
        {
            //transform.position = new Vector3(45.0f, 0.7f, PlayerPos.z - 4.5f);
            transform.position = new Vector3(RightScreenOut, 
                                             PlayerPos.y + FollowCameraPos.y, 
                                             FollowCameraPos.z);         // ジャンプ追従
            //if (PlayerPos.x >= MovePoint){
            //    this.MoveFlg = true;
            //    if(this.MoveFlg == true){
            //        this.transform.position = new Vector3(60.0f, 1.5f, -4.0f);    // カメラの場所を再定
            //    }
            //}
        }


        //---画面外設定(x = 15.0fの地点に到達したらカメラの移動を停止)
        else if (PlayerPos.x < LeftScreenOut)
        {
            //transform.position = new Vector3(15.0f, 0.7f, PlayerPos.z - 4.5f);
            transform.position = new Vector3(LeftScreenOut, 
                                             PlayerPos.y + FollowCameraPos.y, 
                                             FollowCameraPos.z);          // ジャンプ追従
        }
    }
}
