//=============================================================================
//
// �쐬��:2022/03/16
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/16 �쐬
// 2022/03/26 SE�̉��ʂ�30���ɌŒ�
// 2022/03/27 ���̃t�F�[�h��ǉ����ĂȂ�
// 2022/04/01 ���|�[�Y
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
    //  �ڍׁF��������SoundData�̗񋓑̂ɒ�`������[��
    //
    //----------------------------------
    public static void Play(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("�Đ��o���܂���ł���");
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = 0.5f;
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
        float endTime = audioSource.clip.length * Time.timeScale;

    }

    public static void Play(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("�Đ��o���܂���ł���");
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = SoundData.BGMClip[(int)_bgmDataNumber];
        audioSource.volume = 0.2f;
        audioSource.loop = true;
        audioSource.Play();
    }

    public static void IgnorePlay(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("�Đ��o���܂���ł���");
            return; // �Đ��ł��܂���ł���
        }
        audioSource.ignoreListenerPause = true;
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = 0.5f;
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
        float endTime = audioSource.clip.length * Time.timeScale;

    }


    //----------------------------------
    //
    //  �T�E���h�|�[�Y
    //  �쐬�F�ɒn�c�^��
    //  �ڍׁF��������SoundData�̗񋓑̂ɒ�`������[��
    //
    //----------------------------------
    public static void SoundPause(AudioSource[] _audioSourceList) {
        for (int i = 0; i < _audioSourceList.Length; ++i)
        {
            if (_audioSourceList[i].isPlaying == false)
            {
                break;
            }
            _audioSourceList[i].Pause();
        }
    }
    public static void SoundUnPause(AudioSource[] _audioSourceList) {
        for (int i = 0; i < _audioSourceList.Length; ++i)
        {
            if (_audioSourceList[i].isPlaying == false && _audioSourceList[i].clip)
            {
                _audioSourceList[i].UnPause();
            }
        }
    }



    //----------------------------------
    //
    //  �T�E���h�t�F�[�h
    //  �쐬�F�ɒn�c�^��
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
