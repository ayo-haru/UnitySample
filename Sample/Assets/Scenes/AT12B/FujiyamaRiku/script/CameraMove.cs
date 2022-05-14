using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�����ړ�������
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera camera; //�J�����i�[�p(�w�肵�Ȃ��Ă��ꉞ����)
    private float g_MoveNowTime;            //���݂̈ړ��ʊi�[�p
    private float g_MoveTime;               //�ړ����Ԃ̊i�[�p
    private float g_DelayTime;              //�f�B���C�����̎��Ԋi�[�p
    private Vector3 g_CameraPos;            //�J�����̍��W�i�[�p
    private Vector3 g_CameraStartPos;       //�J�����̏����ʒu�i�[�p
    private Vector3 g_CameraEndPos;         //�J�����̓����n�_�i�[�p
    private bool ReturnFlg;                 //�߂鏈�������邽�߂̃t���O
    private bool DelayFlg;                  //�f�B���C�������鎞�p�̃t���O
    private bool InitFlg;                   //�����̏��߂Ɉ�x�����������鎞�p�̃t���O
    // Start is called before the first frame update
    void Start()
    {
        //�J�������w�肵�Ȃ�����̓��C���̃J�����𓮂���
        camera = Camera.main;
        DelayFlg = false;
        g_MoveNowTime = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetCamera(Camera Usecamera)
    {
        camera = Usecamera;
    }

    //�����݂̍��W����w�肵�����W�܂ōs������
    //�x�W�G�Ȑ��ō�邽�ߑ�Z���x�W�G�Ȑ��Ή�
    //==========================================================
    //�ꎟ�x�W�G�Ȑ�
    //���Ԏw��o�[�W����
    //�Ō��bool�͌��̈ʒu�ɓ������x�Ŗ߂��鏈�����g�����ǂ����B
    //�Ō�̂��g�킸�ɓ�x�錾���đ��x��ς��Ă��ǂ�
    /// <summary>
    /// ���Ԃ��w�肵�āA�w�肵�����W�Ɉړ����鏈��(����)
    /// <para>�߂�l�F�������Ă�Ԃ�true�A�����̏I������false</para>
    /// <para>CameraStartPos�F�J�n�n�_�̍��W</para>
    /// <para>CameraEndPos�F�I���n�_�̍��W</para>
    /// <para>MoveTime�F�����܂ł̎���</para>
    /// </summary>
    /// <param name="CameraStartPos">�J�n�n�_�̍��W</param>
    /// <param name="CameraEndPos">�I���n�_�̍��W</param>
    /// <param name="MoveTime">�����܂ł̎���</param>
    /// <returns></returns>
    public bool MoveCameraTime(Vector3 CameraStartPos,Vector3 CameraEndPos, float MoveTime) //�������Ă�Ԃ�true �����̏I������false���Ă�
    {
        g_MoveNowTime += 1.0f / (MoveTime * 60.0f); //�x�W�G�Ȑ���1�܂ł̋����𓮂��Ă����̂�
                                              //���Ԏw�肷�邽�߂Ƀt���[�����������Ă���1�������Ă���
                                              //����𑫂����ƂŁZ�b���������Ă�(1 / ���������� * �P�b�Ԃɏ�������t���[����)
                            
        g_CameraPos = Vector3.Lerp(CameraStartPos, CameraEndPos, g_MoveNowTime);
        //�J�����̍��W�X�V����
        camera.transform.position = g_CameraPos;
        if (g_MoveNowTime >=1.0f) //�x�W�G�Ȑ���1�ɓ��B���Ă����ꍇ(�G���h�|�C���g�ɓ������Ă����ꍇ)
        {
            return false;
        }
        return true;
    }

    //==========================================================
    /// <summary>
    /// ���Ԏw��Ŏw�肵�����W�ֈړ����A���̌㌳�ɖ߂��Ă��鏈��(����)
    /// <para>�߂�l�F�������Ă�Ԃ͂O�A�ړI�̍��W�ɂ����u�ԂP�A���̈ʒu�ɖ߂��Ă����Ƃ��Q</para>
    /// <para>CameraStartPos�F�J�n�n�_�̍��W</para>
    /// <para>CameraEndPos�F�I���n�_�̍��W</para>
    /// <para>MoveTime�F�����܂ł̎���</para>
    /// <para>ReturnMoveTIme�F�߂��Ă���܂ł̎���</para>
    /// <para>ReturnDelayTime�F�f�B���C�̎���</para>
    /// </summary>
    /// <param name="CameraStartPos">�J�n�n�_�̍��W</param>
    /// <param name="CameraEndPos">�I���n�_�̍��W</param>
    /// <param name="MoveTime">�����܂ł̎���</param>
    /// <param name="ReturnMoveTime">�߂��Ă���܂ł̎���</param>
    /// <param name="ReturnDelayTime">�f�B���C�̎���</param>
    /// <returns></returns>
    //�߂��Ă��鎞�Ԃ��w�肵���ꍇ���̍��W�Ɏw�肵�����x�Ŗ߂��Ă���
    public int MoveCameraTime(Vector3 CameraStartPos, Vector3 CameraEndPos, 
                              float MoveTime, float ReturnMoveTime , float ReturnDelayTime) //�������Ă�Ԃ�0 ��x�ړI�̍��W�ɂ����u�ԂP �߂��Ă����u�ԂQ
    {
        //�ŏ��Ɉ�񂾂��������镔��
        if (!InitFlg)
        {
            g_MoveTime = MoveTime;              //�ŏ��ɓ�������܂ł̎��Ԃ��i�[
            g_CameraStartPos = CameraStartPos;  //�����n�_���i�[
            g_CameraEndPos = CameraEndPos;      //�I���n�_���i�[
            InitFlg = true;                     
        }

        //�߂�܂ł̃f�B���C����
        if (DelayFlg)
        {
            //��t���[�����Ƃɑ����Z���Ă���(Update����{�t���[�����ƂɍX�V���Ă���邩�瓮��)
            g_DelayTime += ((1.0f / 60.0f));
            //�Z�b�o�߂�����f�B���C�̏�������߂Ė߂鏈���Ɉڍs����
            if (g_DelayTime >= ReturnDelayTime)
            {
                DelayFlg = false;
            }
        }

        //�f�B���C����������Ă��Ȃ���(�J�����������Ă��鏈�������Ă���Ƃ�)
        else
        {
            g_MoveNowTime += 1 / (MoveTime * 60); //�x�W�G�Ȑ���1�܂ł̋����𓮂��Ă����̂�
                                                  //���Ԏw�肷�邽�߂Ƀt���[�����������Ă���1�������Ă���
                                                  //����𑫂����ƂŁZ�b���������Ă�(1 / ���������� * �P�b�Ԃɏ�������t���[����)

            //�X�^�[�g�ƃG���h�̍��W��؂�ւ��Ă����Ȃ��悤�ϐ��ŏ��������Ă���
            g_CameraPos = Vector3.Lerp(g_CameraStartPos, g_CameraEndPos, g_MoveNowTime);
            //�J�����̍��W�X�V
            camera.transform.position = g_CameraPos;
        }

        if (g_MoveNowTime >= 1.0f) //�x�W�G�Ȑ���1�ɓ��B���Ă����ꍇ
        {
            Debug.Log("����߂�" + ReturnFlg);
            if (ReturnFlg)//���̈ʒu�ɖ߂��Ă����Ƃ��ɂ��̏������I��������Ƃɂ���
            {
                g_MoveNowTime = 0.0f;
                InitFlg = false;
                ReturnFlg = false;
                return 2;
            }
            
            //���ڂ̏������I������Ƃ��ɖ߂鏈���ɕύX����
            g_MoveNowTime = 0.0f;               //�ϐ���Ŏ��Ԃ��ė��p
            MoveTime = ReturnMoveTime;          //�A��̎��Ԃ���
            g_CameraStartPos = CameraEndPos;    //�A��̎��̏o���n�_�̍��W��ύX
            g_CameraEndPos = CameraStartPos;    //�A��̎��̓����n�_�̍��W��ύX
            ReturnFlg = true;                   //�����Ō��̈ʒu�ɖ߂鏈���ɂ���
            DelayFlg = true;                    //�f�B���C�������鏈���p�t���Oon
            return 1;
        }
        return 0;
    }

    //==========================================================
    /// <summary>
    /// ���x���w�肵�āA�w�肵�����W�Ɉړ����鏈��(����)
    /// <para>�߂�l�F�������Ă�Ԃ�true�A�����̏I������false</para>
    /// <para>CameraStartPos�F�J�n�n�_�̍��W</para>
    /// <para>CameraEndPos�F�I���n�_�̍��W</para>
    /// <para>MoveSpeed�F�������x</para>
    /// </summary>
    /// <param name="CameraStartPos">�J�n�n�_�̍��W</param>
    /// <param name="CameraEndPos">�I���n�_�̍��W</param>
    /// <param name="MoveSpeed">�������x</param>
    /// <returns></returns>
    //�X�s�[�h�w��o�[�W����
    public bool MoveCameraSpeed(Vector3 CameraStartPos, Vector3 CameraEndPos, float MoveSpeed) //�������Ă�Ԃ�true �����̏I������false���Ă�
    {
        g_MoveNowTime += (1.0f / 60.0f) * MoveSpeed; //�x�W�G�Ȑ���1�܂ł̋����𓮂��Ă����̂�
                                               //���x���w�肵�ĂP�t���[���œ���������ς��Ă���
                                               //�����l�́Z�{�̑��x�Ȃǂ��ł���B
                                               
        g_CameraPos = Vector3.Lerp(CameraStartPos, CameraEndPos, g_MoveNowTime);
        //�J�����̍��W�X�V
        camera.transform.position = g_CameraPos;

        if (g_MoveNowTime >= 1.0f) //�x�W�G�Ȑ���1�ɓ��B���Ă����ꍇ(�G���h�|�C���g�ɓ������Ă����ꍇ)
        {
            g_MoveNowTime = 0.0f;
            return false;
        }
        return true;
    }

    //==========================================================
    /// <summary>
    /// ���x�w��Ŏw�肵�����W�ֈړ����A���̌㌳�̈ʒu�ɖ߂��Ă��鏈��(����)
    /// <para>�߂�l�F�������Ă�Ԃ͂O�A�ړI�̍��W�ɂ����u�ԂP�A���̈ʒu�ɖ߂��Ă����Ƃ��Q</para>
    /// <para>CameraStartPos�F�J�n�n�_�̍��W</para>
    /// <para>CameraEndPos�F�I���n�_�̍��W</para>
    /// <para>MoveSpeed�F�������x</para>
    /// <para>ReturnMoveSpeed�F�߂��Ă���Ƃ��̑��x</para>
    /// <para>ReturnDelayTime�F�f�B���C�̎���</para>
    /// </summary>
    /// <param name="CameraStartPos">�J�n�n�_�̍��W</param> 
    /// <param name="CameraEndPos">�I���n�_�̍��W</param>
    /// <param name="MoveSpeed"> �������x</param>
    /// <param name="ReturnMoveSpeed">�߂��Ă���Ƃ��̑��x</param>
    /// <param name="ReturnDelayTime">�f�B���C�̎���</param>
    /// <returns></returns>
    //�X�s�[�h�w��Ō��̏ꏊ�ɖ߂�o�[�W����
    public int MoveCameraSpeed(Vector3 CameraStartPos, Vector3 CameraEndPos,
                              float MoveSpeed, float ReturnMoveSpeed, float ReturnDelayTime) //�������Ă�Ԃ�0 ��x�ړI�̍��W�ɂ����u�ԂP �߂��Ă����u�ԂQ
    {
        //�ŏ��Ɉ�񂾂��������镔��
        if (!InitFlg)
        {
            g_MoveTime = MoveSpeed;              //�ŏ��ɓ�������܂ł̎��Ԃ��i�[
            g_CameraStartPos = CameraStartPos;  //�����n�_���i�[
            g_CameraEndPos = CameraEndPos;      //�I���n�_���i�[
            InitFlg = true;
        }

        //�߂�܂ł̃f�B���C����
        if (DelayFlg)
        {
            //��t���[�����Ƃɑ����Z���Ă���(Update����{�t���[�����ƂɍX�V���Ă���邩�瓮��)
            g_DelayTime += ((1.0f / 60.0f));
            //�Z�b�o�߂�����f�B���C�̏�������߂Ė߂鏈���Ɉڍs����
            if (g_DelayTime >= ReturnDelayTime)
            {
                DelayFlg = false;
            }
        }

        //�f�B���C����������Ă��Ȃ���(�J�����������Ă��鏈�������Ă���Ƃ�)
        else
        {
            g_MoveNowTime += (1.0f / 60.0f) * MoveSpeed; //�x�W�G�Ȑ���1�܂ł̋����𓮂��Ă����̂�
                                                         //���x�w��̂��߂P�t���[���œ�������
                                                         //�����I�ɃX�s�[�h���̃t���[�����������Ƃő��x�̕ω����N�����Ă���悤�Ɍ�����
                                                         //����𑫂����Ƃ�(��{�̑��x * �w�肵�����x)

            //�X�^�[�g�ƃG���h�̍��W��؂�ւ��Ă����Ȃ��悤�ϐ��ŏ��������Ă���
            g_CameraPos = Vector3.Lerp(g_CameraStartPos, g_CameraEndPos, g_MoveNowTime);
            //�J�����̍��W�X�V
            camera.transform.position = g_CameraPos;
        }

        if (g_MoveNowTime >= 1.0f) //�x�W�G�Ȑ���1�ɓ��B���Ă����ꍇ
        {
            Debug.Log("����߂�" + ReturnFlg);
            if (ReturnFlg)//���̈ʒu�ɖ߂��Ă����Ƃ��ɂ��̏������I��������Ƃɂ���
            {
                g_MoveNowTime = 0.0f;
                InitFlg = false;
                ReturnFlg = false;
                return 2;
            }

            //���ڂ̏������I������Ƃ��ɖ߂鏈���ɕύX����
            g_MoveNowTime = 0.0f;               //�ϐ���Ŏ��Ԃ��ė��p
            MoveSpeed = ReturnMoveSpeed;          //�A��̑��x����
            g_CameraStartPos = CameraEndPos;    //�A��̎��̏o���n�_�̍��W��ύX
            g_CameraEndPos = CameraStartPos;    //�A��̎��̓����n�_�̍��W��ύX
            ReturnFlg = true;                   //�����Ō��̈ʒu�ɖ߂鏈���ɂ���
            DelayFlg = true;                    //�f�B���C�������鏈���p�t���Oon
            return 1;
        }
        return 0;
    }

    //==========================================================
    //�񎟃x�W�G�Ȑ�


}
