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

    public static string CurrentMapName;    // 現在のマップの名前
    public static Vector3 ReSpawnPos;       // リスポーンポス
    public static Vector3 PlayerPos;        // プレイヤーの座標（現在はGameManagerで毎フレーム代入しているが本来はPlayerクラスが良い(はず)） 
    public static GameObject Player;
    public static VelocityTmp PlayerVelocyty = new VelocityTmp();
    public static int CurrentHP = 5;       // HPの保存(現在の)
    public static int CurrentPiece = 0;     //かけらの所持数
    public static int CurrentPieceGrade = 0;    //かけらの所持枠
    public static bool isFadeOut = false;   //フェードアウト処理の開始、完了を管理するフラグ
    public static bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

    public static bool FireOnOff = true;
    public static bool GateOnOff = true;    //　tureが閉じてる

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
        if (CurrentMapName == "Tester")
        {
            isAliveBoss1 = true;
        }
        //Pause.isPause = false;  // 万が一ポーズ中だった場合ポーズ解除
        Debug.Log("万が一のポーズ解除");

    }

    /// <summary>
    /// シーンの初期化
    /// </summary>
    public static void InitScene() {
        SceneManager.LoadScene(MapName[CurrentMapNumber]);
        //Pause.isPause = false;  // 万が一ポーズ中だった場合ポーズ解除
        Debug.Log("万が一のポーズ解除");
    }

    public static void LoadData() {
        //SaveManager.load();
        ReSpawnPos = SaveManager.sd.LastPlayerPos;
        //CurrentMapNumber = SaveManager.sd.LastMapNumber;
        NextMapNumber = SaveManager.sd.LastMapNumber;
        Debug.Log("ロードデータ" + ReSpawnPos);
        Debug.Log("ロードデータ" + CurrentMapNumber);
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
        Pause.isPause = false;  // 万が一ポーズ中だった場合ポーズ解除
        Debug.Log("万が一のポーズ解除");
    }

    public static void RespawnPlayer() {
        LoadData();
        CurrentHP = 5;
        CurrentPiece = 0;

        Debug.Log("リスポーンプレイヤー" + PlayerPos);
        Debug.Log("リスポーンプレイヤー" + ReSpawnPos);
        Debug.Log("リスポーンプレイヤー" + CurrentMapNumber);
        Debug.Log("リスポーンプレイヤー" + NextMapNumber);
        //SceneManager.LoadScene(MapName[CurrentMapNumber]);
        //NextMapNumber = CurrentMapNumber;

        //Pause.isPause = false;  // 万が一ポーズ中だった場合ポーズ解除
    }
}
