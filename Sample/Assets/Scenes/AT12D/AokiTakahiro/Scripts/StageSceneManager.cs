using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;        // フレームレートを固定

        // プレイヤー初期化
        if(!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(0.0f, 7.0f, 0.0f);
        GameObject player = Instantiate(GameData.Player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
