//=============================================================================
//
// �v���C���[�̊Ǘ�����
//
// �쐬��:2022/03/11
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/11 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�

        if(this.transform.position.y < -5)
        {
            GameData.PlayerPos = GameData.Player.transform.position = this.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        }
    }

    void OnTriggerEnter(Collider other) {




        //*************************************************************************************************
        // �ȉ��v���g�^�C�v�J��
        //*************************************************************************************************
        //if (other.gameObject.tag == "MovePoint1to2")    // ���̖��O�̃^�O�ƏՓ˂�����
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.MAP2_SCENE;   // ���̃V�[���ԍ���ݒ�A�ۑ�
        //}

        //if (other.gameObject.tag == "MovePoint2to1")
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.MAP1_SCENE;    // ���̃V�[���ԍ���ݒ�A�ۑ�
        //}

        //if (other.gameObject.tag == "MovePoint2toBoss")
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;    // ���̃V�[���ԍ���ݒ�A�ۑ�
        //}
    }
}
