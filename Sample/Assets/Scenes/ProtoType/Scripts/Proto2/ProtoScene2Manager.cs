using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene2Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Awake() {
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        GameObject player = Instantiate(GameData.Player);
    }

    // Update is called once per frame
    void Update() {

    }

    public void MoveScene2to1() {
        SceneManager.LoadScene("ProtoTypeScene1");
    }
}
