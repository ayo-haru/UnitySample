//=============================================================================
//
// ボスステージマネージャー[BossStageManager]
//
// 作成日:2022/03/28
// 作成者:吉原飛鳥
//
// <リファレンス>
// プレイヤーのHPUIをこのシーンで使いたいのでボスシーンのマネージャーを作りました
// その役割してそうなやつあったけどそれとは別でシーン管理させたかったんで勝手に作っちゃいました
// ごめんね！！！
//
// <開発履歴>
// 2022/03/28 作成 GameDataを通してプレイヤーを扱う,HPUIの描画,音再生,シーン遷移を実装
// 2022/03/28 ボスの攻撃を食らったらHP減少を実装
// 
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStageManager : MonoBehaviour
{
    public GameObject PlayerPrefab;                             // プレハブ内のプレイヤーを扱う
    public GameObject HPSystem;                                 // プレハブ内のHPUIをクローンする

    private void Awake()
    {
        //---プレイヤープレハブの取得
        if (!GameData.Player)
        {
            GameData.Player = PlayerPrefab;                     // プレイヤーの情報がなかったら
                                                                // GameDataにプレイヤーを定義する
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-80, 19.9f, 0f); // プレイヤーの初期位置を設定
        GameObject Player = Instantiate(GameData.Player);       // プレハブをクローン
        Player.name = GameData.Player.name;                         // プレハブの名前通りにする

         //---プレイヤーUIを表示
         GameObject canvas = GameObject.Find("Canvas");          // シーン上のCanvasを参照し、canvasに定義
        GameObject HPUI = Instantiate(HPSystem);                // プレハブをクローン
        HPUI.transform.SetParent(canvas.transform,false);       // シーン上のCanvasに子オブジェクトとしてアタッチ

        //---Audio再生
        for(int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager.Play(SoundData.eBGM.BGM_BOSS1, SoundData.GameAudioList);

        //---マップの番号(現在のシーン)を保存
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            /* 
             * このif文はエディタ上のデバッグ用。本来はNextMapNumberは値が入っているが
             * unityのエディタ上でこのシーンだけ動かした場合は値が入らないためシリアライズフィールドで
             * インスペクタービューに表示させたcurrentSceneNumで初期化をする。
             * GameData.NextMapNumberは初期化してない場合はかってに0になってるから==
             */
            GameData.OldMapNumber = GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
        GameData.CurrentMapNumber = GameData.NextMapNumber;         // ボスシーンに到達している判定
        //SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // 現在のシーンをセーブ


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //---シーン遷移処理
        if(GameData.CurrentMapNumber != GameData.NextMapNumber)     // 現在のシーンと次のシーンを比較
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);   // 遷移先のシーンを取得
            SceneManager.LoadScene(nextSceneName);                  //　ロード
        }
    }
}
