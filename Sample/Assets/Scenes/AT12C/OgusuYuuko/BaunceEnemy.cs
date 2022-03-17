using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaunceEnemy : MonoBehaviour
{
    public float bounceSpeed = 0.07f;


    private Vector3 targetPos;

    [System.NonSerialized]
    public bool isBounce = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBounce)  // ���˕Ԃ��Ă��Ȃ��Ƃ��͒��˕Ԃ�̗͂������Ȃ�
        {
            return;
        }

        // ���W�ύX
        if(this.transform.position.x < this.targetPos.x)
        {
            transform.position = new Vector3(transform.position.x + bounceSpeed, transform.position.y, transform.position.z);
        }
        else if (this.transform.position.x > this.targetPos.x)
        {
            transform.position = new Vector3(transform.position.x - bounceSpeed, transform.position.y, transform.position.z);
        }

        // �I�u�W�F�N�g�̏���
        Destroy(gameObject, 1.0f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            //�Փ˂����ʂ́A�ڐG�����_�ɂ�����@���x�N�g�����擾
            Vector3 normal = collision.contacts[0].normal;

            // ����ɓ��������u�Ԃ̕���̍��W���Ƃ�
            Vector3 weaponPos = GameObject.Find("Weapon(Clone)").transform.position;
            targetPos = new Vector3(weaponPos.x + 5.0f * normal.x, weaponPos.y + 5.0f * normal.y, weaponPos.z + 5.0f * normal.z);

            // ���˕Ԃ���
            isBounce = true;

            // ���˕Ԃ��ꂽ�������d�͂̌v�Z������
            this.GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}
