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
        GameData.PlayerPos = transform.position;    // �v���C���[�̈ʒu��ۑ�
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MovePoint1to2")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.SceneState.MAP2_SCENE;   // ���̃V�[���ԍ���ݒ�A�ۑ�
        }

        if (other.gameObject.tag == "MovePoint2to1")
        {
            GameData.NextMapNumber = (int)GameData.SceneState.MAP1_SCENE;    // ���̃V�[���ԍ���ݒ�A�ۑ�
        }
    }

}
