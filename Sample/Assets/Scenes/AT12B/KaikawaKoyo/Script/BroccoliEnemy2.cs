//==========================================================
//      �u���b�R���[�G��2�̍U��
//      �쐬���@2022/04/15
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/15      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliEnemy2 : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Quaternion rot;
    private Rigidbody rb;
    private EnemyDown ED;
    //private bool loop = false;

    [SerializeField]
    float MoveSpeed = 5.0f;
    private float PosY;
    private float Jump = 500.0f;
    private bool jump = false;
    private float delay;

    bool InArea = false;
    private bool look = false;

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
        if (!Pause.isPause)
        {
            rot = transform.rotation;

            PosY = transform.position.y + 6.0f;
            // �v���C���[����������U���J�n
            if (InArea && ED.isAlive)
            {
                Vector3 pos = rb.position;
                // �v���C���[�Ɍ������ē��U����
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, Target.position, step);

                // �v���C���[�̃W�����v�ɍ��킹�ăW�����v����
                //transform.position = new Vector3(0.0f, Target.position.y, 0.0f);

                if (Target.position.y > PosY && !jump)
                {
                    delay += Time.deltaTime;
                    if (delay > 0.3f)
                    {
                        rb.AddForce(transform.up * Jump, ForceMode.Force);
                        jump = true;
                        delay = 0.0f;
                    }
                        
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
            jump = false;
        }
    }

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
