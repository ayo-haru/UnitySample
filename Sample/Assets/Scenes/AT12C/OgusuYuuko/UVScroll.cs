//=============================================================================
//
// �e�N�X�`����UV����
//
// �쐬��:2022/04/04
// �쐬��:����T�q
//
// �t�h�p�ɍ쐬
//
// �\���摜�ɂ��̃X�N���v�g�����
// �}�X�N�R���|�[�l���g���A�^�b�`���ꂽ�摜��e�Ɏw�肵�āAShowMaskGraphic�̃`�F�b�N�O��
// �e�̈ʒu���摜�̕\���ʒu�ɂȂ�
//
// <�J������>
// 2022/04/04 �쐬
// 2022/04/06 ���ɐi�ށA�O�ɖ߂��ǉ�
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVScroll : MonoBehaviour
{
    //�R���|�[�l���g�擾
    RectTransform rt;
    //�e �}�X�N�摜
    GameObject pearent;
    //�e�R���|�[�l���g
    RectTransform pearent_rt;
    [SerializeField]
    //�\������g
    private int nFrame = 0;
    [SerializeField]
    //�e�N�X�`����������
    private int split_x = 1;
    [SerializeField]
    //�e�N�X�`���������c
    private int split_y = 1;

    ///1�g�̑傫��
    private float width;
    private float height;

    private void Awake()
    {
        //������RectTransform�擾
        rt = GetComponent<RectTransform>();
        //�e�I�u�W�F�N�g�擾
        pearent = transform.parent.gameObject;
        //�e��RectTransform�擾
        pearent_rt = pearent.GetComponent<RectTransform>();
        //�\������g�̑傫��
        width = rt.sizeDelta.x / split_x;
        height = rt.sizeDelta.y / split_y;
        //�g��e�̃}�X�N�ɐݒ�
        pearent_rt.sizeDelta = new Vector2(width, height);
        //�e�N�X�`���̈ʒu�ݒ�
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                     pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (int)(nFrame / split_x) * height,
                                      0.0f);
    }

    private void Update()
    {
        
    }

    public void SetFrame(int FrameNo)
    {
        nFrame = FrameNo;

        //nFrame�␳�@0�`�e�N�X�`���̕����� - 1�̊Ԃɂ���
        if(nFrame >= split_x * split_y)
        {
            nFrame = FrameNo % (split_x * split_y);
        }else if (nFrame < 0)
        {
            FrameNo *= -1;
            nFrame = (split_x * split_y) - (FrameNo % (split_x * split_y));
            if (nFrame == split_x * split_y)
            {
                nFrame = 0;
            }
        }

        //�e�N�X�`���̈ʒu�ݒ�
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                      pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (nFrame / split_x) / split_y * height,
                                       0.0f);
    }

    public void SetNext()
    {
        ++nFrame;
        if(nFrame >= split_x * split_y)
        {
            nFrame = 0;
        }
        //�e�N�X�`���̈ʒu�ݒ�
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                      pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (nFrame / split_x) / split_y * height,
                                       0.0f);
    }

    public void SetPrev()
    {
        --nFrame;
        if(nFrame < 0)
        {
            nFrame = split_x * split_y - 1;
        }
        //�e�N�X�`���̈ʒu�ݒ�
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                      pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (nFrame / split_x) / split_y * height,
                                       0.0f);
    }

    /*
     *
     *  nFrame�̎擾
     * 
     */
    public int GetnFrame() {
        return nFrame;
    }
}
