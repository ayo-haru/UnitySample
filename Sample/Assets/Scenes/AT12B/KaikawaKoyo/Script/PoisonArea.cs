//==========================================================
//      �ł̏�(�n�`�̒e���甭��)
//      �쐬���@2022/04/13
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/13      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    [SerializeField]
    private float StayTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, StayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
