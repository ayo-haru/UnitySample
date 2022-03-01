using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        // �Ǐ]����I�u�W�F�N�g����ݒ�
        this.Player = GameObject.Find("SD_unitychan_humanoid");
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�ɒǏ]����
        Vector3 PlayerPos = this.Player.transform.position;
        //transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 1.0f, PlayerPos.z - 2.0f);
        transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 0.5f, -4.0f);

        // ��ʊO�ݒ�(x = 45.0f�̒n�_�ɓ��B������J�����̈ړ����~)
        if(PlayerPos.x > 45.0f){
            transform.position = new Vector3(45.0f, PlayerPos.y + 0.5f, -4.0f); 
        }
    }
}
