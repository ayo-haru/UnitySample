//=============================================================================
//
// �Q�[���N���A���o
//
// �쐬��:2022/03/15
// �쐬��:����T�q
//
// <�J������>
// 2022/03/15 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    //�Q�[���N���A�摜
    Image clearImage;

    // Start is called before the first frame update
    void Start()
    {
        clearImage = GetComponent<Image>();
        //�����ɐݒ�
        clearImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        //�摜�\��
        clearImage.color = new Color(1.0f, 1.0f, 1.0f, 255.0f);
    }
}
