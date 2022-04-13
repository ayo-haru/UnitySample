using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //��Ԑ��ځA�񋓌^�@
    public enum Boss_State
    {
        idle,                  //�ҋ@(0)
        damage,               //�_���[�W(1)
        strawberryBomb,      //�C�`�S���e(3)
        charge,             //�ːi(4)
        KnifeThrower,      //�i�C�t����(5)
    }

    private Animator animator;
    //�{�X���o�ꂵ���ŏ��̈ʒu
    private Vector3 defaultPos;
    //�{�X�̏��
    static�@private Boss_State BossState = Boss_State.idle;
    //idle��Ԃ̌o�ߎ���
    private float elapsedTimeOfIdleState = 0f;
    //idle��Ԃŗ��܂鎞��
    [SerializeField]
    private float timeToStayInIdle = 24f;
    //���[�V�����̃����_�����I�p�̐�
    private int RandomNumbe = 0;


    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();�A�j���[�V�����ǉ������ꍇ
        defaultPos = transform.position;//�f�t�H���g�̏o���ʒu��transform.position����擾
    }

    // Update is called once per frame
    void Update()
    {
        if (BossState == Boss_State.idle)       //�����{�X�̏�Ԃ��ҋ@�̏ꍇ
        {
            //Idle();
        }
        else if (BossState == Boss_State.damage)//�����{�X�̏�Ԃ��_���[�W�̏ꍇ
        {
            //damage();
        }
        else if (BossState == Boss_State.strawberryBomb)//�����{�X�̏�Ԃ��C�`�S���e�̏ꍇ
        {
            //strawberryBomb();
        }
        else if (BossState == Boss_State.charge)//�����{�X�̏�Ԃ��ːi�̏ꍇ
        {
            //charge();
        }
        else if (BossState == Boss_State.KnifeThrower)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
        {
            //KnifeThrower();
        }
    }

    //�{�X�̏�Ԑ���
    static�@public void SetState(Boss_State bossState, Transform playerTransform = null)
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

        //�@��莞�Ԃ��o�߂�����e��U����Ԃɂ���
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle��Ԃ̌o�ߎ��Ԃ��O���ɂ���

            RandomNumbe = Random.Range(3, 5);//�U���p�^�[�������_����
            switch (RandomNumbe)            //switch����
            {
                case 3://�C�`�S���e��
                    SetState(Boss_State.strawberryBomb);
                    Debug.Log("�C�`�S���e");
                    break;//break��

                case 4://�ːi��
                    SetState(Boss_State.charge);
                    Debug.Log("�ːi�U��");
                    break;//break��

                case 5://�i�C�t�U��
                    //if(HP�Q�[�W�������̏ꍇ)
                    SetState(Boss_State.KnifeThrower);
                    Debug.Log("�i�C�t�U��");
                    break;//break��
            }
        }
    }
}


