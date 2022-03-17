//=============================================================================
//
// 作成日:2022/03/16
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/16 作成
//
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
    //  詳細：第一引数はSoundDataの列挙体に定義した
    //
    //----------------------------------
    public static void Play(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            return; // 再生できませんでした
        }
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.Play();
    }

    public static void Play(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            return; // 再生できませんでした
        }
        audioSource.clip = SoundData.SEClip[(int)_bgmDataNumber];
        audioSource.Play();
    }
}
