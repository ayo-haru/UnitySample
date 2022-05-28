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

    private ObservedValue<int> checkHP; // HP�̒l���Ď�����

    private GameObject fadeimage;   // �t�F�[�h�̃p�l��

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        fadeimage = GameObject.Find("Fade");

        isHitSavePoint = false;                     // �t���O������
        HitSavePointColorisRed = false;             // �ԐF�̃Z�[�u�|�C���g�Ɠ���������
        shouldRespawn = false;                      // ���X�|�[�����鎞

        checkHP = new ObservedValue<int>(GameData.CurrentHP);
        checkHP.OnValueChange += () => { if (checkHP.Value < 1) PlayerDeath(); };
        _animator = GetComponent<Player2>().animator;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // �v���C���[�̈ʒu��ۑ�
        checkHP.Value = GameData.CurrentHP;

        //if (this.transform.position.y < -10000) // ���������Ƀ��X�|�[��
        //{
        //    this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = GameData.ReSpawnPos;
        //}

        //if (isHitSavePoint) // �Z�[�u�|�C���g�ɓ������Ă��āA���̃t���[���̍ŏ��ɃX�e�B�b�N���X����ꂽ��
        //{
        //    if (GamePadManager.onceTiltStick)
        //    {
        //        if (HitSavePointColorisRed)
        //        {
        //            Warp.canWarp = true;
        //        }
        //        else
        //        {
        //            SaveManager.canSave = true;
        //        }
        //    }
        //}

        if (SaveManager.shouldSave) // �Z�[�u���邪�I�����ꂽ��
        {
            GameData.SaveAll();
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
            shouldRespawn = false;

            //---�t�F�[�h
            if (!GameObject.Find(EffectData.EF[(int)EffectData.eEFFECT.EF_PLAYER_DEATH].name+"(Clone)"))
            {
                GameData.isFadeOut = true;              // �t�F�[�h������
                GameOver.GameOverReset();               // �肷�ۂ�
            }
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
        //GamePadManager.onceTiltStick = false;
    }

    private void PlayerDeath() {
        GameOver.GameOverFlag = true;
        Pause.isPause = true;

        //Vector3 effectPos;
        //effectPos = new Vector3(GameData.PlayerPos.x, GameData.PlayerPos.y + 10.0f, GameData.PlayerPos.z);
        _animator.Play("Death");
        EffectManager.Play(EffectData.eEFFECT.EF_PLAYER_DEATH, GameData.PlayerPos, 7.0f);
        StartCoroutine(this.GetComponent<Player2>().VibrationPlay(1.0f, 1.0f, 5.0f));
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
        }


        //----- �e�V�[���J�� -----
        if (other.gameObject.tag == "toTutorial2")
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial2;
        }
        if (other.gameObject.tag == "toTutorial3")
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial3;
        }

        if (other.gameObject.tag == "toKitchen1"){
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
        }

        if (other.gameObject.tag == "toKitchen2"){
            GameData.isFadeOut = true;              // �t�F�[�h������
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
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage003;
        }
        if (other.gameObject.tag == "toKitchen4")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage004;
        }
        if (other.gameObject.tag == "toKitchen5")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������  
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage005;
        }
        if (other.gameObject.tag == "toKitchen6")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage006;
        }
        if (other.gameObject.tag == "toBoss1")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }

        if (other.gameObject.tag == "toExStage1")
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.BossStage001;
        }
        if (other.gameObject.tag == "toExStage2")
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.BossStage002;
        }
        if (other.gameObject.tag == "toExStage3")
        {
            GameData.isFadeOut = true;  // �t�F�[�h������
            GameData.NextMapNumber = (int)GameData.eSceneState.BossStage003;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // ���̖��O�̃^�O�ƏՓ˂�����
        {
            isHitSavePoint = false; // ���������t���O������
        }
    }
}
