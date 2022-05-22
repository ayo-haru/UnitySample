using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneManager : MonoBehaviour {
    //---- 変数定義 -----
    public GameObject playerPrefab;     // プレイヤーを格納
    //private GameObject Empty;         // 未使用
    [SerializeField]
    private int currentSceneNum;        // デバッグ用現在のシーンを格納

    private GameObject KitchenImage;    // 開始演出で出す画像
    private bool isCalledOnce = false;  // 開始演出で使用。一回だけ処理をするために使う。



    void Awake() {
        //Time.timeScale = 1.0f;   // ゲーム開始時は絶対にゲームのスピードは１



        //----- マップの番号を保存 -----
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            /* 
             * このif文はエディタ上のデバッグ用。本来はNextMapNumberは値が入っているが
             * unityのエディタ上でこのシーンだけ動かした場合は値が入らないためシリアライズフィールドで
             * インスペクタービューに表示させたcurrentSceneNumで初期化をする。
             * GameData.NextMapNumberは初期化してない場合はかってに0になってるから==
             */
            //SaveManager.load();
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
        if ((GameData.ReSpawnPos.x != 0.0f || GameData.ReSpawnPos.y != 0.0f )&& GameData.OldMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            //---タイトルから続きからを選択された場合
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
        }
        else
        {
            //---ゲームシーンをシーン遷移してきた場合やはじめからやる場合
            switch (GameData.CurrentMapNumber)
            {
                //---ステージ1
                case (int)GameData.eSceneState.KitchenStage001:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage002)
                    {

                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(430.0f, 15.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1110.0f, 18.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(770.0f, 115.0f, 0.0f);

                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 20.0f, 0.0f);
//#if UNITY_EDITOR
//                        if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage001)
//                            GameData.SaveAll();
//#endif

                        if (GameData.OldMapNumber == (int)GameData.eSceneState.Tutorial3 || (GameData.OldMapNumber == GameData.CurrentMapNumber && !GameOver.GameOverFlag))
                        {
                            GameData.InitData();
                            GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 20.0f, 0.0f);
                            GameData.SaveAll();
                        }
                    }
                    break;

                //---ステージ2
                case (int)GameData.eSceneState.KitchenStage002:
                    GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(470.0f, -10.0f, 0.0f);

                    break;

                //---ステージ3
                case (int)GameData.eSceneState.KitchenStage003:
                    GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1100.0f, 18.0f, 0.0f);

                    break;

                //---ステージ4
                case (int)GameData.eSceneState.KitchenStage004:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage001)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 18.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(125.0f, 18.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(665.0f, 18.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(50.0f, 16.5f, 0.0f);

                    }
                    break;

                //---ステージ5
                case (int)GameData.eSceneState.KitchenStage005:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(108.0f, 90.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(590.0f, 53.0f, 0.0f);
                    }
                    break;

                //---ステージ6
                case (int)GameData.eSceneState.KitchenStage006:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage003)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 222.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 122.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 22.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 16.0f, 0.0f);
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
        player.name = GameData.Player.name;                     // 名前の後ろに(Clone)とつくのを防ぐため、
                                                                // 強制的にプレハブ名にする処理
                                                                //player.transform.SetParent(Empty.transform, false);

        //----- 開始演出 -----
        //KitchenImage = GameObject.Find("Kitchen");



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
    void Update() {
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
