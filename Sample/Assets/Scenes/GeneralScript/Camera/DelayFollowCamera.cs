//=============================================================================
//
// �v���C���[�̒x���Ǐ]�J����[DelayFollowCamera]
//
// �쐬��:2022/03/08
// �쐬��:�g����
//
// <�J������>
//  2022/05/11 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayFollowCamera : MonoBehaviour
{
    //---�ϐ��錾
    private GameObject player;
    private Player2 playerInfo;

    public Vector3 FollowCameraPos = Vector3.zero;      // �Ǐ]����J����

    public int RightScreenOut;                          // �E�[
    public int LeftScreenOut;                           // ���[
    public int OverScreenOut;                           // ��[
    public int UnderScreenOut;                          // ���[

    // Start is called before the first frame update
    void Start()
    {
        //---�Ǐ]����I�u�W�F�N�g
        player = GameObject.Find(GameData.Player.name);
        playerInfo = player.GetComponent<Player2>();
        
    }

    private void LateUpdate()
    {
        Vector3 PlayerPos = GameData.PlayerPos;

        //---�x������
        //Vector3 move = (playerInfo.OldPlayerPos[0] - transform.position) * 0.01f;
        //move.z = 0;
        //transform.position += move;
        //new Vector3(playerInfo.OldPlayerPos[1].x,
        //            playerInfo.OldPlayerPos[1].y + FollowCameraPos.y,
        //            FollowCameraPos.z);

        transform.position = new Vector3(PlayerPos.x,
                                         playerInfo.OldPlayerPos[1].y + FollowCameraPos.y,
                                         FollowCameraPos.z);;


        //---��ʊO����

        if (PlayerPos.x < LeftScreenOut){                                       //�@���[
            transform.position = new Vector3(LeftScreenOut,
                                             PlayerPos.y + FollowCameraPos.y,
                                             FollowCameraPos.z);
        }

        if (PlayerPos.x > RightScreenOut){                                      // �E�[ 
            transform.position = new Vector3(RightScreenOut,
                                             PlayerPos.y + FollowCameraPos.y,
                                             FollowCameraPos.z);
        }

        if (PlayerPos.y > OverScreenOut){                                       // ��[ 
            transform.position = new Vector3(PlayerPos.x,
                                             OverScreenOut,
                                             FollowCameraPos.z);
        }

        if (PlayerPos.y <  UnderScreenOut){                                     // ���[
            transform.position = new Vector3(PlayerPos.x,
                                             UnderScreenOut,
                                             FollowCameraPos.z);
        }



    }
}
