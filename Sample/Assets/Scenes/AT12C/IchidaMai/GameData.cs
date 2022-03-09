using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Vector3 PlayerPos;
    public static GameObject Player;
    static int roomSize;               // 1部屋のサイズは簡単に触れて欲しくないからpublicにしてない
    public static bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public static bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

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
