//==========================================================
//      ハチ雑魚のエイム
//      作成日　2022/04/06
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/06      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAim : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private BeeEnemy BE;
    private Vector3 aim;
    private Quaternion look;
    private Vector3 targetPosition;
    private float FiringTime;

    [SerializeField]
    private GameObject FiringPoint;

    [SerializeField]
    private GameObject BeeBullet;

    [SerializeField]
    private float speed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
        BE = GetComponent<BeeEnemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = Player.transform;          // プレイヤーの座標取得
        targetPosition = Target.position;

        aim = targetPosition - transform.position;
        look = Quaternion.LookRotation(aim);
        transform.localRotation = look;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PoisonShot();
        }

    }

    private void PoisonShot()
    {
        GameObject newBall = Instantiate(BeeBullet, transform.position, transform.rotation);
        Vector3 direction = newBall.transform.forward;
        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);

    }

}
