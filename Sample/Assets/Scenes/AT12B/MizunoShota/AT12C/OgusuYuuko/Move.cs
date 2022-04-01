using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Move : MonoBehaviour
{
    //移動速度
    public float speed = 0.1f;

    Rigidbody rb;

    private AudioSource[] audioSourceList = new AudioSource[5];    // 一回に同時にならせる数

    // Start is called before the first frame update

    void Start()
    {
        //機能の取得
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = rb.position;
        //右
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed;
            rb.position = pos;
        }

        //左
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed;
            rb.position = pos;
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 300.0f);
            //SoundManager.Play(SoundData.eSE.SE_JUMP, audioSourceList);
        }
    }
}