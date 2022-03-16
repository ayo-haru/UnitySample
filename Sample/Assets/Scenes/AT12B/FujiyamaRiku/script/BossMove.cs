using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //�ړ����x
    [SerializeField] private float MoveSpeed = 1f;
    //���Ă����E�ϊ�
    bool ReftRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //�{�X�̈ړ��̏���
        if(Boss.BossPos.x < -10)
        {
            ReftRight = false;
        }
        if(Boss.BossPos.x > 10 )
        {
            ReftRight = true;
        }
        if(ReftRight)
        {
            Boss.BossPos.x -= 1 * MoveSpeed;
        }
        if (!ReftRight)
        {
            Boss.BossPos.x += 1 * MoveSpeed;
        }
    }
}
