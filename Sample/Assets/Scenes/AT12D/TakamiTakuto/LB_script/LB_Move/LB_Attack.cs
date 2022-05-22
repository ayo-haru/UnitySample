using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_Attack : MonoBehaviour
{
    //�u��Ԑ���:�񋓌^�v===================�@
    public enum Boss_State
    {
        Idle,           //�ҋ@(0)
        Damage,         //�_���[�W(1)
        TrackingBullet, //�Ǐ]�e(2)
        BoundBoll,      //�o�E���h�e(3)
        WarpBullet,     //���[�v�{�[���i4�j
        ArrowAttack,    //�A���[�A�^�b�N(5)
    }
    //------------------------------------

    //[[[[[idle�̏����Ŏg���ϐ�]]]]]=========================================================================
    private Vector3 defaultPos;                             //�{�X���o�ꂵ���ŏ��̈ʒu
    static private Boss_State BossState = Boss_State.Idle;  //�{�X�̏��(�����l��idel)
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
    public GameObject firingPoint;                          //
    [SerializeField] public int Bound_MaxSpeed;             //BoundBoll�̑��x
    //[[[[WarpBullet�̏����Ŏg���ϐ�]]]]]=======================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    public Destroy_WarpBullet destroywarpbullet;
    [SerializeField] public int WarpBullet_MaxSpeed;        //�e�̑��x
    public bool WarpBulletUseFlag = true;                  //�o���b�g���g�p�����ǂ������ׂ�t���O�i�����l�Ftrue)
    public int WarpCount = 0;                              //�A���[�J�E���g
    private float time;
    private float vecX;
    private float vecY;
    //[[[[[Bullet�̏����Ŏg���ϐ�]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    [SerializeField] public int Bullet_MaxSpeed;           //�e�̑��x
    public bool BulletUseFlag = true;                       //�o���b�g���g�p�����ǂ������ׂ�t���O�i�����l�Ftrue)
    //-----------------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
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
            if (LB_Manager.BossState == LB_Manager.LBState.LB_BATTLE)
            {
                if (BossState == Boss_State.Idle)       //�����{�X�̏�Ԃ��ҋ@�̏ꍇ
                {
                    Idle();
                }
                else if (BossState == Boss_State.Damage)//�����{�X�̏�Ԃ��_���[�W�̏ꍇ
                {
                    //damage();
                }
                else if (BossState == Boss_State.TrackingBullet)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
                {
                    BulletAttack();
                }
                else if (BossState == Boss_State.BoundBoll)//�����{�X�̏�Ԃ��C�`�S���e�̏ꍇ
                {
                    BoundBollAttack();
                }
                else if (BossState == Boss_State.WarpBullet)//�����{�X�̏�Ԃ��ːi�̏ꍇ
                {
                    if (WarpCount <= 5)
                    {
                        if (WarpBulletUseFlag)
                        {
                            WarpBulletAttack();
                        }
                    }
                }
                else if (BossState == Boss_State.ArrowAttack)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
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

    //--------------------------------------------------------------------------------------------------------

    //Idel=============================================================================
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //Debug.Log("Time" + elapsedTimeOfIdleState);

        //�A�^�b�N�J�E���g���ő�U���񐔂Ɠ����Ȃ�
        if (AttackCount == MaxAttack)
        {
            //SetState(Boss_State.idle);                //�ҋ@���[�V����
            elapsedTimeOfIdleState = timeToStayInIdle;
            AttackCount = 0;                            //�U�����J�E���g���[����
            Debug.Log("�A�C�h��");                       //�f�o�b�N���O
        }
        if (elapsedTimeOfIdleState >= timeToStayInIdle)
        {    //��莞�Ԃ��o�߂�����e��U����Ԃɂ���
            elapsedTimeOfIdleState = 0f;                 //idle��Ԃ̌o�ߎ��Ԃ�off�ɂ���
            Debug.Log("AttackCount�F" + AttackCount);    //�f�o�b�N���O

            //�����_�����̐�����switch��������̒���
            if (HPgage.currentHp >= 51)
            {   //HP�������ȏ�̏ꍇ
                RandomNumbe = Random.Range(1, 3);        //�U���p�^�[�������_����
                Debug.Log("Random" + RandomNumbe);       //�f�o�b�N���O
            }
            else
            {   //HP�������ȉ��̏ꍇ
                RandomNumbe = Random.Range(1, 4);        //�U���p�^�[�������_����
                Debug.Log("Random" + RandomNumbe);       //�f�o�b�N���O
            }

            //�����_���i���o�[�ɂ��switch����
            switch (RandomNumbe)
            {
                case 1://�C�`�S���e��
                    Debug.Log("�C�`�S���e");
                    break;

                case 2://�ːi��
                    Debug.Log("�ːi�U��");
                    break;

                case 3://�i�C�t�U��
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
        WarpBulletUseFlag = false;                   
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






