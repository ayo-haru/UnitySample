using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Vector3 PlayerPos;
    public static GameObject Player;
    static int roomSize;               // 1�����̃T�C�Y�͊ȒP�ɐG��ė~�����Ȃ�����public�ɂ��ĂȂ�
    public static bool isFadeOut = false;  //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    public static bool isFadeIn = false;   //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O

    public static void SetPlayerPos(Vector3 playerpos) {
        PlayerPos = playerpos;
    }

    public static void SetroomSize(int roomsize) {
        roomSize = roomsize;
    }
    public static int GetroomSize() {
        return roomSize;
    }

}
