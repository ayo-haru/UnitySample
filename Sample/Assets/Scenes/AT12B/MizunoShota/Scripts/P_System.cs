using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_System: MonoBehaviour
{
    // Inspector
    [SerializeField] private ParticleSystem particle;


    //スタート時にエフェクトを再生しない。
    //Hierarchyに無いと生成されないため。そのままだと再生されてしまうから。
    private void Start()
    {
        Stop();
    }

    // 1. 再生(関数呼び出し、もしくは中の処理を記載すると動きます)
    private void Play()
    {
        particle.Play();
    }

    // 2. 一時停止 これはあまり使わないと思う。
    private void Pause()
    {
        particle.Pause();
    }

    // 3. 停止
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
