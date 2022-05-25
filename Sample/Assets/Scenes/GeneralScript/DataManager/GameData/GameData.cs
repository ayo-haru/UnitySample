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
// 2022/05/10 つづきからをやるために色々変えた
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public static class GameData {
    public enum eSceneState {   // シーンステート
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

    public static Gamepad gamepad;                                  // 接続されているコントローラーを保存

    public static int OldMapNumber;                                 // シーン移動前のマップ番号
    public static int CurrentMapNumber;                             // マップの番号いれる
    public static int NextMapNumber;                                // マップの番号いれる
    static string[] MapName = {                                     // マップの名前
        "TitleScene",
        "KitchenStage001", "KitchenStage002", "KitchenStage003", "KitchenStage004", "KitchenStage005", "KitchenStage006",
        "ExStage001", "ExStage002", "ExStage003",
        "Tutorial01","Tutorial02","Tutorial03",
        "Tester"
    };

    public static Vector3 ReSpawnPos;                               // リスポーンポス
    public static Vector3 PlayerPos;                                // プレイヤーの座標
    public static GameObject Player;                                // プレイヤー自体を保存
    public static VelocityTmp PlayerVelocyty = new VelocityTmp();   // プレイヤーのリジッドボディを保存
    public static int CurrentHP = 5;                                // HPの保存(現在の)
    public static int CurrentPiece = 0;                             //かけらの所持数
    public static int CurrentPieceGrade = 0;                        //かけらの所持枠
    public static bool isFadeOut = false;                           //フェードアウト処理の開始、完了を管理するフラグ
    public static bool isFadeIn = false;                            //フェードイン処理の開始、完了を管理するフラグ

    public static bool FireOnOff = true;                            // trueがついてる
    public static bool GateOnOff = true;                            //　tureが閉じてる

    public static bool isAliveBoss1 = true;                         //ボス１の討伐情報保存用

    public static bool[,] isStarGet = new bool[10, 10];             //スターの取得状況　trueが取得済み


    public static string GetNextScene(int nextscene) {
        return MapName[nextscene];
    }

    /// <summary>
    /// データの初期化
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
    /// シーンの初期化
    /// </summary>
    public static void InitScene() {
        SceneManager.LoadScene(MapName[CurrentMapNumber]);  // 現在のシーンを再ロード
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
