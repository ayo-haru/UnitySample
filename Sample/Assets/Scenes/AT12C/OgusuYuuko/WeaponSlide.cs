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
// 2022/03/24 �v���C���[�Ə����������Ȃ��悤�ɂ���
// 2022/03/30 �����v���C���[�ɂ܂Ƃ����悤�ɂ���
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum Dir_Attack { NONE,RIGHT, LEFT, UP, DOWN };  //�U���̕���
public class WeaponSlide : MonoBehaviour
{
    //�X���C�h�̕���
    Vector3 dir;
    //�X���C�h�̑��x
    public float slideSpeed = 0.1f;
    //�v���C���[�ʒu
    Vector3 PlayerPos;
    //�v���C���[�Ə��̍ŏ�����
    //public float minDistance = 1.0f;
    //���̈ړ���
    Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.Find("Player");
        PlayerPos = GameData.PlayerPos;
        dir = gameObject.transform.position - PlayerPos;
        move = dir;
        Debug.Log("�ړ��ʏ����l"+move);
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPause)
        {
            //�v���C���[�ʒu
            PlayerPos = GameData.PlayerPos;

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
            //Vector3 dir = playerPos - pos;
            //dir.Normalize();


            //�ړ��ʕۑ�
            move += dir * slideSpeed;

            //�ړ�
            transform.position = PlayerPos + move;

            //�擾���������ɔ��˔ړ�
            // rb.position += dir * slideSpeed;

            //�v���C���[�Ə��̋������߂��ꍇ�͗���
            //float dis = Vector3.Distance(PlayerPos, rb.position);
            //if (dis < minDistance)
            //{
            //    rb.position += dir * (minDistance - dis);
            //}
        }
    }

    //�����ݒ肷��
    //public void SetDir(Dir_Attack dir) {
    //    nDir = dir;
    //}
}
