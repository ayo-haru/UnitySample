//=============================================================================
//
// ���A�C�e���擾���̃G�t�F�N�g����
//
// �쐬��:2022/05/25
// �쐬��:����T�q
//
//  pieceManager�ɂ����
//
// <�J������>
// 2022/05/25    �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStarItemEffect : MonoBehaviour
{
    //�G�t�F�N�g
    public GameObject effect;
    //���������G�t�F�N�g�i�[�p
    private GameObject instanceEffect;
    //�G�t�F�N�g�̃e�N�X�`���A�j���[�V�����R���|�[�l���g
    private TextureAnimation effectAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //�o�O�o�Ă邩��R�����g�A�E�g
       // StartEffect(GetTotalItem());
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void StartEffect(int _totalItem)
    {
        //�o�O�o�Ă邩��R�����g�A�E�g
        ////�G�t�F�N�g�����������琶��
        //if (_totalItem == 2 || _totalItem == 4 || _totalItem == 7)
        //{
        //    //RectTransform rt = piece[PieceGrade - 1].GetComponent<RectTransform>();
        //    instanceEffect = Instantiate(effect, /*rt.position*/new Vector3(100.0f,100.0f,0.0f), Quaternion.identity);
        //    effectAnimation = instanceEffect.transform.GetChild(0).gameObject.GetComponent<TextureAnimation>();
        //}

        //if (_totalItem == 2 || _totalItem == 5 || _totalItem == 9)
        //{

        //    effectAnimation.finishFrame = 7;
        //}

        //if (_totalItem == 4)
        //{
        //    effectAnimation.finishFrame = 4;
        //}

        //if (_totalItem == 7)
        //{
        //    effectAnimation.finishFrame = 2;
        //}

        //if (_totalItem == 8)
        //{
        //    effectAnimation.finishFrame = 5;
        //}
    }

    public void effectFinish()
    {
        //�o�O�o�Ă邩��R�����g�A�E�g
        //effectAnimation.finishFrame = 14;   //�e�N�X�`���̍ŏI�g�ݒ�
        //effectAnimation.loop = false;       //���[�v����
    }
}
