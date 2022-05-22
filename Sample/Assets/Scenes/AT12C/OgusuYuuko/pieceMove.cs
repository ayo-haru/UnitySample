//=============================================================================
//
// UI�ړ��p
//
// �쐬��:2022/05/17
// �쐬��:����T�q
//
// <�J������>
// 2022/05/17 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceMove : MonoBehaviour
{
    private float theta;
    private RectTransform rt;
    private Vector3 startPos;           //�����ʒu
    private bool ReturnFlag;            //�܂�Ԃ��p�t���O
    private bool FinishFlag;            //�I���t���O
    private bool VibrationFlag;         //�U���p�t���O
    public float moveSpeed = 0.5f;     //�ړ����x
    public float vibrationSpeed = 10.0f;        //�U�����x
    public float moveWidth = 10.0f;     //�ړ���
    public float vibrationWidth = 10.0f;        //�U����
    public bool startFlag = true;                 //�ړ��J�n�p�t���O
    private Vector3 defaultPos;             //�K��ʒu
    

    // Start is called before the first frame update
    void Start()
    {
        theta = 0.0f;
        rt = GetComponent<RectTransform>();
        startPos = rt.position;
        defaultPos = new Vector3(0.0f, 0.0f, 0.0f);
        ReturnFlag = false;
        FinishFlag = false;
        VibrationFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�U������
        if (VibrationFlag)
        {

            //theta�X�V
            theta += vibrationSpeed;

            rt.position += rt.right * Mathf.Sin(theta) * vibrationWidth;

            if(theta >= 90 || theta <= -90)
            {
                rt.position = defaultPos;
                VibrationFlag = false;
            }

            //�I������烊�^�[��
            return;

        }
        if (FinishFlag||!startFlag)
        {
            //�I����Ă��牽�������Ƀ��^�[��
            return;
        }

        //theta�X�V
        theta += moveSpeed;
        if(theta >= 90.0f && !ReturnFlag)
        {
            moveSpeed *= -1;
            ReturnFlag = true;
        }
        rt.position = new Vector3(rt.position.x,startPos.y - (Mathf.Sin(Mathf.Deg2Rad * theta) * moveWidth),rt.position.z);
        if(ReturnFlag && theta <= 75.0f)
        {
            defaultPos = rt.position;
            FinishFlag = true;
        }
    }

    public void vibration()
    {
        //�ړ����������烊�^�[��
        if (!FinishFlag || VibrationFlag)
        {
            return;
        }


        VibrationFlag = true;
        theta = 0.0f;

    }
}
