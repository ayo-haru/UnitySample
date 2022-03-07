using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    //�ړ����x
    public float speed = 0.1f;

    Rigidbody rb;

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //�@�\�̎擾
        rb = gameObject.GetComponent<Rigidbody>();

        //�v���C���[�擾
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = rb.position;
        //�E
        if(pos.x < Player.transform.position.x)
        {
            pos.x += speed;
            rb.position = pos;
        }

        //��
        if (pos.x > Player.transform.position.x)
        {
            pos.x -= speed;
            rb.position = pos;
        }

    }
}
