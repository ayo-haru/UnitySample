//=============================================================================
//
// �U��
//
// �쐬��:2022/03
// �쐬��:����T�q
// �ҏW��:�ɒn�c�^��
//
// <�J������>
// 2022/03    �쐬
// 2022/03/12 ���f���̌����Ɋ֌W�Ȃ����͕����ɏ����o��悤�ɕύX
// 2022/03/13 GameData���g�p����player�̈ʒu���擾����悤�ɕύX
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    //�ړ����x
    public float speed = 0.01f;

    //Rigidbody rb;

    //GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //�@�\�̎擾
        //rb = gameObject.GetComponent<Rigidbody>();

        //�v���C���[�擾
        //Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!(this.GetComponent<BaunceEnemy>().isAlive))
        {
            return;
        }
        //Vector3 pos = rb.position;
        //�E
        if(transform.position.x < GameData.Player.transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            //rb.position = pos;
        }

        //��
        if (transform.position.x > GameData.Player.transform.position.x)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            //rb.position = pos;
        }

    }
}
