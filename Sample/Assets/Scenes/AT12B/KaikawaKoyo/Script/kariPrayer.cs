using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariPrayer : MonoBehaviour
{
    Rigidbody rb;
    private bool jump;
    private float Jump = 500.0f;
    private float MoveSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.right * -MoveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * MoveSpeed, ForceMode.Force);
        }

        if(!jump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * Jump, ForceMode.Force);
                jump = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            jump = false;
        }

        if(gameObject.CompareTag("Damaged"))
        {
            rb.AddForce(transform.right * 10.0f, ForceMode.Force);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.CompareTag("Ground"))
        {
            jump = false;
        }
    }
}
