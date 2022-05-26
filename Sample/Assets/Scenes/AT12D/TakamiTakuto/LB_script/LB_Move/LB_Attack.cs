using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_Attack : MonoBehaviour
{
    //�u��Ԑ���:�񋓌^�v===================�@
    public enum LB_State
    {
        Idle,           //�ҋ@(0)
        RandomMove,     //�����_���ړ�(1)
        Damage,         //�_���[�W(2)
        TrackingBullet, //�Ǐ]�e(3)
        BoundBoll,      //�o�E���h�e(4)
        WarpBullet,     //���[�v�{�[���i5�j
        ArrowAttack,    //�A���[�A�^�b�N(6)
    }
    //------------------------------------

    //[[[[[idle�̏����Ŏg���ϐ�]]]]]=========================================================================
    private Vector3 defaultPos;                             //�{�X���o�ꂵ���ŏ��̈ʒu
    static private LB_State LBState = LB_State.Idle;        //�{�X�̏��(�����l��idel)
    private int RandomNumbe = 0;                            //���[�V�����̃����_�����I�p�̐�
    [SerializeField] private float timeToStayInIdle;        //idle��Ԃŗ��܂鎞��(�t���[���w��)
    private float elapsedTimeOfIdleState = 0f;              //idle��Ԃ̌o�ߎ���
    [SerializeField] private int MaxAttack = 2;             //�ҋ@���[�V�����Ȃ��̍U���񐔁iHP50���ȉ��̂݁j
    public static int AttackCount = 0;                      //�U�����J�E���g
    //[[[[[ArrowAttack�̏����Ŏg���ϐ�]]]]]==================================================================
    public GameObject Arrow_Right;                          //GameObject:Arrow_Right
    public GameObject Arrow_Left;                           //GameObject:Arrow_Left
    public GameObject Arrow_Up;                             //GameObject:Arrow_Up
    public Rigidbody LbRigidbody;                           //���W�b�g�{�f�B
    public bool ArrowUseFlag = true;                        //�A���[���g�p�����ǂ������ׂ�t���O�i�����l�Ftrue)
    public int ArrowCount = 0;                              //�A���[�J�E���g
    [SerializeField] public int Arrow_MaxSpeed;             //�A���[�̑������ߗp
    //���I�p�̕ϐ�-------------------------------------------------------------------------------------------
    List<int> number = new List<int>();                     //���I����ő吔(List�\��)
    private int random;                                     //���I�����ԍ�
    private int Index;                                      //���I����v�f�̎w��
    //[[[[[BoundBoll�̏����Ŏg���ϐ�]]]]]====================================================================
    public GameObject BoundBoll;                            //GameObject:BoundBoll
    public GameObject firingPoint;                          //�e�����˂����ꏊ
    [SerializeField] public int Bound_MaxSpeed;             //BoundBoll�̑��x
    //[[[[WarpBullet�̏����Ŏg���ϐ�]]]]]====================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    GameObject WarpBullets;                                 //WarpBullet�Ő�������e
    [SerializeField] public int WarpBullet_MaxSpeed;        //�e�̑��x
    public int WarpCount = 0;                               //�A���[�J�E���g
    //�����_���ړ��p�̕ϐ�-----------------------------------------------------------------------------------
    private float time;                                     //�ړ��܂ł̎���
    private float vecX;                                     //���W�w��p�FX���W�i�[�ꏊ
    private float vecY;                                     //���W�w��p�FY���W�i�[�ꏊ
    //[[[[[Bullet�̏����Ŏg���ϐ�]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    [SerializeField] public int Bullet_MaxSpeed;            //�e�̑��x
    public bool BulletUseFlag = true;                       //�o���b�g���g�p�����ǂ������ׂ�t���O�i�����l�Ftrue)
    //-----------------------------------------------------------------------------------------------------
    private GameObject HpObject;
    public HPgage HpScript;
    public Animator BossAnim;
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool MoveFlg;
    //�K�E�Z�p�t���O(�Z%�ȉ��ɂȂ����Ƃ����)
    bool UltFlg;


    // Start is called before the first frame update
    void Start()
    {
        LBState = LB_State.Idle;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();
        BossAnim = this.gameObject.GetComponent<Animator>();
        //---���I����ԍ������X�g�\���Ɋi�[
        //---���I�񐔂��w��(�����ł�1~3)
        for (int i = 1; i <= 3; i++) {
            number.Add(i);
        }
    }
    //-----------------------------------------------

    // Update is called once per frame----------------------------------------------------
    void Update()
    {

        if (!Pause.isPause)
        {
            if (LB_Manager.LB_States == LB_Manager.LB_State.LB_BATTLE)
            {
                if (LBState == LB_State.Idle)       //�����{�X�̏�Ԃ��ҋ@�̏ꍇ
                {
                    Idle();
                }
                else if (LBState == LB_State.RandomMove)//�����{�X�̏�Ԃ��_���[�W�̏ꍇ
                {

                }
                else if (LBState == LB_State.Damage)//�����{�X�̏�Ԃ��_���[�W�̏ꍇ
                {
                    //damage();
                }
                else if (LBState == LB_State.TrackingBullet)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
                {
                    BulletAttack();
                }
                else if (LBState == LB_State.BoundBoll)//�����{�X�̏�Ԃ��C�`�S���e�̏ꍇ
                {
                    BoundBollAttack();
                }
                else if (LBState == LB_State.WarpBullet)//�����{�X�̏�Ԃ��ːi�̏ꍇ
                {
                    if (WarpCount <= 5)
                    {
                        if (WarpBullets == null)
                        {
                            WarpBulletAttack();
                        }
                    }
                }
                else if (LBState == LB_State.ArrowAttack)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
                {
                    //---���U���Ȃ��------------
                    if (ArrowUseFlag)
                    {
                        //--�Ō�̃��X�g�̗v�f���ɒB������
                        if (number.Count <= 0)
                        {
                            return;
                        }

                        Index = Random.Range(0, number.Count);             // 0����v�f�̍ő吔�܂ł͈̔͂��烉���_���Œ��I
                        random = number[Index];                            // ���I�����l�ŗv�f���w�肷��

                        ArrowAttack(random);
                    }
                }
            }
        }
    }

    static public void SetState(LB_State LbState, Transform playerTransform = null)
    {
        LBState = LbState;
        if (LBState == LB_State.Idle)
        {
            Debug.Log("�A�C�h��");
        }
        else if (LBState == LB_State.RandomMove)
        {
            Debug.Log("�����_���ړ�");
        }
        else if (LBState == LB_State.Damage)
        {
            Debug.Log("�_���[�W");
        }
        else if (LBState == LB_State.TrackingBullet)
        {
            Debug.Log("�ʏ�U��");
        }
        else if (LBState == LB_State.BoundBoll)
        {
            Debug.Log("�Ռ��g�U��");
        }
        else if (LBState == LB_State.WarpBullet)
        {
            Debug.Log("�`�F�C�X");
        }
    }


    //--------------------------------------------------------------------------------------------------------

    public LB_State GetState()
    {
        return LBState;
    }

    public void AnimFlagOnOff()
    {
        if (!AnimFlg)
        {
            AnimFlg = true;
            return;
        }
        if (AnimFlg)
        {
            AnimFlg = false;
            return;
        }

    }
    public void AnimMoveFlgOnOff()
    {
        if (!MoveFlg)
        {
            MoveFlg = true;
            return;
        }
        if (MoveFlg)
        {
            MoveFlg = false;
            return;
        }
    }

    //Idel=============================================================================
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);
        if (AttackCount == MaxAttack)
        {
            elapsedTimeOfIdleState = timeToStayInIdle;
            AttackCount = 0;          //�U�����J�E���g���[����
            Debug.Log("Idel");
        }
        //�@��莞�Ԃ��o�߂�����e��U����Ԃɂ���
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0f;       //idle��Ԃ̌o�ߎ��Ԃ�off�ɂ���
            Debug.Log("AttackCount�F" + AttackCount);

            //�����_�����̐�����switch��������̒���
            if (!UltFlg && HPgage.currentHp <= 30)
            {
                Debug.Log("�A���[�A�^�b�N");
                UltFlg = true;
                SetState(LB_State.ArrowAttack);
                return;
            }
            if (HPgage.currentHp >= 51)
            {
                RandomNumbe = Random.Range(1, 4);//�U���p�^�[�������_����
                Debug.Log("Random" + RandomNumbe);
            }
            else
            {
                RandomNumbe = Random.Range(1, 5);//�U���p�^�[�������_����
                Debug.Log("Random" + RandomNumbe);
            }
            switch (RandomNumbe)            //switch����
            {
                case 1://�C�`�S���e��
                    SetState(LB_State.TrackingBullet);
                    RandomNumbe = -1;
                    Debug.Log("�C�`�S���e");
                    break;//break��

                case 2://�ːi��
                    SetState(LB_State.BoundBoll);
                    RandomNumbe = -1;
                    Debug.Log("�ːi�U��");
                    break;//break��

                case 3://�W�����v
                    SetState(LB_State.WarpBullet);
                    RandomNumbe = -1;
                    Debug.Log("�W�����v");
                    break;//break��

                case 4://�i�C�t�U��
                    SetState(LB_State.ArrowAttack);
                    RandomNumbe = -1;
                    Debug.Log("�i�C�t�U��");
                    break;
            }



        }
    }
    

//-------------------------------------------------------------------------------

//BulletAttack=====================================================================
private void BulletAttack()
    {
        if (Input.GetKeyDown("c"))
        {
            Vector3 bulletPosition = firingPoint.transform.position;
            GameObject newBullet = Instantiate(Bullet, bulletPosition, transform.rotation);// ��Ŏ擾�����ꏊ�ɁA"bulle
        }
    }
    //---------------------------------------------------------------------------------

    private void WarpBulletAttack()
    {
        vecX = Random.Range(20f, 40f);
        vecY = Random.Range(10f, 20f);
        firingPoint.transform.position = new Vector3(vecX, vecY, 27.0f);
        Vector3 bulletPosition = firingPoint.transform.position;
        GameObject WarpBullets = Instantiate(WarpBullet, bulletPosition, transform.rotation);
        WarpCount++;                 
    }

//BoundBollAttack=====================================================================
private void BoundBollAttack()
    {
        if (Input.GetKeyDown("a"))
        {
            for (int i = 0; i <= 1; i++)
            {
              
                if (i == 0)
                {
                    Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);
                    Vector3 bulletPosition = firingPoint.transform.position;
                    GameObject newBall = Instantiate(BoundBoll, bulletPosition, transform.rotation);// ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
                    float forceMagnitude = 10.0f;                     // ��̌����ɉ����͂̑傫�����`
                    Vector3 force = forceMagnitude * forceDirection;  // �����Ƒ傫������Sphere�ɉ����͂��v�Z����
                    Rigidbody rb = newBall.GetComponent<Rigidbody>(); // Sphere�I�u�W�F�N�g��Rigidbody�R���|�[�l���g�ւ̎Q�Ƃ��擾
                    rb.AddForce(force, ForceMode.Impulse);            //�͂������郁�\�b�h,ForceMode.Impulse�͌���
                }
                if(i == 1)
                {
                    Vector3 forceDirection = new Vector3(-1.0f, 1.0f, 0f);
                    Vector3 bulletPosition = firingPoint.transform.position;
                    GameObject newBall = Instantiate(BoundBoll, bulletPosition, transform.rotation);// ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
                    float forceMagnitude = 10.0f;                     // ��̌����ɉ����͂̑傫�����`
                    Vector3 force = forceMagnitude * forceDirection;  // �����Ƒ傫������Sphere�ɉ����͂��v�Z����
                    Rigidbody rb = newBall.GetComponent<Rigidbody>(); // Sphere�I�u�W�F�N�g��Rigidbody�R���|�[�l���g�ւ̎Q�Ƃ��擾
                    rb.AddForce(force, ForceMode.Impulse);            //�͂������郁�\�b�h,ForceMode.Impulse�͌���
                }


            }
        }
    }
    //-------------------------------------------------------------------------------------
    private void ArrowAttack(int selectnumber)
    {
        ArrowUseFlag = false;

        switch (selectnumber)
        {
            //Arrow_Right�𓮂���-------------------------------------------------------------
            case 1:
                LbRigidbody = Arrow_Right.GetComponent<Rigidbody>();        //rigidbody���擾
                Vector3 ForceArrowRight = new Vector3(-8.0f, 0.0f, 0.0f);   //�͂�ݒ�
                LbRigidbody.AddForce(ForceArrowRight * Arrow_MaxSpeed);     //�͂�������
                //Debug.Log("Arrow_Right��������");                          //�f�o�b�N���O��\��
                break;                                                      //case�𔲂���
            //--------------------------------------------------------------------------------

            //Arrow_Left�𓮂���---------------------------------------------------------------
            case 2:
                LbRigidbody = Arrow_Left.GetComponent<Rigidbody>();         //rigidbody���擾
                Vector3 ForceArrowLeft = new Vector3(8.0f, 0.0f, 0.0f);     //�͂�ݒ�
                LbRigidbody.AddForce(ForceArrowLeft * Arrow_MaxSpeed);      //�͂�������
                //Debug.Log("Arrow_Left��������");                           //�f�o�b�N���O��\��
                break;                                                      //case�𔲂���
            //---------------------------------------------------------------------------------

            //Arrow_Up�𓮂���------------------------------------------------------------------
            case 3:
                LbRigidbody = Arrow_Up.GetComponent<Rigidbody>();           //rigidbody���擾
                Vector3 ForceArrowUp = new Vector3(0.0f, -8.0f, 0.0f);      //�͂�ݒ�
                LbRigidbody.AddForce(ForceArrowUp * Arrow_MaxSpeed);        //�͂�������
                //Debug.Log("Arrow_Up��������");                             //�f�o�b�N���O��\��  
                break;                                                      //case�𔲂���
            //-----------------------------------------------------------------------------------
            default:
                Debug.Log("<dolor = red>���I���s</color>");
            break;
        }
        number.RemoveAt(Index);//���I�Ŏg�p�����l��v�f���甲���o��

    }
}






