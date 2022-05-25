//=============================================================================
//
// �Ă������Ⴀ�ɂ߁[�����
//
// �쐬��:2022/05/2
// �쐬��:����T�q
//
//  UVScrooll�������Ă�摜�ɓ����
//
// <�J������>
// 2022/05/2    �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
    //�\���t���[����
    public int frame = 3;
    //�J�E���g
    private int count;
    //�J�n�g
    public int startFrame = 0;
    //�I���g
    public int finishFrame = 0;
    //UVScrool
    private UVScroll uvScroll;
    //���[�v�L��
    public bool loop = false;
    // Start is called before the first frame update
    void Start()
    {
        //�{�X�V�[����Canvas2�ɕ\��
        GameObject canvas = GameObject.Find("Canvas2");
        if (!canvas)
        {
            //Canvas2������������i�{�X�V�[���ȊO�́jCanvas�ɕ\��
            canvas = GameObject.Find("Canvas");
        }

        transform.parent.gameObject.transform.SetParent(canvas.transform, true);

        uvScroll = gameObject.GetComponent<UVScroll>();
        //�J�n�ʒu�Ƀe�N�X�`���Z�b�g
        uvScroll.SetFrame(startFrame);
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�J�E���g�X�V
        ++count;
        //���̃A�j���[�V������
        if(count >= frame)
        {
            count = 0;
            uvScroll.SetNext();
            //�e�N�X�`���̈ꕔ���Ń��[�v����ꍇ�A�I���g�𒴂��Ă�����J�n�ʒu�ɖ߂�
            if(loop && uvScroll.GetnFrame() > finishFrame)
            {
                uvScroll.SetFrame(startFrame);
            }
            //�e�N�X�`���̈ꕔ���Ń��[�v����ꍇ�A�J�n�g��0����Ȃ����͊J�n�ʒu�ݒ������
            if(loop && startFrame > 0 && uvScroll.GetnFrame() <= 0){
                uvScroll.SetFrame(startFrame);
            }
            //�A�j���[�V��������ʂ�I����āA���[�v���Ȃ��ꍇ�͏���
            if (!loop && uvScroll.GetnFrame() <= 0)
            {
                Destroy(transform.parent.gameObject);  
            }
        }


    }
}
