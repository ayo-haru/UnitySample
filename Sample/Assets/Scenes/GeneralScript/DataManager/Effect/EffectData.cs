//=============================================================================
//
// エフェクトデータ
//
// 作成日:2022/03/18
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/28 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EffectData
{
    //---エフェクト番号割り当て
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
        EF_MAGICSQUARE,

        MAX_EF
    }

    //---エフェクトデータの数だけまとめる
    public static ParticleSystem[] EF = new ParticleSystem[(int)eEFFECT.MAX_EF];

    public static bool isSetEffect = false;
    public static bool oncePauseEffect = true;
    public static GameObject[] activeEffect = new GameObject[(int)eEFFECT.MAX_EF * 5];

    //---エフェクトデータを読み込む
    public static void EFDataSet(ParticleSystem _EF,int i)
    {
        EF[i] = _EF;
    }

}
