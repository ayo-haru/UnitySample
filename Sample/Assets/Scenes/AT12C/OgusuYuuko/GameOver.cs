//=============================================================================
//
// �Q�[���I�[�o�[���o
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

public class GameOver : MonoBehaviour
{
    //�Q�[���I�[�o�[�Ŏg���I�u�W�F�N�g
    //�摜
    GameObject Image;

    // Start is called before the first frame update
    void Start()
    {
        Image = GameObject.Find("GameOverImage");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOverShow()
    {
        //�摜�\��
        Image.SendMessage("Show");
    }
}
