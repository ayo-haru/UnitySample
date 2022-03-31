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

public class DataManager : MonoBehaviour
{
    /*
     * セーブデータの管理・音のデータの管理など素材のデータの管理をする(予定)
     * タイトルで呼び出しをする。ぜったい。
     * 
     * 
     * 
     */

    //-----------------------------------------------------
    // BGM・SE
    //-----------------------------------------------------
    public AudioClip bgm_title;
    public AudioClip bgm_kitchen;
    public AudioClip bgm_kitchenBoss;

    public AudioClip se_jump;
    public AudioClip se_land;
    public AudioClip se_shield;
    public AudioClip se_reflection;
    public AudioClip se_damege;

    public AudioClip se_boss1Dashu;
    public AudioClip se_boss1Strawberry;
    public AudioClip se_boss1Knife;
    public AudioClip se_boss1Damege;

    public AudioClip se_burokori;
    public AudioClip se_ninjin;
    public AudioClip se_tomatobomb;
    public AudioClip se_tomatobound;

    public AudioClip se_kettei;
    public AudioClip se_select;


    //-----------------------------------------------------
    // Efect
    //-----------------------------------------------------
    public ParticleSystem ef_fire;
    public ParticleSystem ef_damage;
    public ParticleSystem ef_dark;
    public ParticleSystem ef_enemydeath;
    public ParticleSystem ef_bossdeath;
    public ParticleSystem ef_healitem;
    public ParticleSystem ef_heal;
    public ParticleSystem ef_shield;
    public ParticleSystem ef_shield2;

    // Start is called before the first frame update
    void Awake()
    {
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
        SoundData.SEDataSet(se_damege, (int)SoundData.eSE.SE_DAMEGE);

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

        //-----------------------------------------------------
        // Efect
        //-----------------------------------------------------
        EffectData.EFDataSet(ef_fire,(int)EffectData.eEFFECT.EF_FIRE);
        EffectData.EFDataSet(ef_damage,(int)EffectData.eEFFECT.EF_DAMAGE);
        EffectData.EFDataSet(ef_dark,(int)EffectData.eEFFECT.EF_DARKAREA);
        EffectData.EFDataSet(ef_enemydeath,(int)EffectData.eEFFECT.EF_ENEMYDOWN);
        EffectData.EFDataSet(ef_bossdeath,(int)EffectData.eEFFECT.EF_BOSSKILL);
        EffectData.EFDataSet(ef_healitem,(int)EffectData.eEFFECT.EF_HEALITEM);
        EffectData.EFDataSet(ef_heal,(int)EffectData.eEFFECT.EF_HEAL);
        EffectData.EFDataSet(ef_shield,(int)EffectData.eEFFECT.EF_SHEILD2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
