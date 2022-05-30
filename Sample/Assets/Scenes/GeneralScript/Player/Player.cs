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

    private bool entryGate;             //ゲートに入ろうとしてるか
    private int nextMapNumber;          //次のシーン番号保存用
    private float moveSpeed = 0.5f;     //扉に入る時の移動速度
    private bool onceFlag;              //一回だけ処理する用
    public float rotSpeed = 1.0f;       //回転速度
    private float rotTimer;             //回転用タイマー
    public float movePos = 15.0f;       //移動終了位置

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

        entryGate = false;
        nextMapNumber = 0;
        onceFlag = true;
        rotTimer = 0.0f;
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

        //if (isHitSavePoint) // セーブポイントに当たっていて、そのフレームの最初にスティックが傾けられたら
        //{
        //    if (GamePadManager.onceTiltStick)
        //    {
        //        if (HitSavePointColorisRed)
        //        {
        //            Warp.canWarp = true;
        //        }
        //        else
        //        {
        //            SaveManager.canSave = true;
        //        }
        //    }
        //}

        if (SaveManager.shouldSave) // セーブするが選択されたら
        {
            GameData.SaveAll();
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
            if (!GameObject.Find(EffectData.EF[(int)EffectData.eEFFECT.EF_PLAYER_DEATH].name+"(Clone)"))
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
        //GamePadManager.onceTiltStick = false;


        //---ゲート入り中
        if (entryGate)
        {
            //キー入力と重力無効かしとく
            gameObject.GetComponent<Player2>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            //プレイヤー移動処理
            Vector3 newPos = transform.position;
            newPos.z += moveSpeed;
            transform.position = newPos;
            //回転の目標値
            Quaternion target = new Quaternion();
            //向きを設定
            target = Quaternion.LookRotation(new Vector3(0.0f,0.0f,1.0f));
            //回転させる
            rotTimer += rotSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotTimer);

            //一定量うしろに行ったらシーン遷移
            if (transform.position.z > movePos)
            {
                
                //シーン遷移
                if (onceFlag)
                {
                    GameData.isFadeOut = true;  // フェードかける
                    switch (nextMapNumber)
                    {
                        case (int)GameData.eSceneState.Tutorial2:
                            GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial2;
                            break;
                        case (int)GameData.eSceneState.Tutorial3:
                            GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial3;
                            break;
                        case (int)GameData.eSceneState.KitchenStage001:
                            GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;
                            break;
                        case (int)GameData.eSceneState.BOSS1_SCENE:
                            GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
                            break;

                    }
                    //変数初期化
                    entryGate = false;
                    nextMapNumber = 0;
                    onceFlag = false;
                    rotTimer = 0.0f;
                }
                
            }
        }
    }

    private void PlayerDeath() {
        GameOver.GameOverFlag = true;
        Pause.isPause = true;

        //Vector3 effectPos;
        //effectPos = new Vector3(GameData.PlayerPos.x, GameData.PlayerPos.y + 10.0f, GameData.PlayerPos.z);
        _animator.Play("Death");
        EffectManager.Play(EffectData.eEFFECT.EF_PLAYER_DEATH, GameData.PlayerPos, 7.0f);
        SoundManager.Play(SoundData.eSE.SE_PLAYER_DEATH, SoundData.IndelibleAudioList);
        StartCoroutine(this.GetComponent<Player2>().VibrationPlay(1.0f, 1.0f, 5.0f));
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
        }


        //----- 各シーン遷移 -----
        if (other.gameObject.tag == "toTutorial2")
        {
            //GameData.isFadeOut = true;  // フェードかける
            //GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial2;

            entryGate = true;
            nextMapNumber = (int)GameData.eSceneState.Tutorial2;
        }
        if (other.gameObject.tag == "toTutorial3")
        {
            //GameData.isFadeOut = true;  // フェードかける
            //GameData.NextMapNumber = (int)GameData.eSceneState.Tutorial3;

            entryGate = true;
            nextMapNumber = (int)GameData.eSceneState.Tutorial3;
        }

        if (other.gameObject.tag == "toKitchen1"){
            //GameData.isFadeOut = true;  // フェードかける
            //GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001;

            entryGate = true;
            nextMapNumber = (int)GameData.eSceneState.KitchenStage001;
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
            //GameData.isFadeOut = true;  // フェードかける
            //GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;

            entryGate = true;
            nextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }

        if (other.gameObject.tag == "toExStage1")
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.BossStage001;
        }
        if (other.gameObject.tag == "toExStage2")
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.BossStage002;
        }
        if (other.gameObject.tag == "toExStage3")
        {
            GameData.isFadeOut = true;  // フェードかける
            GameData.NextMapNumber = (int)GameData.eSceneState.BossStage003;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "SavePoint")    // この名前のタグと衝突したら
        {
            isHitSavePoint = false; // 当たったフラグを下す
        }
    }
}
