using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_System: MonoBehaviour
{
    // Inspector
    [SerializeField] private ParticleSystem particle;

    // 1. çƒê∂
    private void Play()
    {
        particle.Play();
    }

    // 2. àÍéûí‚é~
    private void Pause()
    {
        particle.Pause();
    }

    // 3. í‚é~
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
