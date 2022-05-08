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
    //�I��p����RectTransform
    public GameObject selectArrow;
    private RectTransform rt_selectArrow;

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
        rt_selectArrow = selectArrow.GetComponent<RectTransform>();
        //���ʒu�ݒ�
        Vector3 newPos = new Vector3(rt_selectArrow.transform.position.x, bgmSlider.transform.position.y, rt_selectArrow.transform.position.z);
        rt_selectArrow.transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        //�O�t���[���̑I����ۑ�
        old_select = select;

        //����
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //SelectBGM();
            SelectUp();
        }
        //�����
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //SelectSE();
            SelectDown();
        }
        OnLeftStick();
        OnRightStick();

        //�߂邪�I�����ꂽ��ԂŃG���^�[�L�[�����ꂽ��I�v�V���������
        if(select == (int)SELECT_MODE.BACK)
        {
            if (Input.GetKeyDown(KeyCode.Return))
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
            }
        }

        if (old_select == select)
        {
            //�I�����ς���ĂȂ������烊�^�[��
            return;
        }

        Vector3 newPos;
        switch (select)
        {
            case (int)SELECT_MODE.BGM:
                //se�I������
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //bgm�I��
                bgmSlider.GetComponent<OptionBGM>().selectFlag = true;
                //���ړ�
                newPos = new Vector3(rt_selectArrow.transform.position.x, bgmSlider.transform.position.y, rt_selectArrow.transform.position.z);
                rt_selectArrow.transform.position = newPos;
                break;
            case (int)SELECT_MODE.SE:
                //bgm�I������
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se�I��
                seSlider.GetComponent<OptionSE>().selectFlag = true;
                //���ړ�
                newPos = new Vector3(rt_selectArrow.transform.position.x, seSlider.transform.position.y, rt_selectArrow.transform.position.z);
                rt_selectArrow.transform.position = newPos;
                break;
            case (int)SELECT_MODE.BACK:
                //bgm�I������
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se�I������
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //���ړ�
                newPos = new Vector3(rt_selectArrow.transform.position.x, BackImage.transform.position.y, rt_selectArrow.transform.position.z);
                rt_selectArrow.transform.position = newPos;
                break;
        }
    }

    private void OnEnable()
    {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        ////---Action�C�x���g��o�^
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }

    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }

    private void SelectBGM()
    {
        //select = SELECT_MODE.BGM;
        //if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        //{
        //    SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        //}
        //else
        //{
        //    SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        //}
    }

    private void SelectSE()
    {
        //select = SELECT_MODE.SE;
        //if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        //{
        //    SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        //}
        //else
        //{
        //    SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        //}
    }

    private void OnLeftStick()
    {
        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.y > 0.7f)
        {
            //SelectBGM();
            SelectUp();
        }
        else if (doLeftStick.y < -0.7f)
        {
            //SelectSE();
            SelectDown();
        }
    }

    private void OnRightStick()
    {
        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.y > 0.7f)
        {
            //SelectBGM();
            SelectUp();
        }
        else if (doRightStick.y < -0.7f)
        {
            //SelectSE();
            SelectDown();
        }

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
