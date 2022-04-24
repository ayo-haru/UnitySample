//=============================================================================
//
// マップマネージャー
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
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //プレイヤーアイコン
    public GameObject[] PlayerIcon;
    //魔法陣
    public GameObject MagicCircle;
    //一回だけ呼び出す用のフラグ
    private bool OnceFlag = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        //プレイヤーアイコンを非表示
        for (int i = 0; i < PlayerIcon.Length; ++i)
        {
            PlayerIcon[i].GetComponent<ImageShow>().Hide();
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
        MagicCircle.GetComponent<ImageShow>().Show(1000);
        if (!OnceFlag)
        {
            return;
        }

        //プレイヤーがいるシーンによって表示するプレイヤーアイコンを変える
        //switch (GameData.CurrentMapNumber)
        //{
        //    case (int)GameData.eSceneState.BOSS1_SCENE:
        //        PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        break;
        //    case (int)GameData.eSceneState.KitchenStage001:
        //        PlayerIcon[(int)MapNo.No_1to7].GetComponent<ImageShow>().Show();
        //        break;
        //    case (int)GameData.eSceneState.KitchenStage002:
        //        PlayerIcon[(int)MapNo.No_8to10].GetComponent<ImageShow>().Show();
        //        break;
        //    default:
        //        PlayerIcon[(int)MapNo.No_17].GetComponent<ImageShow>().Show();
        //        break;
        //        //case (int)GameData.eSceneState.BOSS1_SCENE:
        //        //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        //    break;
        //        //case (int)GameData.eSceneState.BOSS1_SCENE:
        //        //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        //    break;
        //        //case (int)GameData.eSceneState.BOSS1_SCENE:
        //        //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        //    break;
        //}

        //シーン名で分岐　CurrentMapNumber分かったらそっちに変える予定
        switch (SceneManager.GetActiveScene().name)
        {
            case "KitchenStage001":
                PlayerIcon[0].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage002":
                PlayerIcon[1].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage003":
                PlayerIcon[2].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage004":
                PlayerIcon[3].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage005":
                PlayerIcon[4].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage006":
                PlayerIcon[5].GetComponent<ImageShow>().Show();
                break;
        }
        OnceFlag = false;
    }
}


