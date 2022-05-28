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

    public GameObject LB;
    //[[[[[idle�̏����Ŏg���ϐ�]]]]]=========================================================================
    private Vector3 defaultPos;                             //�{�X���o�ꂵ���ŏ��̈ʒu
    static private LB_State LBState = LB_State.Idle;        //�{�X�̏��(�����l��idel)
    private int RandomNumbe = 0;                            //���[�V�����̃����_�����I�p�̐�
    [SerializeField] private float timeToStayInIdle=2.4f;    //idle��Ԃŗ��܂鎞��(�t���[���w��)
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
    public Arrow_Disappear Arrow_End;
    //���I�p�̕ϐ�-------------------------------------------------------------------------------------------
    List<int> number = new List<int>();                     //���I����ő吔(List�\��)
    private int random;                                     //���I�����ԍ�
    private int Index;                                      //���I����v�f�̎w��
    //[[[[[BoundBoll�̏����Ŏg���ϐ�]]]]]====================================================================
    public GameObject BoundBoll;                            //GameObject:BoundBoll
    public GameObject BoundBolls;                            //GameObject:BoundBoll
    public GameObject firingPoint;                          //�e�����˂����ꏊ
    [SerializeField] public int BOundDamage = 10;           //�e�̑��x
    public BoundBoll_Division BoundBollEnd;
    private float k;
    public Quaternion rotation = Quaternion.identity;
    //[[[[WarpBullet�̏����Ŏg���ϐ�]]]]]====================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    GameObject WarpBullets;                                 //WarpBullet�Ő�������e
    [SerializeField] public int WarpBullet_MaxSpeed;        //�e�̑��x
    public int WarpCount = 0;                               //�A���[�J�E���g
    [SerializeField] public int WapeDamage = 10;            //�e�̑��x
    public Destroy_WarpBullet WarpEnd;
    //�����_���ړ��p�̕ϐ�-----------------------------------------------------------------------------------
    private float time;                                     //�ړ��܂ł̎���
    private float vecX;                                     //���W�w��p�FX���W�i�[�ꏊ
    private float vecY;                                     //���W�w��p�FY���W�i�[�ꏊ
    //[[[[[Bullet�̏����Ŏg���ϐ�]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    GameObject TrakingBullet;
    [SerializeField] public int Bullet_MaxSpeed;            //�e�̑��x
    public bool BulletUseFlag = true;                       //�o���b�g���g�p�����ǂ������ׂ�t���O�i�����l�Ftrue)
    [SerializeField] public int BulletDamage = 10;            //�e�̑��x
    public DestroyBullet BulletEnd;
    //-----------------------------------------------------------------------------------------------------
    private GameObject HpObject;
    public LastHPGage HpScript;
    //=====================================================================================================
    public Animator BossAnim;
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool MoveFlg;
    //=====================================================================================================
    //�K�E�Z�p�t���O(�Z%�ȉ��ɂȂ����Ƃ����)
    bool UltFlg;
    bool End;
    bool OnlryFlg = true;
    //-----------------------------------------------------------------------------------------------------
    int IdleCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        k = 0;
        LBState = LB_State.Idle;
        LB = GameObject.Find("LB(Clone)");
        firingPoint = GameObject.Find("LB_ShotPoint(Clone)");
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<LastHPGage>();
        Arrow_End = GetComponent<Arrow_Disappear>();
        BoundBollEnd = GetComponent<BoundBoll_Division>();
        BulletEnd = GetComponent<DestroyBullet>();
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
                     Debug.Log("Onlry" + OnlryFlg);
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
                    if (OnlryFlg == true)
                    {
                        OnlryFlg = false;
                        IdleCount = 0;
                        BulletAttack();
                        
                    }
                    if (TrakingBullet==null)
                    {
                        OnlryFlg = true;
                        MoveSelect();
                    }

                }
                else if (LBState == LB_State.BoundBoll)//�����{�X�̏�Ԃ��C�`�S���e�̏ꍇ
                {
                    if (OnlryFlg == true)
                    {
                        OnlryFlg = false;
                        Debug.Log("Onlry" + OnlryFlg);
                        IdleCount = 0;
                        StartCoroutine("BallSet");
                    }
                    if (BoundBoll == null)
                    {
                        OnlryFlg = true;
                        MoveSelect();
                    }

                    else if (LBState == LB_State.WarpBullet)//�����{�X�̏�Ԃ��ːi�̏ꍇ
                    {

                        if (OnlryFlg == true)
                        {
                            IdleCount = 0;
                            OnlryFlg = false;
                            if (WarpCount <= 5)
                            {
                                if (WarpBullets == null)
                                {
                                    WarpBulletAttack();
                                }
                            }
                        }
                        if (WarpBullets == null)
                        {
                            OnlryFlg = true;
                            MoveSelect();
                        }
                    }
                    else if (LBState == LB_State.ArrowAttack)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
                    {
                        IdleCount = 0;
                        if (OnlryFlg == true)
                        {
                            Debug.Log("Onlry" + OnlryFlg);
                            OnlryFlg = false;
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
                            if (End == true)
                            {
                                OnlryFlg = true;
                                MoveSelect();
                            }
                        }
                    }
                }
            }
        }
    }
    private void DelayMethod()
    {
        Debug.Log("Invoke");
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
            Debug.Log("�Ǐ]�e");
        }
        else if (LBState == LB_State.BoundBoll)
        {
            Debug.Log("�o�E���h�e");
        }
        else if (LBState == LB_State.WarpBullet)
        {
            Debug.Log("���[�v�e");
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

    private void MoveSelect()
    {
        //HP��30�ȉ��ɂȂ�������ULT��----------------------------------
        if (!UltFlg && LastHPGage.currentHp <= 30)
        {
            Debug.Log("Hp" + LastHPGage.currentHp);
            Debug.Log("�A���[�A�^�b�N");
            UltFlg = true;
            SetState(LB_State.ArrowAttack);
            return;
        }
        //HP��51�ȏ�̏ꍇ---------------------------------------------
        if (LastHPGage.currentHp >= 51)
        {
            RandomNumbe = Random.Range(0, 3);//�U���p�^�[�������_����
            Debug.Log("�����_���i���o�[��" + RandomNumbe);
        }
        //HP��30�ȉ��̏ꍇ----------------------------------------------
        else
        {
            RandomNumbe = Random.Range(0, 4);//�U���p�^�[�������_����
            Debug.Log("�����_���i���o�[��" + RandomNumbe);
        }
        //switch(�����_���i���o�[)
        switch (RandomNumbe)//switch����
        {
            case 0://�A�C�h��
                SetState(LB_State.Idle);
                break;

            case 1://�Ǐ]�e
                SetState(LB_State.TrackingBullet);
                break;

            case 2://�o�E���h�e
                SetState(LB_State.BoundBoll);
                break;//break��

            case 3://���[�v�o���b�g
                SetState(LB_State.WarpBullet);
                break;//break��

            case 4://�A���[�A�^�b�N
                SetState(LB_State.ArrowAttack);
                break;
        }
    }

    //Idel=============================================================================
    private void Idle()
    {
        //Idle����A���ōs��Ȃ��悤�ɂ���IF
        if (IdleCount == 0)
        {
            elapsedTimeOfIdleState += Time.deltaTime;�@//Idle�o�ߎ���
            Debug.Log("Time" + elapsedTimeOfIdleState);//�f�o�b�N���O
            if (AttackCount == MaxAttack)
            {
                elapsedTimeOfIdleState = timeToStayInIdle;
                AttackCount = 0;          //�U�����J�E���g���[����
                Debug.Log("Idel");
            }
            //�A�C�h���̎��Ԍo�߂��w��l�𒴂�����
            if (elapsedTimeOfIdleState >= timeToStayInIdle)
            {
                elapsedTimeOfIdleState = 0f;//idle��Ԃ̌o�ߎ��Ԃ�off�ɂ���
                MoveSelect();               //�����_���s��
                Debug.Log("Idel");          //�f�o�b�N���O
                IdleCount++;                //�J�E���g����
            }
        }
        else
        {
            MoveSelect();//�����_���s��
        }
    }
    //RandomMove=========================================================================
    private void RandomMove()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            vecX = Random.Range(20f, 40f);
            vecY = Random.Range(10f, 20f);
            LB.transform.position = new Vector3(vecX, vecY, 0.0f);
            time = 1.0f;
        }
    }
    //BulletAttack=====================================================================
    private void BulletAttack()
    {
       DestroyBullet.Flg = false;
       Vector3 bulletPosition = firingPoint.transform.position;
       TrakingBullet = Instantiate(Bullet, bulletPosition, transform.rotation);// ��Ŏ擾�����ꏊ�ɁA"bulle
    }
    //---------------------------------------------------------------------------------

    private void WarpBulletAttack()
    {
        vecX = Random.Range(-180f, 3000f);
        vecY = Random.Range(0f,230f);
        LB.transform.position = new Vector3(vecX, vecY);
        Vector3 bulletPosition = firingPoint.transform.position;
        WarpBullets = Instantiate(WarpBullet, bulletPosition, transform.rotation);
        WarpCount++;                 
    }
    //BoundBollAttack=====================================================================
    IEnumerator BallSet()
    {
        //Vector3 bulletPosition = firingPoint.transform.position;
        //BoundBolls = Instantiate(BoundBoll, bulletPosition, transform.rotation);// ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        //Vector3 direction = BoundBolls.transform.up;
        //// �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        //BoundBolls.GetComponent<Rigidbody>().AddForce(direction * 100, ForceMode.Impulse);
        //Rigidbody rb = BoundBolls.GetComponent<Rigidbody>(); // Sphere�I�u�W�F�N�g��Rigidbody�R���|�[�l���g�ւ̎Q�Ƃ��擾
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("BallSet");
            rotation.eulerAngles = new Vector3(0, k, 0);
            yield return new WaitForSeconds(3.0f);
            Instantiate(BoundBoll, firingPoint.transform.position, rotation);
            k += 30;
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






