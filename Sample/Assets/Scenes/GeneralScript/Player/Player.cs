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

public class Player : MonoBehaviour
{
    //---�ϐ��錾
    private Vector3 ReSpawnPos;     // ���X�|�[���ʒu��ۑ�

    [System.NonSerialized]
    public static bool isHitSavePoint; // �Z�[�u�|�C���g�ɓ���������

    // Start is called before the first frame update
    void Start()
    {
        isHitSavePoint = false; // �t���O������
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�

        if (this.transform.position.y < -10000) // ���������Ƀ��X�|�[��
        {
            this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = ReSpawnPos;
        }


        if (Input.GetKeyDown(KeyCode.P))    // �|�[�Y����
        {
            Pause.isPause = !Pause.isPause; // �g�O��
        }

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
            if (GamePadManager.onceTiltStick)
            {
                SaveManager.canSave = true;

            }
        }

        if (SaveManager.shouldSave) // �Z�[�u���邪�I�����ꂽ��
        {
            //Debug.Log("�Z�[�u����");
            ReSpawnPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�
            SaveManager.saveLastPlayerPos(ReSpawnPos);  // �v���C���[�̈ʒu��ۑ�
            SaveManager.saveBossAlive(GameData.isAliveBoss1);   // �{�X�P�̐����t���O��ۑ�
            SaveManager.saveHP(GameData.CurrentHP); // ���݂�HP��ۑ�
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // ������}�b�v�̔ԍ���ۑ�
            SaveManager.canSave = false;        // �Z�[�u���I������̂Ńt���O������
            SaveManager.shouldSave = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            isHitSavePoint = true;  // ���������t���O�𗧂Ă�
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
            if(GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)  // ������V�[�����{�X�V�[�����������{�X�������Ă���V�[���J�ڂ��Ȃ�
            {
                if (GameData.isAliveBoss1)
                {
                    return;
                }
            }
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
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            isHitSavePoint = false; // ���������t���O������
            SaveManager.canSave = false;    // �Z�[�u�\�ł͂Ȃ����߃t���O������
        }
    }
}
