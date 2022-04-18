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
// 2022/04/18 アニメーション保存
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //---変数宣言
    private Vector3 ReSpawnPos;

    [System.NonSerialized]
    public static bool isHitSavePoint; 

    // Start is called before the first frame update
    void Start()
    {
        isHitSavePoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // プレイヤーの位置を保存

        if (this.transform.position.y < -10000)
        {
            this.transform.position = GameData.Player.transform.position = GameData.PlayerPos = ReSpawnPos;
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause.isPause = !Pause.isPause;
        }

        if (Pause.isPause)
        {
            Pause.PauseStart();
        }
        else
        {
            Pause.PauseFin();
        }

        if (SaveManager.shouldSave)
        {
            Debug.Log("セーブした");
            ReSpawnPos = this.transform.position;    // プレイヤーの位置を保存
            SaveManager.saveLastPlayerPos(ReSpawnPos);
            SaveManager.saveBossAlive(GameData.isAliveBoss1);
            SaveManager.saveHP(GameData.CurrentHP);
            SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);
            //SaveManager.canSave = false;
            SaveManager.shouldSave = false;
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    EffectManager.Play(EffectData.eEFFECT.EF_SHEILD2, GameData.PlayerPos);
        //}
    }

    //private void OnTriggerStay(Collider other) {
        //----- セーブ -----
        //if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        //{
        //    if (this.GetComponent<Player2>().UnderParryNow)
        //    {
        //        Pause.isPause = true;
        //        SaveManager.canSave = true;
        //        if (SaveManager.shouldSave)
        //        {
        //        }
        //    }
        //}
    //}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = true;
            if (GamePadManager.onceTiltStick)
            {
                SaveManager.canSave = true;
                
            }
            GamePadManager.onceTiltStick = false;
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



        //*************************************************************************************************
        // 以下プロトタイプ遷移
        //*************************************************************************************************
        //if (other.gameObject.tag == "MovePoint1to2")    // この名前のタグと衝突したら
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.MAP2_SCENE;   // 次のシーン番号を設定、保存
        //}

        //if (other.gameObject.tag == "MovePoint2to1")
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.MAP1_SCENE;    // 次のシーン番号を設定、保存
        //}

        //if (other.gameObject.tag == "MovePoint2toBoss")
        //{
        //    GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;    // 次のシーン番号を設定、保存
        //}
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = false;
            //SaveManager.canSave = false;
        }
    }
}
