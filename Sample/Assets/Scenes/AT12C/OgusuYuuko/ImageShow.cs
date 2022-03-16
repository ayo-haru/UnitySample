//=============================================================================
//
// �摜��\��
//
//�t�F�[�h�݂����ɂ��񂾂�\��������܂��B
//
// �쐬��:2022/03/16
// �쐬��:����T�q
//
// <�J������>
// 2022/03/16 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour
{
    //�g�p�t���O
    bool useFlag;
    //�摜
    Image Image;
    //�摜�����x
    float alpha;
    //�����x�X�V���x
    public float alphaSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        useFlag = false;

        Image = GetComponent<Image>();
        alpha = 0.0f;
        //�����ɐݒ�
        Image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
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
        Image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
    public void Show()
    {
        useFlag = true;
    }
}
