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
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
   public enum eSceneState {
        TITLE_SCENE = 0,
        Kitchen1_SCENE,
        Kitchen2_SCENE,
        Kitchen3_SCENE,
        Kitchen4_SCENE,
        Kitchen5_SCENE,
        Kitchen6_SCENE,
        BOSS1_SCENE
    }


    static int roomSize;                    // 1�����̃T�C�Y�͊ȒP�ɐG��ė~�����Ȃ�����public�ɂ��ĂȂ�
    public static int OldMapNumber;         // �V�[���ړ��O�̃}�b�v�ԍ�
    public static int CurrentMapNumber;     // �}�b�v�̔ԍ������
    public static int NextMapNumber;        // �}�b�v�̔ԍ������
    public static int MovePoint;            // ���̑J�ڏꏊ
    static string[] MapName                 // �}�b�v�̖��O 
        = { "TitleScene", "Kitchen001", "Kitchen002", "Kitchen003", "Kitchen004", "Kitchen005", "Kitchen006", "Tester" };

    static string[] MovePointName
        = { "KitchenMovePoint001" };


    public static string CurrentMapName;    // ���݂̃}�b�v�̖��O
    public static Vector3 PlayerPos;        // �v���C���[�̍��W�i���݂�GameManager�Ŗ��t���[��������Ă��邪�{����Player�N���X���ǂ�(�͂�)�j 
    public static GameObject Player;
    public static int CurrentHP = 6 ;       // HP�̕ۑ�(���݂�)
    public static bool isFadeOut = false;   //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    public static bool isFadeIn  = false;   //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O

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
}
