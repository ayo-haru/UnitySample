//=============================================================================
//
// �Q�[���I�[�o�[���o
//
// �쐬��:2022/03/16
// �쐬��:����T�q
//
// canvas�ɂ��̃X�N���v�g�����
//
// <�J������>
// 2022/03/16 �쐬
// 2022/03/20 ���o���̓T�u�J�����ɐ؂�ւ���悤�ɂ���
// 2022/03/24 CameraSwitch���炷���g�����J�����؂�ւ��ɕύX
// 2022/03/28 prefab����ǂݍ���ŕ\������悤�ɂ���
// 2022/03/30 ���g���C�ƃ^�C�g���ɖ߂��ǉ�
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private enum SELECT {RETRY,BACKTITLE};
    //�Q�[���I�[�o�[�Ŏg���I�u�W�F�N�g
    //�Q�[���I�[�o�[�摜
    private GameObject GameOverImage;
    //���g���C�摜
    private GameObject RetryImage;
    //�^�C�g���ɖ߂�摜
    private GameObject BackTitleImage;
    //�v��n�u
    private GameObject prefab;
    //�L�����o�X
    Canvas canvas;
    //�I��
    SELECT select;
    //�g�p�t���O
    public bool GameOverFlag;
    //ImageShow�R���|�[�l���g
    ImageShow imageShow_GameOver;
    ImageShow imageShow_Retry;
    ImageShow imageShow_BackTitle;
    //���C���J����
    //public GameObject mainCam;
    //�T�u�J����
    //public GameObject subCam;

    private bool isCalledOnce = false;                              // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

    // Start is called before the first frame update
    void Start()
    {
        //�L�����o�X�擾
        canvas = GetComponent<Canvas>();
        //�Q�[���I�[�o�[�摜�擾
        prefab = (GameObject)Resources.Load("GameOverImage");
        GameOverImage = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        //���g���C�摜�擾
        prefab = (GameObject)Resources.Load("RetryImage");
        RetryImage = Instantiate(prefab, new Vector3(-150.0f, -200.0f, 0.0f), Quaternion.identity);
        //�^�C�g���ɖ߂�摜�擾
        prefab = (GameObject)Resources.Load("BackTitleImage");
        BackTitleImage = Instantiate(prefab, new Vector3(150.0f, -200.0f, 0.0f), Quaternion.identity);

        //canvas�̎q�ɐݒ�
        GameOverImage.transform.SetParent(this.canvas.transform, false);
        RetryImage.transform.SetParent(this.canvas.transform, false);
        BackTitleImage.transform.SetParent(this.canvas.transform, false);

        //ImageShow�R���|�[�l���g�擾
        imageShow_GameOver = GameOverImage.GetComponent<ImageShow>();
        imageShow_Retry = RetryImage.GetComponent<ImageShow>();
        imageShow_BackTitle = BackTitleImage.GetComponent<ImageShow>();

        //�g�p�t���O�ݒ�
        GameOverFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //���g�p�������烊�^�[��
        if (!GameOverFlag)
        {
            return;
        }

        // �Q�[���I�[�o�[�ɂȂ������񂾂��������s��
        if (!isCalledOnce)
        {
            SoundManager.Play(SoundData.eSE.SE_GAMEOVER, SoundData.IndelibleAudioList);
            isCalledOnce = true;
            Pause.PauseStart();
        }
        
        //f4�L�[�������ꂽ��\���I��
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameOverHide();
        }

        //f5�������ꂽ�烊�g���C�i�X�e�B�b�N���ɓ|������j
        if (Input.GetKeyDown(KeyCode.F5))
        {
            select = SELECT.RETRY;
            //���g���C�摜�@���F
            imageShow_Retry.SetColor(1.0f,1.0f,0.0f);
            //�^�C�g���ɖ߂�摜�@��
            imageShow_BackTitle.SetColor(1.0f, 1.0f, 1.0f);
            Debug.Log("���g���C�I��");
        }
        //f6�������ꂽ��^�C�g���ɖ߂�i�X�e�B�b�N�E�ɓ|������j
        if (Input.GetKeyDown(KeyCode.F6))
        {
            select = SELECT.BACKTITLE;
            //�^�C�g���ɖ߂�摜�@���F
            imageShow_BackTitle.SetColor(1.0f, 1.0f, 0.0f);
            // ���g���C�摜�@��
            imageShow_Retry.SetColor(1.0f, 1.0f, 1.0f);
            Debug.Log("�^�C�g���ɖ߂�I��");
        }
        //f7�������ꂽ�猈��
        if (Input.GetKeyDown(KeyCode.F7))
        {
            switch (select)
            {
                case SELECT.RETRY:
                    //�Q�[���ɖ߂�
                    //�V�[���J��
                    GameData.Init();
                    Debug.Log("���g���C��������");
                    break;
                case SELECT.BACKTITLE:
                    //�^�C�g���ɖ߂�
                    //�V�[���J
                    GameData.NextMapNumber = (int)GameData.eSceneState.TITLE_SCENE;
                    SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);
                    Debug.Log("�^�C�g���ɖ߂��������");
                    break;
            }
            //�Q�[���I�[�o�[�\���I��
            GameOverHide();
        }

    }
    public void GameOverShow()
    {
        //�F������
        imageShow_Retry.SetColor(1.0f, 1.0f, 0.0f);
        imageShow_BackTitle.SetColor(1.0f, 1.0f, 1.0f);
        //select������
        select = SELECT.RETRY;
        //�摜�\��
        imageShow_GameOver.Show();
        imageShow_Retry.Show();
        imageShow_BackTitle.Show();
        //�J�����؂�ւ��@���C���J�������T�u�J�����@��Ԃ���
        //CameraSwitch.StartSwitching(mainCam, subCam, true);
        ////���C���J�����I�t
        //mainCam.SetActive(false);
        ////�T�u�J�����I��
        //subCam.SetActive(true);
        //�g�p�t���O���Ă�
        GameOverFlag = true;
    }
    public void GameOverHide()
    {
        //�摜����
        imageShow_GameOver.Hide();
        imageShow_Retry.Hide();
        imageShow_BackTitle.Hide();
        //�J�����؂�ւ� �T�u�J���������C���J�����@��ԂȂ�
        //CameraSwitch.StartSwitching(subCam, mainCam, false);
        ////�T�u�J�����I�t
        //subCam.SetActive(false);
        ////���C���J�����I��
        //mainCam.SetActive(true);
        //�g�p�t���O����
        GameOverFlag = false;

    }
}
