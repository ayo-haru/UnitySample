using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WarpMenuManager : MonoBehaviour
{
    public int nWarpNumber; //�}�b�v�}�l�[�W���Œl�ݒ肷��
    //�͂�
    public GameObject YesButton;
    //������
    public GameObject NoButton;
    //�I��p�t���[��
    public GameObject SelectFrame;
    //���݂̑I��
    private enum ESELECT { YES,NO};
    private ESELECT nSelect;
    // InputAction��UI������
    private Game_pad UIActionAssets;
    // InputAction��select������
    private InputAction LeftStickSelect;
    // InputAction��select������
    private InputAction RightStickSelect;
    // ���肳�ꂽ�t���O
    private bool isDecision;

    void Awake()
    {
        // InputAction�C���X�^���X�𐶐�
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Action�C�x���g��o�^
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();

        //�͂���I��
        nSelect = ESELECT.YES;
        //�t���[���̈ʒu�ݒ�
        SelectFrame.GetComponent<RectTransform>().transform.position = YesButton.GetComponent<RectTransform>().transform.position;
    }

    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[������
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }

        //�����
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectLeft();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                SelectLeft();
            }
        }

        //�E���
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectRight();
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                SelectRight();
            }
        }

        //����{�^�������ꂽ��
        if (isDecision)
        {
            isDecision = false;
            if(nSelect == ESELECT.YES)
            {
                Debug.Log(nWarpNumber + "�Ɉړ�");
                //�w�肳�ꂽ�V�[���ɑJ��

                //�L�b�`���X�e�[�W����AEX�X�e�[�W�@���́@EX�X�e�[�W����L�b�`���X�e�[�W�̈ړ��̏ꍇBGM���ς��̂�BGM�I�u�W�F�N�g���폜���Ă���
                if(GameData.CurrentMapNumber < (int)GameData.eSceneState.BossStage001 && nWarpNumber + 1 > (int)GameData.eSceneState.KitchenStage006)
                {
                    GameObject bgmObject = GameObject.Find("BGMObject(Clone)");
                    if (bgmObject)
                    {
                        Destroy(bgmObject);
                    }
                }

                if(GameData.CurrentMapNumber > (int)GameData.eSceneState.KitchenStage006 && nWarpNumber + 1 < (int)GameData.eSceneState.BossStage001)
                {
                    GameObject bgmObject = GameObject.Find("BGMObject_EX(Clone)");
                    if (bgmObject)
                    {
                        Destroy(bgmObject);
                    }

                }


                // �V�[���֘A
                switch (nWarpNumber)
                {
                    case 0:
                        GameData.CurrentMapNumber = -1; //�O�ɂ���Ə���������Ă��܂��̂�-1�ɂ��Ă�
                        break;
                    case 1:
                    case 3:
                    case 6:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage001;
                        break;
                    case 2:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage006;
                        break;
                    case 4:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage004;
                        break;
                    case 5:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage005;
                        break;
                    case 7:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.BossStage001;
                        break;
                    case 8:
                        GameData.CurrentMapNumber = (int)GameData.eSceneState.BossStage002;
                        break;
                }
                GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001 + nWarpNumber;
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }

    }

    private void OnDecision(InputAction.CallbackContext obj)
    {
        //���炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        isDecision = true;
    }

    private void SelectLeft()
    {
        //���ړ��������̉��炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if(nSelect == ESELECT.YES)
        {
            return;
        }

        //�͂���I��
        nSelect = ESELECT.YES;
        //�t���[���ړ�
        SelectFrame.GetComponent<RectTransform>().transform.position = YesButton.GetComponent<RectTransform>().transform.position;
    }


    private void SelectRight()
    {
        //���ړ��������̉��炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (nSelect == ESELECT.NO)
        {
            return;
        }

        //��������I��
        nSelect = ESELECT.NO;
        //�t���[���ړ�
        SelectFrame.GetComponent<RectTransform>().transform.position = NoButton.GetComponent<RectTransform>().transform.position;
    }
}
