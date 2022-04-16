//=============================================================================
//
// �񕜃A�C�e������
//
// �쐬��:2022/04/16
// �쐬��:�g����
//
// <�J������>
// 2022/04/16   �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    //---�ϐ��錾
    public GameObject prefab;
    Rigidbody rb;
    public float BounceSpeed = 10.0f;                   // �e�����X�s�[�h
    public float BounceVectorMultiple = 2.0f;           // �@���x�N�g���ɏ�Z����l
    public float BouncePower = 100.0f;                  // �e�����l
    private Vector3 Velocity;                           // �e���x�N�g��
    [System.NonSerialized]bool isGroundFlg = false;     // �n�ʂƂ̐ڒn�t���O
    private float aTime;

    // Start is called before the first frame update
    void Start()
    {
        // �v���n�u�𕡐�
        
        //GameObject HealItem = Instantiate(prefab,
        //                                  new Vector3(0.0f,0.0f,0.0f),
        //                                  Quaternion.identity);
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        // �n�ʂɂ����炿����ƕ����Ă���󒆂ɗ��܂�
        if (isGroundFlg)
        {
            aTime += Time.deltaTime;
            if (aTime < 1.0f)
            {
                rb.AddForce(transform.up * (2.0f * aTime), ForceMode.Force);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY;
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
            isGroundFlg = true;
        }

        if (collision.gameObject.name == "Weapon(Clone)" && isGroundFlg)
        {
            // �Փ˂����ʂ̐ڒn�_�̃x�N�g�����擾
            Vector3 normal = collision.contacts[0].normal; 
            
            // �Փ˂������x�x�N�g����P�ʃx�N�g���ɒu��������
            Velocity = collision.rigidbody.velocity.normalized;

            // x,y,z�����ɑ΂��Ė@���x�N�g�����擾
            Velocity += new Vector3(normal.x * BounceVectorMultiple,
                                    normal.y * BounceVectorMultiple,
                                    normal.z * BounceVectorMultiple);

            // �t�����ɒ��˕Ԃ�
            collision.rigidbody.AddForce(-Velocity * BounceSpeed,ForceMode.Impulse);

            
            rb.useGravity = false;                          // �d�͂�����
            rb.angularDrag = 0.0f;                          // ��C��R���[����
            rb.centerOfMass = new Vector3(0.0f,0.0f,0.0f);  // ��]���𒆉��ɂ���

            // �擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            rb.AddForce(Velocity * BouncePower,ForceMode.Impulse);

        }


    }
}
