//=============================================================================
//
// シーンマネージャー
//
// 作成日:2022/03/11
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/11 作成
// 2022/03/13 シーン遷移する
// 2022/03/13 プレイヤーを一個にした
// 2022/03/13 シーン遷移を楽にしたはず
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene2Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Awake() {
        //----- プレイヤー初期化 -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        GameObject player = Instantiate(GameData.Player);

        //----- マップの番号を保存 -----
        GameData.NextMapNumber =  GameData.CurrentMapNumber = (int)GameData.SceneState.MAP2_SCENE;
    }

    // Update is called once per frame
    void Update() {
        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // 保存してあるシーン番号が現在と次が異なったらシーン移動
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
