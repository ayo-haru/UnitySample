using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene2Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        GameData.Player = GameObject.Find("Player");
        GameData.Player.transform.position = new Vector3(2.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update() {

    }

    public void MoveScene2to1() {
        SceneManager.LoadScene("ProtoTypeScene1");
    }
}
