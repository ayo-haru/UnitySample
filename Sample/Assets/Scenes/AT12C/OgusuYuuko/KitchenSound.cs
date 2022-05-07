using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSound : MonoBehaviour
{
    private void Start()
    {
        //ボリューム設定
        gameObject.GetComponent<AudioSource>().volume = SoundManager.bgmVolume;

        //シーン切り替わってもｂｇｍ継続する
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //ボスシーンとタイトルシーンはｂｇｍを切り替えるためオブジェクトを破棄する
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE || GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Destroy(gameObject);
        }
    }
}
