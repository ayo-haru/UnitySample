//=============================================================================
//
// �񕜃A�C�e������
//
// �쐬��:2022/04/16
// �쐬��:�g����
//
// <�J������>
// 2022/04/16   �쐬
// 2022/05/07   �����񕜒�����
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    //---�ϐ��錾
    public GameObject prefab;
    HPManager hpmanager;
    GameObject HP;
    GameObject Player;
    GameObject Star_Fragment;
    Rigidbody rb;
    public float BounceSpeed = 10.0f;                   // �e�����X�s�[�h
    public float BounceVectorMultiple = 2.0f;           // �@���x�N�g���ɏ�Z����l
    public float BouncePower = 10000.0f;                  // �e�����l
    private Vector3 vec;                                // �e���x�N�g��
    [System.NonSerialized]bool isGroundFlg = false;     // �n�ʂƂ̐ڒn�t���O
    private float aTime;
    private bool isBaunceFlg;                           //�e���ꂽ���̔���

    private Vector3 CamRightTop;    // �J�����̉E����W
    private Vector3 CamLeftBot;     // �J�����̍������W
    private Vector3 InAngle;        // ���ˊp
    private Vector3 ReAngle;        // ���ˊp
    private Vector3 inNormalU;      // �@���x�N�g����
    private Vector3 inNormalD;      // �@���x�N�g����
    private Vector3 inNormalR;      // �@���x�N�g���E
    private Vector3 inNormalL;      // �@���x�N�g����
    private float CamZ = -120.0f;
    private bool Reflect;                               //��ʒ[�Œe���ꂽ���̔���

    // Start is called before the first frame update
    void Start()
    {
        // �v���n�u�𕡐�
        //GameObject HealItem = Instantiate(prefab,
        //                                  new Vector3(0.0f,0.0f,0.0f),
        //                                  Quaternion.identity);
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player");

        HP = GameObject.Find("HPSystem(2)(Clone)");
        hpmanager = HP.GetComponent<HPManager>();

        isBaunceFlg = false;    //�܂��e����ĂȂ�

        // �@���x�N�g����`
        inNormalU = new Vector3(0.0f, 1.0f, 0.0f);
        inNormalD = new Vector3(0.0f, -1.0f, 0.0f);
        inNormalR = new Vector3(1.0f, 0.0f, 0.0f);
        inNormalL = new Vector3(-1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        //-----�e����Ă������ɂ�鏈��
        if (isBaunceFlg)
        {
            rb.Resume(gameObject);
            // �J�����̒[�̍��W�擾
            CamRightTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, CamZ));
            CamLeftBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, CamZ));

            // ��ʒ[�Œ��˕Ԃ�����
            // �E�[
            if (transform.position.x >= CamRightTop.x && !Reflect)
            {
                InAngle = rb.velocity;
                ReAngle = Vector3.Reflect(InAngle, inNormalL);
                rb.velocity = ReAngle;
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }
            // ���[
            if (transform.position.x <= CamLeftBot.x && !Reflect)
            {
                InAngle = rb.velocity;
                rb.velocity = Vector3.Reflect(InAngle, inNormalR);
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }
            // ��[
            if (transform.position.y >= CamRightTop.y && !Reflect)
            {
                InAngle = rb.velocity;
                rb.velocity = Vector3.Reflect(InAngle, inNormalD);
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }
            // ���[
            if (transform.position.y <= CamLeftBot.y && !Reflect)
            {
                InAngle = rb.velocity;
                rb.velocity = Vector3.Reflect(InAngle, inNormalU);
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }

            return;
        }
        
        //-----�e����ĂȂ������Ƃ��̏���
        // �v���C���[�ƃI�u�W�F�N�g�̓�_�Ԃ̃x�N�g�������߂�
        vec = (Player.transform.position - transform.position).normalized;
        
        // �n�ʂɂ����炿����ƕ����Ă���󒆂ɗ��܂�
        if (isGroundFlg)
        {
            aTime += Time.deltaTime;
            if (aTime < 1.0f)
            {
                rb.AddForce(transform.up * (10.0f * aTime), ForceMode.Force);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        // �ڒn����
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            rb.useGravity = false;
            isGroundFlg = true;
            
        }

        if (collision.gameObject.name == "Weapon(Clone)")
        {

            //// �Փ˂����ʂ̐ڒn�_�̃x�N�g�����擾
            //Vector3 normal = collision.contacts[0].normal; 
            
            //// �Փ˂������x�x�N�g����P�ʃx�N�g���ɒu��������
            //Velocity = collision.rigidbody.velocity.normalized;

            //// x,y,z�����ɑ΂��Ė@���x�N�g�����擾
            //Velocity += new Vector3(normal.x * BounceVectorMultiple,
            //                        normal.y * BounceVectorMultiple,
            //                        normal.z * BounceVectorMultiple);

            // �t�����ɒ��˕Ԃ�
            //collision.rigidbody.AddForce(-Velocity * BounceSpeed,ForceMode.Impulse);
            
            rb.useGravity = false;                          // �d�͂�����
            rb.angularDrag = 0.0f;                          // ��C��R���[����
            rb.centerOfMass = new Vector3(0.0f,0.0f,0.0f);  // ��]���𒆉��ɂ���

            //�v���C���[�Ƌt�����ɒ��˕Ԃ�
            rb.AddForce(-vec * BouncePower, ForceMode.Force);

            // �񕜃G�t�F�N�g����
            EffectManager.Play(EffectData.eEFFECT.EF_HEAL, GameData.PlayerPos);

            //���ɒe����Ă��珈�����Ȃ�
            if (isBaunceFlg)
            {
                return;
            }
            Destroy(prefab, 1.5f);
            hpmanager.GetPiece();

            //�e���ꂽ
            isBaunceFlg = true;
        }


    }
}
