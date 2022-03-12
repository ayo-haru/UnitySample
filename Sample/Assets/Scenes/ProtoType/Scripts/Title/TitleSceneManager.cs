using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        /*
         todo:�Z�[�u�f�[�^�Ǎ����l�N�X�g�V�[���ɍŌ�ɂ����V�[���ԍ������� 
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
