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
   // private StockManager stockManager;
    // Start is called before the first frame update
    void Start()
    {
        //�Ƃ肠�����O�ɂ��Ă邯�ǁA�Q�[���f�[�^�N���X����񕜂̂�����̐��擾���ē����
        nPiece = GameData.CurrentPiece;
        for(int i = 0; i < nPiece; ++i)
        {
            //������̏����������F�ɂ��ĕ\��
            piece[i].GetComponent<ImageShow>().Show();
        }
        for(int i = nPiece; i < piece.Length; ++i)
        {
            //����������F�ɂ��ĕ\��
            piece[i].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            piece[i].GetComponent<ImageShow>().Show();
        }

       // stockManager = GameObject.Find("StockHPManager").GetComponent<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����炪�T���鎞
        //if(nPiece == piece.Length)
        //{
        //    if (stockManager.IsAddStock())
        //    {
        //        //�X�g�b�N���₹��Ȃ�
        //        //�X�g�b�N���₷
        //        stockManager.AddStock();
        //        //���������
        //        nPiece = 0;
        //        for (int i = 0; i < piece.Length; ++i)
        //        {
        //            piece[i].GetComponent<ImageShow>().Hide();
        //        }
        //        //�Q�[���f�[�^�X�V
        //        //�Z�[�u�f�[�^�X�V
        //    }

        //    //�X�g�b�N���₹�Ȃ��Ȃ炻�̂܂�
        //}
    }

    public void GetPiece()
    {
        //������̐��X�V
        //++nPiece;
        ////�����炪5�W�܂�����񕜂̃X�g�b�N�𑝂₷
        //if(nPiece >= piece.Length)
        //{
        //    if (stockManager.IsAddStock())
        //    {
        //        //�񕜂̃X�g�b�N���₷
        //        //���₹����
        //        stockManager.AddStock();
        //        //���������
        //        nPiece = 0;
        //        for (int i = 0; i < piece.Length; ++i)
        //        {
        //            piece[i].GetComponent<ImageShow>().Hide();
        //        }
        //    }else
        //    {
        //        //���₹�Ȃ�������
        //        nPiece = piece.Length;
        //        piece[nPiece - 1].GetComponent<ImageShow>().Show();
        //    }
        //}
        //else
        //{
        //    piece[nPiece - 1].GetComponent<ImageShow>().Show();
        //}

        //HP���l�`�w�������炩����𑝂₷
        if(GameData.CurrentHP >= 5)
        {
            if (nPiece >= piece.Length)
            {
                return;
            }
            //�����瑝�₷
            ++nPiece;
            piece[nPiece - 1].GetComponent<ImageShow>().SetColor(1.0f,1.0f,1.0f);
            //�Q�[���f�[�^�X�V
            ++GameData.CurrentPiece;
        }
        else
        {
            //HP�������Ă�����g�o��1��
            ++GameData.CurrentHP;
        } 
    }

    public void DelPiece()
    {
        //�����炪����������
        if(nPiece <= 0)
        {
            //HP���炷
            --GameData.CurrentHP;
        }
        else
        {
            //�����猸�炷
            --nPiece;
            //������̐F�����ɂ���
            piece[nPiece].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            //�Q�[���f�[�^�X�V
            --GameData.CurrentPiece;
        }
    }
}
