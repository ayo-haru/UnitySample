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
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private Vector3 startPosition, targetPosition;
    private Vector3 vec;
    private EnemyDown ED;
    private Vector3 aim;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    float speed = 0.0f;
    bool InArea;
    bool Look;
    bool Attack = false;

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
                vec = (Player.transform.position - transform.position).normalized;
                if (!Attack)
                {
                    rb.velocity = vec * MoveSpeed;
                    Attack = true;
                }

                aim = targetPosition - transform.position;
                look = Quaternion.LookRotation(aim);
                transform.localRotation = look;

                //transform.Rotate(90, 0, 0);
                if (!isCalledOnce)     // ��񂾂��Ă�
                {
                    //SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
            }

            //if (Attack)
            //{
            //    Destroy(gameObject, 5.0f);
            //}
        }
        
    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player") && Look == false)
        {
            Target = Player.transform;          // �v���C���[�̍��W�擾
            targetPosition = Target.position;
            startPosition = rb.position;
            speed = 0.0f;
            InArea = true;
            Look = true;
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