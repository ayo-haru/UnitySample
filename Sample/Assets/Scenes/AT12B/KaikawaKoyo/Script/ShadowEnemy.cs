//==========================================================
//      �����e�̕���
//      �쐬���@2022/03/21
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/21      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEnemy : MonoBehaviour
{
    GameObject Enemy;
    private Vector3 position;
    public int EnemyNomber;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        // �N�ɕϐg���邩�̏���
        switch (EnemyNomber)
        {
            case 0:
                Enemy = GameObject.Find("CarrotEnemy");
                break;
            case 1:
                Enemy = GameObject.Find("BroccoliEnemy");
                break;
            case 2:
                Enemy = GameObject.Find("TomatoEnemy");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[����������G�ɕϐg����
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
            Instantiate(Enemy, position, Quaternion.identity);
        }
    }
}
