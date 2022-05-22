//=============================================================================
//
// キッチンマップマネージャー
//
// 作成日:2022/04/22
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/22 作成
// 2022/04/24 魔法陣追加
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    //プレイヤーアイコン
    public GameObject[] PlayerIcon;
    //MapGround
    public GameObject[] MapGround;
    //魔法陣
    public GameObject[] MagicCircle;
    //一回だけ呼び出す用のフラグ
    private bool OnceFlag = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        //プレイヤーアイコンを非表示
        //マップの背景を白に設定
        for (int i = 0; i < PlayerIcon.Length; ++i)
        {
            PlayerIcon[i].SetActive(false);
            MapGround[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        OnceFlag = true;
    }
    private void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //魔法陣表示
        for(int i = 0; i < MagicCircle.Length; ++i)
        {
            MagicCircle[i].GetComponent<ImageShow>().Show(180);
        }
       
        if (!OnceFlag)
        {
            return;
        }

        //シーン名で分岐　CurrentMapNumber分かったらそっちに変える予定
        //switch (SceneManager.GetActiveScene().name)
        //{
        //    case "KitchenStage001":
        //        PlayerIcon[0].SetActive(true);
        //        break;
        //    case "KitchenStage002":
        //        PlayerIcon[1].SetActive(true);
        //        break;
        //    case "KitchenStage003":
        //        PlayerIcon[2].SetActive(true);
        //        break;
        //    case "KitchenStage004":
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //    case "KitchenStage005":
        //        PlayerIcon[4].SetActive(true);
        //        break;
        //    case "KitchenStage006":
        //        PlayerIcon[5].SetActive(true);
        //        break;
        //    default:
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //}

        //CurrentMapNumberを元に表示するプレイヤーアイコン設定
        //eSceneStateが変更されても、連番なら[]の中変更しなくていいようにした
        PlayerIcon[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].SetActive(true);
        MapGround[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].GetComponent<Image>().color = new Color(0.1f, 1.0f, 1.0f, 1.0f);


        OnceFlag = false;
    }
}


