//=============================================================================
//
// ���D�ړ�
//
// �쐬��:2022/05/11
// �쐬��:����T�q
//
// <�J������>
// 2022/05/11 ����
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMove : MonoBehaviour
{
    //�����ʒu
    //private Vector3 startPos;
    //�ړ����x
    public float moveSpeed = 0.01f;
    //����ʒu
    public float finPos = 10.0f;
    //�p�x
    private float theta;
    // Start is called before the first frame update
    void Start()
    {
        //startPos = gameObject.transform.position;
        theta = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        theta += moveSpeed;
        if (theta >= 180.0f)
        {
            theta -= 360.0f;
        }
        //���D�㉺
        gameObject.transform.position += gameObject.transform.up * finPos * Mathf.Sin(theta) * 0.01f;

        //�㏸�A���~�؂�ւ�
        //if(transform.position.y >= finPos�@||�@transform.position.y <= startPos.y)
        //{
        //    moveDir *= -1;
        //}
    }
}
