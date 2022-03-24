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
    public AudioClip bgm_title;
    //[SerializeField]
    //AudioClip bgm_game;
    public AudioClip se_jump;
    public AudioClip se_land;
    public AudioClip se_shield;
    public AudioClip se_reflection;


    // Start is called before the first frame update
    void Start()
    {
        SoundData.BGMDataSet(bgm_title, (int)SoundData.eBGM.BGM_TITLE);

        SoundData.SEDataSet(se_jump, (int)SoundData.eSE.SE_JUMP);
        SoundData.SEDataSet(se_land, (int)SoundData.eSE.SE_LAND);
        SoundData.SEDataSet(se_shield, (int)SoundData.eSE.SE_SHIELD);
        SoundData.SEDataSet(se_reflection, (int)SoundData.eSE.SE_REFLECTION);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
