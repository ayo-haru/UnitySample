//=============================================================================
//
// �摜��\��
//
//�t�F�[�h�݂����ɂ��񂾂�\��������܂��B
//�p�b�ƈ�u�ŕ\�����������Ƃ���alphaSpeed�ɂP��ݒ肵�Ă�������
//
// �쐬��:2022/03/16
// �쐬��:����T�q
//
// <�J������>
// 2022/03/16 �쐬
// 2022/03/19 �\�����I������֐���ǉ�
// 2022/03/23 �����b�����w��ł���悤�ɂ���
// 2022/03/30 �F��ύX�ł���悤�ɂ���
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour
{
    enum ImageMode {NONE,SHOW,HIDE,TIMER };   //�X�V�Ȃ��A�\�����A�B����,�^�C�}�[�X�V
    //�g�p���Ă��郂�[�h
    ImageMode mode;
    //�摜
    Image Image;
    //�摜�����x
    float alpha;
    //�摜�F
    float red;
    float green;
    float blue;
    //�����x�X�V���x
    public float ShowAlphaSpeed = 0.005f;
    public float HideAlphaSpeed = 0.005f;
    //�\������
    int Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        mode = ImageMode.NONE;
        Timer = 0;
        //useFlag = false;

        Image = GetComponent<Image>();
        red = Image.color.r;
        green = Image.color.g;
        blue = Image.color.b;
        alpha = 0.0f;
        //�����ɐݒ�
        Image.color = new Color(red, green, blue, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //�g���ĂȂ������烊�^�[��
        if (mode == ImageMode.NONE)
        {
            return;
        }

        
        if (mode == ImageMode.SHOW)
        {
            //�����x�X�V
            alpha += ShowAlphaSpeed;
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                if (Timer > 0)
                {
                    mode = ImageMode.TIMER;
                }
                else
                {
                    mode = ImageMode.NONE;
                }
                
            }
            //�F�ݒ�
            Image.color = new Color(red, green, blue, alpha);
            return;
        }

        if (mode == ImageMode.HIDE)
        {
            //�����x�X�V
            alpha -= HideAlphaSpeed;
            if (alpha <= 0.0f)
            {
                alpha = 0.0f;
                mode = ImageMode.NONE;
            }
            //�F�ݒ�
            Image.color = new Color(red, green, blue, alpha);
            return;
        }

        if(mode == ImageMode.TIMER)
        {
            //�^�C�}�[�X�V
            --Timer;
            if (Timer <= 0)
            {
                mode = ImageMode.HIDE;
                Timer = 0;
            }
        }
        
       


    }
    public void Show()
    {
        mode = ImageMode.SHOW;
    }

    public void Show(int second)
    {
        if (mode != ImageMode.NONE)
        {
            return;
        }
        mode = ImageMode.SHOW;
        Timer = second;
    }

    public void Hide()
    {
        //�\���r���������烊�^�[��
        if(alpha < 1.0f)
        {
            return;
        }
        mode = ImageMode.NONE;
        alpha = 0.0f;
        //�����ɐݒ�
        Image.color = new Color(red, green, blue, alpha);
    }

    public void SetColor(float r ,float g,float b)
    {
        red = r;
        green = g;
        blue = b;
        //�F�ݒ�
        Image.color = new Color(red, green, blue, alpha);
    }
}
