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
        GameData.PlayerPos = transform.position;    // プレイヤーの位置を保存
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MovePoint1to2")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.SceneState.MAP2_SCENE;   // 次のシーン番号を設定、保存
        }

        if (other.gameObject.tag == "MovePoint2to1")
        {
            GameData.NextMapNumber = (int)GameData.SceneState.MAP1_SCENE;    // 次のシーン番号を設定、保存
        }
    }

}
