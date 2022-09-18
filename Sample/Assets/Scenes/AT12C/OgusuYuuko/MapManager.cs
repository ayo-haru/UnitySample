//=============================================================================
//
// �L�b�`���}�b�v�}�l�[�W���[
//
// �쐬��:2022/04/22
// �쐬��:����T�q
//
// <�J������>
// 2022/04/22 �쐬
// 2022/04/24 ���@�w�ǉ�
// 2022/09/15 �R���g���[���Ή�
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //�v���C���[�A�C�R��
    public GameObject[] PlayerIcon;
    //MapGround
    public GameObject[] MapGround;
    //���@�w
    public GameObject[] MagicCircle;
    //���[�v�p�̖��
    public GameObject[] WarpArrow;
    //��񂾂��Ăяo���p�̃t���O
    private bool OnceFlag = true;
    //���[�v���܂���
    public GameObject WarpMenu;

    // InputAction��UI������
    private Game_pad UIActionAssets;
    // InputAction��select������
    private InputAction LeftStickSelect;
    // InputAction��select������
    private InputAction RightStickSelect;
    // ���肳�ꂽ�t���O
    private bool isDecision;
    //���݂̑I��
    private int nSelect;
    //�I���̑J�ڐ�
    private int[,,] MapTransition = { 
        { {6,0,0 },{1,0,0 },{0,0,0 },{2,3,0 } }, 
        { {-1 ,0,0},{0,0,0 },{0,0,0 },{3,2,0 } },
        { {5,0,0 },{1,0,0 },{-2,0,0 },{3 ,0,0} },
        { {-1,0,0 },{1,0,0 },{-3,-2,0 },{2,0,0 } },
        { {-1,0,0 },{0,0,0 },{-3,0,0 },{1,0,0 } },
        { {3,0,0 },{0,0,0 },{-3,-2,-1 },{0,0,0 } },
        { {0,0,0 },{-6,0,0 },{0,0,0 },{1,0,0 } },
        { {0,0,0 },{-5,0,0 },{-1,0,0 },{1,0,0 } },
        { {0,0,0 },{-3,0,0 },{-1,0,0 },{0,0,0 } },
    };

    void Awake()
    {
        // InputAction�C���X�^���X�𐶐�
        UIActionAssets = new Game_pad();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Action�C�x���g��o�^
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();

        //�v���C���[�A�C�R�����\��
        //�}�b�v�̔w�i�𔒂ɐݒ�
        for (int i = 0; i < PlayerIcon.Length; ++i)
        {
            PlayerIcon[i].SetActive(false);
            if (GameData.isWentMap[i + 1])
            {
                MapGround[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }else
            {
                MapGround[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            
            WarpArrow[i].SetActive(false);
        }
        //���߂̓��[�v���j���[��\��
        WarpMenu.SetActive(false);
        // �������ŏ��͌��肶��Ȃ�
        isDecision = false;
        OnceFlag = true;
        //���݂���}�b�v��I������
        nSelect = GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001;
    }

    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //���[�v���j���[���\������Ă��烊�^�[��
        if (WarpMenu.activeSelf)
        {
            return;
        }
        // �R���g���[���[������
        bool isSetGamePad = false;
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
            isSetGamePad = true;
        }

        //�R���g���[���[�Ń��[�v��I��
        //����
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectUp();
        }else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
            {
                SelectUp();
            }
        }

        //�����
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectDown();
        }else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                SelectDown();
            }
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
            //���[�v���j���[�\��
            WarpMenu.SetActive(true);
            WarpMenu.GetComponent<WarpMenuManager>().nWarpNumber = nSelect;
            isDecision = false;
            //Debug.Log(nSelect + "�Ɉړ�");
            ////�w�肳�ꂽ�V�[���ɑJ��
            //// �V�[���֘A
            //switch (nSelect)
            //{
            //    case 0:
            //        GameData.OldMapNumber = 0;
            //        break;
            //    case 1:
            //    case 3:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage001;
            //        break;
            //    case 2:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage006;
            //        break;
            //    case 4:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage004;
            //        break;
            //    case 5:
            //        GameData.OldMapNumber = (int)GameData.eSceneState.KitchenStage005;
            //        break;
            //}
            //GameData.NextMapNumber = (int)GameData.eSceneState.KitchenStage001 + nSelect;
        }



        //���@�w�\��
        for (int i = 0; i < MagicCircle.Length; ++i)
        {
            MagicCircle[i].GetComponent<ImageShow>().Show(180);
        }

      

       
        if (!OnceFlag)
        {
            return;
        }

        //�V�[�����ŕ���@CurrentMapNumber���������炻�����ɕς���\��
        //switch (SceneManager.GetActiveScene().name)
        //{
        //    case "KitchenStage001":
        //        PlayerIcon[0].SetActive(true);
        //        break;
        //    case "KitchenStage002":
        //        PlayerIcon[1].SetActive(true);
        //        break;
        //    case "KitchenStage003":
        //        PlayerIcon[2].SetActive(true);
        //        break;
        //    case "KitchenStage004":
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //    case "KitchenStage005":
        //        PlayerIcon[4].SetActive(true);
        //        break;
        //    case "KitchenStage006":
        //        PlayerIcon[5].SetActive(true);
        //        break;
        //    default:
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //}

        //CurrentMapNumber�����ɕ\������v���C���[�A�C�R���ݒ�
        //eSceneState���ύX����Ă��A�A�ԂȂ�[]�̒��ύX���Ȃ��Ă����悤�ɂ���
        PlayerIcon[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].SetActive(true);
        MapGround[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].GetComponent<Image>().color = new Color(0.1f, 1.0f, 1.0f, 1.0f);
        WarpArrow[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].SetActive(true);

        OnceFlag = false;
    }

   
    private void OnDecision(InputAction.CallbackContext obj)
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //���炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        isDecision = true;
    }

    private void SelectUp()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //���ړ��������̉��炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if(MapTransition[nSelect,0,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 0,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 0,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
        }
    }

    private void SelectDown()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //���ړ��������̉��炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (MapTransition[nSelect, 1,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 1,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 1,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
            
        }
    }

    private void SelectLeft()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //���ړ��������̉��炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (MapTransition[nSelect, 2,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 2,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 2,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
            
        }
        
    }


    private void SelectRight()
    {
        if (WarpMenu.activeSelf)
        {
            return;
        }
        //���ړ��������̉��炷
        SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        if (MapTransition[nSelect, 3,0] != 0)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (GameData.isWentMap[nSelect + MapTransition[nSelect, 3,i] + 1])
                {
                    WarpArrow[nSelect].SetActive(false);
                    nSelect += MapTransition[nSelect, 3,i];
                    WarpArrow[nSelect].SetActive(true);
                    break;
                }
            }
            
        }
    }

}

