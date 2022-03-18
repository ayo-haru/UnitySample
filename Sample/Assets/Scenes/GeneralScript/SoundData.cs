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
        SE_CLICK = 0,
        SE_DORA,
        SE_BYON,
        SE_SPON,

        MAX_SE
    }

    private static string path = "Assets/SoundData/";   // パスの途中まで。あと後ろに名前と拡張子つける。
    private static string[] SEpath = {path+ "クリック.mp3",path+ "ドラ.mp3", path + "ビヨォン.mp3", path + "すぽーん.mp3", }; // ここで完全なパスになる
    public static AudioClip[] BGMClip = new AudioClip[(int)eBGM.MAX_BGM];   // データをまとめて入れる
    public static AudioClip[] SEClip = new AudioClip[(int)eSE.MAX_SE];

    public static void SEDataSet() {    // SEのデータを読み込む
        for (int i = 0; i < (int)eSE.MAX_SE; i++)
        {
            //SEClip[i] = AssetDatabase.LoadAssetAtPath<AudioClip>(SEpath[i]);
        }
    }
}
