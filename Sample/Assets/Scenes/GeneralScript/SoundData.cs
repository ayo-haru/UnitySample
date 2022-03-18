//=============================================================================
//
// �T�E���h�f�[�^
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
using UnityEditor;

public static class SoundData
{
    public enum eBGM {      // BGM�ԍ�
        BGM_TITLE = 0,
        BGM_GAME,

        MAX_BGM
    }

    public enum eSE {       // SE�ԍ�
        SE_CLICK = 0,
        SE_DORA,
        SE_BYON,
        SE_SPON,

        MAX_SE
    }

    private static string path = "Assets/SoundData/";   // �p�X�̓r���܂ŁB���ƌ��ɖ��O�Ɗg���q����B
    private static string[] SEpath = {path+ "�N���b�N.mp3",path+ "�h��.mp3", path + "�r���H��.mp3", path + "���ہ[��.mp3", }; // �����Ŋ��S�ȃp�X�ɂȂ�
    public static AudioClip[] BGMClip = new AudioClip[(int)eBGM.MAX_BGM];   // �f�[�^���܂Ƃ߂ē����
    public static AudioClip[] SEClip = new AudioClip[(int)eSE.MAX_SE];

    public static void SEDataSet() {    // SE�̃f�[�^��ǂݍ���
        for (int i = 0; i < (int)eSE.MAX_SE; i++)
        {
            //SEClip[i] = AssetDatabase.LoadAssetAtPath<AudioClip>(SEpath[i]);
        }
    }
}
