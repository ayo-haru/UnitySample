//=============================================================================
//
// �Q�[���N���A���o
//
// �쐬��:2022/03/15
// �쐬��:����T�q
//
// <�J������>
// 2022/03/15 �쐬
// 2022/03/16 �摜�\���̋@�\��ImageShow.cs�Ɉړ�
// 2022/03/19 �����̃L�[����������I�����鏈����ǉ�
// 2022/03/20 �g�p����I�u�W�F�N�g��unity���ŕς����悤�ɂ���
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    //�Q�[���N���A�Ŏg���I�u�W�F�N�g
    //�N���A�摜
    public GameObject Image;
    //�e�L�X�g
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //f3�L�[�������ꂽ��\���I��
        if (Input.GetKey(KeyCode.F3))
        {
            GameClearHide();
        }
    }

    public void GameClearShow()
    {
        //�摜�\��
        Image.GetComponent<ImageShow>().Show();
        //�e�L�X�g�\��
        text.GetComponent<TextShow>().Show();
    }

    public void GameClearHide()
    {
        //�摜����
        Image.GetComponent<ImageShow>().Hide();
        //�e�L�X�g����
        text.GetComponent<TextShow>().Hide();
    }
}
