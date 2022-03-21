//=============================================================================
//
// 各シーンのデータ管理[Scene1Manager]
//
// 作成日:2022/03/11
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/11
// 2022/03/22   GameData導入
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public GameObject playerPrefab;                     // プレイヤーのプレハブを扱う

    private void Awake()
    {
        Application.targetFrameRate = 60;

        //---プレイヤープレハブの取得
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;             // GameDataのプレイヤーに取得
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f); // プレイヤーの座標を設定
        GameObject player = Instantiate(GameData.Player);    // プレハブを実体化

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
