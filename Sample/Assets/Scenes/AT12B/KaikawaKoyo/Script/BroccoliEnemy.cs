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
    float MoveSpeed = 5.0f;
    //[SerializeField]
    //float SwingSpeed;
    
    bool InArea = false;

    private bool isCalledOnce = false;                             // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        rb.centerOfMass = new Vector3(0, -1, 0);
        //transform.Rotate(new Vector3(0, 0, 15));
    }

    private void Update()
    {
        if(!Pause.isPause)
        {
            rot = transform.rotation;
            // �v���C���[����������U���J�n
            if (InArea && ED.isAlive)
            {
                Vector3 pos = rb.position;
                // �v���C���[�Ɍ������ē��U����
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, Target.position, step);

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
            rb.constraints = RigidbodyConstraints.FreezePositionY;
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
