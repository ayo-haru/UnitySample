using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossBG : MonoBehaviour
{
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private float CamCenter;
    private float CamZ;
    private Vector3 CamRightTop;    // カメラの右上座標
    private Vector3 CamLeftBot;     // カメラの左下座標

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<>();
        StartPos = transform.position;
        CamZ = Camera.main.transform.position.z + transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if()
        //{
            CamCenter = Camera.main.transform.position.x;
            CamRightTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, CamZ));
            CamLeftBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, CamZ));

            // 左に移動
            if (CamCenter > transform.position.x)
            {
                TargetPos = new Vector3(CamLeftBot.x, transform.position.y, transform.position.z);
            }
            // 右に移動
            if (CamCenter < transform.position.x)
            {
                TargetPos = new Vector3(CamRightTop.x, transform.position.y, transform.position.z);
            }

            float step = 100.0f * Time.deltaTime;

            transform.position = Vector3.Slerp(StartPos, TargetPos, step);
            
        //}
    }
}
