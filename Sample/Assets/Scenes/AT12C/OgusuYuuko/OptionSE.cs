//=============================================================================
//
// SE�ݒ�
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

public class OptionSE : MonoBehaviour
{
    //�X���C�_�[
    private Slider SeSlider;
    //�I��p�t���O
    public bool selectFlag = false;
    //�ړ���
    public float moveSpeed = 0.005f;
    // Start is called before the first frame update
    void Awake()
    {
        //�R���|�[�l���g�擾
        SeSlider = gameObject.GetComponent<Slider>();

        //�T�E���h�}�l�[�W���[����SE�̉��ʂ��擾
        SeSlider.value = SoundManager.seVolume;

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
            SeSlider.value += moveSpeed;
            //���ʐݒ�
            SoundManager.seVolume = SeSlider.value;
        }
        //�����L�[�ŉ��ʁ|
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SeSlider.value -= moveSpeed;
            //���ʐݒ�
            SoundManager.seVolume = SeSlider.value;
        }


    }
}
