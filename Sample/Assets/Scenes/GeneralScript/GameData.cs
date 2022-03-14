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
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
   public enum SceneState {
        TITLE_SCENE = 0,
        MAP1_SCENE,
        MAP2_SCENE,
    }


    static int roomSize;                    // 1�����̃T�C�Y�͊ȒP�ɐG��ė~�����Ȃ�����public�ɂ��ĂȂ�
    public static int CurrentMapNumber;     // �}�b�v�̔ԍ������
    public static int NextMapNumber;        // �}�b�v�̔ԍ������
    static string[] MapName                 // �}�b�v�̖��O 
        = {"TitleScene","ProtoTypeScene1" , "ProtoTypeScene2" };
    public static string CurrentMapName;    // ���݂̃}�b�v�̖��O
    public static Vector3 PlayerPos;        // �v���C���[�̍��W�i���݂�GameManager�Ŗ��t���[��������Ă��邪�{����Player�N���X���ǂ�(�͂�)�j 
    public static GameObject Player;
    public static bool isFadeOut = false;   //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    public static bool isFadeIn = false;    //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O

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
