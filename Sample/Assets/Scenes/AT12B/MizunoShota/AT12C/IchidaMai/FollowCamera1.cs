using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowCamera1 : MonoBehaviour
{
    //GameObject  Player;
   // private Vector3 OffSet;
    // Start is called before the first frame update
    void Start()
    {
        // �Ǐ]����I�u�W�F�N�g����ݒ�
        //this.Player = GameObject.Find("Player");
        //OffSet = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�ɒǏ]����
        //Vector3 PlayerPos = this.Player.transform.position;
        Vector3 PlayerPos = GameData.PlayerPos;
        

        // *****���W*****
        //transform.position = new Vector3(PlayerPos.x,0.7f, PlayerPos.z - 4.5f);
        transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 0.7f, -4.0f);     // �W�����v�Ǐ]
        //transform.position = Vector3.Lerp(transform.position,Player.transform.position + OffSet,6.0f * Time.deltaTime);

        // *****��]*****
        //transform.Rotate = new Vector3(5.0f, 0.0f, 0.0f);

        // ��ʊO�ݒ�(x = 45.0f�̒n�_�ɓ��B������J�����̈ړ����~)
        if(PlayerPos.x > 45.0f){
            //transform.position = new Vector3(45.0f, 0.7f, PlayerPos.z - 4.5f);
            transform.position = new Vector3(45.0f, PlayerPos.y + 0.7f, -4.0f);         // �W�����v�Ǐ]   
        }
        // ��ʊO�ݒ�(x = 45.0f�̒n�_�ɓ��B������J�����̈ړ����~)
        else if (PlayerPos.x < 5.0f){
            //transform.position = new Vector3(15.0f, 0.7f, PlayerPos.z - 4.5f);
            transform.position = new Vector3(5.0f, PlayerPos.y + 0.7f, -4.0f);          // �W�����v�Ǐ]
        }
    }
}
