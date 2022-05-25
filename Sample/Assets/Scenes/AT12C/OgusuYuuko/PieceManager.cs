//=============================================================================
//
// �񕜂̂�����
//
// �쐬��:2022/04/06
// �쐬��:����T�q
//
// <�J������>
// 2022/04/06 �쐬
// 2022/04/19 �A�C�e���擾���̏����ǉ�
// 2022/04/20 hp�̐����HPManager�Ɉړ�
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour
{
    //�q�I�u�W�F�N�g
    public GameObject[] piece;
    //���݂̂�����̐�
    private int nPiece;
    //�����珊���g
    private int PieceGrade;
    //�����g�ő�l
    private int MaxPieceGrade;
    //��񂾂����s����p�̃t���O
    private bool onceFlag;
    //�G�t�F�N�g
    //public GameObject effect;
    //�����^���摜
    //public GameObject PieceImage;
    //UVScroll�R���|�[�l���g
    //private UVScroll pieceUV;
    //�񕜂̃X�g�b�N�}�l�[�W���[
   // private StockManager stockManager;
    // Start is called before the first frame update
    void Start()
    {
        onceFlag = true;

        //�R���|�[�l���g�擾
        //pieceUV = PieceImage.GetComponent<UVScroll>();

       // stockManager = GameObject.Find("StockHPManager").GetComponent<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onceFlag)
        {
            return;
        }
        // �����g�ő�l�ݒ�
        MaxPieceGrade = piece.Length;
        //�Q�[���f�[�^�N���X����񕜂̂�����̐��擾���ē����
        nPiece = GameData.CurrentPiece;
        //�Q�[���f�[�^�N���X����擾���Ă����
        PieceGrade = GameData.CurrentPieceGrade;

        //���݂̂�����̐��ɉ����Ăt�u���W����
        //pieceUV.SetFrame(nPiece);
        //PieceGrade�̐����������^�����o��
        for(int i = 0; i < PieceGrade; ++i)
        {
            piece[i].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
        }
        //nPiece�̐����������^�������点��
        for(int i = 0; i < nPiece; ++i)
        {
            piece[i].GetComponent<UVScroll>().SetFrame(1);
        }

        //for (int i = 0; i < nPiece; ++i)
        //{
        //    //������̏����������F�ɂ��ĕ\��
        //    piece[i].GetComponent<ImageShow>().Show();
        //}

        //for (int i = nPiece; i < PieceGrade; ++i)
        //{
        //    //����������F�ɂ��ĕ\��
        //    piece[i].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
        //    piece[i].GetComponent<ImageShow>().Show();
        //}

        onceFlag = false;
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
            //�����g��葽�������烊�^�[��
            if (nPiece >= PieceGrade)
            {
                return;
            }

            //�����瑝�₷
            ++nPiece;
        //pieceUV.SetFrame(nPiece);
        Debug.Log("nPiece" + nPiece);
        //�����^�������点��
        piece[nPiece - 1].GetComponent<UVScroll>().SetFrame(1);

        //�\��
        //piece[nPiece - 1].GetComponent<ImageShow>().SetColor(1.0f,1.0f,1.0f);
        //    //�G�t�F�N�g����
        //    Instantiate(effect, piece[nPiece - 1].GetComponent<RectTransform>().position, Quaternion.identity);
            //�Q�[���f�[�^�X�V
            ++GameData.CurrentPiece;
            //�ۑ�
            //SaveManager.saveCurrentPiece(GameData.CurrentPiece);

    }

    public bool DelPiece()
    {
        //�����炪����������
        if(nPiece <= 0)
        {
            //HP���炷
            //--GameData.CurrentHP;
            return false;
        }
        else
        {
            //�����猸�炷
            --nPiece;
            //pieceUV.SetFrame(nPiece);
            //�����^���̌�������
            piece[nPiece].GetComponent<UVScroll>().SetFrame(0);

            ////�G�t�F�N�g����
            //GameObject Effect = Instantiate(effect, piece[nPiece].GetComponent<RectTransform>().position, Quaternion.identity);
            ////�G�t�F�N�g�̐F�����ɂ���
            //Effect.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            ////������̐F�����ɂ���
            //piece[nPiece].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            //�Q�[���f�[�^�X�V
            --GameData.CurrentPiece;
            //�ۑ�
            //SaveManager.saveCurrentPiece(GameData.CurrentPiece);

            return true;
        }
    }

    public void GetItem()
    {
        //���ɏ���l���}�b�N�X�������烊�^�[��
        if(PieceGrade >= MaxPieceGrade)
        {
            return;
        }

        //������g���₹��A�C�e����
        int PieceUpGradeNum = 0;
        for(int i = 0; i <= PieceGrade; ++i)
        {
            PieceUpGradeNum += i;
        }
        //�݌v�A�C�e����
        int TotalItem = 0;
        for(int i = 0; i < 10; ++i)
        {
         for(int j = 0; j < 10; ++j)
            {
                if (GameData.isStarGet[j, i])
                {
                    ++TotalItem;
                }
            }   
        }
        Debug.Log(TotalItem);
        if(TotalItem >= PieceUpGradeNum + PieceGrade)   //���Q�b�g�����A�C�e���̏�񂪂܂��z��ɓ����ĂȂ����߃C�R�[���t���Ă�
        {
            //�����珊���g�𑝂₷
            ++PieceGrade;
            //�����^��������₷
            //piece[PieceGrade - 1].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
            //�G�t�F�N�g����

            //�Q�[���f�[�^�X�V
            ++GameData.CurrentPieceGrade;
            //�ۑ�
            //SaveManager.savePieceGrade(GameData.CurrentPieceGrade);
            ////�\��
            //piece[PieceGrade - 1].GetComponent<ImageShow>().SetColor(0.0f, 0.0f, 0.0f);
            //piece[PieceGrade - 1].GetComponent<ImageShow>().Show();
            ////�G�t�F�N�g����
            //Instantiate(effect, piece[PieceGrade - 1].GetComponent<RectTransform>().position, Quaternion.identity);
        }

        if(TotalItem == 0 || TotalItem == 1 || TotalItem == 3 || TotalItem == 6)
        {
            //�����^��������₷
            piece[PieceGrade - 1].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
            //�G�t�F�N�g�o��
            //TotalItem��0�̎��͏o���Ȃ�
        }
        else if(true)//�G�t�F�N�g����������Ă���
        {
            //�G�t�F�N�g�̓����x�X�V
        }
    }

    public void Vibration()
    {
        //PieceImage.transform.parent.gameObject.GetComponent<pieceMove>().vibration();

        for (int i = 0; i < PieceGrade; ++i)
        {
            piece[i].transform.parent.gameObject.GetComponent<pieceMove>().vibration();
        }
    }
}
