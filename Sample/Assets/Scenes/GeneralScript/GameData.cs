//=============================================================================
//
// ゲームのデータを管理するクラス
//
// 作成日:2022/03/10
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/10 作成
// 2022/03/11 マップの番号を入れとく変数作った
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


    static int roomSize;                    // 1部屋のサイズは簡単に触れて欲しくないからpublicにしてない
    public static int CurrentMapNumber;     // マップの番号いれる
    public static int NextMapNumber;        // マップの番号いれる
    static string[] MapName                 // マップの名前 
        = {"TitleScene","ProtoTypeScene1" , "ProtoTypeScene2" };
    public static string CurrentMapName;    // 現在のマップの名前
    public static Vector3 PlayerPos;        // プレイヤーの座標（現在はGameManagerで毎フレーム代入しているが本来はPlayerクラスが良い(はず)） 
    public static GameObject Player;
    public static bool isFadeOut = false;   //フェードアウト処理の開始、完了を管理するフラグ
    public static bool isFadeIn = false;    //フェードイン処理の開始、完了を管理するフラグ

    public static bool isAliveBoss1 = true;    //ボス１の討伐情報保存用


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
