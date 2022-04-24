//=============================================================================
//
// 2D�ړ��p
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

public class Move2DTheta : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //����������
    public Vector2 dir = new Vector2(0.0f,0.0f);
    //�ړ����x
    public float moveSpeed = 0.1f;
    //��������
    public float moveWidth = 0.5f;
    //�p�x
    private float theta;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<RectTransform>();
        theta = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�p�x�X�V
        theta += moveSpeed;
        //�p�x�␳
        if (theta >= 180.0f)
        {
            theta -= 360.0f;
        }
        else if (theta <= -180.0f)
        {
            theta += 360.0f;
        }

        //�摜�ʒu�X�V
        image.transform.position += image.transform.up * dir.y * moveWidth * (Mathf.Sin(theta));
        image.transform.position += image.transform.right * dir.x * moveWidth * (Mathf.Sin(theta));
    }
}
