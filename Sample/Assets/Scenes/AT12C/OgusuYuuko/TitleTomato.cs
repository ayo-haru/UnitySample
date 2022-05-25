//==========================================================
//      タイトル用トマト
//      作成日　2022/05/24
//      作成者　小楠裕子
//      
//      <開発履歴>
//      2022/05/24  作成      
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
