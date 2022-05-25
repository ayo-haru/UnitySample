//==========================================================
//      �^�C�g���p�g�}�g
//      �쐬���@2022/05/24
//      �쐬�ҁ@����T�q
//      
//      <�J������>
//      2022/05/24  �쐬      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTomato : MonoBehaviour
{
    Rigidbody rb;
    public float jumpSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            
            rb.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f),ForceMode.Impulse);
        }
    }
}
