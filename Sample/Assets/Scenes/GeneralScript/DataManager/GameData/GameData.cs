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
// 2022/03/30 左右の遷移をできるようにする
// 2022/04/17 かけらの所持数追加
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public static class GameData
{
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

    static int roomSize;                    // 1部屋のサイズは簡単に触れて欲しくないからpublicにしてない
    public static int OldMapNumber;         // シーン移動前のマップ番号
    public static int CurrentMapNumber;     // マップの番号いれる
    public static int NextMapNumber;        // マップの番号いれる
    public static int MovePoint;            // 次の遷移場所
    static string[] MapName                 // マップの名前 
        = { "TitleScene",
        "KitchenStage001", "KitchenStage002", "KitchenStage003", "KitchenStage004", "KitchenStage005", "KitchenStage006",
        //"Kitchen", "KitchenScene", "Kitchen001", "Kitchen002", "Kitchen003", "Kitchen004", 
        /*"Kitchen005", "Kitchen006","KitchenStage", "KitchenStage 1",*/"Tester" };

    static string[] MovePointName
        = { "KitchenMovePoint001" };


    public static string CurrentMapName;    // 現在のマップの名前
    public static Vector3 PlayerPos;        // プレイヤーの座標（現在はGameManagerで毎フレーム代入しているが本来はPlayerクラスが良い(はず)） 
    public static GameObject Player;
    public static int CurrentHP = 5 ;       // HPの保存(現在の)
    public static int CurrentPiece = 0;     //かけらの所持数
    public static int CurrentPieceGrade = 0;    //かけらの所持枠
    public static bool isFadeOut = false;   //フェードアウト処理の開始、完了を管理するフラグ
    public static bool isFadeIn  = false;   //フェードイン処理の開始、完了を管理するフラグ

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

    /// <summary>
    /// データの初期化
    /// </summary>
    public static void InitData() {
        CurrentHP = 5;
        //SaveManager.saveHP(CurrentHP);
        if(CurrentMapName == "Tester")
        {
            isAliveBoss1 = true;
        }
        Pause.isPause = false;
    }

    /// <summary>
    /// シーンの初期化
    /// </summary>
    public static void InitScene() {
        SceneManager.LoadScene(MapName[CurrentMapNumber]);
        Pause.isPause = false;
    }

    public static void Init() {
        InitData();
        InitScene();
        Pause.isPause = false;
    }
}
