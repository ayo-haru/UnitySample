//=============================================================================
//
// �񕜂̂�����
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

public class PieceManager : MonoBehaviour
{
    //�q�I�u�W�F�N�g
    public GameObject[] piece;
    //���݂̂�����̐�
    private int nPiece;
    //�񕜂̃X�g�b�N�}�l�[�W���[
    private StockManager stockManager;
    // Start is called before the first frame update
    void Start()
    {
        //�Ƃ肠�����O�ɂ��Ă邯�ǁA�Q�[���f�[�^�N���X����񕜂̂�����̐��擾���ē����
        nPiece = 0;

        stockManager = GameObject.Find("StockHPManager").GetComponent<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����炪�T���鎞
        if(nPiece == 5)
        {
            if (stockManager.IsAddStock())
            {
                //�X�g�b�N���₹��Ȃ�
                //�X�g�b�N���₷
                stockManager.AddStock();
                //���������
                nPiece = 0;
                for (int i = 0; i < 5; ++i)
                {
                    piece[i].GetComponent<ImageShow>().Hide();
                }
                //�Q�[���f�[�^�X�V
                //�Z�[�u�f�[�^�X�V
            }

            //�X�g�b�N���₹�Ȃ��Ȃ炻�̂܂�
        }
    }

    public void GetPiece()
    {
        //������̐��X�V
        ++nPiece;
        //�����炪5�W�܂�����񕜂̃X�g�b�N�𑝂₷
        if(nPiece >= 5)
        {
            if (stockManager.IsAddStock())
            {
                //�񕜂̃X�g�b�N���₷
                //���₹����
                stockManager.AddStock();
                //���������
                nPiece = 0;
                for (int i = 0; i < 5; ++i)
                {
                    piece[i].GetComponent<ImageShow>().Hide();
                }
            }else
            {
                //���₹�Ȃ�������
                nPiece = 5;
                piece[nPiece - 1].GetComponent<ImageShow>().Show();
            }
        }
        else
        {
            piece[nPiece - 1].GetComponent<ImageShow>().Show();
        }

        //�Q�[���f�[�^�X�V
        //�Z�[�u�f�[�^�X�V
    }
}
