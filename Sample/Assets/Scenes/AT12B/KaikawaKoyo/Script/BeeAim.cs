//==========================================================
//      �n�`�G���̃G�C��
//      �쐬���@2022/04/06
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
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
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
    }

    // Update is called once per frame
    void Update()
    {
        Target = Player.transform;          // �v���C���[�̍��W�擾
        targetPosition = Target.position;

        // �v���C���[��_��������
        aim = targetPosition - transform.position;
        look = Quaternion.LookRotation(aim);
        transform.localRotation = look;
    }
    
}