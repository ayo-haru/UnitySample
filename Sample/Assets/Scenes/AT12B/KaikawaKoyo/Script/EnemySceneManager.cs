using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySceneManager : MonoBehaviour
{
    public GameObject Player;

    private void Awake()
    {
        //---フレームレート固定
        Application.targetFrameRate = 60;

        //---プレイヤープレハブの取得
        if (!GameData.Player)
        {
            GameData.Player = Player;   // GameDataのプレイヤーに取得
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-80.0f, 20.5f, 0.0f); // プレイヤーの座標を設定
        GameObject player = Instantiate(GameData.Player);       // プレハブを実体化

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
