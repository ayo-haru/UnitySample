//==========================================================
//      ラスボス前の落ちる床
//      作成日　2022/05/29
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/05/29      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LastBossBlock : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private Vector3 Rot;
    private float MoveSpeed;
    private float Timer;
    private float ShakeTime = 0.5f;
    private bool fall;
    private bool ride;
    private bool up = false;
    private bool down = false;

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        StartPos = transform.position;
        TargetPos = new Vector3(StartPos.x, -500.0f, StartPos.z);
        Rot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(down && !ride)
        {
            Timer += Time.deltaTime;
            if(Timer > 0.2f)
            {
                ride = true;
                Timer = 0.0f;
            }
        }
        if(!down && !ride && !up)
        {
            Timer += Time.deltaTime;
            if(Timer > 0.2f)
            {
                transform.position += new Vector3(0.0f, 2.5f, 0.0f);
                up = true;
                Timer = 0.0f;
            }
        }
        
        if(fall)
        {
            Timer += Time.deltaTime;
            if (Timer > ShakeTime)
            {
                float step = MoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
                Rot += new Vector3(0.4f, 0.0f, 0.4f);
                transform.rotation = Quaternion.Euler(Rot);
                MoveSpeed += 1.5f;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        // 弾かれたら震わせて落とす
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            transform.DOShakePosition(duration: ShakeTime, strength: 5.0f);    // ぶるぶる震わせる
            fall = true;
            Destroy(gameObject, 5.0f);
        }

        // プレイヤーが上に乗ったらちょっと沈ませる
        if(collision.gameObject.CompareTag("Player") && !down)
        {
            transform.position -= new Vector3(0.0f, 2.5f, 0.0f);
            up = false;
            down = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // プレイヤーが上からどいたらちょっと浮かせる
        if (collision.gameObject.CompareTag("Player") && ride)
        {
            down = false;
            ride = false;
        }
    }

}
