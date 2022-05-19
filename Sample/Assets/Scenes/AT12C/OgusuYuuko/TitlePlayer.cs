//=============================================================================
//
// タイトル用Player
//
// 作成日:2022/05/18
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/18   作成 
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public Animator animator;                           // アニメーターコンポーネント取得
    private TitleSceneManager titleSceneManager;        //タイトルシーンマネージャ
    public bool pressAnyButtonFlag;                     //PressAnyButtonが押されたか
    private bool selectFlag;                            //選択画面かどうか
    public GameObject[] selectPosObject;                //選択肢の位置
    public GameObject[] selectUI;                       //選択肢のUI
    public float moveSpeed = 0.1f;                      //プレイヤー移動速度
    private bool rightFlag;                             //右向いてるか
    private Vector3 scale;                              //プレイヤー回転時のスケール変更用
    public int max_timer = 60;                          //演出待ち時間
    public int first_timer = 45;                        //pressanybuttonの時の演出待ち時間
    private int timer;                                  //演出時間計測用
    public bool decisionFlag;                           //決定ボタンが押されたか

    AnimatorStateInfo animeStateInfo;                   //アニメーションの状態

    // Start is called before the first frame update
    void Start()
    {
        //タイトルシーンマネージャ取得
        titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
        //プレイヤー回転　右に向かせる
        transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
        //変数初期化
        pressAnyButtonFlag = false;
        selectFlag = false;
        rightFlag = true;
        scale = transform.localScale;
        timer = 0;
        decisionFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //-----PressAnyButtonが押された時の処理
        if (pressAnyButtonFlag)
        {
            if(timer <= 0)
            {
                animator.SetTrigger("Attack");
                transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));

                timer = first_timer;
            }
            else
            {
                --timer;
                if(timer <= 0)
                {
                    pressAnyButtonFlag = false;
                    selectFlag = true;
                    Camera.main.GetComponent<TitleCamera>().startFlag = true;
                }
            }


        }

        if (!selectFlag)
        {
            return;
        }
        //-----メニュー選択中の処理
        //選択肢の右にいる場合
        if (transform.position.z > selectPosObject[titleSceneManager.select].transform.position.z)
        {
            if (rightFlag)
            {
                //方向転換
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                scale.x *= -1;
                transform.localScale = scale;
                rightFlag = false;
            }
            //アニメーション再生
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }

            //左に動かす
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed);
            if (transform.position.z < selectPosObject[titleSceneManager.select].transform.position.z)
            {
                //行きすぎたら戻す
                transform.position = new Vector3(transform.position.x, transform.position.y, selectPosObject[titleSceneManager.select].transform.position.z);
            }
        }
        //選択肢の左にいる場合
        else if (transform.position.z < selectPosObject[titleSceneManager.select].transform.position.z)
        {
            if (!rightFlag)
            {
                //方向転換
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                transform.localScale = scale;
                rightFlag = true;
            }
            //アニメーション再生
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
            //右に動かす
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed);
            if (transform.position.z > selectPosObject[titleSceneManager.select].transform.position.z)
            {
                //行きすぎたら戻す
                transform.position = new Vector3(transform.position.x, transform.position.y, selectPosObject[titleSceneManager.select].transform.position.z);
            }
        }
        else//左右移動しなかったとき
        {
            //アニメーション再生
            if (animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", false);
            }

            //決定ボタンが押されてたら
            if (decisionFlag)
            {
                //アニメーションの状態取得
                animeStateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (timer == 0)
                {
                    //アニメーション再生
                    animator.SetTrigger("Attack_DOWN");
                    // 弾く音
                    SoundManager.Play(SoundData.eSE.SE_SHIELD, SoundData.TitleAudioList);
                    
                }

                //下パリイのモーションでアニメーションが半分を過ぎてたら
                if (animeStateInfo.normalizedTime > 0.5f && animeStateInfo.IsName("Attack_DOWN"))
                {
                    //UI弾く
                    selectUI[titleSceneManager.select].GetComponent<Move2DTheta>().UnderParry();
                }

                //タイマーが終了してたら
                if (timer > max_timer)
                {
                    //タイマー初期化
                    timer = 0;
                    //フラグ下す
                    decisionFlag = false;
                }
                else
                {
                    //タイマー更新
                    ++timer;
                }
            }

        }
    }

}
