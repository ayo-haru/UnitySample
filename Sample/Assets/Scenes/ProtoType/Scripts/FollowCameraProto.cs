//=============================================================================
//
// �v���C���[�̒Ǐ]�J����[FollowCamera]
//
// �쐬��:2022/03/08
// �쐬��:�g����
//
// <�J������>
// 2022/03/08 �쐬
// 2022/03/10 �J�����Ǐ]�̒l��ύX�A�C���X�y�N�^�[�ŕύX�ł���悤�ɂ����B	
// 2022/03/10 �}�b�v�J�ڂ�ǉ�
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraProto : MonoBehaviour
{
    //---�ϐ��錾
    public Vector3 FollowCameraPos = new Vector3(0.0f,1.5f,-6.0f);     // �Ǐ]����J�����̍���(x,y,z)
    public int RightScreenOut = 40;          // �E�̉�ʊO�ݒ�
    public int LeftScreenOut = 0;           // ���̉�ʊO�ݒ�
    public float MovePoint;             // �}�b�v�J�ڂ��邽�߂̒n�_
    bool MoveFlg;                       // �}�b�v�J�ڂ̃t���O

    // Start is called before the first frame update
    void Start()
    {
        //---�Ǐ]����I�u�W�F�N�g����ݒ�
        this.MoveFlg = false;    
    }

    // Update is called once per frame
    void Update()
    {
        //---�v���C���[�ɒǏ]����
        Vector3 PlayerPos = GameData.PlayerPos;

        // *****���W*****
        //transform.position = new Vector3(PlayerPos.x,0.7f, PlayerPos.z - 4.0f);
        transform.position = new Vector3(PlayerPos.x, 
                                         PlayerPos.y + FollowCameraPos.y, 
                                         FollowCameraPos.z);                       // �W�����v�Ǐ]

        //---��ʊO�ݒ�(x = 45.0f�̒n�_�ɓ��B������J�����̈ړ����~)
        if (this.MoveFlg == false && PlayerPos.x > RightScreenOut)
        {
            //transform.position = new Vector3(45.0f, 0.7f, PlayerPos.z - 4.5f);
            transform.position = new Vector3(RightScreenOut, 
                                             PlayerPos.y + FollowCameraPos.y, 
                                             FollowCameraPos.z);         // �W�����v�Ǐ]
            //if (PlayerPos.x >= MovePoint){
            //    this.MoveFlg = true;
            //    if(this.MoveFlg == true){
            //        this.transform.position = new Vector3(60.0f, 1.5f, -4.0f);    // �J�����̏ꏊ���Ē�
            //    }
            //}
        }


        //---��ʊO�ݒ�(x = 15.0f�̒n�_�ɓ��B������J�����̈ړ����~)
        else if (PlayerPos.x < LeftScreenOut)
        {
            //transform.position = new Vector3(15.0f, 0.7f, PlayerPos.z - 4.5f);
            transform.position = new Vector3(LeftScreenOut, 
                                             PlayerPos.y + FollowCameraPos.y, 
                                             FollowCameraPos.z);          // �W�����v�Ǐ]
        }
    }
}
