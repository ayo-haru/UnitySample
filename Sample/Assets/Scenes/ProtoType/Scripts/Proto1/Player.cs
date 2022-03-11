using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = GameData.Player.transform.position;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MovePoint1to2")
        {
            /*
             * ProtoScene1Manager�Ƃ����Q�[���I�u�W�F�N�g�����̃V�[��������T��.
             * ���̃I�u�W�F�N�g�ɓ����Ă���R���|�[�l���g���擾(���̏ꍇ�̓X�N���v�g).
             * ���̃X�N���v�g����MoveScene1to2���\�b�h���Ăяo��
             */

            GameObject.Find("ProtoScene1Manager").
                GetComponent<ProtoScene1Manager>().
                MoveScene1to2();
        }

    }

}
