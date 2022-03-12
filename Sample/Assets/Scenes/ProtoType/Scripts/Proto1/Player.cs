//=============================================================================
//
// プレイヤーの管理する
//
// 作成日:2022/03/11
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/11 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = transform.position;
    }

    void OnTriggerEnter(Collider other) {
        if (SceneManager.GetActiveScene().name == "ProtoTypeScene1") { 

            if (other.gameObject.tag == "MovePoint1to2")
            {
                /*
                 * ProtoScene1Managerというゲームオブジェクトをそのシーン内から探す.
                 * そのオブジェクトに入っているコンポーネントを取得(この場合はスクリプト).
                 * そのスクリプト内のMoveScene1to2メソッドを呼び出す
                 */

                GameObject.Find("ProtoScene1Manager").
                    GetComponent<ProtoScene1Manager>().
                    MoveScene1to2();
            }
        }

        if (SceneManager.GetActiveScene().name == "ProtoTypeScene2")
        {
            if (other.gameObject.tag == "MovePoint2to1")
            {

                GameObject.Find("ProtoScene2Manager")
                    .GetComponent<ProtoScene2Manager>().
                    MoveScene2to1();
            }
        }
    }

}
