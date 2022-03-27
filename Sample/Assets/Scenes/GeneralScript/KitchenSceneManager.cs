using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField]
    private int currentSceneNum;

    private GameObject KitchenImage;                                // �J�n���o�ŏo���摜
    private bool isCalledOnce = false;                             // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

    // Start is called before the first frame update
    void Awake()
    {
        //----- �}�b�v�̔ԍ���ۑ� -----
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)    
        {
            /* 
             * ����if���̓G�f�B�^��̃f�o�b�O�p�B�{����NextMapNumber�͒l�������Ă��邪
             * unity�̃G�f�B�^��ł��̃V�[���������������ꍇ�͒l������Ȃ����߃V���A���C�Y�t�B�[���h��
             * �C���X�y�N�^�[�r���[�ɕ\��������currentSceneNum�ŏ�����������B
             * GameData.NextMapNumber�͏��������ĂȂ��ꍇ�͂����Ă�0�ɂȂ��Ă邩��==
             
             */
            GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber =  GameData.NextMapNumber;
        SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        //----- �v���C���[������ -----
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


        //----- �J�n���o -----
        //KitchenImage = GameObject.Find("Kitchen");

        //----- ���炷���� -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isCalledOnce)     // ��񂾂��Ă�
        //{
        //    KitchenImage.GetComponent<ImageShow>().Show(2);
        //    isCalledOnce = true;
        //}

        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // �ۑ����Ă���V�[���ԍ������݂Ǝ����قȂ�����V�[���ړ�
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
