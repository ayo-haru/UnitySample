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
    //�����^���������^�C�~���O(�A�C�e����)���i�[�@
    private int[] rantanDown = { 1, 2, 4, 7 };
    //�����^���̃��l�i�[
    private float[] rantanAlpha = { 1.0f,0.5f,1.0f,0.3f,0.6f,1.0f,0.2f,0.4f,0.6f,1.0f};
    // Start is called before the first frame update
    void Start()
    {
        onceFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onceFlag)
        {
            return;
        }
        onceFlag = false;

        // �����g�ő�l�ݒ�
        MaxPieceGrade = piece.Length;
        //�Q�[���f�[�^�N���X����񕜂̂�����̐��擾���ē����
        nPiece = GameData.CurrentPiece;
        //�Q�[���f�[�^�N���X����擾���Ă����
        PieceGrade = GameData.CurrentPieceGrade;
        //�����^��������
        lanthanumDown();
        //���l����
        lanthanumAlpha();
        //nPiece�̐����������^�������点��
        for(int i = 0; i < nPiece; ++i)
        {
            piece[i].GetComponent<UVScroll>().SetFrame(1);
        }
    }

    public void GetPiece()
    {
        //�����g��葽�������烊�^�[��
        if (nPiece >= PieceGrade)
        {
            return;
        }

        //�����瑝�₷
        ++nPiece;
        //�����^�������点��
        piece[nPiece - 1].GetComponent<UVScroll>().SetFrame(1);
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
            return false;
        }
        else
        {
            //�����猸�炷
            --nPiece;
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
        int TotalItem = GetTotalItem();

        Debug.Log(TotalItem + "�Ɓ[���邠���Ă�");
        
        if(TotalItem > PieceUpGradeNum + PieceGrade) 
        {
            //�����珊���g�𑝂₷
            ++PieceGrade;
            //�����^��������₷
            //piece[PieceGrade - 1].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
            //�G�t�F�N�g���[�v����
            //if(TotalItem > 1)
            //{
            //    //�o�O�o�Ă邩��R�����g�A�E�g
            //    //effectManager.effectFinish();
            //}
            
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
        //�����^������
        lanthanumDown();
        //���l����
        lanthanumAlpha();
    }

    public void lanthanumDown()
    {
        //���݂̃A�C�e���擾��
        int totalItem = GetTotalItem();

        for(int i = 0; i < rantanDown.Length; ++i)
        {
            if(totalItem >= rantanDown[i])
            {
                //�����^��������
                piece[i].transform.parent.gameObject.GetComponent<pieceMove>().startFlag = true;
                //�����x�����l�ݒ�
                piece[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Debug.Log("�����^��������");
            }
        }

    }

    public void lanthanumAlpha()
    {
        // ���݂̃A�C�e���擾��
        int totalItem = GetTotalItem();
        //�A�C�e���擾���ĂȂ������烊�^�[��
        if(totalItem <= 0)
        {
            return;
        }
        //���l��������I�u�W�F�N�g�̃R���|�[�l���g�擾
        Image alphaSetImage;
        if (rantanAlpha[totalItem - 1] < 1.0f)
        {
            alphaSetImage = piece[PieceGrade].GetComponent<Image>();
        }
        else
        {
            alphaSetImage = piece[PieceGrade - 1].GetComponent<Image>();
        }
        
        //���l�ݒ�
        alphaSetImage.color = new Color(1.0f, 1.0f, 1.0f, rantanAlpha[totalItem - 1]);
    }

    public int GetTotalItem()
    {
        int _totalItem = 0;
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                if (GameData.isStarGet[j, i])
                {
                    ++_totalItem;
                }
            }
        }

        return _totalItem;
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
