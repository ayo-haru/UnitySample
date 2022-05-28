//==========================================================
//      �w�i�̃j���W��
//      �쐬���@2022/05/26
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/05/26      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCarrot : MonoBehaviour
{
    [SerializeField]
    Vector3 rotpos;

    [SerializeField]
    private float speed;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position + rotpos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPause)
        {
            transform.RotateAround(position, Vector3.down, speed);
        }
    }
}