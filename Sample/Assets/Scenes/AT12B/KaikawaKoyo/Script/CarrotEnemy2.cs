//==========================================================
//      �j���W���G������̍U��
//      �쐬���@2022/04/16
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarrotEnemy2 : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 position;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed;
    private int AttackPattern;
    private float angle = 5;
    private float Timer;
    private float idlingTime = 0.5f;
    private bool IdringFlg = true;
    private bool Attack;
    private bool R;

    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
        transform.DOShakePosition(duration: idlingTime, strength: 5.0f);    // �Ԃ�Ԃ�k�킹��
        if(transform.position.x > Player.transform.position.x)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(-180.0f, 0.0f, 0.0f));    // ������ς���
            position = transform.position + new Vector3(Player.transform.position.x  + -20.0f, 15.0f, 0.0f);     // ��]�̒��S�_�����߂�
            R = false;
        }
        if (transform.position.x < Player.transform.position.x)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(180.0f, 0.0f, 0.0f));     // ������ς���
            position = transform.position + new Vector3(Player.transform.position.x + 20.0f, 15.0f, 0.0f);     // ��]�̒��S�_�����߂�
            R = true;
        }

    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            // �v���C���[����������U���J�n
            if (ED.isAlive)
            {
                // �A�C�h�����O�����
                if (IdringFlg)
                {
                    // �f�B���C�����Ă���U������
                    Timer += Time.deltaTime;
                    if (Timer >= idlingTime)
                    {
                        if(R)
                        {
                            rb.velocity = new Vector3(80.0f, 0.0f, 0.0f);
                            if (transform.position.x < position.x)
                            {
                                // �U���J�n
                                Attack = true;
                                // �T�E���h�t���O�ؑ�
                                isCalledOnce = false;
                                // �A�C�h�����O�I��
                                IdringFlg = false;
                                // �^�C�}�[�̃��Z�b�g
                                Timer = 0.0f;
                            }
                        }
                    }
                }
                if (Attack)
                {
                    // ������Ɖ��
                    transform.RotateAround(
                        position,
                        Vector3.back,
                        angle);
                    
                    if (transform.position.x > position.x && transform.position.y < position.y)
                    {
                        AttackPattern = 1;
                        Attack = false;
                    }
                }

                // �ǔ��J�n
                if (AttackPattern == 1)
                {
                    vec = (Player.transform.position - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
        if ((collision.gameObject.CompareTag("Default") || collision.gameObject.CompareTag("Ground")) && !IdringFlg)
        {
            // �A�C�h�����O�J�n
            IdringFlg = true;
        }
    }
}
