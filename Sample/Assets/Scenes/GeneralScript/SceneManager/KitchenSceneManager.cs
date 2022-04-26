using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject Empty;
    [SerializeField]
    private int currentSceneNum;

    private GameObject KitchenImage;                                // 開始演出で出す画像
    private bool isCalledOnce = false;                              // 開始演出で使用。一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Awake()
    {
        //----- マップの番号を保存 -----
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)    
        {
            /* 
             * このif文はエディタ上のデバッグ用。本来はNextMapNumberは値が入っているが
             * unityのエディタ上でこのシーンだけ動かした場合は値が入らないためシリアライズフィールドで
             * インスペクタービューに表示させたcurrentSceneNumで初期化をする。
             * GameData.NextMapNumberは初期化してない場合はかってに0になってるから==
             */
            SaveManager.load();
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
            
            //GameData.ReSpawnPos = SaveManager.sd.LastPlayerPos;
        }
        GameData.CurrentMapNumber =  GameData.NextMapNumber;
        //SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        //----- プレイヤー初期化 -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        switch(GameData.CurrentMapNumber) 
        {
            //---ステージシーン
            //case (int)GameData.eSceneState.KitchenStage_SCENE:
            //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(5.0f, 80.0f, 0.0f);
            //    break;

            //---ステージ1
            case (int)GameData.eSceneState.KitchenStage001:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage002)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(398.0f, 15.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1110.0f, 18.0f, 0.0f);
                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 20.0f, 0.0f);
                }
                //else // ステージ1テスト用
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(800.0f, 18.0f, 0.0f);
                //}
                //else // ステージ4テスト用
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(400.0f, 18.0f, 0.0f);
                //}
                //else // ステージ3, 5テスト用
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1050.0f, 18.0f, 0.0f);
                //}
                //else // ステージ5テスト
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(560.0f, 53.0f, 0.0f);
                //}
                //else // ステージ6テスト
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 223.0f, 0.0f);
                //}
                break;

            //---ステージ2
            case (int)GameData.eSceneState.KitchenStage002:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(470.0f, -5.0f, 0.0f);

                break;

            //---ステージ3
            case (int)GameData.eSceneState.KitchenStage003:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1100.0f, 18.0f, 0.0f);
                }
                break;

            //---ステージ4
            case (int)GameData.eSceneState.KitchenStage004:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage001)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 18.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(125.0f, 18.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(615.0f, 18.0f, 0.0f);
                }
                break;

            //---ステージ5
            case (int)GameData.eSceneState.KitchenStage005:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(108.0f, 90.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(570.0f, 53.0f, 0.0f);
                }
                break;

            //---ステージ6
            case (int)GameData.eSceneState.KitchenStage006:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage003)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 222.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 122.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 22.0f, 0.0f);
                }
                break;


            ////---シーン0
            //case (int)GameData.eSceneState.Kitchen_SCENE:
            //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 40.5f, -1.0f);
            //    break;

            ////---シーン1
            //case (int)GameData.eSceneState.Kitchen1_SCENE:
            //    if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen2_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
            //    }
            //    else if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen5_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 32.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---シーン2
            //case (int)GameData.eSceneState.Kitchen2_SCENE:
            //    if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen3_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(43.0f, 11.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---シーン3
            //case (int)GameData.eSceneState.Kitchen3_SCENE:
            //    if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen6_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(8.0f,30.0f,-1.0f);
            //    }
            //    else if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen4_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);

            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---シーン4
            //case (int)GameData.eSceneState.Kitchen4_SCENE:
            //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    break;

            ////---シーン5
            //case (int)GameData.eSceneState.Kitchen5_SCENE:
            //    if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen6_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---シーン6
            //case (int)GameData.eSceneState.Kitchen6_SCENE:
            //    if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen3_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(10.0f, 12.5f, -1.0f);
            //        GameData.Player.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f,ForceMode.Force);
            //    }
            //    else if(GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;
            default:
                break;
        }

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
        SoundManager.Play(SoundData.eBGM.BGM_KITCHEN, SoundData.GameAudioList);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isCalledOnce)     // 一回だけ呼ぶ
        //{
        //    KitchenImage.GetComponent<ImageShow>().Show(2);
        //    isCalledOnce = true;
        //}

        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // 保存してあるシーン番号が現在と次が異なったらシーン移動
        {
            if (!GameData.isFadeOut)
            {
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
