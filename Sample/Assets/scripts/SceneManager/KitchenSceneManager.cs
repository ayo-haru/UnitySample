using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneManager : MonoBehaviour {
    //---- �ϐ���` -----
    public GameObject playerPrefab;     // �v���C���[���i�[
    //private GameObject Empty;         // ���g�p
    [SerializeField]
    private int currentSceneNum;        // �f�o�b�O�p���݂̃V�[�����i�[
    [SerializeField]
    private GameObject kitchenstart;
    private GameObject KitchenStart;

    private Canvas canvas;

    private bool isCalledOnce = false;  // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B
    


    void Awake() {
        //Time.timeScale = 1.0f;   // �Q�[���J�n���͐�΂ɃQ�[���̃X�s�[�h�͂P



        //----- �f�o�b�O�p�}�b�v�̔ԍ���ۑ� -----
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            /* 
             * ����if���̓G�f�B�^��̃f�o�b�O�p�B�{����NextMapNumber�͒l�������Ă��邪
             * unity�̃G�f�B�^��ł��̃V�[���������������ꍇ�͒l������Ȃ����߃V���A���C�Y�t�B�[���h��
             * �C���X�y�N�^�[�r���[�ɕ\��������currentSceneNum�ŏ�����������B
             * GameData.NextMapNumber�͏��������ĂȂ��ꍇ�͂����Ă�0�ɂȂ��Ă邩��==
             */
            //SaveManager.load();
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
            //----- ���������Ƃ���}�b�v�t���O�̏����� -----
            for (int i = 0; i < System.Enum.GetValues(typeof(GameData.eSceneState)).Length; i++) {
                GameData.isWentMap[i] = false;
            }
        }
        GameData.CurrentMapNumber = GameData.NextMapNumber;



        //----- �v���C���[������ -----
        // �v���C���[���g���i�[����Ă��Ȃ��Ƃ��Ƀv���n�u���i�[
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        // �v���C���[�̏������W�i�[
        if ((GameData.ReSpawnPos.x != 0.0f || GameData.ReSpawnPos.y != 0.0f )&& GameData.OldMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            //---�^�C�g���ő��������I�����ꂽ�ꍇ
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
        }
        else
        {
            ////----- ���������Ƃ���}�b�v�t���O�̏����� -----
            //for (int i = 0; i< System.Enum.GetValues(typeof(GameData.eSceneState)).Length; i++) {
            //    GameData.isWentMap[i] = false;
            //}

            //---�Q�[���V�[�����V�[���J�ڂ��Ă����ꍇ��͂��߂�����ꍇ
            switch (GameData.CurrentMapNumber)
            {
                //---�X�e�[�W1
                case (int)GameData.eSceneState.KitchenStage001:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.KitchenStage001] = true;
                    
                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage002)
                    {

                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(430.0f, 15.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1110.0f, 18.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(770.0f, 115.0f, 0.0f);

                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 20.0f, 0.0f);

                        if ((GameData.OldMapNumber == (int)GameData.eSceneState.Tutorial3 && !GameOver.GameOverFlag) || (GameData.OldMapNumber == GameData.CurrentMapNumber && !GameOver.GameOverFlag && !Player.shouldRespawn)) {
                            // ��O�̃V�[�����`���[�g�R�ł͂Ȃ��Ƃ����Q�[���I�[�o�ł͂Ȃ��i�����ɏ��߂���n�߂ăV�[���P�Ŏ��񂾂Ƃ��ɂ͂���if�ɂ͓���Ȃ��j
                            // �܂��́A�f�o�b�O���ɓr������J�n�����Ƃ��͂���if�ɂ͓���Ȃ�
                            GameData.InitData();
                            GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 20.0f, 0.0f);
                            GameData.SaveAll();
                        }
                    }
                    break;

                //---�X�e�[�W2
                case (int)GameData.eSceneState.KitchenStage002:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.KitchenStage002] = true;

                    GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(470.0f, -10.0f, 0.0f);

                    break;

                //---�X�e�[�W3
                case (int)GameData.eSceneState.KitchenStage003:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.KitchenStage003] = true;

                    GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                    GameData.PlayerPos = GameData.Player.transform.position = new Vector3(1100.0f, 18.0f, 0.0f);

                    break;

                //---�X�e�[�W4
                case (int)GameData.eSceneState.KitchenStage004:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.KitchenStage004] = true;

                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage001)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(25.0f, 18.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(125.0f, 18.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(665.0f, 18.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(50.0f, 16.5f, 0.0f);

                    }
                    break;

                //---�X�e�[�W5
                case (int)GameData.eSceneState.KitchenStage005:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.KitchenStage005] = true;

                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(108.0f, 90.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage006)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(590.0f, 53.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(108.0f, 90.0f, 0.0f);
                    }
                    break;

                //---�X�e�[�W6
                case (int)GameData.eSceneState.KitchenStage006:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.KitchenStage006] = true;

                    if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage003)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 222.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage004)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 122.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.KitchenStage005)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(18.0f, 22.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 16.0f, 0.0f);
                    }
                    break;


                // EX�X�e�[�W 1
                case (int)GameData.eSceneState.BossStage001:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.BossStage001] = true;

                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage002)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(480.0f, 107.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(15.0f, 23.0f, 0.0f);
                    }
                    break;

                // EX�X�e�[�W 2
                case (int)GameData.eSceneState.BossStage002:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.BossStage002] = true;

                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage001)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }
                    else if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage003)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(510.0f, 135.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(30.0f, 107.0f, 0.0f);
                    }

                    break;
                // EX�X�e�[�W 3
                case (int)GameData.eSceneState.BossStage003:
                    // ���������Ƃ���t���O���Ă�
                    GameData.isWentMap[(int)GameData.eSceneState.BossStage003] = true;

                    if (GameData.OldMapNumber == (int)GameData.eSceneState.BossStage002)
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        GameData.PlayerVelocyty.SetVelocity(Vector3.zero);
                        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
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
            //---�M�~�b�N��瀂��ꂽ�Ƃ����A�ŏ���������
            // ���O�ɒʂ������X�|�[���n�_�փ��X�|�[��
            GameData.PlayerPos = GameData.Player.transform.position = GameData.ReSpawnPos;
            Player.shouldRespawn = false;
        }



        //----- �v���C���[���Q�[����ʂ֕��� -----
        //---�v���C���[����I�u�W�F�N�g�̎q�ɕ�������
        //Empty = GameObject.Find("Player");
        GameObject player = Instantiate(GameData.Player);
        player.name = GameData.Player.name;                     // ���O�̌���(Clone)�Ƃ��̂�h�����߁A
                                                                // �����I�Ƀv���n�u���ɂ��鏈��
                                                                //player.transform.SetParent(Empty.transform, false);


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

    private void Start() {
        //----- �J�n���o -----
        //KitchenImage = GameObject.Find("Kitchen");
        if (kitchenstart)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            KitchenStart = Instantiate(kitchenstart);
            KitchenStart.transform.SetParent(this.canvas.transform, false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isCalledOnce)     // ��񂾂��Ă�
        {
            //---�t�F�[�h�C������
            GameData.isFadeIn = true;
            if (GameData.OldMapNumber == (int)GameData.eSceneState.Tutorial3 && kitchenstart)
            {
                KitchenStart.GetComponent<ImageShow>().Show(2);
            }
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