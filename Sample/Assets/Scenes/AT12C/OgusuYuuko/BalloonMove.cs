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
    private Vector3 startPos;
    //�ړ����x
    public float moveSpeed = 0.1f;
    //����ʒu
    public float finPos = 100.0f;
    //�ړ�����
    private float moveDir;  //�P���㏸�|�P�����~
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        moveDir = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //���D�㉺
        gameObject.transform.position += gameObject.transform.up * moveSpeed * moveDir;

        //�㏸�A���~�؂�ւ�
        if(transform.position.y >= finPos�@||�@transform.position.y <= startPos.y)
        {
            moveDir *= -1;
        }
    }
}
