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
    private enum SELECT_MODE { BGM, SE };
    private SELECT_MODE select;     //���݂̑I��
    private SELECT_MODE old_select; //�O�t���[���̑I��
    //�I��p����RectTransform
    public GameObject selectArrow;
    private RectTransform rt_selectArrow;
    //�I��p���ʒu
    public Vector3 posBGM;
    public Vector3 posSE;

    //BGM�X���C�_�[
    public GameObject bgmSlider;
    //SE�X���C�_�[
    public GameObject seSlider;

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
        select = SELECT_MODE.BGM;
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
        //�����BGM�I��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectBGM();
        }
        //������SE�I��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectSE();
        }
        OnLeftStick();
        OnRightStick();

        if (old_select == select)
        {
            //�I�����ς���ĂȂ������烊�^�[��
            return;
        }

        Vector3 newPos;
        switch (select)
        {
            case SELECT_MODE.BGM:
                //se�I������
                seSlider.GetComponent<OptionSE>().selectFlag = false;
                //bgm�I��
                bgmSlider.GetComponent<OptionBGM>().selectFlag = true;
                //���ړ�
                newPos = new Vector3(rt_selectArrow.transform.position.x, bgmSlider.transform.position.y, rt_selectArrow.transform.position.z);
                rt_selectArrow.transform.position = newPos;
                break;
            case SELECT_MODE.SE:
                //bgm�I������
                bgmSlider.GetComponent<OptionBGM>().selectFlag = false;
                //se�I��
                seSlider.GetComponent<OptionSE>().selectFlag = true;
                //���ړ�
                newPos = new Vector3(rt_selectArrow.transform.position.x, seSlider.transform.position.y, rt_selectArrow.transform.position.z);
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
        select = SELECT_MODE.BGM;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
    }

    private void SelectSE()
    {
        select = SELECT_MODE.SE;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
    }

    private void OnLeftStick()
    {
        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.y > 0.7f)
        {
            SelectBGM();
        }
        else if (doLeftStick.y < -0.7f)
        {
            SelectSE();
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
            SelectBGM();
        }
        else if (doRightStick.y < -0.7f)
        {
            SelectSE();
        }

    }

}
