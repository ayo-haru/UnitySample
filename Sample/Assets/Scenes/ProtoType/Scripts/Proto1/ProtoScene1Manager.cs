//=============================================================================
//
// シーンマネージャー
//
// 作成日:2022/03/11
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/11 作成
// 2022/03/13 シーン遷移する
// 2022/03/13 プレイヤーを一個にした
// 2022/03/13 シーン遷移を楽にしたはず
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene1Manager : MonoBehaviour {
    public int roomSize = 50;           // 部屋の大きさ。フェードに使用
    public GameObject playerPrefab;     // プレイヤーのプレハブを入れるところ

    //[System.NonSerialized]
    //public AudioSource[] audioSourceList = new AudioSource[5];    // 一回に同時にならせる数
    private GameObject GardenImage;                                // 開始演出で出す画像
    private bool isCalledOnce = false;                             // 開始演出で使用。一回だけ処理をするために使う。


    // Start is called before the first frame update
    void Awake() {
        Application.targetFrameRate = 60;           // フレームレートを固定

        GameData.SetroomSize(roomSize);             // 部屋のサイズをセット

        //----- プレイヤー初期化 -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        GameObject player = Instantiate(GameData.Player);

        //----- マップの番号を保存 -----
        //GameData.NextMapNumber = GameData.CurrentMapNumber = (int)GameData.eSceneState.MAP1_SCENE;
        SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        // audioSourceList配列の数だけAudioSourceを自身に生成して配列に格納
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }

        //----- 開始演出 -----
        GardenImage = GameObject.Find("Garden");
    }

    // Update is called once per frame
    void Update() {
        if (!isCalledOnce)     // 一回だけ呼ぶ
        {
            GardenImage.GetComponent<ImageShow>().Show(2);
            isCalledOnce = true;
        }


        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // 保存してあるシーン番号が現在と次が異なったらシーン移動
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}