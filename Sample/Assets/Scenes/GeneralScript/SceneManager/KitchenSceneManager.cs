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
    private bool isCalledOnce = false;                              // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

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
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber =  GameData.NextMapNumber;
        SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        //----- �v���C���[������ -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        switch(GameData.CurrentMapNumber) {

            //---�X�e�[�W�V�[��
            case (int)GameData.eSceneState.KitchenStage_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(5.0f, 80.0f, 0.0f);
                break;

            //---�V�[��0
            case (int)GameData.eSceneState.Kitchen_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 40.5f, -1.0f);
                break;

            //---�V�[��1
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

            //---�V�[��2
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

            //---�V�[��3
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

            //---�V�[��4
            case (int)GameData.eSceneState.Kitchen4_SCENE:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
                break;

            //---�V�[��5
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
            
            //---�V�[��6
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
        //if (!isCalledOnce)     // ��񂾂��Ă�
        //{
        //    KitchenImage.GetComponent<ImageShow>().Show(2);
        //    isCalledOnce = true;
        //}

        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // �ۑ����Ă���V�[���ԍ������݂Ǝ����قȂ�����V�[���ړ�
        {
            GameData.OldMapNumber = GameData.CurrentMapNumber;
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}