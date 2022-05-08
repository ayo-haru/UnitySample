//=============================================================================
//
// BGM�ݒ�
//
// �쐬��:2022/04/26
// �쐬��:����T�q
//
// <�J������>
// 2022/04/26    �쐬

//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class OptionBGM : MonoBehaviour
{
    //�X���C�_�[
    private Slider BgmSlider;
    //�I��p�t���O
    public bool selectFlag = true;
    //�ړ���
    public float moveSpeed = 0.005f;
    // InputAction��UI������
    private Game_pad UIActionAssets;               
    // InputAction��select������
    private InputAction LeftStickSelect;            
    // InputAction��select������
    private InputAction RightStickSelect;           

    // Start is called before the first frame update
    void Awake()
    {
        // InputAction�C���X�^���X�𐶐�
        UIActionAssets = new Game_pad();

        //�R���|�[�l���g�擾
        BgmSlider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        //Awake���Ƃ܂��f�[�^�����[�h����ĂȂ�����start�Ŏ��s
        //�T�E���h�}�l�[�W���[����bgm�̉��ʂ��擾
        BgmSlider.value = SoundManager.bgmVolume;
    }

    // Update is called once per frame
    void Update()
    {
        //�I������ĂȂ������烊�^�[��
        if (!selectFlag)
        {
            return;
        }

        //�E���L�[�ŉ��ʃv���X
        if (Input.GetKey(KeyCode.RightArrow))
        {
            VolUp();
        }
        OnRightStick();
        //�����L�[�ŉ��ʁ|
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            VolDown();
        }
        OnLeftStick();


    }
    private void OnEnable()
    {
        //---�X�e�B�b�N�̒l����邽�߂̐ݒ�
        LeftStickSelect = UIActionAssets.UI.LeftStickSelect;
        RightStickSelect = UIActionAssets.UI.RightStickSelect;

        //---Action�C�x���g��o�^
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }

    //�ݒ��ʕ����Ƃ��ɉ��ʕۑ�
    private void OnDisable()
    {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();

        //SaveManager.saveBGMVolume(SoundManager.bgmVolume);
    }

    // �{�����[���グ��
    private void VolUp()
    {
        BgmSlider.value += moveSpeed;
        //���ʐݒ�
        SoundManager.bgmVolume = BgmSlider.value;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.setVolume(SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.setVolume(SoundData.GameAudioList);
        }

    }

    // �{�����[��������
    private void VolDown()
    {
        BgmSlider.value -= moveSpeed;
        //���ʐݒ�
        SoundManager.bgmVolume = BgmSlider.value;
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE)
        {
            SoundManager.setVolume(SoundData.TitleAudioList);
        }
        else
        {
            SoundManager.setVolume(SoundData.GameAudioList);
        }

    }

    private void OnLeftStick()
    {
        if (!selectFlag)
        {
            return;
        }

        //---���X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = LeftStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doLeftStick.x > 0.5f)
        {
            VolUp();
        }
        else if (doLeftStick.x < -0.5f)
        {
            VolDown();
        }
    }

    private void OnRightStick()
    {
        if (!selectFlag)
        {
            return;
        }

        //---�E�X�e�b�N�̃X�e�b�N���͂��擾
        Vector2 doRightStick = Vector2.zero;
        doRightStick = RightStickSelect.ReadValue<Vector2>();

        //---�����ł��|���ꂽ�珈���ɓ���
        if (doRightStick.x > 0.5f)
        {
            VolUp();
        }
        else if (doRightStick.x < -0.5f)
        {
            VolDown();
        }

    }

}
