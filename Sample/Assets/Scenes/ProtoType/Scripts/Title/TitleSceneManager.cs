using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        /*
         todo:セーブデータ読込→ネクストシーンに最後にいたシーン番号を入れる 
         */

        GameData.CurrentMapNumber = (int)GameData.SceneState.TITLE_SCENE;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            string nextSceneName = GameData.GetNextScene(2);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
