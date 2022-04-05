//=============================================================================
//
// �v���C���[�̊Ǘ�����
//
// �쐬��:2022/03/11
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/11 �쐬
// 2022/03/30 HP�ۑ�
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector3 ReSpawnPos;

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
            this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = ReSpawnPos;
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause.isPause = !Pause.isPause;
        }

        if (Pause.isPause)
        {
            Pause.PauseStart();
        }
        else
        {
            Pause.PauseFin();
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    EffectManager.Play(EffectData.eEFFECT.EF_SHEILD2, GameData.PlayerPos);
        //}
    }

    void OnTriggerEnter(Collider other) {
        //----- �Z�[�u -----
        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            ReSpawnPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�
            SaveManager.saveLastPlayerPos(ReSpawnPos);
        }


        //----- �V�[���J�� -----
        if (other.gameObject.tag == "toKitchen1")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
        }
        if (other.gameObject.tag == "toKitchen2")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen2_SCENE;
        }
        if (other.gameObject.tag == "toKitchen3")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen3_SCENE;
        }
        if (other.gameObject.tag == "toKitchen4")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen4_SCENE;
        }
        if (other.gameObject.tag == "toKitchen5")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen5_SCENE;
        }
        if (other.gameObject.tag == "toKitchen6")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen6_SCENE;
        }
        if (other.gameObject.tag == "toBoss1")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }



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
