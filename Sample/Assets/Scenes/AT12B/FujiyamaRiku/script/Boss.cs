using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public static GameObject Bossobj;
    public static Vector3 BossPos;
    // Start is called before the first frame update
    void Start()
    {
        Bossobj = GameObject.Find("Boss");
        BossPos = GameObject.Find("BossPoint").transform.position;
        this.gameObject.transform.position = BossPos;
    }

    // Update is called once per frame
    void Update()
    {
        //ボスの座標
        this.gameObject.transform.position = BossPos;
    }
}
