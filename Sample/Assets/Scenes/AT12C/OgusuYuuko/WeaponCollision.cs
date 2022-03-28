//=============================================================================
//
// ���˔����蔻��
//
//
// �쐬��:2022/03/27
// �쐬��:����T�q
//
// <�J������>
// 2022/03/27 �쐬
//=============================================================================

//�R�����g�ǉ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    //�v���C���[
    GameObject Player;
    //�v���C���[�̃��W�b�g�{�f�B
    Rigidbody player_rb;
    //�n�ʃp���C�������̂͂˕Ԃ葬�x
    public float baunceGround = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameData.Player;
        Player = GameObject.Find("Player(Clone)");
        player_rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            //����
           // Vector3 dir = Player.transform.position - collision.transform.position;
            Vector3 dir = Player.transform.position - gameObject.transform.position;
            dir.Normalize();
            //�n�ʃp���C
            player_rb.AddForce(dir * baunceGround,ForceMode.Impulse);
        }

        //������
        Destroy(gameObject);
    }
}
