//=============================================================================
//
// BGM設定
//
// 作成日:2022/04/26
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/26    作成

//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionBGM : MonoBehaviour
{
    //スライダー
    private Slider BgmSlider;
    //選択用フラグ
    public bool selectFlag = true;
    //移動量
    public float moveSpeed = 0.005f;
    //現在のシーン保存用
    private int currentScene;
    // Start is called before the first frame update
    void Awake()
    {
        //コンポーネント取得
        BgmSlider = gameObject.GetComponent<Slider>();

        //サウンドマネージャーからbgmの音量を取得
        BgmSlider.value = SoundManager.bgmVolume;
    }

    private void Start()
    {
        //Awakeで実行するとゲームデータが更新される前に参照してしまうので、Startで参照する
        currentScene = GameData.CurrentMapNumber;
        Debug.Log("現在のシーン" + currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        //選択されてなかったらリターン
        if (!selectFlag)
        {
            return;
        }

        //右矢印キーで音量プラス
        if (Input.GetKey(KeyCode.RightArrow))
        {
            BgmSlider.value += moveSpeed;
            //音量設定
            SoundManager.bgmVolume = BgmSlider.value;
            if (currentScene == (int)GameData.eSceneState.TITLE_SCENE)
            {
                SoundManager.setVolume(SoundData.TitleAudioList);
            }
            else
            {
                SoundManager.setVolume(SoundData.GameAudioList);
            }
            
        }
        //左矢印キーで音量−
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            BgmSlider.value -= moveSpeed;
            //音量設定
            SoundManager.bgmVolume = BgmSlider.value;
            if (currentScene == (int)GameData.eSceneState.TITLE_SCENE)
            {
                SoundManager.setVolume(SoundData.TitleAudioList);
            }
            else
            {
                SoundManager.setVolume(SoundData.GameAudioList);
            }
        }


    }
    //設定画面閉じたときに音量保存
    private void OnDisable()
    {
        SaveManager.saveBGMVolume(SoundManager.bgmVolume);
    }
}
