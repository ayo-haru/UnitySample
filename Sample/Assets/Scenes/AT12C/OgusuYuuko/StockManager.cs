//=============================================================================
//
// �񕜂̃X�g�b�N
//
// �쐬��:2022/04/06
// �쐬��:����T�q
//
// <�J������>
// 2022/04/06 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    //�q�I�u�W�F�N�g
    public GameObject[] stock;
    //���݂̃X�g�b�N�̐�
    private int nStock;
    // Start is called before the first frame update
    void Start()
    {
        //�Ƃ肠�����O�ɂ��Ă邯�ǁA�Q�[���f�[�^�N���X����񕜂̃X�g�b�N�̐��擾���ē����
        nStock = 0;
        //�X�g�b�N�̏��������\������
        for(int i = 0; i < nStock; ++i)
        {
            stock[i].GetComponent<ImageShow>().Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�X�g�b�N�����������牽�����Ȃ�
        if(nStock <= 0)
        {
            return;
        }

        //�v���C���[��HP�������Ă�����
        if(GameData.CurrentHP < 6)
        {
            //�X�g�b�N����
            --nStock;
            //�X�g�b�N����
            stock[nStock].GetComponent<ImageShow>().Hide();
            //�v���C���[��HP���₷
            ++GameData.CurrentHP;
            
            //�Q�[���f�[�^�X�V
            //�Z�[�u�f�[�^�X�V
        }
    }

    public void AddStock()
    {
        //�X�g�b�N���ő吔�������烊�^�[��
        if(nStock >= stock.Length)
        {
            return;
        }

        ++nStock;
        stock[nStock - 1].GetComponent<ImageShow>().Show();
        //�Q�[���f�[�^�X�V
        //�Z�[�u�f�[�^�X�V
    }

    public bool IsAddStock()
    {
        if(nStock >= stock.Length)
        {
            return false;
        }

        return true;
        
    }
}
