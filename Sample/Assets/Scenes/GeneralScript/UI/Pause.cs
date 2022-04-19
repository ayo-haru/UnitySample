using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pause
{
    public static bool isPause = false;

    public static void PauseStart() {
        //Time.timeScale = 0;
        isPause = true;

        if (!SoundData.isSetSound)  // サウンド未使用のシーンなら以下の処理をスキップ
        {
            return;
        }
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.SoundPause(SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.SoundPause(SoundData.GameAudioList);
        }
    }

    public static void PauseFin() {
        //Time.timeScale = 1.0f;
        isPause = false;

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
