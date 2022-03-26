//==========================================================
//      �G���G�̒e���ꂽ�Ƃ�
//      �쐬���@2022/03/20
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/20
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    public float bounceSpeed = 5.0f;
    public float bounceVectorMultiple = 2f;
    private float bouncePower = 1000.0f;

    public bool isAlive;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            //�Փ˂����ʂ́A�ڐG�����_�ɂ�����@���x�N�g�����擾
            Vector3 normal = collision.contacts[0].normal;
            //�Փ˂������x�x�N�g����P�ʃx�N�g���ɂ���
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            //x,y,z�����ɑ΂��Ė@���x�N�g�����擾
            velocity += new Vector3(normal.x * bounceVectorMultiple, normal.y * bounceVectorMultiple, normal.z * bounceVectorMultiple);
            //�v���C���[���t�����ɒ��˕Ԃ�
            collision.rigidbody.AddForce(-velocity * bounceSpeed, ForceMode.Impulse);
            //�e���������
            isAlive = false;
            // �d�͂�����
            rb.useGravity = false;
            // ��C��R���[����
            rb.angularDrag = 0.0f;
            // ��]���𒆉���
            rb.centerOfMass = new Vector3(0, 0, 0);
            // ��]������
            rb.AddTorque(0.0f, 0.0f, -100.0f);
            //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            rb.AddForce(velocity * bouncePower, ForceMode.Force);
        }

        if (isAlive == false && collision.gameObject.CompareTag("Ground"))
        {
            // �ǂɓ��������������
            Destroy(gameObject, 0.0f);
        }

    }


}