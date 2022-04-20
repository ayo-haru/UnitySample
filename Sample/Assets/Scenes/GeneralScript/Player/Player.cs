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

public class Player : MonoBehaviour
{
    //---変数宣言
    private Vector3 ReSpawnPos;     // リスポーン位置を保存

    [System.NonSerialized]
    public static bool isHitSavePoint; // セーブポイントに当たったか

    // Start is called before the first frame update
    void Start()
    {
        isHitSavePoint = false; // フラグ初期化
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // プレイヤーの位置を保存

        if (this.transform.position.y < -10000) // 落下死時にリスポーン
        {
            this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = ReSpawnPos;
        }


        if (Input.GetKeyDown(KeyCode.P))    // ポーズする
        {
            Pause.isPause = !Pause.isPause; // トグル
        }

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
            if (GamePadManager.onceTiltStick)
            {
                SaveManager.canSave = true;

            }
        }

        if (SaveManager.shouldSave) // セーブするが選択されたら
        {
            //Debug.Log("セーブした");
            ReSpawnPos = this.transform.position;    // プレイヤーの位置を保存
            SaveManager.saveLastPlayerPos(ReSpawnPos);  // プレイヤーの位置を保存
            SaveManager.saveBossAlive(GameData.isAliveBoss1);   // ボス１の生存フラグを保存
            SaveManager.saveHP(GameData.CurrentHP); // 現在のHPを保存
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // 今いるマップの番号を保存
            SaveManager.canSave = false;        // セーブが終わったのでフラグを下す
            SaveManager.shouldSave = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = true;  // 当たったフラグを立てる
        }

        //----- シーン遷移 -----
        if (other.gameObject.tag == "toKitchen1")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen1_SCENE;
        }
        if (other.gameObject.tag == "toKitchen2")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen2_SCENE;
        }
        if (other.gameObject.tag == "toKitchen3")    // この名前のタグと衝突したら
        {
            if(GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)  // 今いるシーンがボスシーンだった時ボスが生きてたらシーン遷移しない
            {
                if (GameData.isAliveBoss1)
                {
                    return;
                }
            }
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen3_SCENE;
        }
        if (other.gameObject.tag == "toKitchen4")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen4_SCENE;
        }
        if (other.gameObject.tag == "toKitchen5")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen5_SCENE;
        }
        if (other.gameObject.tag == "toKitchen6")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.Kitchen6_SCENE;
        }
        if (other.gameObject.tag == "toBoss1")    // この名前のタグと衝突したら
        {
            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = false; // 当たったフラグを下す
            SaveManager.canSave = false;    // セーブ可能ではないためフラグを下す
        }
    }
}
