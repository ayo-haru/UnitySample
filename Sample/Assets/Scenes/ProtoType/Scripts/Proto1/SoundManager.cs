using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    //private static AudioSource[] audioSourceList = new AudioSource[20];    // 一回に同時にならせる数
    //private static GameObject _gameobject = new GameObject();

    // Start is called before the first frame update
    //private static void Awake() {
    // audioSourceList配列の数だけAudioSourceを自身に生成して配列に格納
    //for(int i = 0;i < audioSourceList.Length; ++i)
    //{
    //     audioSourceList[i] = _gameobject.AddComponent<AudioSource>();
    //}
    //}

    private static AudioSource GetUnusedSource(AudioSource[] audioSourceList) {
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }
        }
        return null;    // 未使用のAudioSourceはみつからなかった
    }

    public static void Play(AudioClip clip, AudioSource[] audioSourceList) {
        AudioSource audioSource = GetUnusedSource(audioSourceList);
        if (audioSource == null)
        {
            return; // 再生できませんでした
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
