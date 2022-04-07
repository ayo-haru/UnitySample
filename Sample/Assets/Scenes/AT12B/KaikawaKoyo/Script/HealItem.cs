//==========================================================
//      �񕜃A�C�e���̏���
//      �쐬���@2022/04/05
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/05      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    Rigidbody rb;
    bool isGround = false;
    float aTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �n�ʂɂ����炿����ƕ����Ă���󒆂ɗ��܂�
        if (isGround)
        {
            aTime += Time.deltaTime;
            if(aTime < 1.0f)
            {
                rb.AddForce(transform.up * (4.0f * aTime), ForceMode.Force);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                // ���邭��񂵂���(�ǁX)
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �ڒn����
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.useGravity = false;
            isGround = true;
        }

        // �e���ꂽ�����
        if (collision.gameObject.name == "Weapon(Clone)" && isGround)
        {
            Destroy(gameObject, 0.0f);
        }


    }
}
