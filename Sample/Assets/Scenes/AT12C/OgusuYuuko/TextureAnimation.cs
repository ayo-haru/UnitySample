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
            //�A�j���[�V��������ʂ�I����āA���[�v���Ȃ��ꍇ�͏���
            if (!loop && uvScroll.GetnFrame() <= 0)
            {
                Destroy(transform.parent.gameObject);  
            }
        }


    }
}
