using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaunceEnemy : MonoBehaviour
{
    public float bounceSpeed = 5.0f;
    public float bounceVectorMultiple = 2f;

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

            //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            rb.AddForce(velocity * bounceSpeed, ForceMode.Impulse);

            //�v���C���[���t�����ɒ��˕Ԃ�
            collision.rigidbody.AddForce(-velocity * bounceSpeed, ForceMode.Impulse);

            //�e���������
            isAlive = false;
            Destroy(gameObject, 1.0f);
        }
        
    }
}
