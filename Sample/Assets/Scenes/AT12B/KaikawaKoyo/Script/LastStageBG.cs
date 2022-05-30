using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStageBG : MonoBehaviour
{
    /*[SerializeField]
    private GameObject S;
    private ShadowEnemy SE;*/
    private Rigidbody rb;
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private Vector3 Velocity;
    private Vector3 rot = new Vector3(0.0f, 0.0f, 5.0f);
    private Quaternion StartRot;

    private float dis;
    private float MoveSpeed = 300.0f;
    private float MoveSpeed2 = 200.0f;
    private float CamCenter;
    private float CamZ;
    private Vector3 CamRightTop;    // カメラの右上座標
    private Vector3 CamLeftBot;     // カメラの左下座標

    private bool True = true;
    private bool Right;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Start()
    {
        /*SE = S.GetComponent<ShadowEnemy>();*/
        rb = gameObject.GetComponent<Rigidbody>();
        StartPos = transform.position;
        StartRot = transform.rotation;
        CamZ = Vector3.Distance(new Vector3(0.0f, 0.0f, transform.position.z), new Vector3(0.0f, 0.0f, Camera.main.transform.position.z));
        Velocity = new Vector3(0.0f, 200.0f, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(/*SE.TT && */ True)
        {
            CamRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, CamZ));
            CamLeftBot = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, CamZ));
            if (!isCalledOnce)
            {
                CamCenter = Camera.main.transform.position.x;

                // 右に移動
                if (CamCenter < transform.position.x)
                {
                    TargetPos = new Vector3(CamRightTop.x + 120.0f, StartPos.y, StartPos.z);
                    rb.velocity = Velocity;
                    Right = true;
                }
                // 左に移動
                if (CamCenter > transform.position.x)
                {
                    TargetPos = new Vector3(CamLeftBot.x + -120.0f, StartPos.y, StartPos.z);
                    rb.velocity = Velocity;
                    Right = false;
                }
                
                isCalledOnce = true;
            }

           rb.velocity -= new Vector3(0.0f, 2.0f, 2.5f);

            if(Right)
            {
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(transform.position, TargetPos, step);
                transform.Rotate(rot, Space.Self);
                if (transform.position.x > CamRightTop.x + 100.0f)
                {
                    transform.position = new Vector3(StartPos.x, CamLeftBot.y - 50.0f, StartPos.z);
                    transform.rotation = StartRot;
                    rb.velocity = new Vector3(0, 0, 0);
                    True = false;
                }
            }
            if (!Right)
            {
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(transform.position, TargetPos, step);
                transform.Rotate(rot, Space.Self);
                if (transform.position.x < CamLeftBot.x - 100.0f)
                {
                    transform.position = new Vector3(StartPos.x, CamLeftBot.y - 50.0f, StartPos.z);
                    transform.rotation = StartRot;
                    rb.velocity = new Vector3(0, 0, 0);
                    True = false;
                }
            }
        }

        if(!True/* && !SE.TT*/)
        {
            if (transform.position.y < StartPos.y)
            {
                rb.velocity = new Vector3(0.0f, MoveSpeed2, 0.0f);
                MoveSpeed2 += 3.0f;
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
                MoveSpeed2 = 200.0f;
                isCalledOnce = false;
                True = true;
            }
        }
    }
}
