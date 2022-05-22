//==========================================================
//      �u���b�R���[�G������̍U��
//      �쐬���@2022/04/15
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/15      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliEnemy2 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Vector3 TargetPos;
    private Rigidbody rb;
    private EnemyDown ED;

    [SerializeField]
    private float MoveSpeed = 5.0f;
    [SerializeField]
    private float JumpPower;
    private float PosY;
    private bool jump = false;
    private float delay;
    private float InvincibleTime = 2.0f;
    private float DamageTime;
    private bool isGround = false;
    private bool Invincible = false;

    private bool isCalledOnce = false;                             // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

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
            if (Invincible)
            {
                gameObject.layer = LayerMask.NameToLayer("Invincible");
                transform.Rotate(0, 0, 0);
                DamageTime += Time.deltaTime;
                if (DamageTime > InvincibleTime)
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    DamageTime = 0.0f;
                    Invincible = false;
                }
            }

            // �v���C���[����������U���J�n
            if (ED.isAlive)
            {
                Vector3 pos = rb.position;

                // �v���C���[�Ɍ������ē��U����
                float a = Player.transform.position.y - transform.position.y;
                TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);

                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, TargetPos, step);

                // �v���C���[���W�����v������W�����v����
                PosY = transform.position.y + 10.0f;
                if (Target.position.y > PosY && !jump)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f)
                    {
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        jump = true;
                        delay = 0.0f;
                    }
                }

                if (Target.position.x < transform.position.x)   // �v���C���[�̂ق�����������
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(-180, 0, 0));
                }
                if (Target.position.x > transform.position.x)  // �v���C���[�̂ق�����������
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(180, 0, 0));
                }

                // ��������
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -1.0f, 0.0f);
                }

                // SE�̏���
                if (!isCalledOnce)     // ��񂾂��Ă�
                {
                    SoundManager.Play(SoundData.eSE.SE_BUROKORI, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ڐG����
        if (collision.gameObject.CompareTag("Player"))
        {
            Invincible = true;
        }

        // �ڒn����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            jump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �ڒn����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}