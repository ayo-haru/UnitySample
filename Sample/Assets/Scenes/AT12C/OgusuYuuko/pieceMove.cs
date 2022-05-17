//ランタン移動用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceMove : MonoBehaviour
{
    public float moveSpeed = 0.5f;     //移動速度
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
        //移動
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
    }
}
