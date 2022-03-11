//=============================================================================
//
// シーン遷移[MoveMap]
//
// 作成日:2022/03/09
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/09 作成
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
        //---シーンを切り替えても破壊しないものを入れる
        DontDestroyOnLoad(GameObject.Find("SD_unitychan_humanoid"));
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        // タグでオブジェクトを判断する。
        if (other.gameObject.tag == "MovePoint1to2")
        //if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("PlayerScene2");
        }

        else if (other.gameObject.tag == "MovePoint2to1")
        //if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("PlayerScene1");
        }

    }
}
