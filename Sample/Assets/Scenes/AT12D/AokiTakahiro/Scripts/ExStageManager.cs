using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExStageManager : MonoBehaviour
{
    //---- �ϐ���` -----
    public GameObject playerPrefab;     // �v���C���[���i�[
    //private GameObject Empty;         // ���g�p
    [SerializeField]
    private int currentSceneNum;        // �f�o�b�O�p���݂̃V�[�����i�[

    private bool isCalledOnce = false;  // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B


    void Awake()
    {
        //----- �}�b�v�̔ԍ���ۑ� -----
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber = GameData.NextMapNumber;


        //----- �v���C���[������ -----
        // �v���C���[���g���i�[����Ă��Ȃ��Ƃ��Ƀv���n�u���i�[
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        // �v���C���[�̏������W�i�[
        if ((GameData.ReSpawnPos.x != 0.0f || GameData.ReSpawnPos.y != 0.0f) && GameData.OldMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            //---�^�C�g�����瑱�������I�����ꂽ�ꍇ
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
        }
        else
        {
            //---�Q�[���V�[�����V�[���J�ڂ��Ă����ꍇ��͂��߂�����ꍇ
            switch (GameData.CurrentMapNumber)
            {
                // EX�X�e�[�W 1
                case (int)GameData.eSceneState.BossStage001:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage002)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(640.0f, 107.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(15.0f, 23.0f, 0.0f);


                        if (GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE || (GameData.OldMapNumber == GameData.CurrentMapNumber && !GameOver.GameOverFlag))
                        {
                            GameData.InitData();
                            GameData.PlayerPos = GameData.Player.transform.position = new Vector3(15.0f, 23.0f, 0.0f);
                            GameData.SaveAll();
                        }
                    }
                    break;

                // EX�X�e�[�W 2
                case (int)GameData.eSceneState.BossStage002:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage001)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage003)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(700.0f, 135.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }

                    break;
                // EX�X�e�[�W 3
                case (int)GameData.eSceneState.BossStage003:
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage002)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    break;

                default:
                    break;
            }
        }
        if (GameOver.GameOverFlag)
        {
            //---�Q�[���I�[�o�[��
            // �ŏI�Z�[�u�|�C���g���A�V�[���P�̏����ʒu�Ƀ��X�|�[��
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
            GameOver.GameOverFlag = false;
        }
        else if (Player.shouldRespawn)
        {
            //---�M�~�b�N�ɎE���ꂽ�Ƃ����A�ŏ���������
            // ���O�ɒʂ������X�|�[���n�_�փ��X�|�[��
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
            Player.shouldRespawn = false;
        }



        //----- �v���C���[���Q�[����ʂ֕��� -----
        //---�v���C���[����I�u�W�F�N�g�̎q�ɕ�������
        //Empty = GameObject.Find("Player");
        GameObject player = Instantiate(GameData.Player);
        player.name = GameData.Player.name;                   

        //----- ���炷���� -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        //SoundManager.Play(SoundData.eBGM.BGM_KITCHEN, SoundData.GameAudioList);
        //�������Đ��̃I�u�W�F�N�g����������ĂȂ���������
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
        if (!isCalledOnce)     // ��񂾂��Ă�
        {
            //---�t�F�[�h�C������
            GameData.isFadeIn = true;
            //KitchenImage.GetComponent<ImageShow>().Show(2);
            isCalledOnce = true;    // ���ȏ�͂���Ȃ��悤�ɔ��]
        }



        //----- �V�[���J�� -----
        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // �ۑ����Ă���V�[���ԍ������݂Ǝ����قȂ�����V�[���ړ�
        {
            //---�t�F�[�h�A�E�g�̏I���҂�
            if (!GameData.isFadeOut)
            {
                GameData.OldMapNumber = GameData.CurrentMapNumber;
                string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
                SceneManager.LoadScene(nextSceneName);
            }
        }

    }
}
