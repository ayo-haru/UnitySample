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
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private Vector3 startPosition, targetPosition;
    private EnemyDown ED;
    private Vector3 aim;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 0.005f;
    float speed = 0.0f;
    bool InArea;
    bool Look;

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

        // プレイヤーを見つけたら攻撃開始
        if (InArea && ED.isAlive)
        {

            if (speed <= 1)
            {
                speed += MoveSpeed * Time.deltaTime;
            }
            // プレイヤーに向かって特攻する
            rb.position = Vector3.Lerp(startPosition, targetPosition, speed);

            aim = targetPosition - transform.position;
            look = Quaternion.LookRotation(aim);
            transform.localRotation = look;

            transform.Rotate(90, 0, 0);

            //var goal = targetPosition + aim;

        }

        if (rb.position == targetPosition)
        {
            transform.Rotate(-90, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            Destroy(gameObject, 1.0f);
        }
    }

    public void OnTriggerEnter(Collider other)    // コライダーでプレイヤーを索敵したい
    {
        if (other.CompareTag("Player") && Look == false)
        {
            Target = Player.transform;          // プレイヤーの座標取得
            targetPosition = Target.position;
            startPosition = rb.position;
            speed = 0.0f;
            InArea = true;
            Look = true;
        }
    }

}