//==========================================================
//      �v�����g�܂킷��
//      �쐬���@2022/05/27
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/05/27      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGPlantRota : MonoBehaviour
{
    [SerializeField]
    private bool TurnRight;

    [SerializeField]
    private float TurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPause)
        {
            if (TurnRight)
            {
                transform.Rotate(new Vector3(0.0f, TurnSpeed, 0.0f), Space.Self);
            }
            if(!TurnRight)
            {
                transform.Rotate(new Vector3(0.0f, -TurnSpeed, 0.0f), Space.Self);
            }
        }
    }
}
