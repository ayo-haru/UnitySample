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
        shouldPause.OnValueChange += () => { if (isPause) { PauseStart(); } else { PauseFin(); } };
    }

    private void Update()
    {
        //if (shouldPause.Value)  // ポーズフラグによってポーズするかやめるか
        //{
        //    PauseStart();
        //}
        //else
        //{
        //    PauseFin();
        //}
    }

    /// <summary>
    /// ポーズスタート
    /// </summary>
    public static void PauseStart() {
        //Time.timeScale = 0;
        //isPause = true;

        if (EffectData.isSetEffect){
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

        if (EffectData.isSetEffect)
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
