//==========================================================
//      �G���G�̒e���ꂽ�Ƃ�
//      �쐬���@2022/03/20
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/20
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    [SerializeField]
    GameObject Item;
    GameObject Player;
    private float bouncePower = 200.0f;
    private Vector3 Pos;
    private Vector3 EnemyPos;
    private Vector3 velocity;
    private Vector3 vec;
    private Animator animator;

    [SerializeField]
    private int DropRate;           // �񕜃A�C�e���̃h���b�v��

    [SerializeField]
    private int EnemyNumber;        // �G����
    private int Drop;

    public bool isAlive;

    float DeadTime = 0.0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);
            if(isAlive)
            {
                animator.speed = 1;
            }
            // ���Ԃŏ����鏈��
            if (!isAlive)
            {
                DeadTime += Time.deltaTime;
            }

            if (DeadTime > 1.0f)
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                if(EnemyNumber == 2)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_TOMATOBOMB, transform.position, 0.9f);
                }
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMYDOWN, Pos, 2.0f);
            }
        }
        else
        {
            rb.Pause(gameObject);
            animator.speed = 0;
        }
 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!Pause.isPause)
        {
            rb.Resume(gameObject);
            if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
            {
                //EnemyPos = transform.position + new Vector3(0.0f, 5.0f, 0.0f);
                //print(EnemyPos);
                vec = (Player.transform.position - transform.position).normalized;
                //�v���C���[���t�����ɒ��˕Ԃ�
                collision.rigidbody.AddForce(vec * 5.0f, ForceMode.Impulse);
                // �A�j���[�V�������~�߂�
                animator.speed = 0;
                // �d�͂�����
                rb.useGravity = false;
                // ��C��R���[����
                rb.angularDrag = 0.0f;
                // ���C���[�ύX
                gameObject.layer = LayerMask.NameToLayer("DownEnemy");

                // ��]����ύX
                if (EnemyNumber == 1)
                {
                    rb.centerOfMass = new Vector3(0.0f, 5.0f, 2.0f);
                }
                else if (EnemyNumber == 2)
                {
                    rb.centerOfMass = new Vector3(0.0f, 0.3f, 0.0f);
                }
                else
                {
                    rb.centerOfMass = new Vector3(0.0f, 0.0f, 0.0f);
                }

                // �񕜃A�C�e���𗎂Ƃ�
                Pos = transform.position;
                Drop = Random.Range(0, 100);
                if (Drop < DropRate)
                {
                    Instantiate(Item, Pos, Quaternion.identity);
                }

                //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
                //rb.AddForce(velocity * bouncePower, ForceMode.Force);
                rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY;
                //�v���C���[�Ƌt�����ɒ��˕Ԃ�
                rb.velocity = -vec * bouncePower;
                
                // ��]������
                if(Player.transform.position.x < transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);

                }
                if (Player.transform.position.x > transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
                }
                
                //SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
                
                //�e���������
                isAlive = false;
            }

            //if (!isAlive)
            //{
            //    //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            //    rb.AddForce(velocity * bouncePower, ForceMode.Force);
            //    // ��]������
            //    rb.AddTorque(0.0f, 0.0f, -300.0f);

            //}

            if (isAlive == false && collision.gameObject.CompareTag("Ground"))
            {
                rb.velocity = -vec * bouncePower;
                // �ǁA���ɓ��������������
                //Destroy(gameObject, 0.0f);
            }
        }
        else
        {
            rb.Pause(gameObject);
        }
    }
}
