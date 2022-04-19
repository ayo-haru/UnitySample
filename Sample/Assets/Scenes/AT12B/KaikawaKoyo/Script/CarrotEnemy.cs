//==========================================================
//      ニンジン雑魚の攻撃
//      作成日　2022/03/16
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    private float InvincibleTime;
    bool InArea;
    bool Look;
    bool Attack = false;
    private bool Invincible = false;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        Look = false;
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            // プレイヤーを見つけたら攻撃開始
            if (InArea && ED.isAlive)
            {
                if (Invincible)
                {
                    gameObject.layer = LayerMask.NameToLayer("Invincible");
                    InvincibleTime += Time.deltaTime;
                }
                if (InvincibleTime > 2.0f)
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    Invincible = false;
                }
                if (!Attack)
                {
                    vec = (Player.transform.position - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                    Attack = true;
                }

                if (!isCalledOnce)     // 一回だけ呼ぶ
                {
                    //SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
            }

            if (Attack)
            {
                Destroy(gameObject, 2.0f);
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)    // コライダーでプレイヤーを索敵したい
    {
        if (other.CompareTag("Player") && Look == false)
        {
            InArea = true;
            Look = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invincible = true;
            //Destroy(gameObject, 0.0f);
        }
    }

}