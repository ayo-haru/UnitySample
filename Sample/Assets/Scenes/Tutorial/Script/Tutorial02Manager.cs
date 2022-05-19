using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial02Manager : MonoBehaviour
{
    //---- �ϐ���` -----
    [SerializeField]
    private GameObject playerPrefab;     // �v���C���[���i�[
    private GameObject player;
    private int currentSceneNum = (int)GameData.eSceneState.Tutorial2;        // �f�o�b�O�p���݂̃V�[�����i�[
    private bool isCalledOnce = false;  // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B
    [SerializeField]
    private GameObject pancakeobj;
    private GameObject PancakeObj;

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
            //SaveManager.load();
            GameData.OldMapNumber = GameData.NextMapNumber = currentSceneNum;
        }
        GameData.CurrentMapNumber = GameData.NextMapNumber;



        //----- �v���C���[������ -----
        // �v���C���[���g���i�[����Ă��Ȃ��Ƃ��Ƀv���n�u���i�[
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }
        PlayerAppearance();
    
        //----- �p���P�[�L������ -----
        PancakeAppearance();


        //----- ���炷���� -----                                                       //----- ���炷���� -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
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

    /// <summary>
    /// �p���P�[�L�o��
    /// </summary>
    public void PancakeAppearance() {
        PancakeObj = Instantiate(pancakeobj);
        PancakeObj.transform.position = new Vector3(50.0f, 23.0f, 0.0f);
        PancakeObj.name = pancakeobj.name;
    }

    /// <summary>
    /// �p���P�[�L����
    /// </summary>
    public void PancakeDestroy() {
        Destroy(PancakeObj);
    }


    /// <summary>
    /// �v���C���[�o��
    /// </summary>
    public void PlayerAppearance() {
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-80.0f, 17.0f, 0.0f);
        this.player = Instantiate(GameData.Player,GameData.PlayerPos,Quaternion.EulerAngles(0.0f,90.0f,0.0f));
        //GameData.Player.transform.rotation = Quaternion.identity;
        player.name = GameData.Player.name;                     // ���O�̌���(Clone)�Ƃ��̂�h�����߁A
    }

    /// <summary>
    /// �v���C���[����
    /// </summary>
    public void PlayerDestroy() {
        Destroy(GameObject.Find(player.name));
    }
}
