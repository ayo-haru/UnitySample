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
        SaveManager.load();
        GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
        GameData.CurrentMapNumber = (int)GameData.SceneState.TITLE_SCENE;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SaveManager.sd.LastMapNumber == 0)
            {
                string nextSceneName = GameData.GetNextScene(1);
                SceneManager.LoadScene(nextSceneName);
            }else{
                //if(続きからを押されたら)
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
