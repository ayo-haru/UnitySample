//=============================================================================
//
// シーンマネージャー
//
// 作成日:2022/03/11
// 作成者:小楠裕子
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
    public int roomSize = 50;
    public GameObject playerPrefab;

    private AudioSource[] audioSourceList = new AudioSource[5];    // 一回に同時にならせる数


    [SerializeField]
    public AudioClip clip1;
    [SerializeField]
    public AudioClip clip2;
    [SerializeField]
    public AudioClip clip3;
    [SerializeField]
    public AudioClip clip4;


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
        GameData.NextMapNumber = GameData.CurrentMapNumber = (int)GameData.SceneState.MAP1_SCENE;
        SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        // audioSourceList配列の数だけAudioSourceを自身に生成して配列に格納
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // 保存してあるシーン番号が現在と次が異なったらシーン移動
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }

        if (Input.GetKey(KeyCode.U))
        {
            SoundManager.Play(clip1, audioSourceList);
        }
        if (Input.GetKey(KeyCode.I))
        {
            SoundManager.Play(clip2, audioSourceList);
        }
        if (Input.GetKey(KeyCode.O))
        {
            SoundManager.Play(clip3, audioSourceList);
        }
        if (Input.GetKey(KeyCode.P))
        {
            SoundManager.Play(clip4, audioSourceList);
        }


    }
}