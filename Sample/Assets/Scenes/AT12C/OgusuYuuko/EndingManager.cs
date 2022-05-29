//==========================================================
//      �G���f�B���O
//      �쐬���@2022/05/29
//      �쐬�ҁ@����T�q
//
//      ���̃X�N���v�g���������v���n�u(EndingManager)�����X�{�X�V�[����Canvas2�ɓ����
//      �K���v���C���[��HP�̎�O�ɒu��
//      
//      <�J������>
//      2022/05/29  �쐬      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public bool startFlag;              //�G���f�B���O�J�n�t���O
    private GameObject BackGroundImage;  //�w�i
    private GameObject TitleLogoImage;   //�^�C�g�����S
    private GameObject FinImage;          //Fin�̕���
    private GameObject BackTitleImage;   //�^�C�g���ɖ߂�
    private GameObject AButtonImage;     //A�{�^��
    private GameObject FadeImage;        //�t�F�[�h�p�摜

    public int StopTime = 180;                //��~����

    private int step;

    // InputAction��UI������
    private Game_pad UIActionAssets;
    private bool isDecision;

    void Awake()
    {
        // InputAction�C���X�^���X�𐶐�
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void Start()
    {
        startFlag = false;
        step = 0;
        isDecision = false;

        BackGroundImage = GameObject.Find("EndingBGImage");
        TitleLogoImage = GameObject.Find("TitleLogo");
        FinImage = GameObject.Find("FinImage");
        AButtonImage = GameObject.Find("AbuttonImage");
        BackTitleImage = GameObject.Find("BackTitleImage");
        FadeImage = GameObject.Find("FadeImage");

        //�Ńo�b�N�p
        if(GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            GameData.CurrentMapNumber = (int)GameData.eSceneState.BOSS1_SCENE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���X�{�X�|���ꂽ�t���O�����Ă���X�^�[�g����
        //if (GameData.isAlivelastBoss)
        //{
        //    startFlag = true;
        //}
        //�t���O�����ĂȂ������牽�����Ȃ�
        if (!startFlag)
        {
            return;
        }

        switch (step)
        {
            case 0:
                //���o���n�܂�����w�i�ƃ^�C�g�����S��\��
                BackGroundImage.GetComponent<ImageShow>().Show();
                TitleLogoImage.GetComponent<ImageShow>().Show();
                ++step;
                break;
            case 1:
                //�w�i�ƃ^�C�g�����S�����S�ɕ\�����ꂽ��fin��\��
                if (BackGroundImage.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE && TitleLogoImage.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
                {
                    FinImage.GetComponent<ImageShow>().Show();
                    ++step;
                }
                break;
            case 2:
                //fin���\�����ꂽ��StopTime�ҋ@
                --StopTime;
                if(StopTime <= 0)
                {
                    ++step;
                }
                break;
            case 3:
                //�ҋ@�I����^�C�g���ɖ߂��A�{�^���摜��\��
                BackTitleImage.GetComponent<ImageShow>().Show();
                AButtonImage.GetComponent<ImageShow>().Show();
                ++step;
                break;
            case 4:
                //�^�C�g���ɖ߂邪�\������Ă���A�{�^���������ꂽ��^�C�g���ɖ߂�
                if (isDecision)
                {
                    isDecision = false;
                    // ���艹
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.GameAudioList);
                    ++step;
                }
                break;
            case 5:
                //�t�F�[�h�\��
                FadeImage.GetComponent<ImageShow>().Show();
                ++step;
                break;
            case 6:
                //���S�Ƀt�F�[�h�A�E�g������V�[���؂�ւ�
                if(FadeImage.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
                {
                    // �V�[���֘A
                    GameData.OldMapNumber = GameData.CurrentMapNumber;
                    string nextSceneName = GameData.GetNextScene((int)GameData.eSceneState.TITLE_SCENE);
                    SceneManager.LoadScene(nextSceneName);
                    ++step;
                }
                break;
        }
    }

    private void OnEnable()
    {
        //---Action�C�x���g��o�^

        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    /// <summary>
    /// ����{�^��
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if(step != 4)
        {
            return;
        }
        isDecision = true;
    }

}
