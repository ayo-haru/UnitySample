using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map001SceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField]
    private int currentSceneNum;

    private GameObject KitchenImage;       // �J�n���o�ŏo���摜
    private bool isCalledOnce = false;     // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

    // Start is called before the first frame update
    void Awake()
    {
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);

        GameObject player = Instantiate(GameData.Player);


        //----- �J�n���o -----
        //KitchenImage = GameObject.Find("Kitchen");

        //----- ���炷���� -----
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
