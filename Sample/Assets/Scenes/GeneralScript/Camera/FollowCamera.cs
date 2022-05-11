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

public class FollowCamera : MonoBehaviour
{
    //---�ϐ��錾
    GameObject Player;
    Player2 player2;
    Vector3 Offset;                                                         // ���΋����擾�p
    public Vector3 FollowCameraPos = Vector3.zero;     // �Ǐ]����J�����̍���(x,y,z)
    public int RightScreenOut;          // �E�̉�ʊO�ݒ�
    public int LeftScreenOut;           // ���̉�ʊO�ݒ�
    public int HeightScreenOut;         // ��̉�ʊO�ݒ�
    public int UnderScreenOut;          // ���̉�ʊO�ݒ�

    // Start is called before the first frame update
    void Start()
    {
        //---�Ǐ]����I�u�W�F�N�g����ݒ�
        this.Player = GameObject.Find(GameData.Player.name);
        player2 = Player.GetComponent<Player2>();

        // �J������Player�̑��΋��������߂�
        Offset = transform.position - Player.transform.position;

        Debug.Log("Offset"+Offset); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //---�v���C���[�ɒǏ]����

        Vector3 PlayerPos = GameData.PlayerPos;

		// *****���W*****
		//transform.position = new Vector3(PlayerPos.x,
		//                         FollowCameraPos.y,
		//                         FollowCameraPos.z);                               // �W�����v�Ǐ]

		//transform.position = new Vector3(PlayerPos.x,
		//								 PlayerPos.y + FollowCameraPos.y,
		//								 FollowCameraPos.z);                       // �W�����v�Ǐ]

		//---�O�t���[���̍��W�ŒǏ]
		//---���X�ؐ搶�v���O����
		Vector3 move = (player2.OldPlayerPos[0] - transform.position) * 0.01f;
		move.z = 0;
		transform.position += move;
		new Vector3(player2.OldPlayerPos[1].x,
							player2.OldPlayerPos[1].y + FollowCameraPos.y,
							FollowCameraPos.z);

		//Debug.Log("�J�����൰����߽" + player2.OldPlayerPos[9].x);
		
		//---���Ƃ̂��
		//transform.position = new Vector3(PlayerPos.x,
		//												player2.OldPlayerPos[5].y + FollowCameraPos.y,
		//												FollowCameraPos.z);

		////---��ʊO�ݒ�(x = 45.0f�̒n�_�ɓ��B������J�����̈ړ����~)
		if (PlayerPos.x > RightScreenOut){
			//transform.position = new Vector3(45.0f, 0.7f, PlayerPos.z - 4.5f);
			transform.position = new Vector3(RightScreenOut,PlayerPos.y + FollowCameraPos.y,FollowCameraPos.z);         // �W�����v�Ǐ]
		    //if (PlayerPos.x >= MovePoint){
		    //    this.MoveFlg = true;
		    //    if(this.MoveFlg == true){
		    //        this.transform.position = new Vector3(60.0f, 1.5f, -4.0f);    // �J�����̏ꏊ���Ē�
		    //    }
		    //}
		}
		//---��ʊO�ݒ�(x = 15.0f�̒n�_�ɓ��B������J�����̈ړ����~)
		if (PlayerPos.x < LeftScreenOut)
		{
			//transform.position = new Vector3(15.0f, 0.7f, PlayerPos.z - 4.5f);
			transform.position = new Vector3(LeftScreenOut,
											 PlayerPos.y + FollowCameraPos.y,
											 FollowCameraPos.z);          // �W�����v�Ǐ]
		}

			//---��̉�ʊO�ݒ�(y)
			if (PlayerPos.y > HeightScreenOut){
            transform.position = new Vector3(PlayerPos.x,
                                             HeightScreenOut,
                                             FollowCameraPos.z);
        }

        //---���̉�ʊO�ݒ�(y)
        if (PlayerPos.y < UnderScreenOut){
            transform.position = new Vector3(PlayerPos.x,
                                             UnderScreenOut,
                                             FollowCameraPos.z);
        }


	}
}
