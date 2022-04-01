//=============================================================================
//
// �J�����؂�ւ��p
//
// StartSwitching�ŃJ�����̐؂�ւ����ł��܂�
// �����ɂ͌��݂̃J�����A���̃J�����A�؂�ւ��̊Ԃ��Ԃ��邩�i����ꍇ��true�j���w�肵�Ă�������
// ��Ԃ�����ꍇ�̓V�[���ɕ�ԗp�̃J������1����āA�����InterpolationCamera.cs��ǉ����ĉ������B
// ��������Ȃ��ƕ�Ԃ��Ă���Ȃ��ł�
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

public static class CameraSwitch
{
    private static GameObject NextCamera = null;
    public static GameObject InterpolationCamera = null;
    //�J�����؂�ւ�
    //���� : ���̃J�����A���̃J�����A�Ԃ�⊮���邩
    public static void StartSwitching(GameObject Nowcam, GameObject Nextcam, bool b_interpolation)
    {
        //�J�����ۑ�
        NextCamera = Nextcam;

        //���̃J�����I�t
        Nowcam.GetComponent<Camera>().enabled = false;
        //��Ԃ��Ȃ��ꍇ�ƕ�ԗp�J�����������ꍇ�̓J�����؂�ւ��ă��^�[��
        if (!b_interpolation || InterpolationCamera == null)
        {
            //���̃J�����I��
            Nextcam.GetComponent<Camera>().enabled = true;
            return;
        }

        //�����̌v�Z
        Vector3 dir = Nextcam.transform.position - Nowcam.transform.position;
        //��ԗp�J�����ɕϐ����Z�b�g
        InterpolationCamera.GetComponent<InterpolationCamera>().StartInterpolation(Nowcam.transform.position, dir);
        //��ԗp�J�����L����
        InterpolationCamera.GetComponent<Camera>().enabled = true;
    }

    public static void FinishSwitching()
    {
        //��ԗp�J�����I�t
        InterpolationCamera.GetComponent<Camera>().enabled = false;
        //���̃J�����I��
        NextCamera.GetComponent<Camera>().enabled = true;

        NextCamera = null;
    }



}
