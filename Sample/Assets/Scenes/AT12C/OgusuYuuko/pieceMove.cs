//�����^���ړ��p
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceMove : MonoBehaviour
{
    public float moveSpeed = 0.5f;     //�ړ����x
    private int cnt = 0;
    public int StopCnt = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cnt >= StopCnt)
        {
            return;
        }
        ++cnt;
        //�ړ�
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
    }
}
