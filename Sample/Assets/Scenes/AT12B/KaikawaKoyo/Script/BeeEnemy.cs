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

    public bool InArea;
    float A = 2.0f;
    float B = 5.0f;
    float Width = 6.0f;     // 飛び回る横幅
    float Vertical = 0.7f;  // 飛び回る縦幅

    private bool isCalledOnce = false;                             // 一回だけ処理をするために使う。

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        ED = GetComponent<EnemyDown>();
        Enemypos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 飛び回る処理
        transform.position = new Vector3(Mathf.Sin(A * Time.time) * Width + Enemypos.x,
            Mathf.Cos(B * Time.time) * Vertical + Enemypos.y, Enemypos.z);
        
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
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }
}
