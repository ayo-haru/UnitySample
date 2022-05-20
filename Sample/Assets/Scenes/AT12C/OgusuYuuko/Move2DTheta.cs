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
// 2022/05/19 ���ɒe����鏈����ǉ�
// 2022/05/20 �e���ꂽ���̏�����ʃX�N���v�g�Ɉړ�
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
    //�����ʒu
    private Vector3 startPos;
    //��񂾂���������悤
    private bool onceFlag;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<RectTransform>();
        startPos = image.position;
        theta = 0.0f;
        onceFlag = true;
    }

    private void OnDisable()
    {
        theta = 0.0f;
        onceFlag = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onceFlag)
        {
            image.position = startPos;
            onceFlag = false;
        }

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
