//=============================================================================
//
// カメラコントロール
//
// 作成日:2022/04/17
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/04/17   作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //---変数宣言
    public GameObject Player;           // 追従するオブジェクト変数
    private Vector3 Offset;            // 追従するオブジェクトとカメラの位置関係を保存

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時にプレイヤーとカメラの位置を保存
        Offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //　プレイヤーの現在の位置から新しいカメラの位置を作成
        Vector3 vector = Player.transform.position + Offset;

        // 縦方向は固定
        vector.y = transform.position.y;

        // カメラの位置を移動
        transform.position = vector;
    }
}
