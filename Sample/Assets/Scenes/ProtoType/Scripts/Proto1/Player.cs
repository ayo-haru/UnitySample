//=============================================================================
//
// プレイヤーの管理する
//
// 作成日:2022/03/11
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/11 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // プレイヤーの位置を保存

        if(this.transform.position.y < -5)
        {
            GameData.PlayerPos = GameData.Player.transform.position = this.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        }
    }

    void OnTriggerEnter(Collider other) {




        //*************************************************************************************************
        // 以下プロトタイプ遷移
        //*************************************************************************************************
        //if (other.gameObject.tag == "MovePoint1to2")    // この名前のタグと衝突したら
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.MAP2_SCENE;   // 次のシーン番号を設定、保存
        //}

        //if (other.gameObject.tag == "MovePoint2to1")
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.MAP1_SCENE;    // 次のシーン番号を設定、保存
        //}

        //if (other.gameObject.tag == "MovePoint2toBoss")
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;    // 次のシーン番号を設定、保存
        //}
    }
}
