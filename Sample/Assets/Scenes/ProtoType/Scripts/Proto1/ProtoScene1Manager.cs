using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene1Manager : MonoBehaviour
{
    public int roomSize = 50;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;           // フレームレートを固定

        GameData.SetroomSize(roomSize);
        GameData.Player = GameObject.Find("Player");
        GameData.PlayerPos = GameData.Player.transform.position;

        DontDestroyOnLoad(GameData.Player);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveScene1to2() {
        SceneManager.LoadScene("ProtoTypeScene2");
    }
}
