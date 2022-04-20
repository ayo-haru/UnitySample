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

public class CarrotEnemy2 : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 PP;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    private int AttackPattern = 1;
    private float AttackTime;
    private float RandomTime;
    private float rotTime;
    private bool InArea;
    private bool Look;
    private bool Attack = false;
    private bool Attack2 = false;
    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        Look = false;
        ED = GetComponent<EnemyDown>();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Update()
    {
        if (!Pause.isPause)
        {
            // �v���C���[����������U���J�n
            if (InArea && ED.isAlive)
            {
                if (!Attack)
                {
                    switch(AttackPattern)
                    {
                        case 0:
                            vec = (Player.transform.position - transform.position).normalized;
                            look = Quaternion.LookRotation(vec);
                            transform.localRotation = look;
                            rb.velocity = vec * MoveSpeed;
                            Attack = true;
                            break;
                        case 1:
                            PP = Player.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
                            vec = (PP - transform.position).normalized;
                            look = Quaternion.LookRotation(vec);
                            transform.localRotation = look;
                            rb.velocity = vec * MoveSpeed;
                            RandomTime = Random.Range(0.0f, 1.5f);
                            Attack = true;
                            break;
                        case 2:

                            break;

                    }
                }

                if (AttackTime > RandomTime && !Attack2)
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
                        Attack2 = true;
                    }
                }

                if (!isCalledOnce)     // ��񂾂��Ă�
                {
                    //SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }

                if (Attack)
                {
                    AttackTime += Time.deltaTime;
                    Destroy(gameObject, 3.0f);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player") && Look == false)
        {
            InArea = true;
            Look = true;
            //AttackPattern = Random.Range(0, 4);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
    }
}
