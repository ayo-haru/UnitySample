using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotation : MonoBehaviour
{
    //---ïœêîêÈåæ
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = target.transform.position - this.transform.position;
        vector3.y = 0.0f;

        Quaternion quaternion = Quaternion.LookRotation(vector3);

        this.transform.rotation = quaternion;
    }
}
