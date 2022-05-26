//=============================================================================
//
// 開始演出
//
// 作成日:2022/05/26
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/26 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStagePllayer : MonoBehaviour
{
    public float width = 10.0f;                                     //振れ幅
    public Vector3 startPos = new Vector3(-250.0f, 50.0f, 0.0f);    //初期位置
    public float finishPosX = -200.0f;                              //終了位置
    private Vector2 moveSpeed = new Vector3(0.1f, 1.0f);            //移動速度
    private Vector3 playerPos;
    private float theta;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー２からアニメーター取得
        playerAnimator = gameObject.GetComponent<Player2>().animator;

        //重力とか入力を無視したいのでplayer2とrigidbody無効
        gameObject.GetComponent<Player2>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        //初期位置設定
        gameObject.transform.position = startPos;
        playerPos = startPos;

        theta = 0.0f;

        //右向かせる
        gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
        //アニメーション再生
        if (!playerAnimator.GetBool("Walk"))
        {
            playerAnimator.SetBool("Walk", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //x座標更新
        playerPos.x += moveSpeed.x;
        //y座標更新
        theta += moveSpeed.y;
        if(theta >= 180.0f)
        {
            theta -= 360.0f;
        }
        float SinY = Mathf.Sin(Mathf.Deg2Rad * theta) * width;
        playerPos.y = startPos.y + SinY;

        gameObject.transform.position = playerPos;

        if(gameObject.transform.position.x > finishPosX)
        {
            //アニメーション再生 シャボン玉割る
            playerAnimator.SetTrigger("Attack_UP");
            //演出が終わったので、player2とrigidbodyを元に戻す
            gameObject.GetComponent<Player2>().enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            //コンポーネント消す
            Destroy(this);
        }

    }
}
