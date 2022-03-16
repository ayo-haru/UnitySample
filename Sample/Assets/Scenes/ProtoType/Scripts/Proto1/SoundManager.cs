using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    //private static AudioSource[] audioSourceList = new AudioSource[20];    // ���ɓ����ɂȂ点�鐔
    //private static GameObject _gameobject = new GameObject();

    // Start is called before the first frame update
    //private static void Awake() {
    // audioSourceList�z��̐�����AudioSource�����g�ɐ������Ĕz��Ɋi�[
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
        return null;    // ���g�p��AudioSource�݂͂���Ȃ�����
    }

    public static void Play(AudioClip clip, AudioSource[] audioSourceList) {
        AudioSource audioSource = GetUnusedSource(audioSourceList);
        if (audioSource == null)
        {
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
