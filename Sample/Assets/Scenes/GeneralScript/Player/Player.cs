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
// 2022/04/18 �A�j���[�V�����ۑ����ĂȂ���
// 2022/04/19 �Z�[�u�|�C���g�ɂ��Z�[�u����
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    //---�ϐ��錾
    //private Vector3 ReSpawnPos;     // ���X�|�[���ʒu��ۑ�

    [System.NonSerialized]
    public static bool isHitSavePoint; // �Z�[�u�|�C���g�ɓ���������
    [System.NonSerialized]
    public static bool HitSavePointColorisRed;
    [System.NonSerialized]
    public static bool shouldRespawn;

    // Start is called before the first frame update
    void Start()
    {
        isHitSavePoint = false; // �t���O������
        HitSavePointColorisRed = false;
        shouldRespawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�

        //if (this.transform.position.y < -10000) // ���������Ƀ��X�|�[��
        //{
        //    this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = GameData.ReSpawnPos;
        //}

        if (Pause.isPause)  // �|�[�Y�t���O�ɂ���ă|�[�Y���邩��߂邩
        {
            Pause.PauseStart();
        }
        else
        {
            Pause.PauseFin();
        }

        if (isHitSavePoint) // �Z�[�u�|�C���g�ɓ������Ă��āA���̃t���[���̍ŏ��ɃX�e�B�b�N���X����ꂽ��
        {
            //Debug.Log("�Z�[�u�|�C���g�ɓ������Ă�");
            if (GamePadManager.onceTiltStick)
            {
                if (HitSavePointColorisRed)
                {
                    Warp.canWarp = true;
                }
                else
                {
                    SaveManager.canSave = true;
                }
                //Debug.Log("�Z�[�u���̂�");
            }
        }

        if (SaveManager.shouldSave) // �Z�[�u���邪�I�����ꂽ��
        {
            Debug.Log("�Z�[�u����");
            GameData.ReSpawnPos = this.transform.position;              // �v���C���[�̈ʒu��ۑ�
            SaveManager.saveLastPlayerPos(GameData.ReSpawnPos);         // �v���C���[�̈ʒu��ۑ�
            SaveManager.saveBossAlive(GameData.isAliveBoss1);           // �{�X�P�̐����t���O��ۑ�
            SaveManager.saveHP(GameData.CurrentHP);                     // ���݂�HP��ۑ�
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // ������}�b�v�̔ԍ���ۑ�
            SaveManager.saveCurrentPiece(GameData.CurrentPiece);        // ���݂̂������ۑ�
            SaveManager.savePieceGrade(GameData.CurrentPieceGrade);     // ���݂̂�����̘g��ۑ�
            SaveManager.canSave = false;                                // �Z�[�u���I������̂Ńt���O������
            SaveManager.shouldSave = false;
        }

        if (Warp.shouldWarp)
        {
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE)
            {
                GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
            }
            else
            {
                throw new Exception("�V�[�����ǂ��ɖ߂邩�킩��Ȃ���������{�X�V�[������߂�Ȃ����Ă���");
                // ���̃l�N�X�g�ɓ����̃V�[���ԍ���V�����̂ɂ��Ă�
                GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
            }
            Warp.shouldWarp = false;
        }

        if (shouldRespawn)
        {
            if (!GameData.isFadeOut)
            {
                GameData.InitScene();
                Pause.isPause = false;
            }
        }
    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Respawn")
        {
            GameData.ReSpawnPos = this.transform.position;
            Debug.Log(GameData.ReSpawnPos);
        }

        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            
            isHitSavePoint = true;  // ���������t���O�𗧂Ă�
        }

        //----- �V�[���J�� -----
        if (other.gameObject.tag == "toKitchen1")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
        }
        if (other.gameObject.tag == "toKitchen2")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage002;
        }
        if (other.gameObject.tag == "toKitchen3")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)  // ������V�[�����{�X�V�[�����������{�X�������Ă���V�[���J�ڂ��Ȃ�
            {
                if (GameData.isAliveBoss1)
                {
                    return;
                }
            }
            GameData.isFadeOut = true;  // �t�F�[�h������
            Pause.isPause = true;   // �@�t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage003;
        }
        if (other.gameObject.tag == "toKitchen4")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage004;
        }
        if (other.gameObject.tag == "toKitchen5")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������  
            Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage005;
        }
        if (other.gameObject.tag == "toKitchen6")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage006;
        }
        if (other.gameObject.tag == "toBoss1")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            isHitSavePoint = false; // ���������t���O������
            //Pause.isPause = false;  // �o�O�h�~
            //Debug.Log("�o�O�Ƃ�|�[�Y����");
            //Debug.Log("�Z�[�u�\����Ȃ�");
        }
    }
}
