//=============================================================================
//
// ���˕Ԃ��M�~�b�N
//
// �쐬��:2022/03
// �쐬��:����T�q
//
// <�J������>
// 2022/03    �쐬
// 2022/03/17 ���̋������˕Ԃ����悤�ɕύX
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaunceGimmick : MonoBehaviour
{
    //���˕Ԃ����x
    public float bounceSpeed = 5.0f;
    //public float bounceVectorMultiple = 2f;
    //�v���C���[
    GameObject Player;
    //�v���C���[�̃��W�b�g�{�f�B
    Rigidbody player_rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        player_rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            ////�Փ˂����ʂ́A�ڐG�����_�ɂ�����@���x�N�g�����擾
            //Vector3 normal = collision.contacts[0].normal;
            ////�Փ˂������x�x�N�g����P�ʃx�N�g���ɂ���
            //Vector3 velocity = collision.rigidbody.velocity.normalized;
            ////x,y,z�����ɑ΂��ċt�����̖@���x�N�g�����擾
            //velocity += new Vector3(-normal.x * bounceVectorMultiple, -normal.y * bounceVectorMultiple, -normal.z * bounceVectorMultiple);
            ////�擾�����@���x�N�g���ɒ��˕Ԃ��������|���āA���˕Ԃ�
            //collision.rigidbody.AddForce(velocity * bounceSpeed, ForceMode.Impulse);

            //�����擾
            Vector3 dir =  Player.transform.position - gameObject.transform.position;
            //z���ړ����Ȃ��悤�ɂO�ɂ��Ă���
            dir.z = 0;
            dir.Normalize();
            //�v���C���[�̃��W�b�g�{�f�B����
            player_rb.velocity = Vector3.zero;
            //���˕Ԃ�
            player_rb.AddForce(dir * bounceSpeed, ForceMode.Impulse);
            //������
            Destroy(collision.gameObject);

        }
    }
}
