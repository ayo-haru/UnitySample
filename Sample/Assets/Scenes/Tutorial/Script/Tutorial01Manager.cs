using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial01Manager : MonoBehaviour
{
    //---- 変数定義 -----
    [SerializeField]
    private GameObject playerPrefab;     // プレイヤーを格納
    private int currentSceneNum;        // デバッグ用現在のシーンを格納
    private bool isCalledOnce = false;  // 開始演出で使用。一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Start()
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
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-165.0f, 17.0f, 0.0f);
        GameObject player = Instantiate(GameData.Player);
        player.name = GameData.Player.name;                     // 名前の後ろに(Clone)とつくのを防ぐため、



        //----- 音鳴らす準備 -----                                                       //----- 音鳴らす準備 -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
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
