using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExStageManager : MonoBehaviour
{
    //---- 変数定義 -----
    public GameObject playerPrefab;     // プレイヤーを格納
    //private GameObject Empty;         // 未使用
    [SerializeField]
    private int currentSceneNum;        // デバッグ用現在のシーンを格納

    private bool isCalledOnce = false;  // 開始演出で使用。一回だけ処理をするために使う。


    void Awake()
    {
        //----- マップの番号を保存 -----
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber = GameData.NextMapNumber;


        //----- プレイヤー初期化 -----
        // プレイヤー自身が格納されていないときにプレハブを格納
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        // プレイヤーの初期座標格納
        if ((GameData.ReSpawnPos.x != 0.0f || GameData.ReSpawnPos.y != 0.0f) && GameData.OldMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            //---タイトルから続きからを選択された場合
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
        }
        else
        {
            //---ゲームシーンをシーン遷移してきた場合やはじめからやる場合
            switch (GameData.CurrentMapNumber)
            {
                // EXステージ 1
                case (int)GameData.eSceneState.BossStage001:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage002)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(640.0f, 107.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(15.0f, 23.0f, 0.0f);


                        if (GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE || (GameData.OldMapNumber == GameData.CurrentMapNumber && !GameOver.GameOverFlag))
                        {
                            GameData.InitData();
                            GameData.PlayerPos = GameData.Player.transform.position = new Vector3(15.0f, 23.0f, 0.0f);
                            GameData.SaveAll();
                        }
                    }
                    break;

                // EXステージ 2
                case (int)GameData.eSceneState.BossStage002:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage001)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage003)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(700.0f, 135.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }

                    break;
                // EXステージ 3
                case (int)GameData.eSceneState.BossStage003:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage002)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    break;

                default:
                    break;
            }
        }
        if (GameOver.GameOverFlag)
        {
            //---ゲームオーバー時
            // 最終セーブポイントか、シーン１の初期位置にリスポーン
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
            GameOver.GameOverFlag = false;
        }
        else if (Player.shouldRespawn)
        {
            //---ギミックに殺されたときか、毒沼落ちた時
            // 直前に通ったリスポーン地点へリスポーン
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
            Player.shouldRespawn = false;
        }



        //----- プレイヤーをゲーム画面へ複製 -----
        //---プレイヤーを空オブジェクトの子に複製する
        //Empty = GameObject.Find("Player");
        GameObject player = Instantiate(GameData.Player);
        player.name = GameData.Player.name;                   

        //----- 音鳴らす準備 -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        //SoundManager.Play(SoundData.eBGM.BGM_KITCHEN, SoundData.GameAudioList);
        //ｂｇｍ再生のオブジェクトが生成されてなかったら作る
        GameObject bgmObject = GameObject.Find("BGMObject(Clone)");
        if (!bgmObject)
        {
            bgmObject = (GameObject)Resources.Load("BGMObject");
            Instantiate(bgmObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)     // 一回だけ呼ぶ
        {
            //---フェードイン処理
            GameData.isFadeIn = true;
            //KitchenImage.GetComponent<ImageShow>().Show(2);
            isCalledOnce = true;    // 二回以上はいらないように反転
        }



        //----- シーン遷移 -----
        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // 保存してあるシーン番号が現在と次が異なったらシーン移動
        {
            //---フェードアウトの終了待ち
            if (!GameData.isFadeOut)
            {
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }

    }
}
