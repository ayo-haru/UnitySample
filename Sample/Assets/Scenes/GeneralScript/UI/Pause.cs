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
        //if (shouldPause.Value)  // �|�[�Y�t���O�ɂ���ă|�[�Y���邩��߂邩
        //{
        //    PauseStart();
        //}
        //else
        //{
        //    PauseFin();
        //}
    }

    /// <summary>
    /// �|�[�Y�X�^�[�g
    /// </summary>
    public static void PauseStart() {
        //Time.timeScale = 0;
        //isPause = true;

        if (EffectData.isSetEffect){
            EffectManager.EffectPause();
        }

        if (SoundData.isSetSound)  // �T�E���h���g�p�̃V�[���Ȃ�ȉ��̏������X�L�b�v
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
    /// �|�[�Y�I���
    /// </summary>
    public static void PauseFin() {
        //Time.timeScale = 1.0f;
        //isPause = false;

        if (EffectData.isSetEffect)
        {
            EffectManager.EffectUnPause();
        }


        if (!SoundData.isSetSound)  // �T�E���h���g�p�̃V�[���Ȃ�ȉ��̏������X�L�b�v
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
