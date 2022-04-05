//==========================================================
//      ハチ雑魚の攻撃
//      作成日　2022/04/04
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/04      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private Vector3 Enemypos;
    private EnemyDown ED;
    private Vector3 aim;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    float speed = 0.0f;
    bool InArea;
    bool Look;
    float A = 2.0f;
    float B = 5.0f;

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        Look = false;
        ED = GetComponent<EnemyDown>();
        Enemypos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(A * Time.time) * 60.0f + Enemypos.x,
            Mathf.Cos(B * Time.time) * 7.0f + Enemypos.y, Enemypos.z);


        // サウンド処理
        /*
        if (!isCalledOnce)     // 一回だけ呼ぶ
        {
            SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
            isCalledOnce = true;
        }
        */
    }

    public void OnTriggerEnter(Collider other)    // コライダーでプレイヤーを索敵したい
    {
        if (other.CompareTag("Player") && Look == false)
        {
            Target = Player.transform;          // プレイヤーの座標取得
            speed = 0.0f;
            InArea = true;
            Look = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
    }
}
