//=============================================================================
//
// �Q�[���̃f�[�^���Ǘ�����N���X
//
// �쐬��:2022/03/10
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/10 �쐬
// 2022/03/11 �}�b�v�̔ԍ������Ƃ��ϐ������
// 2022/03/30 ���E�̑J�ڂ��ł���悤�ɂ���
// 2022/04/17 ������̏������ǉ�
// 2022/05/10 �Â��������邽�߂ɐF�X�ς���
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public static class GameData {
    public enum eSceneState {   // �V�[���X�e�[�g
        TITLE_SCENE = 0,
        KitchenStage001,
        KitchenStage002,
        KitchenStage003,
        KitchenStage004,
        KitchenStage005,
        KitchenStage006,
        ExStage001,
        ExStage002,
        ExStage003,
        Tutorial1,
        Tutorial2,
        Tutorial3,

        BOSS1_SCENE
    }

    public static Gamepad gamepad;                                  // �ڑ�����Ă���R���g���[���[��ۑ�

    public static int OldMapNumber;                                 // �V�[���ړ��O�̃}�b�v�ԍ�
    public static int CurrentMapNumber;                             // �}�b�v�̔ԍ������
    public static int NextMapNumber;                                // �}�b�v�̔ԍ������
    static string[] MapName = {                                     // �}�b�v�̖��O
        "TitleScene",
        "KitchenStage001", "KitchenStage002", "KitchenStage003", "KitchenStage004", "KitchenStage005", "KitchenStage006",
        "ExStage001", "ExStage002", "ExStage003",
        "Tutorial01","Tutorial02","Tutorial03",
        "Tester"
    };

    public static Vector3 ReSpawnPos;                               // ���X�|�[���|�X
    public static Vector3 PlayerPos;                                // �v���C���[�̍��W
    public static GameObject Player;                                // �v���C���[���̂�ۑ�
    public static VelocityTmp PlayerVelocyty = new VelocityTmp();   // �v���C���[�̃��W�b�h�{�f�B��ۑ�
    public static int CurrentHP = 5;                                // HP�̕ۑ�(���݂�)
    public static int CurrentPiece = 0;                             //������̏�����
    public static int CurrentPieceGrade = 0;                        //������̏����g
    public static bool isFadeOut = false;                           //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    public static bool isFadeIn = false;                            //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O

    public static bool FireOnOff = true;                            // true�����Ă�
    public static bool GateOnOff = true;                            //�@ture�����Ă�

    public static bool isAliveBoss1 = true;                         //�{�X�P�̓������ۑ��p

    public static bool[,] isStarGet = new bool[10, 10];             //�X�^�[�̎擾�󋵁@true���擾�ς�


    public static string GetNextScene(int nextscene) {
        return MapName[nextscene];
    }

    /// <summary>
    /// �f�[�^�̏�����
    /// </summary>
    public static void InitData() {
        PlayerPos = new Vector3(0.0f, 0.0f, 0.0f);
        ReSpawnPos = new Vector3(0.0f, 0.0f, 0.0f);
        CurrentHP = 5;
        isAliveBoss1 = true;
        FireOnOff = true;
        GateOnOff = true;
        CurrentPiece = 0;
        CurrentPieceGrade = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                isStarGet[i, j] = false;
            }
        }
    }

    /// <summary>
    /// �V�[���̏�����
    /// </summary>
    public static void InitScene() {
        SceneManager.LoadScene(MapName[CurrentMapNumber]);  // ���݂̃V�[�����ă��[�h
    }

    public static void LoadData() {
        ReSpawnPos = SaveManager.sd.LastPlayerPos;
        NextMapNumber = SaveManager.sd.LastMapNumber;
        CurrentHP = SaveManager.sd.HP;
        isAliveBoss1 = SaveManager.sd.isBoss1Alive;
        FireOnOff = SaveManager.sd.fireOnOff;
        GateOnOff = SaveManager.sd.gateOnOff;
        CurrentPiece = SaveManager.sd.CurrentPiece;
        if (!GameOver.GameOverFlag)
        {
            CurrentPieceGrade = SaveManager.sd.PieceGrade;
        }
        SoundManager.bgmVolume = SaveManager.sd.bgmVolume;
        SoundManager.seVolume = SaveManager.sd.seVolume;
    }

    public static void SaveAll() {
        SaveManager.saveLastMapNumber(CurrentMapNumber);
        SaveManager.saveLastPlayerPos(PlayerPos);
        SaveManager.saveHP(CurrentHP);
        SaveManager.saveBossAlive(isAliveBoss1);
        SaveManager.saveFireOnOff(FireOnOff);
        SaveManager.saveGateOnOff(GateOnOff);
        SaveManager.saveCurrentPiece(CurrentPiece);
        SaveManager.savePieceGrade(CurrentPieceGrade);
        SaveManager.saveBGMVolume(SoundManager.bgmVolume);
        SaveManager.saveSEVolume(SoundManager.seVolume);
    }

    //public static void Init() {
    //    InitScene();
    //    InitData();
    //}

    public static void RespawnPlayer() {
        LoadData();
        CurrentHP = 5;
        CurrentPiece = 0;
    }
}
