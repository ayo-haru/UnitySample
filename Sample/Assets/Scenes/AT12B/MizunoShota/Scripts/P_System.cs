using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_System: MonoBehaviour
{
    // Inspector
    [SerializeField] private ParticleSystem particle;


    //�X�^�[�g���ɃG�t�F�N�g���Đ����Ȃ��B
    //Hierarchy�ɖ����Ɛ�������Ȃ����߁B���̂܂܂��ƍĐ�����Ă��܂�����B
    private void Start()
    {
        Stop();
    }

    // 1. �Đ�(�֐��Ăяo���A�������͒��̏������L�ڂ���Ɠ����܂�)
    private void Play()
    {
        particle.Play();
    }

    // 2. �ꎞ��~ ����͂��܂�g��Ȃ��Ǝv���B
    private void Pause()
    {
        particle.Pause();
    }

    // 3. ��~
    private void Stop()
    {
        particle.Stop();
    }

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        Play();
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        particle.Pause();
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        particle.Stop();
    //    }
    //}
}
