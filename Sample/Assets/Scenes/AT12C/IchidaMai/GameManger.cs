using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public int roomSize;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;           // フレームレートを固定
        GameData.SetroomSize(roomSize);
        GameData.Player = GameObject.Find("Player");
        GameData.PlayerPos = GameData.Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = GameData.Player.transform.position;
    }
}
