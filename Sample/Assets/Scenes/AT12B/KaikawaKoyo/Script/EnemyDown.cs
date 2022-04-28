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
    private GameObject Item;
    private GameObject Player;
    private ParticleSystem TomatoBom;
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
    private bool ItemDrop = false;
    public bool isAlive;
    float DeadTime = 0.0f;

    Rigidbody rb;

    //---�f�B�]���u�����̂��߂̒ǋL(2022/04/28.�g��)
    Dissolve _dissolve;
    private bool isCalledOnce = false;      // Update���ň�񂾂��������s�������̂�bool�^�̕ϐ���p��
    private bool FinDissolve = false;       // Dissolve�}�e���A���ɍ����ւ��鏈�����I�������Ƃ𔻒肷��

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
        _dissolve = this.GetComponent<Dissolve>();
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

                //---�f�B�]���}�e���A���ɕύX
                if (!isCalledOnce)
                {
                    if (EnemyNumber == 1 || EnemyNumber == 0)
                    {
                        _dissolve.Invoke("Play", 0.2f);
                        isCalledOnce = true;
                        FinDissolve = true;
                    }
                }
            }

            if (DeadTime > 1.5f)
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                if (EnemyNumber == 2)
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
            // �e���ꂽ��x�N�g�����v�Z���Ċ֐����Ăяo��
            if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
            {
                vec = (transform.position -Player.transform.position).normalized;
                EnemyDead(vec , Player.transform.position.x);
            }
        }
        else
        {
            rb.Pause(gameObject);
        }
    }

    // ���ʎ��̏���
    public void EnemyDead(Vector3 vec , float x)
    {
        if (!Pause.isPause)
        {
            rb.Resume(gameObject);
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
            if (Drop < DropRate && !ItemDrop)
            {
                Instantiate(Item, Pos, Quaternion.identity);
                ItemDrop = true;
            }

            //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            //rb.AddForce(velocity * bouncePower, ForceMode.Force);
            rb.constraints = RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY;
            //�v���C���[�Ƌt�����ɒ��˕Ԃ�
            rb.velocity = vec * bouncePower;

            // ��]������
            if (x < transform.position.x)
            {
                rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);

            }
            if (x > transform.position.x)
            {
                rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
            }

            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
            
            isAlive = false;
        }
        else
        {
            rb.Pause(gameObject);
        }
       
    }
}
