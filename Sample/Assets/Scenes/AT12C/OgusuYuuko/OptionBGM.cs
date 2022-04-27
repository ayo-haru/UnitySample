//=============================================================================
//
// BGM�ݒ�
//
// �쐬��:2022/04/26
// �쐬��:����T�q
//
// <�J������>
// 2022/04/26    �쐬

//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionBGM : MonoBehaviour
{
    //�X���C�_�[
    private Slider BgmSlider;
    //�I��p�t���O
    public bool selectFlag = true;
    //�ړ���
    public float moveSpeed = 0.005f;
    // Start is called before the first frame update
    void Awake()
    {
        //�R���|�[�l���g�擾
        BgmSlider = gameObject.GetComponent<Slider>();

        //�T�E���h�}�l�[�W���[����bgm�̉��ʂ��擾
        BgmSlider.value = SoundManager.bgmVolume;
        
    }

    // Update is called once per frame
    void Update()
    {
        //�I������ĂȂ������烊�^�[��
        if (!selectFlag)
        {
            return;
        }

        //�E���L�[�ŉ��ʃv���X
        if (Input.GetKey(KeyCode.RightArrow))
        {
            BgmSlider.value += moveSpeed;
            //���ʐݒ�
            SoundManager.bgmVolume = BgmSlider.value;
            SoundManager.setVolume(SoundData.GameAudioList);
        }
        //�����L�[�ŉ��ʁ|
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            BgmSlider.value -= moveSpeed;
            //���ʐݒ�
            SoundManager.bgmVolume = BgmSlider.value;
            SoundManager.setVolume(SoundData.GameAudioList);
        }


    }
    //�ݒ��ʕ����Ƃ��ɉ��ʕۑ�
    private void OnDisable()
    {
        SaveManager.saveBGMVolume(SoundManager.bgmVolume);
    }
}
