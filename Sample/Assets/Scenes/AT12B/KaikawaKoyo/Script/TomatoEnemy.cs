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
    //private float distance;
    private EnemyDown ED;
    //private Vector3 aim;
    private bool look;              // �v���C���[�̂ق�������t���O
    private bool isGround;          // �ڒn�t���O
    private float delay;            // �W�����v�̃f�B���C

    [SerializeField]
    private float JumpPower;        // �W�����v��

    [SerializeField]
    float MoveSpeed = 2.0f;          // �ړ����x
    //int DetecDist = 8;
    bool InArea = false;                

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        //distance = 1.0f;
        ED = GetComponent<EnemyDown>();
        transform.Rotate(0, -90, 0);     
        look = false;
        rb.velocity += new Vector3(0.0f, -0.5f, 0.0f);
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            print(rb.velocity.y);

            // �v���C���[����������U���J�n
            if (InArea && ED.isAlive)
            {
                Vector3 pos = rb.position;
                if (!isGround)
                {
                    // �v���C���[�Ɍ������ē��U����
                    float step = MoveSpeed * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(pos, Target.position, step);
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
                if (isGround)
                {
                    delay += Time.deltaTime;
                    if(delay > 0.3f)
                    {
                        rb.velocity = new Vector3(0.0f, JumpPower, 0.0f);
                        //SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);
                        isGround = false;
                        delay = 0.0f;
                    }
                }

                // �㏸���x&�������x����
                if (!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -0.8f, 0.0f);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ڒn����
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

        // �v���C���[�ɓ��������������
        if (collision.gameObject.CompareTag("Player"))
        {
            //SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
            Destroy(gameObject, 0.0f);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    �ڒn����
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGround = false;
    //    }
    //}

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InArea = false;
        }
    }
}
