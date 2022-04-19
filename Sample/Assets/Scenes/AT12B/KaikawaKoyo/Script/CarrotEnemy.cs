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

public class CarrotEnemy : MonoBehaviour
{
    GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;
    private EnemyDown ED;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    private float InvincibleTime;
    bool InArea;
    bool Look;
    bool Attack = false;
    private bool Invincible = false;

    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        Look = false;
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            // �v���C���[����������U���J�n
            if (InArea && ED.isAlive)
            {
                if (Invincible)
                {
                    gameObject.layer = LayerMask.NameToLayer("Invincible");
                    InvincibleTime += Time.deltaTime;
                }
                if (InvincibleTime > 2.0f)
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    Invincible = false;
                }
                if (!Attack)
                {
                    vec = (Player.transform.position - transform.position).normalized;
                    look = Quaternion.LookRotation(vec);
                    transform.localRotation = look;
                    rb.velocity = vec * MoveSpeed;
                    Attack = true;
                }

                if (!isCalledOnce)     // ��񂾂��Ă�
                {
                    //SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
            }

            if (Attack)
            {
                Destroy(gameObject, 2.0f);
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player") && Look == false)
        {
            InArea = true;
            Look = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invincible = true;
            //Destroy(gameObject, 0.0f);
        }
    }

}