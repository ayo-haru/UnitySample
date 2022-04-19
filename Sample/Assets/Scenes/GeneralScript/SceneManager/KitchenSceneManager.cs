using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
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
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber =  GameData.NextMapNumber;
        //SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        //----- プレイヤー初期化 -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        switch(GameData.CurrentMapNumber) {

            //---ステージシーン
            case (int)GameData.eSceneState.KitchenStage_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(5.0f, 80.0f, 0.0f);
                break;

            //---シーン0
            case (int)GameData.eSceneState.Kitchen_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 40.5f, -1.0f);
                break;

            //---シーン1
            case (int)GameData.eSceneState.Kitchen1_SCENE:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen2_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen5_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 32.5f, -1.0f);
                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1.0f, 11.5f, -1.0f);
                }
                break;

            //---シーン2
            case (int)GameData.eSceneState.Kitchen2_SCENE:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen3_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(43.0f, 11.5f, -1.0f);
                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
                }
                break;

            //---シーン3
            case (int)GameData.eSceneState.Kitchen3_SCENE:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen6_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(8.0f,30.0f,-1.0f);
                }
                else if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen4_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);

                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
                }
                break;

            //---シーン4
            case (int)GameData.eSceneState.Kitchen4_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
                break;

            //---シーン5
            case (int)GameData.eSceneState.Kitchen5_SCENE:
                if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen6_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
                }
                break;
            
            //---シーン6
            case (int)GameData.eSceneState.Kitchen6_SCENE:
                if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen3_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(10.0f, 12.5f, -1.0f);
                    GameData.Player.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f,ForceMode.Force);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
                }
                break;
            default:
                break;
        }


        GameObject player = Instantiate(GameData.Player);
        player.name = GameData.Player.name;                     // 名前の後ろに(Clone)とつくのを防ぐため、
                                                                                      // 強制的にプレハブ名にする処理

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
            GameData.OldMapNumber = GameData.CurrentMapNumber;
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
