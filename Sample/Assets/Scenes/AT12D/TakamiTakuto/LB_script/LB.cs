using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB : MonoBehaviour
{
    public static GameObject LB_obj;
    public static Vector3 LB_Pos;
    // Start is called before the first frame update
    void Start()
    {
        LB_obj = GameObject.Find("LB(Clone)");
        LB_Pos = GameObject.Find("LB_Point").transform.position;
        this.gameObject.transform.position = LB_Pos;
    }

    // Update is called once per frame
    void Update()
    {
        //É{ÉXÇÃç¿ïW

    }
}
