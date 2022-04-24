//=============================================================================
//
// �}�b�v�̌��ݒn�����������
//
//
// �쐬��:2022/04/24
// �쐬��:����T�q
//
// <�J������>
// 2022/04/24 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownArrow : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //�p�x
    private float theta;
    //����������
    public float moveSpeed = 0.1f;
    //��������
    public float moveWidth = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<RectTransform>();
        theta = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //theta�X�V
        theta += moveSpeed;
        //theta�␳
        if(theta >= 180.0f)
        {
            theta -= 360.0f;
        }else if(theta <= -180.0f)
        {
            theta += 360.0f;
        }
        //�摜�ʒu�X�V
        image.transform.position += image.transform.up * moveWidth * (Mathf.Sin(theta));
    }
}
