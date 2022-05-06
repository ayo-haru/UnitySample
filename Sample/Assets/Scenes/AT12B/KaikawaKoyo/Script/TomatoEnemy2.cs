//==========================================================
//      �g�}�g�G������̍U��
//      �쐬���@2022/04/29
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/29      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy2 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private Vector3 TargetPos;
    private Vector3 vec;
    private bool look = true;       // �v���C���[�̂ق�������t���O
    private bool isGround;          // �ڒn�t���O
    private bool TomatoDead;
    private bool Attack;
    private bool Plunge;
    private bool AttackEnd;
    private bool Explosion;
    private float delay;            // �W�����v�̃f�B���C

    [SerializeField]
    private float JumpPower;        // �W�����v��

    [SerializeField]
    float MoveSpeed = 2.0f;          // �ړ����x
    float AttackSpeed;
    //int DetecDist = 8;
    //bool InArea = false;                

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        //distance = 1.0f;
        ED = GetComponent<EnemyDown>();
        AttackSpeed = MoveSpeed + 10.0f;
        transform.Rotate(0, -90, 0);
        rb.velocity += new Vector3(0.0f, -0.5f, 0.0f);
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            rb.Resume(gameObject);

            Vector3 pos = rb.position;
            // �v���C���[����������U���J�n
            if (ED.isAlive && !TomatoDead)
            {
                if (!isGround)
                {
                    // �v���C���[�Ɍ������ē��U����
                    if(!Attack && !Plunge)
                    {
                        float a = Player.transform.position.y - transform.position.y;
                        TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);
                        float step = MoveSpeed * Time.deltaTime;
                        rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                    }
                    if(Attack && !Plunge)
                    {
                        float step = AttackSpeed * Time.deltaTime;
                        rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                        AttackEnd = true;
                    }
                }

                // �h���U��
                if(Plunge)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
                    {
                        // �W�����v����
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, 100.0f, 0.0f);
                        // ���W�v�Z
                        TargetPos = Target.position - new Vector3(0.0f, 15.0f, 0.0f);
                        
                        isGround = false;
                        TomatoDead = true;
                    }
                }

                // ���˂鏈��
                if (isGround && !Plunge)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f && !Attack)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                    if(delay > 0.5f && Attack)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, 3.0f * JumpPower, 0.0f);
                        SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                }
                
                if (Target.position.x < transform.position.x && !look && !Attack)   // �v���C���[�̂ق�����������
                {
                    transform.Rotate(0, -180, 0);
                    look = true;
                }
                if (Target.position.x > transform.position.x && look && !Attack)  // �v���C���[�̂ق�����������
                {
                    transform.Rotate(0, 180, 0);
                    look = false;
                }

                // �㏸���x&�������x����
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }
            }

            if(AttackEnd)
            {
                // ��]������
                if (look)
                {
                    transform.Rotate(new Vector3(-20.0f, 0.0f, 0.0f), Space.Self);
                }
                if (!look)
                {
                    transform.Rotate(new Vector3(20.0f, 0.0f, 0.0f), Space.Self);
                }
            }

            if(TomatoDead)
            {
                float step = 100.0f * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, TargetPos, step);
         
                // �㏸���x&�������x����
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -2.0f, 0.0f);
                }
                Explosion = true;
            }

        }
        else
        {
            rb.Pause(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ڒn����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY;
            if (AttackEnd)
            {
                Plunge = true;
            }
            if(Explosion)
            {
                SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
                EffectManager.Play(EffectData.eEFFECT.EF_TOMATOBOMB, transform.position, 0.9f);
                Destroy(gameObject, 0.0f);
            }
        }

        // �v���C���[�ɓ��������������
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            EffectManager.Play(EffectData.eEFFECT.EF_TOMATOBOMB, transform.position, 0.9f);
            Destroy(gameObject, 0.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!Attack && isGround)
            {
                if (look)
                {
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(20.0f, a, 0.0f);
                }
                if (!look)
                {
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(-20.0f, a, 0.0f);
                }
                Attack = true;
            }
        }
    }
}
