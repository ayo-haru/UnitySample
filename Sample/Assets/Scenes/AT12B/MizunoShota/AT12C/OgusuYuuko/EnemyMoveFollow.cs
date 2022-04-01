//=============================================================================
//
// 攻撃
//
// 作成日:2022/03
// 作成者:小楠裕子
// 編集者:伊地田真衣
//
// <開発履歴>
// 2022/03    作成
// 2022/03/12 モデルの向きに関係なく入力方向に盾が出るように変更
// 2022/03/13 GameDataを使用してplayerの位置を取得するように変更
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    //移動速度
    public float speed = 0.05f;
    // 移動許可フラグ
    private bool moveFlg = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!moveFlg)
        {
            if ((this.GetComponent<BaunceEnemy>().isBounce))
            {
                return;
            }
            return;
        }

        //右
        if (transform.position.x < GameData.PlayerPos.x)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }

        //左
        if (transform.position.x > GameData.PlayerPos.x)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }

    }
    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag == "Player")
        {
            moveFlg = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player")
        {
            moveFlg = false;
        }
    }
    //if ((this.GetComponent<BaunceEnemy>().isBounce))    // 跳ね返るときは追尾しない
    //{
    //    //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    //this.GetComponent<Rigidbody>().AddForce(-Direction);
    //    return;
    //}

    //// プレイヤーと自分の位置から進行方向を決める
    //this.Direction = GameData.PlayerPos - this.transform.position;
    //this.Direction.Normalize();
    //this.GetComponent<Rigidbody>().AddForce(Direction * speed);
}
