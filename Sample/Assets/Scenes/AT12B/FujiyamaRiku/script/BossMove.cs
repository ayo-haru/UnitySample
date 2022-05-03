using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //��Ԑ��ځA�񋓌^�@
    public enum Boss_State
    {
        idle,                  //�ҋ@(0)
        damage,               //�_���[�W(1)
        strawberryBomb,      //�C�`�S���e(3)
        charge,             //�ːi(4)
        KnifeThrower,      //�i�C�t����(5)
        Rain,             //����J(6)
    }

    private Animator animator;
    //�{�X���o�ꂵ���ŏ��̈ʒu
    private Vector3 defaultPos;
    //�{�X�̏��
    static private Boss_State BossState = Boss_State.idle;
    //idle��Ԃ̌o�ߎ���
    private float elapsedTimeOfIdleState = 0f;
    //idle��Ԃŗ��܂鎞��
    [SerializeField]
    private float timeToStayInIdle = 24f;
    //���[�V�����̃����_�����I�p�̐�
    private int RandomNumbe = 0;

    //�U�����J�E���g
    public static int AttackCount = 0;
    //�ҋ@���[�V�����Ȃ��̍U���񐔁iHP50���ȉ��̂݁j
    [SerializeField]
    private int MaxAttack = 2;
    //�K�E�Z�p�t���O(�Z%�ȉ��ɂȂ����Ƃ����)
    private bool UltFlg;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();�A�j���[�V�����ǉ������ꍇ
        defaultPos = transform.position;//�f�t�H���g�̏o���ʒu��transform.position����擾
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            if (Boss1Manager.BossState == Boss1Manager.Boss1State.BOSS1_BATTLE)
            {
                if (BossState == Boss_State.idle)       //�����{�X�̏�Ԃ��ҋ@�̏ꍇ
                {
                    Idle();
                }
                else if (BossState == Boss_State.damage)//�����{�X�̏�Ԃ��_���[�W�̏ꍇ
                {
                    //damage();
                }
                else if (BossState == Boss_State.strawberryBomb)//�����{�X�̏�Ԃ��C�`�S���e�̏ꍇ
                {
                    //strawberryBomb();
                    this.GetComponent<Boss1Attack>().BossStrawberry.Boss1Strawberry();
                }
                else if (BossState == Boss_State.charge)//�����{�X�̏�Ԃ��ːi�̏ꍇ
                {
                    //charge();
                    Boss1Attack BossAttack;
                    BossAttack = this.GetComponent<Boss1Attack>();
                    BossAttack.BossRush.Boss1Fork();
                    //this.GetComponent<Boss1Attack>().BossRush.Boss1Fork();
                }
                else if (BossState == Boss_State.KnifeThrower)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
                {
                    //KnifeThrower();
                    this.GetComponent<Boss1Attack>().BossKnife.Boss1Knife();
                }
                else if(BossState == Boss_State.Rain)
                {
                    this.GetComponent<Boss1Attack>().BossRain.BossRain();
                }
            }
        }
    }

    //�{�X�̏�Ԑ���
    static public void SetState(Boss_State bossState, Transform playerTransform = null)
    {
        BossState = bossState;
        if (BossState == Boss_State.idle)
        {
            Debug.Log("�A�C�h��");
        }
        else if (BossState == Boss_State.damage)
        {
            Debug.Log("�_���[�W");
        }
        else if (BossState == Boss_State.strawberryBomb)
        {
            Debug.Log("�ʏ�U��");
        }
        else if (BossState == Boss_State.charge)
        {
            Debug.Log("�Ռ��g�U��");
        }
        else if (BossState == Boss_State.KnifeThrower)
        {
            Debug.Log("�`�F�C�X");
        }
    }

    //�@��Ԏ擾
    public Boss_State GetState()
    {
        return BossState;
    }

    //�ҋ@���[�V����
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);
        if (AttackCount == MaxAttack)
        {
            //SetState(Boss_State.idle);//�ҋ@���[�V����
            elapsedTimeOfIdleState = timeToStayInIdle;
            AttackCount = 0;          //�U�����J�E���g���[����
            Debug.Log("�A�C�h��??");
        }
        //�@��莞�Ԃ��o�߂�����e��U����Ԃɂ���
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle��Ԃ̌o�ߎ��Ԃ�off�ɂ���
            Debug.Log("AttackCount�F" + AttackCount);

            //�����_�����̐�����switch��������̒���
            if(!UltFlg && HPgage.currentHp <= 30)
            {
                UltFlg = true;
                SetState(Boss_State.Rain);
                return;
            }
            if (HPgage.currentHp >= 51)
            {
                RandomNumbe = Random.Range(1, 3);//�U���p�^�[�������_����
                Debug.Log("Random" + RandomNumbe);
            }
            else
            {
                RandomNumbe = Random.Range(1, 4);//�U���p�^�[�������_����
                Debug.Log("Random" + RandomNumbe);
            }
            switch (RandomNumbe)            //switch����
                {
                    case 1://�C�`�S���e��
                        SetState(Boss_State.strawberryBomb);
                        RandomNumbe = -1;
                        Debug.Log("�C�`�S���e");
                        break;//break��

                    case 2://�ːi��
                        SetState(Boss_State.charge);
                        RandomNumbe = -1;
                        Debug.Log("�ːi�U��");
                        break;//break��

                    case 3://�i�C�t�U��
                            SetState(Boss_State.KnifeThrower);
                            RandomNumbe = -1;
                        Debug.Log("�i�C�t�U��");
                        break;//break��

                    case 4://�J�U��
                    SetState(Boss_State.Rain);
                    RandomNumbe = -1;
                    Debug.Log("�J�U��");
                    break;

                }
            
            
        }
    }
}

