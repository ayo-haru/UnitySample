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

public class FollowCamera : MonoBehaviour
{
    //---変数宣言
    GameObject Player;
    Player2 player2;
    Vector3 Offset;                                                         // 相対距離取得用
    public Vector3 FollowCameraPos = Vector3.zero;     // 追従するカメラの高さ(x,y,z)
    public int RightScreenOut;          // 右の画面外設定
    public int LeftScreenOut;           // 左の画面外設定
    public int HeightScreenOut;         // 上の画面外設定
    public int UnderScreenOut;          // 下の画面外設定

    // Start is called before the first frame update
    void Start()
    {
        //---追従するオブジェクト名を設定
        this.Player = GameObject.Find(GameData.Player.name);
        player2 = Player.GetComponent<Player2>();

        // カメラとPlayerの相対距離を求める
        Offset = transform.position - Player.transform.position;

        Debug.Log("Offset"+Offset); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //---プレイヤーに追従する

        Vector3 PlayerPos = GameData.PlayerPos;

		// *****座標*****
		//transform.position = new Vector3(PlayerPos.x,
		//                         FollowCameraPos.y,
		//                         FollowCameraPos.z);                               // ジャンプ追従

		//transform.position = new Vector3(PlayerPos.x,
		//								 PlayerPos.y + FollowCameraPos.y,
		//								 FollowCameraPos.z);                       // ジャンプ追従

		//---前フレームの座標で追従
		//---佐々木先生プログラム
		Vector3 move = (player2.OldPlayerPos[0] - transform.position) * 0.01f;
		move.z = 0;
		transform.position += move;
		new Vector3(player2.OldPlayerPos[1].x,
							player2.OldPlayerPos[1].y + FollowCameraPos.y,
							FollowCameraPos.z);

		//Debug.Log("カメラ内ｵｰﾙﾄﾞﾎﾟｽ" + player2.OldPlayerPos[9].x);
		
		//---もとのやつ
		//transform.position = new Vector3(PlayerPos.x,
		//												player2.OldPlayerPos[5].y + FollowCameraPos.y,
		//												FollowCameraPos.z);

		////---画面外設定(x = 45.0fの地点に到達したらカメラの移動を停止)
		if (PlayerPos.x > RightScreenOut){
			//transform.position = new Vector3(45.0f, 0.7f, PlayerPos.z - 4.5f);
			transform.position = new Vector3(RightScreenOut,PlayerPos.y + FollowCameraPos.y,FollowCameraPos.z);         // ジャンプ追従
		    //if (PlayerPos.x >= MovePoint){
		    //    this.MoveFlg = true;
		    //    if(this.MoveFlg == true){
		    //        this.transform.position = new Vector3(60.0f, 1.5f, -4.0f);    // カメラの場所を再定
		    //    }
		    //}
		}
		//---画面外設定(x = 15.0fの地点に到達したらカメラの移動を停止)
		if (PlayerPos.x < LeftScreenOut)
		{
			//transform.position = new Vector3(15.0f, 0.7f, PlayerPos.z - 4.5f);
			transform.position = new Vector3(LeftScreenOut,
											 PlayerPos.y + FollowCameraPos.y,
											 FollowCameraPos.z);          // ジャンプ追従
		}

			//---上の画面外設定(y)
			if (PlayerPos.y > HeightScreenOut){
            transform.position = new Vector3(PlayerPos.x,
                                             HeightScreenOut,
                                             FollowCameraPos.z);
        }

        //---下の画面外設定(y)
        if (PlayerPos.y < UnderScreenOut){
            transform.position = new Vector3(PlayerPos.x,
                                             UnderScreenOut,
                                             FollowCameraPos.z);
        }


	}
}
