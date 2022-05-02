using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if(rb == null){
            return;
        }

        var Indirection = rb.velocity;

        var InNormal = transform.up;

        var Result = Vector3.Reflect(Indirection,InNormal);

        rb.velocity = Result * 50.0f;
    }
}
