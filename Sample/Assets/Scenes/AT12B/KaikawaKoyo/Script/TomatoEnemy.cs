//==========================================================
//      �g�}�g�G���̍U��
//      �쐬���@2022/03/17
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/17      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private Vector3 TargetPos;
    private bool isGround;          // �ڒn�t���O
    private float delay;            // �W�����v�̃f�B���C

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
        if(!Pause.isPause)
        {
            // �v���C���[����������U���J�n
            if (ED.isAlive)
            {
                Vector3 pos = rb.position;
                if (!isGround)
                {
                    // �v���C���[�Ɍ������ē��U����
                    float a = Player.transform.position.y - transform.position.y;
                    TargetPos = Target.position - new Vector3(0.0f, a, 0.0f);
                    float step = MoveSpeed * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(pos, TargetPos, step);
                }

                if (Target.position.x < transform.position.x)   // �v���C���[�̂ق�����������
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(-180, 0, 0));
                }
                if (Target.position.x > transform.position.x)  // �v���C���[�̂ق�����������
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(180, 0, 0));
                }

                // ���˂鏈��
                if (isGround)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
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
        }

        // �v���C���[�ɓ��������������
        if (collision.gameObject.CompareTag("Player") && ED.isAlive)
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
            Destroy(gameObject, 0.0f);
        }
    }
}
