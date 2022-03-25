//=============================================================================
//
// サウンドデータ
//
// 作成日:2022/03/16
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/16 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class SoundData
{
    public enum eBGM {      // BGM番号
        BGM_TITLE = 0,
        BGM_GAME,

        MAX_BGM
    }

    public enum eSE {       // SE番号
        SE_JUMP = 0,
        SE_LAND,
        SE_SHIELD,
        SE_REFLECTION,

        MAX_SE
    }

    private static string path = "Assets/SoundData/";   // パスの途中まで。あと後ろに名前と拡張子つける。
    private static string[] SEpath = {path+ "クリック.mp3",path+ "ドラ.mp3", path + "ビヨォン.mp3", path + "すぽーん.mp3", }; // ここで完全なパスになる
    public static AudioClip[] BGMClip = new AudioClip[(int)eBGM.MAX_BGM];   // データをまとめて入れる
    public static AudioClip[] SEClip = new AudioClip[(int)eSE.MAX_SE];

    public static AudioSource[] TitleAudioList = new AudioSource[5];    // 一回に同時にならせる数
    public static AudioSource[] GameAudioList = new AudioSource[10];    // 一回に同時にならせる数



    public static void SEDataSet(AudioClip _SE, int i)    // SEのデータを読み込む
    {
        SEClip[i] = _SE;
    }
    public static void BGMDataSet(AudioClip _BGM, int i)    // SEのデータを読み込む
    {
        BGMClip[i] = _BGM;
    }

}
