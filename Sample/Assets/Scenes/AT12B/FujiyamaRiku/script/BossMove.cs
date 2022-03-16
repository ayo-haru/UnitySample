using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //移動速度
    [SerializeField] private float MoveSpeed = 1f;
    //当てつけ左右変換
    bool ReftRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //ボスの移動の処理
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
