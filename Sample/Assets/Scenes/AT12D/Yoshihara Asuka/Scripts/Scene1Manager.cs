//=============================================================================
//
// 各シーンのデータ管理[Scene1Manager]
//
// 作成日:2022/03/11
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/11
// 2022/03/22   GameData導入
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public GameObject playerPrefab;                             // プレイヤーのプレハブを扱う
    public GameObject HPSystem;     
    //private int SceneNumber = (int)GameData.eSceneState.Kitchen1_SCENE;

    private void Awake()
    {

        Debug.Log("Awake");
        //---フレームレート固定
        Application.targetFrameRate = 60;


        //---プレイヤープレハブの取得
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;                     // GameDataのプレイヤーに取得
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f); // プレイヤーの座標を設定
        GameObject player = Instantiate(GameData.Player);       // プレハブを実体化

        //---UIを表示
        GameObject canvas = GameObject.Find("Canvas");          // シーン上のCanvasを参照し,canvasに定義
        var instance = Instantiate(HPSystem);                    // HPUIを取得
        instance.transform.SetParent(canvas.transform,false);   // canvasの子オブジェクトにアタッチ

        //for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        //{
        //    SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        //}
        SoundManager.Play(SoundData.eBGM.BGM_TITLE, SoundData.GameAudioList);

        //Debug.Log(SceneNumber);
        Debug.Log("Awake");
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
    //    {
    //        SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
    //    }
    //    SoundManager.Play(SoundData.eBGM.BGM_TITLE,SoundData.GameAudioList);
    //}

    private void Start()
    {
        Debug.Log("start");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("log");

    }
}
