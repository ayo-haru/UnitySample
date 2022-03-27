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
    private bool isCalledOnce = false;                             // 開始演出で使用。一回だけ処理をするために使う。

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
            GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber =  GameData.NextMapNumber;
        SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        //----- プレイヤー初期化 -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        switch(GameData.CurrentMapNumber) {
            case (int)GameData.eSceneState.Kitchen1_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-13.0f, 5.0f, -1.0f);
                break;
            case (int)GameData.eSceneState.Kitchen2_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
                break;
            case (int)GameData.eSceneState.Kitchen3_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
                break;
            case (int)GameData.eSceneState.Kitchen4_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
                break;
            case (int)GameData.eSceneState.Kitchen5_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
                break;
            case (int)GameData.eSceneState.Kitchen6_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
                break;
            default:
                break;
        }


        GameObject player = Instantiate(GameData.Player);


        //----- 開始演出 -----
        //KitchenImage = GameObject.Find("Kitchen");

        //----- 音鳴らす準備 -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
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
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
