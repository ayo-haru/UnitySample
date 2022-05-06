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
    GameObject _Player;
    //�v���C���[�̃��W�b�g�{�f�B
    Rigidbody player_rb;
    //�n�ʃp���C�������̂͂˕Ԃ葬�x
    public float baunceGround = 2.0f;
    //�V�[���h�}�l�[�W��
    ShieldManager shield_Manager;

    Player2 player2;

    private bool CanCollision = true;                      // �����蔻��̎g�p�t���O


    // Start is called before the first frame update
    void Awake()
    {
        //Player = GameData.Player;
        this._Player = GameObject.Find(GameData.Player.name);
        player_rb = this._Player.GetComponent<Rigidbody>();
        shield_Manager = this._Player.GetComponent<ShieldManager>();
        player2 = this._Player.GetComponent<Player2>();


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
        if (CanCollision)
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "GroundDameged")
            {
                if (Player.isHitSavePoint)
                {
                    Debug.Log("�Z�[�u�|�C���g�Ɠ�������");
                    return;
                }

                BounceCheck();
                //����
                //Vector3 dir = Player.transform.position - collision.transform.position;

                Vector2 dir = (this._Player.transform.position - this.gameObject.transform.position).normalized;
                //Vector2 dir = this.gameObject.transform.position - Player.transform.position;
                //Debug.Log("��������" + dir);

                //dir.Normalize();
                Debug.Log("��������(���K��)" + dir);


                player2.rb.velocity = Vector3.zero;

                //�n�ʃp���C
                //player_rb.AddForce(dir, ForceMode.Impulse);
                player_rb.AddForce(dir * baunceGround, ForceMode.Impulse);
                //player2.SetJumpPower(dir * baunceGround);

                CanCollision = false;
            }

            if (collision.gameObject.tag == "Enemy")
            {
                player2.OnAttackHit();
            }
            else
            {
                player2.CanHitStopflg = false;
            }
        }

        //�v���C���[�ȊO�Ɠ������Ă��珂����
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject,0.1f);
        }

        Debug.Log(collision.gameObject.name + "�Ɠ�������");
    }

    // ���˕Ԃ�͒����֐�
    private void BounceCheck()
    {
        Vector3 PlayerVelocity = player_rb.velocity;
        PlayerVelocity.z = 0;

        if(PlayerVelocity.sqrMagnitude > baunceGround * baunceGround)
        {
            player_rb.velocity = PlayerVelocity.normalized * baunceGround;
        }
    }
}
