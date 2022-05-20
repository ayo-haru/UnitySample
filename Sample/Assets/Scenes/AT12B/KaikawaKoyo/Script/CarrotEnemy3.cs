//==========================================================
//      ニンジン雑魚亜種の攻撃
//      作成日　2022/04/16
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarrotEnemy3 : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 position;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed;
    private int AttackPattern;
    private float angle = 5;
    private float Startangle;
    private float Timer;
    private float idlingTime = 0.5f;
    private bool IdringFlg = true;
    private bool Attack;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        position = transform.position + new Vector3(0.0f, 15.0f, 0.0f);     // 回転の中心点
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
        Startangle = angle / 2;
        transform.DOShakePosition(duration: idlingTime, strength: 5.0f);    // ぶるぶる震わせる
        transform.localRotation = Quaternion.LookRotation(new Vector3(-180.0f, 0.0f, 0.0f));
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            // プレイヤーを見つけたら攻撃開始
            if (ED.isAlive)
            {
                // アイドリングするよ
                if (IdringFlg)
                {
                    // ディレイかけてから攻撃する
                    Timer += Time.deltaTime;
                    if (Timer >= idlingTime)
                    {
                        // 角度調整
                        transform.localRotation = Quaternion.LookRotation(new Vector3(-180.0f, 20.0f, 0.0f));
                        // 攻撃開始
                        Attack = true;
                        // サウンドフラグ切替
                        isCalledOnce = false;
                        // アイドリング終了
                        IdringFlg = false;
                        // タイマーのリセット
                        Timer = 0.0f;
                    }
                }
                if (Attack)
                {
                    // くるっと回る
                    transform.RotateAround(
                        position,
                        Vector3.back,
                        Startangle);

                    if (angle > Startangle)
                    {
                        Startangle += 0.5f;
                    }

                    if (transform.position.x > position.x && transform.position.y < position.y)
                    {
                        AttackPattern = 1;
                        Attack = false;
                    }
                }

                // 追尾開始
                if (AttackPattern == 1)
                {
                    vec = (Player.transform.position - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
        if ((collision.gameObject.CompareTag("Default") || collision.gameObject.CompareTag("Ground")) && !IdringFlg)
        {
            // アイドリング開始
            IdringFlg = true;
        }
    }
}
