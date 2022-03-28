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
    public AudioClip bgm_kitchen;
    public AudioClip bgm_kitchenBoss;

    public AudioClip se_jump;
    public AudioClip se_land;
    public AudioClip se_shield;
    public AudioClip se_reflection;

    public AudioClip se_boss1Dashu;
    public AudioClip se_boss1Strawberry;
    public AudioClip se_boss1Knife;

    public AudioClip se_kettei;
    public AudioClip se_select;

    // Start is called before the first frame update
    void Start()
    {
        SoundData.BGMDataSet(bgm_title, (int)SoundData.eBGM.BGM_TITLE);
        SoundData.BGMDataSet(bgm_kitchen, (int)SoundData.eBGM.BGM_KITCHEN);
        SoundData.BGMDataSet(bgm_kitchenBoss, (int)SoundData.eBGM.BGM_BOSS1);

        SoundData.SEDataSet(se_jump, (int)SoundData.eSE.SE_JUMP);
        SoundData.SEDataSet(se_land, (int)SoundData.eSE.SE_LAND);
        SoundData.SEDataSet(se_shield, (int)SoundData.eSE.SE_SHIELD);
        SoundData.SEDataSet(se_reflection, (int)SoundData.eSE.SE_REFLECTION);

        SoundData.SEDataSet(se_boss1Dashu, (int)SoundData.eSE.SE_BOOS1_DASHU);
        SoundData.SEDataSet(se_boss1Strawberry, (int)SoundData.eSE.SE_BOOS1_STRAWBERRY);
        SoundData.SEDataSet(se_boss1Knife, (int)SoundData.eSE.SE_BOOS1_KNIFE);

        SoundData.SEDataSet(se_kettei, (int)SoundData.eSE.SE_KETTEI);
        SoundData.SEDataSet(se_select, (int)SoundData.eSE.SE_SELECT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
