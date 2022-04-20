//==========================================================
//      �u���b�R���[�G���̍U��
//      �쐬���@2022/03/18
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/18      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Quaternion rot;
    private Rigidbody rb;
    private EnemyDown ED;
    //private bool loop = false;

    [SerializeField]
    private float MoveSpeed = 5.0f;

    private float InvincibleTime = 2.0f;
    private float DamageTime;
    bool InArea = false;
    private bool look = false;
    private bool isGround = false;
    private bool Invincible = false;

    private bool isCalledOnce = false;                             // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        rb.centerOfMass = new Vector3(0, -1, 0);
        //transform.Rotate(new Vector3(0, 0, 15));
        transform.Rotate(0, -90, 0);
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            rot = transform.rotation;
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
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, Target.position, step);

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

                if(!isGround)
                {
                    rb.velocity += new Vector3(0.0f, -0.5f, 0.0f);
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
        // �ڒn����
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY |
              RigidbodyConstraints.FreezePositionZ |
              RigidbodyConstraints.FreezeRotationX |
              RigidbodyConstraints.FreezeRotationY;
        }

        // �ڐG����
        if (collision.gameObject.CompareTag("Player"))
        {
            Invincible = true;
        }
    }

}
