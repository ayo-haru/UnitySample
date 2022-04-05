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
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector3 ReSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = this.transform.position;    // プレイヤーの位置を保存

        if(this.transform.position.y < -5)
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

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    EffectManager.Play(EffectData.eEFFECT.EF_SHEILD2, GameData.PlayerPos);
        //}
    }

    void OnTriggerEnter(Collider other) {
        //----- セーブ -----
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            ReSpawnPos = this.transform.position;    // プレイヤーの位置を保存
            SaveManager.saveLastPlayerPos(ReSpawnPos);
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
}
