//=============================================================================
//
// �_�ړ�
//
// �쐬��:2022/05/12
// �쐬��:����T�q
//
// <�J������>
// 2022/05/12 ����
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    //�ړ����x
    private float moveSpeed;
    //�ړ��͈�
    private float moveWidth;
    //�p�x
    private float theta;
    // Start is called before the first frame update
    void Start()
    {
        //�����_���ňړ����x�ƈړ�������
        moveSpeed = Random.Range(1, 5) / 1000.0f;
        moveWidth = Random.Range(10, 20) / 100.0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!Pause.isPause)
        {
            theta += moveSpeed;
            if (theta >= 180.0f)
            {
                theta -= 360.0f;
            }
            //�ړ�
            gameObject.transform.position += gameObject.transform.right * moveWidth * Mathf.Sin(theta);
        }
    }
}
