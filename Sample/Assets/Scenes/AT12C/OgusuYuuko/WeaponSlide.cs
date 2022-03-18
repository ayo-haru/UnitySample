//=============================================================================
//
// ���˔X���C�h
//
//
// �쐬��:2022/03/16
// �쐬��:����T�q
//
// <�J������>
// 2022/03/16 �쐬
// 2022/03/17 �X���C�h��S�����ł���悤�ɂ���
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum Dir_Attack { NONE,RIGHT, LEFT, UP, DOWN };  //�U���̕���
public class WeaponSlide : MonoBehaviour
{
    //����
   // Dir_Attack nDir;
    //�X���C�h�̑��x
    public float slideSpeed = 0.1f;

    Rigidbody rb;

    //�v���C���[
    GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Player = GameObject.Find("Player");
        Player = GameData.Player;
    }

    // Update is called once per frame
    void Update()
    {
        //���˔ʒu
        Vector3 pos = rb.position;
        //�v���C���[�ʒu
        Vector3 playerPos = Player.transform.position;

        //switch (nDir)
        //{
        //    case Dir_Attack.RIGHT:
        //        //���˔E�Ɉړ�
        //        pos.x += slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.LEFT:
        //        //���˔��Ɉړ�
        //        pos.x -= slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.UP:
        //        //���˔�Ɉړ�
        //        pos.y += slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.DOWN:
        //        //���˔��Ɉړ�
        //        pos.y -= slideSpeed;
        //        rb.position = pos;
        //        break;
        //    case Dir_Attack.NONE:
        //        break;
        //}

        //�v���C���[���甽�˔̕������擾
        Vector3 dir = playerPos - pos;
        dir.Normalize();
        //�擾���������ɔ��˔ړ�
        rb.position -= dir * slideSpeed;

    }

    //�����ݒ肷��
    //public void SetDir(Dir_Attack dir) {
    //    nDir = dir;
    //}
}
