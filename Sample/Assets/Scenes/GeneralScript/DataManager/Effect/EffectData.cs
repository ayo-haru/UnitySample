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
    /// <summary>
    /// エフェクトの割り当て
    /// 追加する場合は命名規則として
    /// EF_オブジェクト名_内容　とします。
    /// 例) EF_PLAYER_SHIELD
    /// </summary>
    public enum eEFFECT
    {
        //---ギミック関連
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

        //---プレイヤー関連
        EF_PLAYER_SHIELD,
        EF_PLAYER_HEAL,
        EF_PLAYER_DAMAGE,
        EF_PLAYER_DEATH,

        //---雑魚的関連
        EF_ENEMY_DARKAREA,
        EF_ENEMY_DEATH,
        EF_ENEMY_TOMATOBOMB,

        //---ボス関連
        EF_BOSS_DEATH,
        EF_BOSS_KNIFEDAMAGE,
        EF_BOSS_STRAWBERRY,
        EF_BOSS_FORK,
        EF_BOSS_RAINZONE,

        EF_SHEILD2,

        MAX_EF
    }

    //---エフェクトデータの数だけまとめる
    public static ParticleSystem[] EF = new ParticleSystem[(int)eEFFECT.MAX_EF];

    public static bool isSetEffect = false;
    public static bool onceSearchEffect = true;
    public static GameObject[] activeEffect = new GameObject[300];
    //public static ParticleSystem[] activeEffect = new ParticleSystem[(int)eEFFECT.MAX_EF * 5];

    //---エフェクトデータを読み込む
    public static void EFDataSet(ParticleSystem _EF,int i)
    {
        EF[i] = _EF;
    }

}
