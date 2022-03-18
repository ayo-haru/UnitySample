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
    public float speed = 0.05f;
    // �ړ����t���O
    private bool moveFlg = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!moveFlg)
        {
            if ((this.GetComponent<BaunceEnemy>().isBounce))
            {
                return;
            }
            return;
        }

        //�E
        if (transform.position.x < GameData.PlayerPos.x)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }

        //��
        if (transform.position.x > GameData.PlayerPos.x)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }

    }
    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag == "Player")
        {
            moveFlg = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player")
        {
            moveFlg = false;
        }
    }
    //if ((this.GetComponent<BaunceEnemy>().isBounce))    // ���˕Ԃ�Ƃ��͒ǔ����Ȃ�
    //{
    //    //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    //this.GetComponent<Rigidbody>().AddForce(-Direction);
    //    return;
    //}

    //// �v���C���[�Ǝ����̈ʒu����i�s���������߂�
    //this.Direction = GameData.PlayerPos - this.transform.position;
    //this.Direction.Normalize();
    //this.GetComponent<Rigidbody>().AddForce(Direction * speed);
}
