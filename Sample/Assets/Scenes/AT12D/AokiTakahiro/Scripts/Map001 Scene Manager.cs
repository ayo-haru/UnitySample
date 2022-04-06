using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map001SceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField]
    private int currentSceneNum;

    private GameObject KitchenImage;       // 開始演出で出す画像
    private bool isCalledOnce = false;     // 開始演出で使用。一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Awake()
    {
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);

        GameObject player = Instantiate(GameData.Player);


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

    }
}
