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
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public static class GameData {
    public enum eSceneState {
        TITLE_SCENE = 0,
        KitchenStage001,
        KitchenStage002,
        KitchenStage003,
        KitchenStage004,
        KitchenStage005,
        KitchenStage006,
        //KitchenStage_SCENE,
        //Kitchen_SCENE,
        //Kitchen1_SCENE,
        //Kitchen2_SCENE,
        //Kitchen3_SCENE,
        //Kitchen4_SCENE,
        //Kitchen5_SCENE,
        //Kitchen6_SCENE,
        //KitchenXXX_SCENE,
        //KitchenYYY_SCENE,

        BOSS1_SCENE
    }

    public static Gamepad gamepad;

    static int roomSize;                    // 1�����̃T�C�Y�͊ȒP�ɐG��ė~�����Ȃ�����public�ɂ��ĂȂ�
    public static int OldMapNumber;         // �V�[���ړ��O�̃}�b�v�ԍ�
    public static int CurrentMapNumber;     // �}�b�v�̔ԍ������
    public static int NextMapNumber;        // �}�b�v�̔ԍ������
    public static int MovePoint;            // ���̑J�ڏꏊ
    static string[] MapName                 // �}�b�v�̖��O 
        = { "TitleScene",
        "KitchenStage001", "KitchenStage002", "KitchenStage003", "KitchenStage004", "KitchenStage005", "KitchenStage006",
        //"Kitchen", "KitchenScene", "Kitchen001", "Kitchen002", "Kitchen003", "Kitchen004", 
        /*"Kitchen005", "Kitchen006","KitchenStage", "KitchenStage 1",*/"Tester" };

    public static string CurrentMapName;    // ���݂̃}�b�v�̖��O
    public static Vector3 ReSpawnPos;       // ���X�|�[���|�X
    public static Vector3 PlayerPos;        // �v���C���[�̍��W�i���݂�GameManager�Ŗ��t���[��������Ă��邪�{����Player�N���X���ǂ�(�͂�)�j 
    public static GameObject Player;
    public static VelocityTmp PlayerVelocyty = new VelocityTmp();
    public static int CurrentHP = 5;       // HP�̕ۑ�(���݂�)
    public static int CurrentPiece = 0;     //������̏�����
    public static int CurrentPieceGrade = 0;    //������̏����g
    public static bool isFadeOut = false;   //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    public static bool isFadeIn = false;   //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O

    public static bool FireOnOff = true;
    public static bool GateOnOff = true;    //�@ture�����Ă�

    public static bool isAliveBoss1 = true;    //�{�X�P�̓������ۑ��p


    public static void SetPlayerPos(Vector3 playerpos) {
        PlayerPos = playerpos;
    }

    public static void SetroomSize(int roomsize) {
        roomSize = roomsize;
    }
    public static int GetroomSize() {
        return roomSize;
    }

    public static string GetNextScene(int nextscene) {
        return MapName[nextscene];
    }

    /// <summary>
    /// �f�[�^�̏�����
    /// </summary>
    public static void InitData() {
        CurrentHP = 5;
        //SaveManager.saveHP(CurrentHP);
        if (CurrentMapName == "Tester")
        {
            isAliveBoss1 = true;
        }
        //Pause.isPause = false;  // ������|�[�Y���������ꍇ�|�[�Y����
        Debug.Log("������̃|�[�Y����");

    }

    /// <summary>
    /// �V�[���̏�����
    /// </summary>
    public static void InitScene() {
        SceneManager.LoadScene(MapName[CurrentMapNumber]);
        //Pause.isPause = false;  // ������|�[�Y���������ꍇ�|�[�Y����
        Debug.Log("������̃|�[�Y����");
    }

    public static void LoadData() {
        //SaveManager.load();
        ReSpawnPos = SaveManager.sd.LastPlayerPos;
        //CurrentMapNumber = SaveManager.sd.LastMapNumber;
        NextMapNumber = SaveManager.sd.LastMapNumber;
        Debug.Log("���[�h�f�[�^" + ReSpawnPos);
        Debug.Log("���[�h�f�[�^" + CurrentMapNumber);
        CurrentHP = SaveManager.sd.HP;
        //isAliveBoss1 = SaveManager.sd.isBoss1Alive;
        //FireOnOff = SaveManager.sd.fireOnOff;
        //GateOnOff = SaveManager.sd.gateOnOff;
        CurrentPiece = SaveManager.sd.CurrentPiece;
        CurrentPieceGrade = SaveManager.sd.PieceGrade;
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

    public static void Init() {
        InitScene();
        InitData();
        Pause.isPause = false;  // ������|�[�Y���������ꍇ�|�[�Y����
        Debug.Log("������̃|�[�Y����");
    }

    public static void RespawnPlayer() {
        LoadData();
        CurrentHP = 5;
        CurrentPiece = 0;

        Debug.Log("���X�|�[���v���C���[" + PlayerPos);
        Debug.Log("���X�|�[���v���C���[" + ReSpawnPos);
        Debug.Log("���X�|�[���v���C���[" + CurrentMapNumber);
        Debug.Log("���X�|�[���v���C���[" + NextMapNumber);
        //SceneManager.LoadScene(MapName[CurrentMapNumber]);
        //NextMapNumber = CurrentMapNumber;

        //Pause.isPause = false;  // ������|�[�Y���������ꍇ�|�[�Y����
    }
}
