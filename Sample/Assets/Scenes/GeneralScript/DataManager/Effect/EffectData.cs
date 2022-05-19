//=============================================================================
//
// �G�t�F�N�g�f�[�^
//
// �쐬��:2022/03/18
// �쐬��:�g����
//
// <�J������>
// 2022/03/28 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EffectData
{
    /// <summary>
    /// �G�t�F�N�g�̊��蓖��
    /// �ǉ�����ꍇ�͖����K���Ƃ���
    /// EF_�I�u�W�F�N�g��_���e�@�Ƃ��܂��B
    /// ��) EF_PLAYER_SHIELD
    /// </summary>
    public enum eEFFECT
    {
        //---�M�~�b�N�֘A
        EF_GIMICK_FIRE = 0,
        EF_GIMICK_HEALITEM,
        EF_GIMICK_MAGICCIRCLE_RED,
        EF_GIMICK_MAGICCIRCLE_BLUE,
        EF_GIMICK_GUIDE_LEFT,
        EF_GIMICK_GUIDE_LEFT_UP,
        EF_GIMICK_GUIDE_LEFT_DOWN,
        EF_GIMICK_GUIDE_RIGHT,
        EF_GIMICK_GUIDE_RIGHT_UP,
        EF_GIMICK_GUIDE_RIGHT_DOWN,

        //---�v���C���[�֘A
        EF_PLAYER_SHIELD,
        EF_PLAYER_HEAL,
        EF_PLAYER_DAMAGE,
        EF_PLAYER_DEATH,

        //---�G���I�֘A
        EF_ENEMY_DARKAREA,
        EF_ENEMY_DEATH,
        EF_ENEMY_TOMATOBOMB,

        //---�{�X�֘A
        EF_BOSS_DEATH,
        EF_BOSS_KNIFEDAMAGE,
        EF_BOSS_STRAWBERRY,
        EF_BOSS_FORK,
        EF_BOSS_RAINZONE,

        EF_SHEILD2,

        MAX_EF
    }

    //---�G�t�F�N�g�f�[�^�̐������܂Ƃ߂�
    public static ParticleSystem[] EF = new ParticleSystem[(int)eEFFECT.MAX_EF];

    public static bool isSetEffect = false;
    public static bool onceSearchEffect = true;
    public static GameObject[] activeEffect = new GameObject[300];
    //public static ParticleSystem[] activeEffect = new ParticleSystem[(int)eEFFECT.MAX_EF * 5];

    //---�G�t�F�N�g�f�[�^��ǂݍ���
    public static void EFDataSet(ParticleSystem _EF,int i)
    {
        EF[i] = _EF;
    }

}
