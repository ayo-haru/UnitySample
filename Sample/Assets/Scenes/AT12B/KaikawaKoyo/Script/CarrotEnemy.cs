//==========================================================
//      �j���W���G���̍U��
//      �쐬���@2022/03/16
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarrotEnemy : MonoBehaviour
{
    GameObject Player;
    private GameObject Jet;
    private Rigidbody rb;
    private Vector3 vec;
    private Vector3 EffectPos;
    private EnemyDown ED;
    private Quaternion look;
    private ParticleSystem effect;

    [SerializeField]
    float MoveSpeed;
    float MovingSpeed;
    private float Timer;
    private float idlingTime = 0.5f;
    private float AttackTime = 0.5f;
    bool start = true;
    bool IdringFlg = true;
    bool InArea = true;
    bool Attack;
    //bool pause = false;

    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Jet = transform.Find("JetPos").gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        MovingSpeed = MoveSpeed / 2;
        transform.DOShakePosition(duration: idlingTime, strength: 5.0f);    // �Ԃ�Ԃ�k�킹��
        //effect = Instantiate(EffectData.EF[0]);
        //effect.gameObject.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        //effect.Play();
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);

            // �W�F�b�g�̃G�t�F�N�g�o����
            //effect.transform.position = Jet.transform.position;

            // �v���C���[����������U���J�n
            if (ED.isAlive)
            {
                // �A�C�h�����O�����
                if(IdringFlg)
                {
                    Idling();
                }
                // �U������
                if (Attack)
                {
                    start = false;
                    // �v���C���[�̂ق�������
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;

                    // �T�E���h����
                    if (!isCalledOnce)     // ��񂾂��Ă�
                    {
                        SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                        isCalledOnce = true;
                    }

                    // ����
                    rb.velocity = vec * MovingSpeed;
                    if(MoveSpeed >= MovingSpeed)
                    {
                        MovingSpeed += 3.0f;
                    }
                    else
                    {
                        Attack = false;

                    }
                }
                //if (pause)
                //{
                //    rb.velocity = vec * MoveSpeed;
                //    pause = false;
                //}

                // ���ȏ㗣�ꂽ���]���Ă���ēx�U������
                if (!InArea)
                {
                    // ���邭���]������
                    rb.constraints = RigidbodyConstraints.FreezePosition |
                        RigidbodyConstraints.FreezeRotationX |
                        RigidbodyConstraints.FreezeRotationY;
                    Timer += Time.deltaTime;
                    transform.Rotate(new Vector3(30.0f, 0.0f, 0.0f), Space.Self);
                    if (Timer > 0.5f)
                    {
                        // �v���C���[�̍��W�̎擾
                        vec = (Player.transform.position - transform.position).normalized;
                        // ���W�̌Œ�
                        rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                  RigidbodyConstraints.FreezeRotationX |
                                  RigidbodyConstraints.FreezeRotationY;
                        // �U���J�n
                        Attack = true;
                        InArea = true;
                        // �^�C�}�[�̃��Z�b�g
                        Timer = 0.0f;
                    }
                }
            }
        }
        else
        {
            rb.Pause(gameObject);
            //pause = true;
        }
    }

    // �A�C�h�����O����
    private void Idling()
    {
        if(start)
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
            // �T�E���h�t���O�ؑ�
            isCalledOnce = false;
            // �A�C�h�����O�I��
            IdringFlg = false;
            // �^�C�}�[�̃��Z�b�g
            Timer = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Pause.isPause)
        {
            if (other.CompareTag("Player"))
            {
                InArea = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(effect.gameObject, 0.0f);
            Destroy(gameObject, 0.0f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �v���C���[�̍��W�̎擾
            vec = (Player.transform.position - transform.position).normalized;
            // �A�C�h�����O�J�n
            IdringFlg = true;
        }
    }

}