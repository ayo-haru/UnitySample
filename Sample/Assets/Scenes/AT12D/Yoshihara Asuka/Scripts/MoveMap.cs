//=============================================================================
//
// マップ遷移[MoveMap]
//
// 作成日:2022/03/09
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/09 作成
// 2022/03/11 破壊しないオブジェクトを追加
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //---遷移後も破壊しないオブジェクト
        DontDestroyOnLoad(GameObject.Find("SD_unitychan_humanoid"));
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovePoint1to2")
        {
            SceneManager.LoadScene("PlayerScene2");
        }
    }
}
