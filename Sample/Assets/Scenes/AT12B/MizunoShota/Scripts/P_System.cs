using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_System: MonoBehaviour
{
    // Inspector
    [SerializeField] private ParticleSystem particle;

    // 1. �Đ�
    private void Play()
    {
        particle.Play();
    }

    // 2. �ꎞ��~
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
