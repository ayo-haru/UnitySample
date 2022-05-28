using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBall_Deceleration : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 70.0f;
    }

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }
}