using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Application.targetFrameRate = 60;           // �t���[�����[�g���Œ�
        SaveManager.load();
        GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
        GameData.CurrentMapNumber = (int)GameData.eSceneState.TITLE_SCENE;
    }
    
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))   // �͂��߂���
        {
                string nextSceneName = GameData.GetNextScene(1);
                SceneManager.LoadScene(nextSceneName);
        }

        if (Input.GetKeyDown(KeyCode.RightShift))    // �Â�����
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
