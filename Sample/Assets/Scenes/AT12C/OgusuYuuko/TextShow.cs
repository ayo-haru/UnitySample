//=============================================================================
//
// �e�L�X�g��\��
//
//�t�F�[�h�݂����ɂ��񂾂�\��������܂��B
//�p�b�ƈ�u�ŕ\�����������Ƃ���alphaSpeed�ɂP��ݒ肵�Ă�������
//
// �쐬��:2022/03/20
// �쐬��:����T�q
//
// <�J������>
// 2022/03/20 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    //�g�p�t���O
    bool useFlag;
    //�e�L�X�g
    Text text;
    //�����x
    float alpha;
    //�F
    public float red = 0.0f;
    public float green = 0.0f;
    public float blue = 0.0f;
    //�����x�X�V���x
    public float alphaSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        useFlag = false;

        text = GetComponent<Text>();
        alpha = 0.0f;
        //�����ɐݒ�
        text.color = new Color(red,green,blue, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //�g���ĂȂ������烊�^�[��
        if (!useFlag)
        {
            return;
        }

        //�����x�X�V
        alpha += alphaSpeed;
        if (alpha > 1.0f)
        {
            alpha = 1.0f;
        }
        //�F�ݒ�
        text.color = new Color(red, green, blue, alpha);
    }
    public void Show()
    {
        useFlag = true;
    }

    public void Hide()
    {
        //�\���r���������烊�^�[��
        if (alpha < 1.0f)
        {
            return;
        }
        useFlag = false;
        alpha = 0.0f;
        //�����ɐݒ�
        text.color = new Color(red, green, blue, alpha);
    }
}
