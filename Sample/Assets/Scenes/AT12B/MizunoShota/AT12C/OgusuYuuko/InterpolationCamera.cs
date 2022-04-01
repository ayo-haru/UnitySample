//=============================================================================
//
// �J�����؂�ւ���ԗp�J����
//
//
// �쐬��:2022/03/24
// �쐬��:����T�q
//
// <�J������>
// 2022/03/24 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolationCamera : MonoBehaviour
{
    //�ړ�����
    public int MoveTime = 60;
    //����
    Vector3 Dir;
    //�^�C�}�[
    int Timer;
    //�g�p�t���O
    bool useFlag;
    // Start is called before the first frame update
    void Start()
    {
        //��ԗp�J�����ݒ�
        CameraSwitch.InterpolationCamera = gameObject;
        useFlag = false;
    }

    private void OnDestroy()
    {
        //��ԗp�J�����j��
        CameraSwitch.InterpolationCamera = null;
    }

    // Update is called once per frame
    void Update()
    {
        //�g���ĂȂ������烊�^�[��
        if (!useFlag)
        {
            return;
        }

        if (Timer < MoveTime)
        {
            //�^�C�}�[�X�V
            ++Timer;
            //�ʒu�X�V
            transform.position += Dir;
        }
        else
        {
            CameraSwitch.FinishSwitching();
            useFlag = false;
        }
    }
    
    //���� : �ʒu�A����
    public void StartInterpolation(Vector3 pos,Vector3 dir)
    {
        //�����ʒu�ݒ�
        gameObject.transform.position = pos;
        //�����ݒ�
        Dir = dir;
        Dir /= MoveTime;
        //�^�C�}�[�ݒ�
        Timer = 0;
        //�g�p�t���O���Ă�
        useFlag = true;
    }
}
