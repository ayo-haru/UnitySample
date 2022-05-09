using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause :MonoBehaviour
{
    public static bool isPause = false;

    ObservedValue<bool> shouldPause;

    private void Start()
    {
        shouldPause = new ObservedValue<bool>(isPause);
        shouldPause.OnValueChange += () => { if (isPause　|| GameData.isFadeIn || GameData.isFadeOut) { PauseStart(); } else { PauseFin(); } };
    }

    private void Update()
    {
        shouldPause.Value = isPause;
    }

    /// <summary>
    /// ポーズスタート
    /// </summary>
    public static void PauseStart() {
        //Time.timeScale = 0;
        //isPause = true;
        Debug.Log("ポーズスタート");
        if (EffectData.isSetEffect && !GameOver.GameOverFlag){
            EffectManager.EffectPause();
        }

        if (SoundData.isSetSound)  // サウンド未使用のシーンなら以下の処理をスキップ
        {
            if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
            {
                SoundManager.SoundPause(SoundData.TitleAudioList);
            }
            else
            {
                SoundManager.SoundPause(SoundData.GameAudioList);
            }
        }
    }

    /// <summary>
    /// ポーズ終わり
    /// </summary>
    public static void PauseFin() {
        //Time.timeScale = 1.0f;
        //isPause = false;
        Debug.Log("ポーズフィン");
        if (EffectData.isSetEffect && !GameOver.GameOverFlag)
        {
            EffectManager.EffectUnPause();
        }


        if (!SoundData.isSetSound)  // サウンド未使用のシーンなら以下の処理をスキップ
        {
            return;
        }
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.SoundUnPause(SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.SoundUnPause(SoundData.GameAudioList);
        }

    }
}
