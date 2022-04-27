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

public class OptionManager : MonoBehaviour
{
    //�I�����[�h
    private enum SELECT_MODE {BGM,SE };
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

    //���݂̃V�[���ۑ��p
    private int currentScene;

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

        //���݂̃V�[���擾
        currentScene = GameData.CurrentMapNumber;
    }

    // Update is called once per frame
    void Update()
    {
        //�O�t���[���̑I����ۑ�
        old_select = select;
        //�����BGM�I��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            select = SELECT_MODE.BGM;
            if(currentScene == (int)GameData.eSceneState.TITLE_SCENE)
            {
                SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
            }
            else
            {
                SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
            }
            
        }
        //������SE�I��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            select = SELECT_MODE.SE;
            if (currentScene == (int)GameData.eSceneState.TITLE_SCENE)
            {
                SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
            }
            else
            {
                SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
            }
        }

        if(old_select == select)
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
}
