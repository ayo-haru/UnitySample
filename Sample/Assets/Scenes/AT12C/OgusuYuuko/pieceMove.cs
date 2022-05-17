//�����^���ړ��p
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
    public float moveSpeed = 0.5f;     //�ړ����x
    public float moveWidth = 10.0f;     //�ړ���
    public bool startFlag;                 //�ړ��J�n�p�t���O

    // Start is called before the first frame update
    void Start()
    {
        theta = 0.0f;
        rt = GetComponent<RectTransform>();
        startPos = rt.position;
        ReturnFlag = false;
        FinishFlag = false;
        //startFlag = false;
        startFlag = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if(ReturnFlag && theta <= 75.0f)
        {
            FinishFlag = true;
        }
        rt.position = new Vector3(rt.position.x,startPos.y - (Mathf.Sin(Mathf.Deg2Rad * theta) * moveWidth),rt.position.z);
        Debug.Log("�T�C��"+Mathf.Sin(Mathf.Deg2Rad * theta));
    }
}
