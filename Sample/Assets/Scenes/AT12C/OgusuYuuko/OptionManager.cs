//=============================================================================
//
// �ݒ���
//
// �쐬��:2022/04/26
// �쐬��:����T�q
//
// <�J������>
// 2022/04/26    �쐬
// 2022/04/27   SE�t����
// 2022/05/10    �p�b�h���S�Ή�
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionManager : MonoBehaviour
{
    //�I�����[�h
    private enum SELECT_MODE { BGM, SE,BACK,MAX_MODE };
    private int select;     //���݂̑I��
    private int old_select; //�O�t���[���̑I��
    //�I��p�g��RectTransform
    public GameObject selectFrame;
    private RectTransform rt_selectFrame;

    //BGM�X���C�_�[
    public GameObject bgmSlider;
    //SE�X���C�_�[
    public GameObject seSlider;
    //�߂�摜
    public GameObject BackImage;

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
        //���߂�bgm��I��
        select = (int)SELECT_MODE.BGM;
        //�R���|�[�l���g�擾
        rt_selectFrame = selectFrame.GetComponent<RectTransform>();
        //���ʒu�ݒ�
        Vector3 newPos = new Vector3(rt_selectFrame.position.x, bgmSlider.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
        rt_selectFrame.position = newPos;
        // �������ŏ��͌��肶��Ȃ�
        isDecision = false;
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

        //�O�t���[���̑I����ۑ�
        old_select = select;
        Debug.Log(select);

        //����
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectUp();
        }
        else if (isSetGamePad)
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
        }
        else if (isSetGamePad)
        {
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                SelectDown();
            }
        }


        //�߂邪�I�����ꂽ��ԂŃG���^�[�L�[�����ꂽ��I�v�V���������
        if (select == (int)SELECT_MODE.BACK)
        {
            if (isDecision)
            {
                //���艹
                if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.TitleAudioList);
                }
                else
                {
                    SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.GameAudioList);
                    if(GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE)
                    {
                        GameObject bgmObject = GameObject.Find("BGMObject(Clone)");
                        if (bgmObject)
                        {
                            bgmObject.GetComponent<AudioSource>().volume = SoundManager.bgmVolume;
                        }
                    }
                }
                //���ʕۑ�
                SaveManager.saveSEVolume(SoundManager.seVolume);
                SaveManager.saveBGMVolume(SoundManager.bgmVolume);
                gameObject.SetActive(false);
                isDecision = false;
            }
        }

        /*
         �A�b�v�f�[�g����SelectUp�ADown���Ă΂ꂽ�Ƃ��͉��̃��^�[���̓X���[���邪�A
         �X�e�B�b�N�őI�������ꍇ�͉��̂��ӂԂ�łЂ�������
         */

        //if (old_select == select)
        //{
        //    //�I�����ς���ĂȂ������烊�^�[��
        //    return;
        //}

        Vector3 newPos;
        switch (select)
        {
            case (int)SELECT_MODE.BGM:
                //se�I������
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //bgm�I��
                bgmSlider.GetComponent<OptionBGM>().selectFlag = true;
                //�I���t���[���ړ�
                newPos = new Vector3(rt_selectFrame.position.x, bgmSlider.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
                rt_selectFrame.position = newPos;
                break;
            case (int)SELECT_MODE.SE:
                //bgm�I������
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se�I��
                seSlider.GetComponent<OptionSE>().selectFlag = true;
                //�I���t���[���ړ�
                newPos = new Vector3(rt_selectFrame.position.x, seSlider.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
                rt_selectFrame.position = newPos;
                break;
            case (int)SELECT_MODE.BACK:
                //bgm�I������
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se�I������
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //�I���t���[���ړ�
                newPos = new Vector3(rt_selectFrame.position.x, BackImage.GetComponent<RectTransform>().position.y, rt_selectFrame.position.z);
                rt_selectFrame.position = newPos;
                break;
        }
    }

    private void OnEnable()
    {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Action�C�x���g��o�^
        UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }

    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    private void OnLeftStick(InputAction.CallbackContext obj)
    {
        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.y > 0.05f)
        {
            SelectUp();
        }
        else if (doLeftStick.y < -0.05f)
        {
            SelectDown();
        }
    }

    private void OnRightStick(InputAction.CallbackContext obj)
    {
        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.y > 0.05f)
        {
            //SelectBGM();
            SelectUp();
        }
        else if (doRightStick.y < -0.05f)
        {
            //SelectSE();
            SelectDown();
        }

    }
    private void OnDecision(InputAction.CallbackContext obj) {
        if (select != (int)SELECT_MODE.BACK)
        {
            return;
        }
        isDecision = true;
    }

    private void SelectUp()
    {
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        //���ړ��������̉��炷
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
        select--;
        if (select < 0)
        {
            select = 0;
        }
    }

    private void SelectDown()
    {
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        //���ړ��������̉��炷
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }

        select++;
        if (select >= (int)SELECT_MODE.MAX_MODE)
        {
            select = (int)SELECT_MODE.BACK;
        }
    }

}
