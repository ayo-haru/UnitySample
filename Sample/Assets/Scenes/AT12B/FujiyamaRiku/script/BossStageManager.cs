//=============================================================================
//
// �{�X�X�e�[�W�}�l�[�W���[[BossStageManager]
//
// �쐬��:2022/03/28
// �쐬��:�g����
//
// <���t�@�����X>
// �v���C���[��HPUI�����̃V�[���Ŏg�������̂Ń{�X�V�[���̃}�l�[�W���[�����܂���
// ���̖������Ă����Ȃ���������ǂ���Ƃ͕ʂŃV�[���Ǘ���������������ŏ���ɍ�����Ⴂ�܂���
// ���߂�ˁI�I�I
//
// <�J������>
// 2022/03/28 �쐬 GameData��ʂ��ăv���C���[������,HPUI�̕`��,���Đ�,�V�[���J�ڂ�����
// 2022/03/28 �{�X�̍U����H�������HP����������
// 
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStageManager : MonoBehaviour
{
    public GameObject PlayerPrefab;                             // �v���n�u���̃v���C���[������
    public GameObject HPSystem;                                 // �v���n�u����HPUI���N���[������

    private void Awake()
    {
        //---�v���C���[�v���n�u�̎擾
        if (!GameData.Player)
        {
            GameData.Player = PlayerPrefab;                     // �v���C���[�̏�񂪂Ȃ�������
                                                                // GameData�Ƀv���C���[���`����
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-80, 19.9f, 0f); // �v���C���[�̏����ʒu��ݒ�
        GameObject Player = Instantiate(GameData.Player);       // �v���n�u���N���[��
        Player.name = GameData.Player.name;                         // �v���n�u�̖��O�ʂ�ɂ���

         //---�v���C���[UI��\��
         GameObject canvas = GameObject.Find("Canvas");          // �V�[�����Canvas���Q�Ƃ��Acanvas�ɒ�`
        GameObject HPUI = Instantiate(HPSystem);                // �v���n�u���N���[��
        HPUI.transform.SetParent(canvas.transform,false);       // �V�[�����Canvas�Ɏq�I�u�W�F�N�g�Ƃ��ăA�^�b�`

        //---Audio�Đ�
        for(int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager.Play(SoundData.eBGM.BGM_BOSS1, SoundData.GameAudioList);

        //---�}�b�v�̔ԍ�(���݂̃V�[��)��ۑ�
        if (GameData.NextMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            /* 
             * ����if���̓G�f�B�^��̃f�o�b�O�p�B�{����NextMapNumber�͒l�������Ă��邪
             * unity�̃G�f�B�^��ł��̃V�[���������������ꍇ�͒l������Ȃ����߃V���A���C�Y�t�B�[���h��
             * �C���X�y�N�^�[�r���[�ɕ\��������currentSceneNum�ŏ�����������B
             * GameData.NextMapNumber�͏��������ĂȂ��ꍇ�͂����Ă�0�ɂȂ��Ă邩��==
             */
            GameData.OldMapNumber = GameData.NextMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
        GameData.CurrentMapNumber = GameData.NextMapNumber;         // �{�X�V�[���ɓ��B���Ă��锻��
        //SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);   // ���݂̃V�[�����Z�[�u


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //---�V�[���J�ڏ���
        if(GameData.CurrentMapNumber != GameData.NextMapNumber)     // ���݂̃V�[���Ǝ��̃V�[�����r
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);   // �J�ڐ�̃V�[�����擾
            SceneManager.LoadScene(nextSceneName);                  //�@���[�h
        }
    }
}
