using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene1Manager : MonoBehaviour
{
    public int roomSize = 50;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;           // フレームレートを固定

        GameData.SetroomSize(roomSize);
        GameData.Player = playerPrefab;
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        GameObject player = Instantiate(GameData.Player);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveScene1to2() {
        SceneManager.LoadScene("ProtoTypeScene2");
    }
}
