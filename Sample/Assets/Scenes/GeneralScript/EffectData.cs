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
    //---�G�t�F�N�g�ԍ����蓖��
    public enum eEFFECT
    {
        EF_FIRE = 0,
        EF_DAMAGE,
        EF_DARKAREA,
        EF_ENEMYDOWN,
        EF_BOSSKILL,
        EF_HEALITEM,
        EF_HEAL,
        EF_SHIELD,
        EF_SHEILD2,

        MAX_EF
    }

    //---�G�t�F�N�g�f�[�^�̐������܂Ƃ߂�
    public static ParticleSystem[] EF = new ParticleSystem[(int)eEFFECT.MAX_EF];


    //---�G�t�F�N�g�f�[�^��ǂݍ���
    public static void EFDataSet(ParticleSystem _EF,int i)
    {
        EF[i] = _EF;
    }

}