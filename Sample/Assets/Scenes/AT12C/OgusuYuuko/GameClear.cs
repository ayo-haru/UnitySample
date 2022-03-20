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
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    //�Q�[���N���A�Ŏg���I�u�W�F�N�g
    //�N���A�摜
    GameObject Image;
    //�e�L�X�g
    GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        Image = GameObject.Find("GameClearImage");
        text = GameObject.Find("GameClearText");
    }

    // Update is called once per frame
    void Update()
    {
        //��������̃L�[�������ꂽ��\���I��
        if (Input.anyKey)
        {
            GameClearHide();
        }
    }

    public void GameClearShow()
    {
        //�摜�\��
        Image.SendMessage("Show");
        //�e�L�X�g�\��
        text.SendMessage("Show");
    }

    public void GameClearHide()
    {
        //�摜����
        Image.SendMessage("Hide");
        //�e�L�X�g����
        text.SendMessage("Hide");
    }
}
