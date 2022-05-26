//==========================================================
//      îwåiÇÃÇ®ãõ
//      çÏê¨ì˙Å@2022/05/26
//      çÏê¨é“Å@äCêÏçWók
//      
//      <äJî≠óöó>
//      2022/05/26      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFish : MonoBehaviour
{
    [SerializeField]
    Vector3 rotpos;

    [SerializeField]
    private float rotz;

    [SerializeField]
    private float speed;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position + rotpos;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, rotz));
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(position, Vector3.back, speed);
    }
    
}