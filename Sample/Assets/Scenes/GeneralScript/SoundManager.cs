//=============================================================================
//
// 作成日:2022/03/16
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/16 作成
// 2022/03/26 SEの音量を30％に固定
// 2022/03/27 音のフェードを追加
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    private static AudioSource GetUnusedSource(AudioSource[] audioSourceList) { // 未使用のAudioSourceを探す
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }
        }
        return null;    // 未使用のAudioSourceはみつからなかった
    }


    //----------------------------------
    //
    //  サウンド再生
    //  作成：伊地田真衣
    //  詳細：第一引数はSoundDataの列挙体に定義したやーつ
    //
    //----------------------------------
    public static void Play(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("再生出来ませんでした");
            return; // 再生できませんでした
        }
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = 0.5f;
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
    }

    public static void Play(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("再生出来ませんでした");
            return; // 再生できませんでした
        }
        audioSource.clip = SoundData.BGMClip[(int)_bgmDataNumber];
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    //----------------------------------
    //
    //  サウンドフェード
    //  作成：伊地田真衣
    //
    //----------------------------------
    //public static IEnumerator VolumeDown(AudioSource[] _audioSources) {
    //    while (_audioSources.volume > 0)
    //    {
    //        _audioSources.volume -= 0.01f;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
}
