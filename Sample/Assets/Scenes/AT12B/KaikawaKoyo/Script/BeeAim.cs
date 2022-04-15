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
    //GameObject BeeEnemy;
    //private BeeEnemy BE;
    private Vector3 aim;
    private Quaternion look;
    private Vector3 targetPosition;
    private float FiringTime;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを探す
    }

    // Update is called once per frame
    void Update()
    {
        Target = Player.transform;          // プレイヤーの座標取得
        targetPosition = Target.position;

        // プレイヤーを狙い続ける
        aim = targetPosition - transform.position;
        look = Quaternion.LookRotation(aim);
        transform.localRotation = look;
    }
    
}