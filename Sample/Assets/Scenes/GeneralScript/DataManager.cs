//=============================================================================
//
// �f�[�^�}�l�[�W���[
//
// �쐬��:2022/03/16
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/16 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /*
     * �Z�[�u�f�[�^�̊Ǘ��E���̃f�[�^�̊Ǘ��ȂǑf�ނ̃f�[�^�̊Ǘ�������(�\��)
     * �^�C�g���ŌĂяo��������B���������B
     * 
     * 
     * 
     */
    public AudioClip bgm_title;
    //[SerializeField]
    //AudioClip bgm_game;
    public AudioClip se_click;


    // Start is called before the first frame update
    void Start()
    {
        SoundData.BGMDataSet(bgm_title, (int)SoundData.eBGM.BGM_TITLE);

        SoundData.SEDataSet(se_click, (int)SoundData.eSE.SE_CLICK);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
