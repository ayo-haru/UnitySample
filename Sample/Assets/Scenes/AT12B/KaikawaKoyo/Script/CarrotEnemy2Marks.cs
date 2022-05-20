using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy2Marks : MonoBehaviour
{
    GameObject Player;
    Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(-80.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //print(rb.velocity);
        //if(rb.velocity.x < 80)
        //{
        //    rb.velocity += new Vector3(2.0f, 0.0f, 0.0f);
        //}
        //else if()
        //if(rb.velocity.y < 80 && )
        //{
        //    rb.velocity += new Vector3(0.0f, 2.0f, 0.0f);
        //}
        //else if(rb.velocity.y > 0)
        //{
        //    rb.velocity -= new Vector3(0.0f, 2.0f, 0.0f);
        //}
        
     

    }
 
}
