//==========================================================
//      �g�}�g�G������̍U��2
//      �쐬���@2022/05/04
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/05/04      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy3 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private Vector3 TargetPos;
    private Vector3 PlayerPosX;
    private Vector3 TomatoPosX;
    private bool look;              // �v���C���[�̂ق�������t���O
    private bool isGround;          // �ڒn�t���O
    private bool Attack;
    private bool AttackEnd;
    private bool Plunge;
    private bool TomatoDead;
    private bool Explosion;
    private float delay;            // �W�����v�̃f�B���C
    private float dis;
    private float Distance = 80.0f;
    
    [SerializeField]
    private float JumpPower;        // �W�����v��

    [SerializeField]
    float MoveSpeed = 2.0f;          // �ړ����x       

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            rb.Resume(gameObject);

            Vector3 pos = rb.position;
            // �v���C���[�Ƃ�X���Ԃ̋��������߂�
            PlayerPosX = Player.transform.position - new Vector3(0.0f, Player.transform.position.y, Player.transform.position.z);
            TomatoPosX = transform.position - new Vector3(0.0f, transform.position.y, transform.position.z);
            dis = Vector3.Distance(PlayerPosX, TomatoPosX);

            // �v���C���[����������U���J�n
            if (ED.isAlive && !TomatoDead)
            {
                if (!isGround)
                {
                    // �v���C���[�Ɍ������ē��U����
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);
                    float step = MoveSpeed * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                    if(Attack)
                    {
                        // ��]������
                        if (look)
                        {
                            transform.Rotate(new Vector3(-8.2f, 0.0f, 0.0f), Space.Self);
                        }
                        if (!look)
                        {
                            transform.Rotate(new Vector3(8.2f, 0.0f, 0.0f), Space.Self);
                        }
                        AttackEnd = true;
                    }
                }

                // �h���U��
                if (Plunge)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.7f)
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

                if (Target.position.x < transform.position.x && look)   // �v���C���[�̂ق�����������
                {
                    transform.Rotate(0, -180, 0);
                    look = false;
                }
                if (Target.position.x > transform.position.x && !look)  // �v���C���[�̂ق�����������
                {
                    transform.Rotate(0, 180, 0);
                    look = true;
                }

                // ���˂鏈��
                if (isGround && !Plunge)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                }

                // �㏸���x&�������x����
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }
            }

            if (TomatoDead)
            {
                float step = 100.0f * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                // ��]������
                if (look)
                {
                    transform.Rotate(new Vector3(-20.0f, 0.0f, 0.0f), Space.Self);
                }
                if (!look)
                {
                    transform.Rotate(new Vector3(20.0f, 0.0f, 0.0f), Space.Self);
                }
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
            if (dis <= Distance)
            {
                Attack = true;
            }
            if (AttackEnd)
            {
                Plunge = true;
            }
            if (Explosion)
            {
                SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
                Destroy(gameObject, 0.0f);
            }
        }

        // �v���C���[�ɓ��������������
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
            Destroy(gameObject, 0.0f);
        }
    }
}
