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
    //private Vector3 ReSpawnPos;                   // ���X�|�[���ʒu��ۑ�

    [System.NonSerialized]
    public static bool isHitSavePoint;              // �Z�[�u�|�C���g�ɓ���������
    [System.NonSerialized]
    public static bool HitSavePointColorisRed;
    [System.NonSerialized]
    public static bool shouldRespawn;


    GameObject fadeimage;
    // Start is called before the first frame update
    void Start()
    {
        fadeimage = GameObject.Find("Fade");

        isHitSavePoint = false;                     // �t���O������
        HitSavePointColorisRed = false;             // �ԐF�̃Z�[�u�|�C���g�Ɠ���������
        shouldRespawn = false;                      // ���X�|�[�����鎞
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�

        //if (this.transform.position.y < -10000) // ���������Ƀ��X�|�[��
        //{
        //    this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = GameData.ReSpawnPos;
        //}

        if (isHitSavePoint) // �Z�[�u�|�C���g�ɓ������Ă��āA���̃t���[���̍ŏ��ɃX�e�B�b�N���X����ꂽ��
        {
            //Debug.Log("�Z�[�u�|�C���g�ɓ������Ă�");
            if (GamePadManager.onceTiltStick)
            {
                Pause.isPause = true;
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
            //GameData.ReSpawnPos = this.transform.position;              // �v���C���[�̈ʒu��ۑ�
            //SaveManager.saveLastPlayerPos(GameData.ReSpawnPos);         // �v���C���[�̈ʒu��ۑ�
            //GameData.ReSpawnPos = this.transform.position;              // �v���C���[�̈ʒu��ۑ�
            SaveManager.saveLastPlayerPos(GameData.PlayerPos);         // �v���C���[�̈ʒu��ۑ�

            SaveManager.saveBossAlive(GameData.isAliveBoss1);           // �{�X�P�̐����t���O��ۑ�
            SaveManager.saveHP(GameData.CurrentHP);                     // ���݂�HP��ۑ�
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // ������}�b�v�̔ԍ���ۑ�
            SaveManager.saveCurrentPiece(GameData.CurrentPiece);        // ���݂̂������ۑ�
            SaveManager.savePieceGrade(GameData.CurrentPieceGrade);     // ���݂̂�����̘g��ۑ�
            SaveManager.saveFireOnOff(GameData.FireOnOff);
            SaveManager.saveGateOnOff(GameData.GateOnOff);
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
                GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
            }
            Warp.shouldWarp = false;
        }

        //---�Q�[���I�[�o�[
        if (GameData.CurrentHP < 1)
        {
            //GameObject.Find("Canvas").GetComponent<GameOver>().GameOverShow();
            //hp.GetComponent<GameOver>().GameOverShow();
            shouldRespawn = false;

            //---�t�F�[�h
            //Pause.isPause = true;                 // �t�F�[�h�I���܂Ń|�[�Y
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.isFadeOut = true;              // �t�F�[�h������
            GameOver.GameOverReset();               // �肷�ۂ�
        }


        //---���ɖ߂莞
        if (shouldRespawn)
        {
            if (!GameData.isFadeOut)
            {
                GameData.InitScene();
                //Pause.isPause = false;
            }
        }
    }


    private void OnTriggerStay(Collider other) {
        //---�Z�[�u�|�C���g�n�_�̏���
        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {

            isHitSavePoint = true;                  // ���������t���O�𗧂Ă�
        }

    }

    void OnTriggerEnter(Collider other) {

        //---���X�|�[���n�_�̏���
        if(other.gameObject.tag == "Respawn")
        {
            GameData.ReSpawnPos = this.transform.position;
            Debug.Log(GameData.ReSpawnPos);
        }


        //----- �e�V�[���J�� -----

        if (other.gameObject.tag == "toKitchen1"){
            //Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;  // �t�F�[�h������
            //Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
        }

        if (other.gameObject.tag == "toKitchen2"){
            //Pause.isPause = true;                  // �t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;              // �t�F�[�h������
            Debug.Log("�L�b�`���Q�ɍs���t�F�[�h�J�n");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage002;
        }

        if (other.gameObject.tag == "toKitchen3"){
            if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)  // ������V�[�����{�X�V�[�����������{�X�������Ă���V�[���J�ڂ��Ȃ�
            {
                if (GameData.isAliveBoss1)
                {
                    return;
                }
            }
            //Pause.isPause = true;   // �@�t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;  // �t�F�[�h������
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage003;
        }
        if (other.gameObject.tag == "toKitchen4")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            //Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;  // �t�F�[�h������
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage004;
        }
        if (other.gameObject.tag == "toKitchen5")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            //Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;  // �t�F�[�h������  
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage005;
        }
        if (other.gameObject.tag == "toKitchen6")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            //Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;  // �t�F�[�h������
            Debug.Log("�t�F�[�h�͂��߂̃|�[�Y");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage006;
        }
        if (other.gameObject.tag == "toBoss1")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            //Pause.isPause = true;   // �t�F�[�h�I���܂Ń|�[�Y
            GameData.isFadeOut = true;  // �t�F�[�h������
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
