//==========================================================
//      �j���W���G������̍U��
//      �쐬���@2022/04/04
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/04      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy2 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private Vector3 startPosition, targetPosition;
    private EnemyDown ED;
    private Vector3 aim;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 1.0f;
    float speed = 0.0f;
    bool InArea;
    bool Look;

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
        if (!Pause.isPause)
        {
            // �v���C���[����������U���J�n
            if (InArea && ED.isAlive)
            {
                if (speed <= 1)
                {
                    speed += MoveSpeed * Time.deltaTime;
                }
                // �v���C���[�Ɍ������ē��U����
                rb.position = Vector3.Slerp(startPosition, targetPosition, speed);

                aim = targetPosition - transform.position;
                look = Quaternion.LookRotation(aim);
                transform.localRotation = look;

                //transform.Rotate(90, 0, 0);
                if (!isCalledOnce)     // ��񂾂��Ă�
                {
                    SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
            }

            if (rb.position == targetPosition)
            {
                //transform.Rotate(-90, 0, 0);
                //rb.constraints = RigidbodyConstraints.FreezeRotationX;
                Destroy(gameObject, 1.0f);
            }
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