//=============================================================================
//
// データマネージャー
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
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    /*
     * セーブデータの管理・音のデータの管理など素材のデータの管理をする(予定)
     * タイトルで呼び出しをする。ぜったい。
     * 
     * 
     * 
     */

    static int TargetFrame = 60;

    //-----------------------------------------------------
    // BGM・SE
    //-----------------------------------------------------
    [SerializeField]
    private AudioClip bgm_title;
    [SerializeField]
    private AudioClip bgm_kitchen;
    [SerializeField]
    private AudioClip bgm_kitchenBoss;

    [SerializeField]
    private AudioClip se_jump;
    [SerializeField]
    private AudioClip se_land;
    [SerializeField]
    private AudioClip se_shield;
    [SerializeField]
    private AudioClip se_reflection;
    [SerializeField]
    private AudioClip se_reflectionstar;
    [SerializeField]
    private AudioClip se_damege;
    [SerializeField]
    private AudioClip se_heal;

    [SerializeField]
    private AudioClip se_boss1Dashu;
    [SerializeField]
    private AudioClip se_boss1Strawberry;
    [SerializeField]
    private AudioClip se_boss1Knife;
    [SerializeField]
    private AudioClip se_boss1Damege;

    [SerializeField]
    private AudioClip se_burokori;
    [SerializeField]
    private AudioClip se_ninjin;
    [SerializeField]
    private AudioClip se_tomatobomb;
    [SerializeField]
    private AudioClip se_tomatobound;

    [SerializeField]
    private AudioClip se_kettei;
    [SerializeField]
    private AudioClip se_select;
    [SerializeField]
    private AudioClip se_gameover;

    [SerializeField]
    private AudioClip se_gateopen;
    [SerializeField]
    private AudioClip se_extinguish;
    [SerializeField]
    private AudioClip se_switch;


    //-----------------------------------------------------
    // Efect
    //-----------------------------------------------------
    [SerializeField]
    private ParticleSystem ef_fire;
    [SerializeField]
    private ParticleSystem ef_damage;
    [SerializeField]
    private ParticleSystem ef_death;
    [SerializeField]
    private ParticleSystem ef_dark;
    [SerializeField]
    private ParticleSystem ef_enemydeath;
    [SerializeField]
    private ParticleSystem ef_tomatobomb;
    [SerializeField]
    private ParticleSystem ef_bossdeath;
    [SerializeField]
    private ParticleSystem ef_bossstrawberry;
    [SerializeField]
    private ParticleSystem ef_healitem;
    [SerializeField]
    private ParticleSystem ef_heal;
    [SerializeField]
    private ParticleSystem ef_shield;
    [SerializeField]
    private ParticleSystem ef_shield2;
    [SerializeField]
    private ParticleSystem ef_magicspuare_red;
    [SerializeField]
    private ParticleSystem ef_magicspuare_blue;


    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = TargetFrame;
        //-----------------------------------------------------
        // BGM・SE
        //-----------------------------------------------------
        SoundData.BGMDataSet(bgm_title, (int)SoundData.eBGM.BGM_TITLE);
        SoundData.BGMDataSet(bgm_kitchen, (int)SoundData.eBGM.BGM_KITCHEN);
        SoundData.BGMDataSet(bgm_kitchenBoss, (int)SoundData.eBGM.BGM_BOSS1);

        SoundData.SEDataSet(se_jump, (int)SoundData.eSE.SE_JUMP);
        SoundData.SEDataSet(se_land, (int)SoundData.eSE.SE_LAND);
        SoundData.SEDataSet(se_shield, (int)SoundData.eSE.SE_SHIELD);
        SoundData.SEDataSet(se_reflection, (int)SoundData.eSE.SE_REFLECTION);
        SoundData.SEDataSet(se_reflectionstar, (int)SoundData.eSE.SE_REFLECTION_STAR);
        SoundData.SEDataSet(se_damege, (int)SoundData.eSE.SE_DAMEGE);
        SoundData.SEDataSet(se_heal, (int)SoundData.eSE.SE_HEAL);

        SoundData.SEDataSet(se_boss1Dashu, (int)SoundData.eSE.SE_BOOS1_DASHU);
        SoundData.SEDataSet(se_boss1Strawberry, (int)SoundData.eSE.SE_BOOS1_STRAWBERRY);
        SoundData.SEDataSet(se_boss1Knife, (int)SoundData.eSE.SE_BOOS1_KNIFE);
        SoundData.SEDataSet(se_boss1Damege, (int)SoundData.eSE.SE_BOOS1_DAMEGE);

        SoundData.SEDataSet(se_burokori, (int)SoundData.eSE.SE_BUROKORI);
        SoundData.SEDataSet(se_ninjin, (int)SoundData.eSE.SE_NINJIN);
        SoundData.SEDataSet(se_tomatobomb, (int)SoundData.eSE.SE_TOMATO_BOMB);
        SoundData.SEDataSet(se_tomatobound, (int)SoundData.eSE.SE_TOMATO_BOUND);

        SoundData.SEDataSet(se_kettei, (int)SoundData.eSE.SE_KETTEI);
        SoundData.SEDataSet(se_select, (int)SoundData.eSE.SE_SELECT);
        SoundData.SEDataSet(se_gameover, (int)SoundData.eSE.SE_GAMEOVER);

        SoundData.SEDataSet(se_gateopen, (int)SoundData.eSE.SE_GATEOPEN);
        SoundData.SEDataSet(se_extinguish, (int)SoundData.eSE.SE_EXTINGUISH);
        SoundData.SEDataSet(se_switch, (int)SoundData.eSE.SE_SWITCH);

        //-----------------------------------------------------
        // Efect
        //-----------------------------------------------------
        EffectData.EFDataSet(ef_fire,(int)EffectData.eEFFECT.EF_FIRE);
        EffectData.EFDataSet(ef_damage,(int)EffectData.eEFFECT.EF_DAMAGE);
        EffectData.EFDataSet(ef_death,(int)EffectData.eEFFECT.EF_DEATH);
        EffectData.EFDataSet(ef_dark,(int)EffectData.eEFFECT.EF_DARKAREA);
        EffectData.EFDataSet(ef_enemydeath,(int)EffectData.eEFFECT.EF_ENEMYDOWN);
        EffectData.EFDataSet(ef_tomatobomb,(int)EffectData.eEFFECT.EF_TOMATOBOMB);
        EffectData.EFDataSet(ef_bossdeath,(int)EffectData.eEFFECT.EF_BOSSKILL);
        EffectData.EFDataSet(ef_bossdeath,(int)EffectData.eEFFECT.EF_BOSSSTRAWBERRY);
        EffectData.EFDataSet(ef_healitem,(int)EffectData.eEFFECT.EF_HEALITEM);
        EffectData.EFDataSet(ef_heal,(int)EffectData.eEFFECT.EF_HEAL);
        EffectData.EFDataSet(ef_shield,(int)EffectData.eEFFECT.EF_SHIELD);
        EffectData.EFDataSet(ef_shield2,(int)EffectData.eEFFECT.EF_SHEILD2);
        EffectData.EFDataSet(ef_magicspuare_red,(int)EffectData.eEFFECT.EF_MAGICSQUARE_RED);
        EffectData.EFDataSet(ef_magicspuare_blue,(int)EffectData.eEFFECT.EF_MAGICSQUARE_BLUE);

        SoundData.isSetSound = true;    // デバッグ時サウンド初期化してない場合にエラーが出るからけす
        EffectData.isSetEffect = true;  // デバッグ時エフェクト初期化していない場合にエラーを出さない

        // 絶対に消されない音のいれる場所
        for (int i = 0; i < 3; i++) {
            SoundData.IndelibleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }

        // ゲームパッド初期化
        if (Gamepad.current != null)
        {
            GameData.gamepad = Gamepad.current;
        }
    }
}