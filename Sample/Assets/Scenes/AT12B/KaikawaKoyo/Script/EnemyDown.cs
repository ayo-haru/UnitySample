//==========================================================
//      �G���G�̒e���ꂽ�Ƃ�
//      �쐬���@2022/03/20
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/20
//      2022/05/03  �q�b�g�X�g�b�v���o�ǉ�-�g��
//      2022/05/18  ��ʂŒ��˕Ԃ������́AUI�U���̐U���ǉ�
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyDown : MonoBehaviour
{
    [SerializeField]
    private GameObject Item;
    private GameObject Player;
    private Camera maincam;
    private Vector3 Pos;
    private Vector3 vec;
    private Vector3 Pvec;           // �|�[�Y���̑��x�i�[�p
    private Vector3 CamRightTop;    // �J�����̉E����W
    private Vector3 CamLeftBot;     // �J�����̍������W
    private Vector3 InAngle;        // ���ˊp
    private Vector3 ReAngle;        // ���ˊp
    private Vector3 inNormalU;      // �@���x�N�g����
    private Vector3 inNormalD;      // �@���x�N�g����
    private Vector3 inNormalR;      // �@���x�N�g���E
    private Vector3 inNormalL;      // �@���x�N�g����
    private Animator animator;

    [SerializeField]
    private int DropRate;           // �񕜃A�C�e���̃h���b�v��

    [SerializeField]
    private int EnemyNumber;        // �G����
    private int Drop;
    private int reflect;
    private bool ItemDrop = false;
    public bool isAlive;
    private bool Reflect;
    private bool pause;
    float Timer;
    float DeadTime = 1.5f;
    private float bouncePower = 300.0f;
    private float speed;
    private float dis;
    private float CamZ;

    Rigidbody rb;

    //---�f�B�]���u�����̂��߂̒ǋL(2022/04/28.�g��)
    Dissolve _dissolve;
    private bool isCalledOnce = false;      // Update���ň�񂾂��������s�������̂�bool�^�̕ϐ���p��
    private bool FinDissolve = false;       // Dissolve�}�e���A���ɍ����ւ��鏈�����I�������Ƃ𔻒肷��

    //---�q�b�g�X�g�b�v���o(2022/05/02.�g��)
    Player2 player2;
    public float Width = 0.1f;
    public int   RoundCnt = 4;
    public float Duration = 0.23f;

    //---UI�U���p
    private HPManager ui;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
        _dissolve = this.GetComponent<Dissolve>();
        player2 = Player.GetComponent<Player2>();
        CamZ = Camera.main.transform.position.z;
        // �@���x�N�g����`
        inNormalU = transform.up;
        inNormalD = -transform.up;
        inNormalR = -transform.right;
        inNormalL = transform.right;

        //ui�擾
        ui = GameObject.Find("HPSystem(2)(Clone)").GetComponent<HPManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            // �|�[�Y�������ꂽ�Ƃ�
            if (pause)
            {
                if(isCalledOnce)
                {
                    _dissolve.Invoke("Play", 0.0f);
                }
                rb.Resume(gameObject);
                rb.velocity = Pvec;
                pause = false;
            }

            // �����Ă鎞�̏���
            if (isAlive)
            {
                // �A�j���[�V�����͏�ɍĐ�
                animator.speed = 1.0f;

                // ��ʊO�̓G�͏�������
                dis = Vector3.Distance(transform.position, Player.transform.position);
                if (dis >= 200.0f)
                {
                    Destroy(gameObject, 0.0f);
                }
            }

            // ���Ԃŏ����鏈��
            if (!isAlive)
            {
                // �^�C�}�[�N��
                Timer += Time.deltaTime;
                // �G�̑��x���擾
                speed = rb.velocity.magnitude;

                // �J�����̒[�̍��W�擾
                CamRightTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, CamZ));
                CamLeftBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, CamZ));

                // ��]������
                if (Player.transform.position.x < transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);
                }
                if (Player.transform.position.x > transform.position.x)
                {
                    rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
                }

                // ��ʒ[�Œ��˕Ԃ�����
                // �E�[
                if (transform.position.x >= CamRightTop.x && !Reflect)
                {
                    reflect = 1;//Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalL);
                        rb.velocity = ReAngle;
                    }
                    else if(reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalL);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui�U��
                    ui.Vibration();
                }
                else if(transform.position.x < CamRightTop.x && Reflect)
                {
                    Reflect = false;
                }
                // ���[
                if (transform.position.x <= CamLeftBot.x && !Reflect)
                {
                    reflect = Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalR);
                        rb.velocity = ReAngle;
                    }
                    else if (reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalR);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui�U��
                    ui.Vibration();
                }
                else if (transform.position.x > CamLeftBot.x && Reflect)
                {
                    Reflect = false;
                }
                // ��[
                if (transform.position.y >= CamRightTop.y && !Reflect)
                {
                    reflect = Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalD);
                        rb.velocity = ReAngle;
                    }
                    else if (reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalD);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui�U��
                    ui.Vibration();
                }
                else if (transform.position.y < CamRightTop.y && Reflect)
                {
                    Reflect = false;
                }
                // ���[
                if (transform.position.y <= CamLeftBot.y && !Reflect)
                {
                    reflect = Random.Range(0, 2);
                    if (reflect == 0)
                    {
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalU);
                        rb.velocity = ReAngle;
                    }
                    else if (reflect == 1)
                    {
                        //maincam = Camera.main;
                        //vec = (maincam.transform.position - transform.position).normalized;
                        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                         RigidbodyConstraints.FreezeRotationY;
                        InAngle = rb.velocity;
                        ReAngle = Vector3.Reflect(InAngle, inNormalU);
                        rb.velocity = ReAngle - new Vector3(0.0f, 0.0f, 100.0f);
                    }
                    Reflect = true;
                    //ui�U��
                    ui.Vibration();
                }
                else if (transform.position.y > CamLeftBot.y && Reflect)
                {
                    Reflect = false;
                }

                //---�f�B�]���}�e���A���ɕύX
                if (!isCalledOnce)
                {
                    if (EnemyNumber == 1 || EnemyNumber == 0 || EnemyNumber == 4 || EnemyNumber == 3)
                    {
                        _dissolve.Invoke("Play", 0.2f);
                        isCalledOnce = true;
                        FinDissolve = true;
                    }
                }
                // ���Ԃ��o����������B�g�}�g�̓X�s�[�h�����Ȃ������_�ŏ������Ⴂ����
                if (Timer > DeadTime || (EnemyNumber == 2 || EnemyNumber == 5) && speed <= 0.5f)
                {
                    Pos = transform.position;
                    Destroy(gameObject, 0.0f);
                    if (EnemyNumber == 2 || EnemyNumber == 5)
                    {
                        EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
                    }
                    EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_DEATH, Pos, 2.0f);
                }
            }
            Pvec = rb.velocity;
        }
        else
        {
            if (isCalledOnce)
            {
                _dissolve.Invoke("Stop", 0.0f);
            }
            rb.Pause(gameObject);
            animator.speed = 0.0f;
            pause = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!Pause.isPause)
        {
            // �e���ꂽ��x�N�g�����v�Z���Ċ֐����Ăяo��
            if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
            {
                vec = (transform.position - Player.transform.position).normalized;

                // ���C���[�ύX
                if (EnemyNumber == 2)
                {
                    gameObject.layer = LayerMask.NameToLayer("TomatoDown");
                }
                gameObject.layer = LayerMask.NameToLayer("DownEnemy");

                //---�q�b�g�X�g�b�v���o
                var seq = DOTween.Sequence();
                //---Enemy�̐U�����o
                seq.Append(transform.DOShakePosition(player2.HitStopTime,1f,100,fadeOut:false));

                //EnemyDead(vec , Player.transform.position.x);
                //---���̃^�C�~���O�ŃG�l�~�[�̎��S�������Ăяo��
                seq.AppendCallback(() => EnemyDead(vec));
                Shake(0.1f, 5, 0.23f);
            }

            // �g�}�g���ق��̓G�ɓ��������甚������
            if (collision.gameObject.CompareTag("Enemy") && !isAlive && (EnemyNumber == 2 || EnemyNumber == 5))
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB, transform.position, 0.9f);
            }
        }
    }

    // ���ʎ��̏���
    public void EnemyDead(Vector3 vec)
    {
        // �A�j���[�V�������~�߂�
        animator.speed = 0;
        // �d�͂�����
        rb.useGravity = false;
        // ��C��R���[����
        rb.angularDrag = 0.0f;

        // ��]����ύX
        if (EnemyNumber == 1 || EnemyNumber == 4)
        {
            rb.centerOfMass = new Vector3(0.0f, 5.0f, 2.0f);
        }
        else if (EnemyNumber == 2 || EnemyNumber == 5)
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

        SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);

        isAlive = false;
    }
    /// <summary>
    /// �J�����U�����o
    /// </summary>
    /// <param name="width"></param>    �J�����̐U�ꕝ
    /// <param name="cnt"></param>      ������
    /// <param name="duration"></param> ����
    public void Shake(float width,int cnt,float duration)
    {
        var camera = Camera.main.transform;
        var seq = DOTween.Sequence();

        var partDuration = duration / cnt / 2f;

        var widthHalf = width / 2f;

        for(int i = 0; i < cnt - 1; i++)
        {
            seq.Append(camera.DOLocalRotate(new Vector3(-width,0f),partDuration));
            seq.Append(camera.DOLocalRotate(new Vector3( width,0f),partDuration));
        }

        seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf,0f),partDuration));
        seq.Append(camera.DOLocalRotate(Vector3.zero,partDuration));
    }
}
