using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Vector3 BossPos = new Vector3(10, 0.02f, -2);
    //int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = BossPos;
    }

    // Update is called once per frame
    void Update()
    {
        //ボスの座標
        this.gameObject.transform.position = BossPos;
    }
}
