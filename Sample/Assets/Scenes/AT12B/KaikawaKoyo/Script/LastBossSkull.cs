using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossSkull : MonoBehaviour
{
    GameObject Player;
    private Vector3 vec;
    private Quaternion look;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // プレイヤーのオブジェクトを取得
    }

    private void Update()
    {
        vec = (Player.transform.position - new Vector3(0.0f, Player.transform.position.y, 0.0f) - (transform.position - new Vector3(0.0f, transform.position.y, 0.0f))).normalized;
        look = Quaternion.LookRotation(vec);
        transform.localRotation = look;
    }
}

