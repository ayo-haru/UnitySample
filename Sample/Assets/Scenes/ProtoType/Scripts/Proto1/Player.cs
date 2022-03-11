using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPos = GameData.Player.transform.position;
    }

    void OnTriggerEnter(Collider other) {
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

}
