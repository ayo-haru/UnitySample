//=============================================================================
//
// �}�b�v�J��[MoveMap]
//
// �쐬��:2022/03/09
// �쐬��:�g����
//
// <�J������>
// 2022/03/09 �쐬
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{

    //---�ϐ��錾
    GameObject Player;
    public Vector3 MovePoint;         // �}�b�v�J�ڂ̒n�_�ƂȂ�l
                                    // 
    // Start is called before the first frame update
    void Start()
    {
        //---�Ǐ]����I�u�W�F�N�g����ݒ�
        this.Player = GameObject.Find("SD_unitychan_humanoid");

    }

    // Update is called once per frame
    void Update()
    {
        //---�v���C���[�̌����W���擾
        Vector3 PlayerPos = this.Player.transform.position;

        if(PlayerPos.x >= MovePoint.x){
            transform.position = new Vector3(60.0f,1.5f,-4.0f);
        }

    }
}
