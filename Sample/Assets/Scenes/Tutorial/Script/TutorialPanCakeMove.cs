using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanCakeMove : MonoBehaviour
{
    //��Ԑ��ځA�񋓌^�@
    public enum Boss_State {
        idle,                  //�ҋ@(0)
        damage,               //�_���[�W(1)
        strawberryBomb,      //�C�`�S���e(3)
        charge,             //�ːi(4)
        Jump,              //�W�����v
        KnifeThrower,     //�i�C�t����(5)
        Rain,            //����J(6)

    }

    private Boss1Attack BossAttack;

    private Animator animator;
    //�{�X���o�ꂵ���ŏ��̈ʒu
    private Vector3 defaultPos;
    //�{�X�̏��
    static private Boss_State BossState;
    //idle��Ԃ̌o�ߎ���
    private float elapsedTimeOfIdleState = 0f;
    //idle��Ԃŗ��܂鎞��
    [SerializeField]
    private float timeToStayInIdle = 24f;


    // Start is called before the first frame update
    void Start() {
        //animator = GetComponent<Animator>();�A�j���[�V�����ǉ������ꍇ
        defaultPos = transform.position;//�f�t�H���g�̏o���ʒu��transform.position����擾
        BossAttack = this.GetComponent<Boss1Attack>();
        BossState = Boss_State.idle;
    }

    // Update is called once per frame
    void Update() {
        if (!Pause.isPause)
        {
            if (Boss1Manager.BossState == Boss1Manager.Boss1State.BOSS1_BATTLE)
            {
                if (BossState == Boss_State.idle)       //�����{�X�̏�Ԃ��ҋ@�̏ꍇ
                {
                    Idle();
                }
                else if (BossState == Boss_State.charge)//�����{�X�̏�Ԃ��ːi�̏ꍇ
                {
                    //charge();

                    BossAttack.BossRush.Boss1Fork();
                }
            }
        }
    }

    //�{�X�̏�Ԑ���
    static public void SetState(Boss_State bossState, Transform playerTransform = null) {
        BossState = bossState;
        if (BossState == Boss_State.idle)
        {
            Debug.Log("�A�C�h��");
        }
        else if (BossState == Boss_State.charge)
        {
            Debug.Log("�Ռ��g�U��");
        }
    }

    //�@��Ԏ擾
    public Boss_State GetState() {
        return BossState;
    }

    //�ҋ@���[�V����
    private void Idle() {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);
        //�@��莞�Ԃ��o�߂�����e��U����Ԃɂ���
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle��Ԃ̌o�ߎ��Ԃ�off�ɂ���

            //�����_�����̐�����switch��������̒���
            //if (!UltFlg && HPgage.currentHp <= 30)
            //{
            //    Debug.Log("����Ƃ���");
            //    UltFlg = true;
            //    SetState(Boss_State.Rain);
            //    return;
            //}
            //if (HPgage.currentHp >= 51)
            //{

            //    RandomNumbe = Random.Range(1, 4);//�U���p�^�[�������_����
            //    Debug.Log("Random" + RandomNumbe);
            //    if (BossAttack.JampFlg)
            //    {
            //        RandomNumbe = Random.Range(1, 3);//�U���p�^�[�������_����
            //        BossAttack.JampFlg = false;
            //    }
            //}
            //else
            //{
            //    RandomNumbe = Random.Range(1, 5);//�U���p�^�[�������_����
            //    if (BossAttack.JampFlg)
            //    {
            //        if (RandomNumbe == 3)
            //        {
            //            RandomNumbe = 4;
            //            BossAttack.JampFlg = false;
            //        }
            //    }
            //}
            //switch (RandomNumbe)            //switch����
            //{
            //    case 1://�C�`�S���e��
            //        SetState(Boss_State.strawberryBomb);
            //        RandomNumbe = -1;
            //        Debug.Log("�C�`�S���e");
            //        break;//break��

            //    case 2://�ːi��
            //        SetState(Boss_State.charge);
            //        RandomNumbe = -1;
            //        Debug.Log("�ːi�U��");
            //        break;//break��

            //    case 3://�W�����v
            //        SetState(Boss_State.Jump);
            //        RandomNumbe = -1;
            //        Debug.Log("�W�����v");
            //        break;//break��

            //    case 4://�i�C�t�U��
            //        SetState(Boss_State.KnifeThrower);
            //        RandomNumbe = -1;
            //        Debug.Log("�i�C�t�U��");
            //        break;

            //    case 5://�J�U��
            //        SetState(Boss_State.Rain);
            //        RandomNumbe = -1;
            //        Debug.Log("�J�U��");
            //        break;

            //}


        }
    }
}
