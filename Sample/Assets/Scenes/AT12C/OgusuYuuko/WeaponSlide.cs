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
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Dir_Attack { NONE,RIGHT, LEFT, UP, DOWN };  //�U���̕���
public class WeaponSlide : MonoBehaviour
{
    //����
    Dir_Attack nDir;
    //�X���C�h�̑��x
    public float slideSpeed = 0.1f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = rb.position;
        switch (nDir)
        {
            case Dir_Attack.RIGHT:
                //���˔E�Ɉړ�
                pos.x += slideSpeed;
                rb.position = pos;
                break;
            case Dir_Attack.LEFT:
                //���˔��Ɉړ�
                pos.x -= slideSpeed;
                rb.position = pos;
                break;
            case Dir_Attack.UP:
                //���˔�Ɉړ�
                pos.y += slideSpeed;
                rb.position = pos;
                break;
            case Dir_Attack.DOWN:
                //���˔��Ɉړ�
                pos.y -= slideSpeed;
                rb.position = pos;
                break;
            case Dir_Attack.NONE:
                break;
        }
    }

    //�����ݒ肷��
    public void SetDir(Dir_Attack dir) {
        nDir = dir;
    }
}
