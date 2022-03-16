//=============================================================================
//
// �쐬��:2022/03/16
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/16 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    private static AudioSource GetUnusedSource(AudioSource[] audioSourceList) { // ���g�p��AudioSource��T��
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }
        }
        return null;    // ���g�p��AudioSource�݂͂���Ȃ�����
    }


    //----------------------------------
    //
    //  �T�E���h�Đ�
    //  �쐬�F�ɒn�c�^��
    //  �ڍׁF��������SoundData�̗񋓑̂ɒ�`����
    //
    //----------------------------------
    public static void Play(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.Play();
    }

    public static void Play(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = SoundData.SEClip[(int)_bgmDataNumber];
        audioSource.Play();
    }
}
