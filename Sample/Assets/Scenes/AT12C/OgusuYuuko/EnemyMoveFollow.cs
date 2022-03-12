using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    //移動速度
    public float speed = 0.01f;

    //Rigidbody rb;

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //機能の取得
        //rb = gameObject.GetComponent<Rigidbody>();

        //プレイヤー取得
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!(this.GetComponent<BaunceEnemy>().isAlive))
        {
            return;
        }
        //Vector3 pos = rb.position;
        //右
        if(transform.position.x < Player.transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            //rb.position = pos;
        }

        //左
        if (transform.position.x > Player.transform.position.x)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            //rb.position = pos;
        }

    }
}
