//=============================================================================
//
// マップマネージャー
//
// 作成日:2022/04/22
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/22 作成
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    enum MapNo {No_1to7,No_8to10,No_4,No_11to12and15to16,No_13to14,No_17 };
    //プレイヤーアイコン
    public GameObject[] PlayerIcon;
    //一回だけ呼び出す用のフラグ
    private bool OnceFlag = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("OnEnable");
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
    void Update()
    {
        if (!OnceFlag)
        {
            return;
        }
        //プレイヤーがいるシーンによって表示するプレイヤーアイコンを変える
        switch (GameData.CurrentMapNumber)
        {
            case (int)GameData.eSceneState.BOSS1_SCENE:
                PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
                break;
            case (int)GameData.eSceneState.KitchenStage001:
                PlayerIcon[(int)MapNo.No_1to7].GetComponent<ImageShow>().Show();
                break;
            case (int)GameData.eSceneState.KitchenStage002:
                PlayerIcon[(int)MapNo.No_8to10].GetComponent<ImageShow>().Show();
                break;
            default:
                PlayerIcon[(int)MapNo.No_17].GetComponent<ImageShow>().Show();
                break;
                //case (int)GameData.eSceneState.BOSS1_SCENE:
                //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
                //    break;
                //case (int)GameData.eSceneState.BOSS1_SCENE:
                //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
                //    break;
                //case (int)GameData.eSceneState.BOSS1_SCENE:
                //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
                //    break;
        }
        OnceFlag = false;
    }
}
