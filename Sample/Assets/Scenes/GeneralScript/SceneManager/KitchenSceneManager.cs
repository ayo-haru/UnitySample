using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject Empty;
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
            SaveManager.load();
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
            
            //GameData.ReSpawnPos = SaveManager.sd.LastPlayerPos;
        }
        GameData.CurrentMapNumber =  GameData.NextMapNumber;
        //SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        //----- �v���C���[������ -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        switch(GameData.CurrentMapNumber) 
        {
            //---�X�e�[�W�V�[��
            //case (int)GameData.eSceneState.KitchenStage_SCENE:
            //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(5.0f, 80.0f, 0.0f);
            //    break;

            //---�X�e�[�W1
            case (int)GameData.eSceneState.KitchenStage001:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage002)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(398.0f, 15.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1110.0f, 18.0f, 0.0f);
                }
                else
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 20.0f, 0.0f);
                }
                //else // �X�e�[�W1�e�X�g�p
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(800.0f, 18.0f, 0.0f);
                //}
                //else // �X�e�[�W4�e�X�g�p
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(400.0f, 18.0f, 0.0f);
                //}
                //else // �X�e�[�W3, 5�e�X�g�p
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1050.0f, 18.0f, 0.0f);
                //}
                //else // �X�e�[�W5�e�X�g
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(560.0f, 53.0f, 0.0f);
                //}
                //else // �X�e�[�W6�e�X�g
                //{
                //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 223.0f, 0.0f);
                //}
                break;

            //---�X�e�[�W2
            case (int)GameData.eSceneState.KitchenStage002:
                GameData.PlayerPos = GameData.Player.transform.position = new Vector3(470.0f, -5.0f, 0.0f);

                break;

            //---�X�e�[�W3
            case (int)GameData.eSceneState.KitchenStage003:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1100.0f, 18.0f, 0.0f);
                }
                break;

            //---�X�e�[�W4
            case (int)GameData.eSceneState.KitchenStage004:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage001)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 18.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(125.0f, 18.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(615.0f, 18.0f, 0.0f);
                }
                break;

            //---�X�e�[�W5
            case (int)GameData.eSceneState.KitchenStage005:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(108.0f, 90.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(570.0f, 53.0f, 0.0f);
                }
                break;

            //---�X�e�[�W6
            case (int)GameData.eSceneState.KitchenStage006:
                if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage003)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 222.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 122.0f, 0.0f);
                }
                else if(GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                {
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 22.0f, 0.0f);
                }
                break;


            ////---�V�[��0
            //case (int)GameData.eSceneState.Kitchen_SCENE:
            //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 40.5f, -1.0f);
            //    break;

            ////---�V�[��1
            //case (int)GameData.eSceneState.Kitchen1_SCENE:
            //    if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen2_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
            //    }
            //    else if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen5_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 32.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---�V�[��2
            //case (int)GameData.eSceneState.Kitchen2_SCENE:
            //    if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen3_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(43.0f, 11.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---�V�[��3
            //case (int)GameData.eSceneState.Kitchen3_SCENE:
            //    if(GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen6_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(8.0f,30.0f,-1.0f);
            //    }
            //    else if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen4_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);

            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---�V�[��4
            //case (int)GameData.eSceneState.Kitchen4_SCENE:
            //    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    break;

            ////---�V�[��5
            //case (int)GameData.eSceneState.Kitchen5_SCENE:
            //    if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen6_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;

            ////---�V�[��6
            //case (int)GameData.eSceneState.Kitchen6_SCENE:
            //    if (GameData.OldMapNumber == (int)GameData.eSceneState.Kitchen3_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(10.0f, 12.5f, -1.0f);
            //        GameData.Player.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f,ForceMode.Force);
            //    }
            //    else if(GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(44.0f, 11.5f, -1.0f);
            //    }
            //    else
            //    {
            //        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-1.0f, 11.5f, -1.0f);
            //    }
            //    break;
            default:
                break;
        }

        //---�v���C���[����I�u�W�F�N�g�̎q�ɕ�������
        //Empty = GameObject.Find("Player");
        GameObject player = Instantiate(GameData.Player);
        player.name = GameData.Player.name;                     // ���O�̌���(Clone)�Ƃ��̂�h�����߁A
                                                                                      // �����I�Ƀv���n�u���ɂ��鏈��
        //player.transform.SetParent(Empty.transform, false);

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
            if (!GameData.isFadeOut)
            {
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
