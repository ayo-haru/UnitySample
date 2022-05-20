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
    private Vector3 PP;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed;
    float MovingSpeed;
    private int AttackPattern;
    private float dis;
    private float Timer;
    private float RandomTime;
    private float rotTime;
    private float idlingTime = 0.5f;
    bool start = true;
    bool IdringFlg = true;
    private bool Attack;
    private bool Attack2;
    bool disFlg;

    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
        MovingSpeed = MoveSpeed / 2;
        transform.DOShakePosition(duration: idlingTime, strength: 5.0f);    // �Ԃ�Ԃ�k�킹��
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
                    // �ŏ������v���C���[�̂ق��������ăv���v������
                    if (start)
                    {
                        // �v���C���[�̂ق���������
                        vec = (Player.transform.position - transform.position).normalized;
                        look = Quaternion.LookRotation(vec);
                        transform.localRotation = look;
                    }
                    else
                    {
                        // ���邭���]������
                        rb.constraints = RigidbodyConstraints.FreezePosition |
                                       RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationY;
                        transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    }
                    // �f�B���C�����Ă���U������
                    Timer += Time.deltaTime;
                    if (Timer >= idlingTime)
                    {
                        // �v���C���[�̍��W�̎擾
                        vec = (Player.transform.position - transform.position).normalized;
                        // ���W�̌Œ�
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                  RigidbodyConstraints.FreezeRotationX |
                                  RigidbodyConstraints.FreezeRotationY;
                        // �U���J�n
                        Attack = true;
                        //AttackPattern = Random.Range(0, 2);
                        // �T�E���h�t���O�ؑ�
                        isCalledOnce = false;
                        // �A�C�h�����O�I��
                        IdringFlg = false;
                        // �^�C�}�[�̃��Z�b�g
                        Timer = 0.0f;
                    }
                }
                if (Attack)
                {
                    start = false;
                    PP = Player.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
                    vec = (PP - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                    RandomTime = Random.Range(0.5f, 1.5f);
                    AttackPattern = 1;
                    Attack = false;
                    //case 2:
                    //    rb.velocity = new Vector3(0.0f, -MoveSpeed, 0.0f);
                    //    vec = ((transform.position -= new Vector3(0.0f, 15.0f, 0.0f)) - transform.position).normalized;
                    //    look = Quaternion.LookRotation(vec);
                    //    transform.localRotation = look;
                    //    Attack = true;
                    //    break;

                }

                if (AttackPattern == 1)
                {
                    Timer += Time.deltaTime;
                }

                if (Timer > RandomTime && AttackPattern == 1)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition |
                                         RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                    rotTime += Time.deltaTime;
                    transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    if (rotTime > 0.5f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                       RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationY;
                        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                        vec = (Player.transform.position - transform.position).normalized;
                        look = Quaternion.LookRotation(vec);
                        transform.localRotation = look;
                        rb.velocity = vec * MoveSpeed;
                        //AttackPattern = 3;
                    }
                }

                //if (AttackPattern == 2)
                //{
                //    if (rb.velocity.y < 0.0f)
                //    {
                //        rb.velocity += new Vector3(0.0f, 3.0f, 0.0f);
                //    }
                //    if (transform.position.x > Player.transform.position.x && rb.velocity.x > -MoveSpeed)
                //    {
                //        rb.velocity -= new Vector3(3.0f, 0.0f, 0.0f);
                //        if (rb.velocity.x <= -MoveSpeed)
                //        {
                //            vec = (Player.transform.position - transform.position).normalized;
                //            look = Quaternion.LookRotation(vec);
                //            transform.localRotation = look;
                //            rb.velocity = vec * MoveSpeed;
                //            AttackPattern = 3;
                //        }
                //    }
                //    if (transform.position.x < Player.transform.position.x && rb.velocity.x < MoveSpeed)
                //    {
                //        rb.velocity += new Vector3(3.0f, 0.0f, 0.0f);
                //        if (rb.velocity.x >= MoveSpeed)
                //        {
                //            vec = (Player.transform.position - transform.position).normalized;
                //            look = Quaternion.LookRotation(vec);
                //            transform.localRotation = look;
                //            rb.velocity = vec * MoveSpeed;
                //            AttackPattern = 3;
                //        }
                //    }
                //}
                
                //// �v���C���[�Ƃ̋������v�Z
                //dis = Vector3.Distance(transform.position, Player.transform.position);
                //// ��苗�����ꂽ��ēx�A�C�h�����O���U��
                //if (dis >= 70.0f && !disFlg)
                //{
                //    IdringFlg = true;
                //    disFlg = true;
                //}
                //else if (dis < 60.0f && disFlg)
                //{
                //    disFlg = false;
                //}
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
