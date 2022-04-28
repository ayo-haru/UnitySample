//=============================================================================
//
// プレイヤーの管理する
//
// 作成日:2022/03/11
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/11 作成
// 2022/03/30 HP保存
// 2022/04/18 アニメーション保存してないわ
// 2022/04/19 セーブポイントによるセーブ実装
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    //---変数宣言
    //private Vector3 ReSpawnPos;     // リスポーン位置を保存

    [System.NonSerialized]
    public static bool isHitSavePoint; // セーブポイントに当たったか
    [System.NonSerialized]
    public static bool HitSavePointColorisRed;
    [System.NonSerialized]
    public static bool shouldRespawn;

    // Start is called before the first frame update
    void Start()
    {
        isHitSavePoint = false; // フラグ初期化
        HitSavePointColorisRed = false;
        shouldRespawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // プレイヤーの位置を保存

        //if (this.transform.position.y < -10000) // 落下死時にリスポーン
        //{
        //    this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = GameData.ReSpawnPos;
        //}

        if (Pause.isPause)  // ポーズフラグによってポーズするかやめるか
        {
            Pause.PauseStart();
        }
        else
        {
            Pause.PauseFin();
        }

        if (isHitSavePoint) // セーブポイントに当たっていて、そのフレームの最初にスティックが傾けられたら
        {
            //Debug.Log("セーブポイントに当たってる");
            if (GamePadManager.onceTiltStick)
            {
                if (HitSavePointColorisRed)
                {
                    Warp.canWarp = true;
                }
                else
                {
                    SaveManager.canSave = true;
                }
                //Debug.Log("セーブかのう");
            }
        }

        if (SaveManager.shouldSave) // セーブするが選択されたら
        {
            Debug.Log("セーブした");
            GameData.ReSpawnPos = this.transform.position;              // プレイヤーの位置を保存
            SaveManager.saveLastPlayerPos(GameData.ReSpawnPos);         // プレイヤーの位置を保存
            SaveManager.saveBossAlive(GameData.isAliveBoss1);           // ボス１の生存フラグを保存
            SaveManager.saveHP(GameData.CurrentHP);                     // 現在のHPを保存
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // 今いるマップの番号を保存
            SaveManager.saveCurrentPiece(GameData.CurrentPiece);        // 現在のかけらを保存
            SaveManager.savePieceGrade(GameData.CurrentPieceGrade);     // 現在のかけらの枠を保存
            SaveManager.canSave = false;                                // セーブが終わったのでフラグを下す
            SaveManager.shouldSave = false;
        }

        if (Warp.shouldWarp)
        {
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE)
            {
                GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
            }
            else
            {
                throw new Exception("シーン名どこに戻るかわからなかったからボスシーンから戻れなくしてある");
                // 下のネクストに入れるのシーン番号を新しいのにしてね
                GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
            }
            Warp.shouldWarp = false;
        }

        if (shouldRespawn)
        {
            if (!GameData.isFadeOut)
            {
                GameData.InitScene();
                Pause.isPause = false;
            }
        }
    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Respawn")
        {
            GameData.ReSpawnPos = this.transform.position;
            Debug.Log(GameData.ReSpawnPos);
        }

        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            
            isHitSavePoint = true;  // 当たったフラグを立てる
        }

        //----- シーン遷移 -----
        if (other.gameObject.tag == "toKitchen1")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            Pause.isPause = true;   // フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
        }
        if (other.gameObject.tag == "toKitchen2")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            Pause.isPause = true;   // フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage002;
        }
        if (other.gameObject.tag == "toKitchen3")    // この名前のタグと衝突したら
        {
            if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)  // 今いるシーンがボスシーンだった時ボスが生きてたらシーン遷移しない
            {
                if (GameData.isAliveBoss1)
                {
                    return;
                }
            }
            GameData.isFadeOut = true;  // フェードかける
            Pause.isPause = true;   // 　フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage003;
        }
        if (other.gameObject.tag == "toKitchen4")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            Pause.isPause = true;   // フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage004;
        }
        if (other.gameObject.tag == "toKitchen5")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける  
            Pause.isPause = true;   // フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage005;
        }
        if (other.gameObject.tag == "toKitchen6")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            Pause.isPause = true;   // フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage006;
        }
        if (other.gameObject.tag == "toBoss1")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            Pause.isPause = true;   // フェード終わるまでポーズ
            Debug.Log("フェードはじめのポーズ");
            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = false; // 当たったフラグを下す
            //Pause.isPause = false;  // バグ防止
            //Debug.Log("バグとるポーズ解除");
            //Debug.Log("セーブ可能じゃない");
        }
    }
}
