//=============================================================================
//
// プレイヤーの遅延追従カメラ[DelayFollowCamera]
//
// 作成日:2022/03/08
// 作成者:吉原飛鳥
//
// <開発履歴>
//  2022/05/11 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayFollowCamera : MonoBehaviour
{
    //---変数宣言
    private GameObject player;
    private Player2 playerInfo;

    public Vector3 FollowCameraPos = Vector3.zero;      // 追従するカメラ

    public int RightScreenOut;                          // 右端
    public int LeftScreenOut;                           // 左端
    public int OverScreenOut;                           // 上端
    public int UnderScreenOut;                          // 下端

    // Start is called before the first frame update
    void Start()
    {
        //---追従するオブジェクト
        player = GameObject.Find(GameData.Player.name);
        playerInfo = player.GetComponent<Player2>();
        
    }

    private void LateUpdate()
    {
        Vector3 PlayerPos = GameData.PlayerPos;

		//---遅延処理
		//Vector3 move = (playerInfo.OldPlayerPos[0] - transform.position) * 0.01f;
		//move.z = 0;
		//transform.position += move;
		//new Vector3(playerInfo.OldPlayerPos[1].x,
		//			playerInfo.OldPlayerPos[1].y + FollowCameraPos.y,
		//			FollowCameraPos.z);

		transform.position = new Vector3(PlayerPos.x,
										 playerInfo.OldPlayerPos[1].y + FollowCameraPos.y,
										 FollowCameraPos.z); ;


		//---画面外処理
    
        if (PlayerPos.x < LeftScreenOut){                                       //　左端
            transform.position = new Vector3(LeftScreenOut,
                                             PlayerPos.y + FollowCameraPos.y,
                                             FollowCameraPos.z);

        }

        if (PlayerPos.x > RightScreenOut){                                      // 右端 
            //transform.position = new Vector3(RightScreenOut,
            //                                 PlayerPos.y + FollowCameraPos.y,
            //                                 FollowCameraPos.z);
            
        }

        if (PlayerPos.y > OverScreenOut){                                       // 上端 
            transform.position = new Vector3(PlayerPos.x,
                                             OverScreenOut,
                                             FollowCameraPos.z);
        }

        if (PlayerPos.y <  UnderScreenOut){                                     // 下端
            transform.position = new Vector3(PlayerPos.x,
                                             UnderScreenOut,
                                             FollowCameraPos.z);
        }



    }
}
