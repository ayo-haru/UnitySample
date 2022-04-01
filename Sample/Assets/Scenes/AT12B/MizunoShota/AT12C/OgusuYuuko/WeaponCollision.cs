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
// 2022/03/29 ���̌�������t����
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
    //�V�[���h�}�l�[�W��
    ShieldManager shield_Manager;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameData.Player;
        Player = GameObject.Find("Rulaby 1(Clone)");
        player_rb = Player.GetComponent<Rigidbody>();
        shield_Manager = Player.GetComponent<ShieldManager>();
        //�����ő吔�𒴂��Ă�����
        if (!shield_Manager.AddShield())
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        shield_Manager.DestroyShield();
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
            player_rb.velocity = Vector3.zero;
            //�n�ʃp���C
            player_rb.AddForce(dir * baunceGround,ForceMode.Impulse);
        }

        //������
        Destroy(gameObject);

        Debug.Log(collision.gameObject.name + "�Ɠ�������");
    }
}
