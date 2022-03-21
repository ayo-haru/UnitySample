using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {
    private AudioSource[] audioSourceList = new AudioSource[5];    // ���ɓ����ɂȂ点�鐔

    // Start is called before the first frame update
    void Start() {
        Application.targetFrameRate = 60;           // �t���[�����[�g���Œ�
        SaveManager.load();
        GameData.NextMapNumber = SaveManager.sd.LastMapNumber;
        GameData.CurrentMapNumber = (int)GameData.eSceneState.TITLE_SCENE;
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // ���炷
        SoundManager.Play(SoundData.eBGM.BGM_TITLE, audioSourceList);

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))   // �͂��߂���
        {
            string nextSceneName = GameData.GetNextScene(1);
            SceneManager.LoadScene(nextSceneName);
            SoundManager.Play(SoundData.eSE.SE_CLICK, audioSourceList);
        }

        if (Input.GetKeyDown(KeyCode.RightShift))    // �Â�����
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
