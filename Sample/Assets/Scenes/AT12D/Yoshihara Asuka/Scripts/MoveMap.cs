//=============================================================================
//
// マップ遷移[MoveMap]
//
// 作成日:2022/03/09
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/09 作成
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{

    //---変数宣言
    GameObject Player;
    public Vector3 MovePoint;         // マップ遷移の地点となる値
                                    // 
    // Start is called before the first frame update
    void Start()
    {
        //---追従するオブジェクト名を設定
        this.Player = GameObject.Find("SD_unitychan_humanoid");

    }

    // Update is called once per frame
    void Update()
    {
        //---プレイヤーの現座標を取得
        Vector3 PlayerPos = this.Player.transform.position;

        if(PlayerPos.x >= MovePoint.x){
            transform.position = new Vector3(60.0f,1.5f,-4.0f);
        }

    }
}
