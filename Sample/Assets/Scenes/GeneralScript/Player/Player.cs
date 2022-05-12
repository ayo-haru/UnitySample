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
    //private Vector3 ReSpawnPos;                   // リスポーン位置を保存

    [System.NonSerialized]
    public static bool isHitSavePoint;              // セーブポイントに当たったか
    [System.NonSerialized]
    public static bool HitSavePointColorisRed;
    [System.NonSerialized]
    public static bool shouldRespawn;

    private ObservedValue<int> checkHP; // HPの値を監視する

    private GameObject fadeimage;   // フェードのパネル

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        fadeimage = GameObject.Find("Fade");

        isHitSavePoint = false;                     // フラグ初期化
        HitSavePointColorisRed = false;             // 赤色のセーブポイントと当たったか
        shouldRespawn = false;                      // リスポーンする時

        checkHP = new ObservedValue<int>(GameData.CurrentHP);
        checkHP.OnValueChange += () => { if (checkHP.Value < 1) PlayerDeath(); };
        _animator = GetComponent<Player2>().animator;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // プレイヤーの位置を保存
        checkHP.Value = GameData.CurrentHP;

        //if (this.transform.position.y < -10000) // 落下死時にリスポーン
        //{
        //    this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = GameData.ReSpawnPos;
        //}

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
            SaveManager.saveLastPlayerPos(GameData.PlayerPos);         // プレイヤーの位置を保存

            SaveManager.saveBossAlive(GameData.isAliveBoss1);           // ボス１の生存フラグを保存
            SaveManager.saveHP(GameData.CurrentHP);                     // 現在のHPを保存
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // 今いるマップの番号を保存
            SaveManager.saveCurrentPiece(GameData.CurrentPiece);        // 現在のかけらを保存
            SaveManager.savePieceGrade(GameData.CurrentPieceGrade);     // 現在のかけらの枠を保存
            SaveManager.saveFireOnOff(GameData.FireOnOff);
            SaveManager.saveGateOnOff(GameData.GateOnOff);
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
                GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
            }
            Warp.shouldWarp = false;
        }

        //---ゲームオーバー
        if (GameData.CurrentHP < 1)
        {
            shouldRespawn = false;

            //---フェード
            if (!GameObject.Find("Player_deth(Clone)"))
            {
                GameData.isFadeOut = true;              // フェードかける
                GameOver.GameOverReset();               // りすぽん
            }
        }


        //---死に戻り時
        if (shouldRespawn)
        {
            if (!GameData.isFadeOut)
            {
                GameData.InitScene();
                //Pause.isPause = false;
            }
        }
    }

    private void PlayerDeath() {
        GameOver.GameOverFlag = true;
        Pause.isPause = true;

        //Vector3 effectPos;
        //effectPos = new Vector3(GameData.PlayerPos.x, GameData.PlayerPos.y + 10.0f, GameData.PlayerPos.z);
        _animator.Play("Death");
        EffectManager.Play(EffectData.eEFFECT.EF_DEATH, GameData.PlayerPos, 7.0f);
    }


    private void OnTriggerStay(Collider other) {
        //---セーブポイント地点の処理
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {

            isHitSavePoint = true;                  // 当たったフラグを立てる
        }

    }

    void OnTriggerEnter(Collider other) {

        //---リスポーン地点の処理
        if(other.gameObject.tag == "Respawn")
        {
            GameData.ReSpawnPos = this.transform.position;
            Debug.Log(GameData.ReSpawnPos);
        }


        //----- 各シーン遷移 -----
        if (other.gameObject.tag == "toTutorial2")
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial2;
        }
        if (other.gameObject.tag == "toTutorial3")
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial3;
        }

        if (other.gameObject.tag == "toKitchen1"){
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
        }

        if (other.gameObject.tag == "toKitchen2"){
            GameData.isFadeOut = true;              // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage002;
        }

        if (other.gameObject.tag == "toKitchen3"){
            if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)  // 今いるシーンがボスシーンだった時ボスが生きてたらシーン遷移しない
            {
                if (GameData.isAliveBoss1)
                {
                    return;
                }
            }
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage003;
        }
        if (other.gameObject.tag == "toKitchen4")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage004;
        }
        if (other.gameObject.tag == "toKitchen5")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける  
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage005;
        }
        if (other.gameObject.tag == "toKitchen6")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage006;
        }
        if (other.gameObject.tag == "toBoss1")    // この名前のタグと衝突したら
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = false; // 当たったフラグを下す
        }
    }
}
