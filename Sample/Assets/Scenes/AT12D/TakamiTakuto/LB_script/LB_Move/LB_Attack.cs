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
        CircleBullet,   //�T�[�N���e(4)
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
    public int ArrowNum;                                    //�A���[�̍s���񐔕ۑ�
    //���I�p�̕ϐ�-------------------------------------------------------------------------------------------
    List<int> number = new List<int>();                     //���I����ő吔(List�\��)
    private int random;                                     //���I�����ԍ�
    private int Index;                                      //���I����v�f�̎w��
    //[[[[[BoundBoll�̏����Ŏg���ϐ�]]]]]====================================================================
    public GameObject CircleBullet;                         //GameObject:BoundBoll
    public GameObject CircleBulletobj;                      
    public GameObject firingPoint;                          //�e�����˂����ꏊ
    [SerializeField] public int BOundDamage = 10;           //�e�̑��x
    private float Angle;                                    //�p�x
    public Quaternion rotation = Quaternion.identity;       //Quaternion
    public int Circlenum;
    //[[[[WarpBullet�̏����Ŏg���ϐ�]]]]]====================================================================
    public GameObject WarpBullet;                           //GameObject:Bullet
    GameObject WarpBullets;                                 //WarpBullet�Ő�������e
    [SerializeField] public int WarpBullet_MaxSpeed;        //�e�̑��x
    public int WarpCount = 0;                               //�A���[�J�E���g
    [SerializeField] public int WapeDamage = 10;            //�e�̑��x
    public int WarpNum;
    //�����_���ړ��p�̕ϐ�-----------------------------------------------------------------------------------
    private float time;                                     //�ړ��܂ł̎���
    private float vecX;                                     //���W�w��p�FX���W�i�[�ꏊ
    private float vecY;                                     //���W�w��p�FY���W�i�[�ꏊ
    //[[[[[Bullet�̏����Ŏg���ϐ�]]]]]=======================================================================
    public GameObject Bullet;                               //GameObject:Bullet
    GameObject TrakingBullet;                               //GameObject:TrakingBUllet
    [SerializeField] public int Bullet_MaxSpeed;            //�e�̑��x
    public bool BulletUseFlag = true;                       //�o���b�g���g�p�����ǂ������ׂ�t���O�i�����l�Ftrue)
    [SerializeField] public int BulletDamage = 10;          //�e�̑��x
    //-----------------------------------------------------------------------------------------------------
    private GameObject HpObject;                            //HP�o�[
    public LastHPGage HpScript;                             //HPgage       
    //=====================================================================================================
    [SerializeField]  public Animator LBossAnim;            //�A�j���[�V����
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool OneTimeFlg;
    //�K�E�Z�p�t���O(�Z%�ȉ��ɂȂ����Ƃ����)=================================================================
    bool UltFlg;                                            
    public bool OnlryFlg = true;
    //-----------------------------------------------------------------------------------------------------
    int IdleCount = 0;
    public GameObject Effect = null;

    // Start is called before the first frame update
    void Start()
    {
        Angle = 0;
        LBState = LB_State.Idle;
        firingPoint = GameObject.Find("LB_ShotPoint(Clone)");
        HpObject = GameObject.Find("HPGage");
        LBossAnim = this.gameObject.GetComponent<Animator>();
        HpScript = HpObject.GetComponent<LastHPGage>();
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
                        LBossAnim.SetTrigger("AttakTr");
                        EffectManager.Play(EffectData.eEFFECT.EF_LASTBOSS_ENERGYBALL, firingPoint.transform.position);      //effect����
                        Effect = GameObject.Find("energyBall(Clone)");
                        

                    }
                    if (TrakingBullet != null)
                    {
                         Effect.transform.position = TrakingBullet.transform.position;                             //Effect��ww
                    }
                    if (OneTimeFlg)
                    {
                        Destroy(Effect);
                        TrakingBullet = null;
                        OneTimeFlg = false;
                        OnlryFlg = true;
                        MoveSelect();
                    }

                }
                else if (LBState == LB_State.CircleBullet)
                {
                    Debug.Log("�T�[�N����" + Circlenum);
                    if (OnlryFlg == true)
                    {
                        Circlenum = 0;
                        OnlryFlg = false;
                        Debug.Log("Onlry" + OnlryFlg);
                        IdleCount = 0;
                        if (!AnimFlg)
                        {
                            LBossAnim.SetTrigger("CircleBulletTrigger");
                            AnimFlg = true;
                        }
                    }
                    if (CircleBulletobj != null)
                    {
                                                     //Effect��ww
                    }
                    if (OneTimeFlg && Circlenum >= 20)
                    {
                        Circlenum = 0;
                        OnlryFlg = true;
                        AnimFlg = false;
                        OneTimeFlg = false;
                        MoveSelect();
                    }
                }
                else if (LBState == LB_State.WarpBullet)
                {
                    Debug.Log("���[�v���W" + this.transform.position);
                    Debug.Log("Onlry" + OnlryFlg);
                    Debug.Log("���[�v��" + WarpNum);
                    if (OneTimeFlg && WarpNum >= 5)
                    {
                        WarpNum = 0;
                        OnlryFlg = true;
                        OneTimeFlg = false;
                        WarpCount = 0;
                        MoveSelect();
                        return;
                    }
                    if (OnlryFlg == true)
                    {
                        IdleCount = 0;
                        OnlryFlg = false;
                        if (WarpCount <= 5)
                        {
                            if (WarpBullets == null)
                            {
                                if (!AnimFlg)
                                {
                                    vecX = Random.Range(-180f, 300f);                          //���W�����_���ʒu����(X)
                                    vecY = Random.Range(60, 230f);                            //���W�����_���ʒu����(Y)
                                    this.transform.position = new Vector3(vecX, vecY, 120.0f); //�����_���ړ�
                                    LBossAnim.SetTrigger("WarpTrigger");
                                    AnimFlg = true;
                                }

                            }
                        }
                    }
                    
                }
                else if (LBState == LB_State.ArrowAttack)//�����{�X�̏�Ԃ��i�C�t�����̏ꍇ
                {
                    IdleCount = 0;
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
                   if (OneTimeFlg)
                   {
                       OneTimeFlg = false;
                        MoveSelect();
                    }
                }
                
            }
        }
    }
    //SetState============================================================================================
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
        else if (LBState == LB_State.CircleBullet)
        {
            Debug.Log("�o�E���h�e");
        }
        else if (LBState == LB_State.WarpBullet)
        {
            Debug.Log("���[�v�e");
        }
        else if (LBState == LB_State.ArrowAttack)
        {
            Debug.Log("�A���[�A�^�b�N");
        }
    }
    //LB_State=============================================================================================
    public LB_State GetState()
    {
        return LBState;
    }
    //MoveSelect===========================================================================================
    private void MoveSelect()
    {
        //HP��30�ȉ��ɂȂ�������ULT��-----------------------------------------------
        if (!UltFlg && LastHPGage.currentHp <= 30)
        {
            Debug.Log("Hp" + LastHPGage.currentHp);      //�f�o�b�N���O
            Debug.Log("�A���[�A�^�b�N");                  //�f�o�b�N���O
            UltFlg = true;                              //
            SetState(LB_State.ArrowAttack);
            return;
        }
        //HP��51�ȏ�̏ꍇ--------------------------------------------------------
        if (LastHPGage.currentHp >= 51)
        {
            RandomNumbe = Random.Range(0, 4);            //�U���p�^�[�������_����
            Debug.Log("�����_���i���o�[��" + RandomNumbe);//�f�o�b�N���O
        }
        //HP��50�ȉ��̏ꍇ---------------------------------------------------------
        else
        {
            RandomNumbe = Random.Range(1, 4);            //�U���p�^�[�������_����
            Debug.Log("�����_���i���o�[��" + RandomNumbe);//�f�o�b�N���O
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

            case 2://�T�[�N���o���b�g�e
                SetState(LB_State.CircleBullet);
                break;//break��

            case 3://���[�v�o���b�g
                SetState(LB_State.WarpBullet);
                break;//break��
        }
    }
    //Idel==================================================================================================
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
                AttackCount = 0;                      //�U�����J�E���g���[����
                Debug.Log("Idel");
            }
            //�A�C�h���̎��Ԍo�߂��w��l�𒴂�����
            if (elapsedTimeOfIdleState >= timeToStayInIdle){
                elapsedTimeOfIdleState = 0f;          //idle��Ԃ̌o�ߎ��Ԃ�off�ɂ���
                MoveSelect();                         //�����_���s��
                Debug.Log("Idel");                    //�f�o�b�N���O
                IdleCount++;                          //�J�E���g����
            }
        }
        //IdleCount��0�ȏ�̎��ɓ���
        else{
            MoveSelect();                             //�s������
        }
    }
    //RandomMove==============================================================================================
    private void RandomMove(){
        time -= Time.deltaTime;                                                      //���Ԍ��Z
        if (time <= 0){                                                              //�o�ߎ��Ԃ�0�b�ȏ�̏ꍇ
            vecX = Random.Range(20f, 40f);                                           //���W�����_���ʒu����(X)                                     
            vecY = Random.Range(10f, 20f);                                           //���W�����_���ʒu����(Y)
            LB.transform.position = new Vector3(vecX, vecY, 0.0f);                   //�����_�����[�v���s
            time = 1.0f;                                                             //���Ԃ�1.0f��
        }
    }
    //BulletAttack============================================================================================
    private void BulletAttack(){
       Vector3 bulletPosition = firingPoint.transform.position;                       //���ˈʒu��firingPoint��
       TrakingBullet = Instantiate(Bullet, bulletPosition, transform.rotation);       //��Ŏ擾�����ꏊ��
    }
    //WarpeBulletAttack========================================================================================
    private void WarpBulletAttack(){
        Vector3 bulletPosition = firingPoint.transform.position;                       //���ˈʒu��firingPoint��
        WarpBullets = Instantiate(WarpBullet, bulletPosition, this.transform.rotation);//warpBullet����
        WarpCount++;                                                                   //�J�E���g����               
    }
    //CircleBulletAttack=======================================================================================
    IEnumerator CircleBulletAttack(){
        for (int i = 0; i < 20; i++){                                                  //20��J��Ԃ�
            Debug.Log("�S�~��������baaaaaaaaaaaaaaaaaaaaaaaaakaaaaaaaaaaaaaa"+i);
            rotation.eulerAngles = new Vector3(0.0f,0.0f,Angle);                       //�N�H�[�^�j�I�����I�C���[�p�ւ̕ϊ�
            yield return new WaitForSeconds(0.3f);                                     //0.3f�҂� 
            EffectManager.Play(EffectData.eEFFECT.EF_LASTBOSS_ENERGYBALL, firingPoint.transform.position);      //effect����
            Effect = GameObject.Find("energyBall(Clone)");
            CircleBulletobj =Instantiate(CircleBullet, firingPoint.transform.position, rotation);       //CircleBullet�𐶐�
            CircleBulletobj.name = "Circle" + i;
            Effect.name = "EF_BALL" + i;

            GameObject.Find("EF_BALL" + i).transform.parent = GameObject.Find("EF_BALL" + i).transform;
            
            Angle += 30;                                                               //k(Z�p�x)��30������
        }
    }
    private void StartCircle()
    {
        Debug.Log("Arrow_Up��������");                               //�f�o�b�N���O��\��  
        StartCoroutine("CircleBulletAttack");
        Debug.Log("Arrow_Up��������");                               //�f�o�b�N���O��\��  
    }
    //ArrowAttack==============================================================================================
    private void ArrowAttack(int selectnumber){
        Debug.Log("�A���[�E�S�L�}�X��");
        ArrowUseFlag = false;
        LBossAnim.SetInteger("UltCount", selectnumber);
        
        switch (selectnumber) {
            //Arrow_Right�𓮂���-------------------------------------------------------------
            case 1:
                Vector3 Right_Pos;
                Right_Pos = new Vector3(-465,85, 0);
                GameObject ArrowRightobj = Instantiate(Arrow_Right, Right_Pos, transform.rotation);          //��Ŏ擾����
                LbRigidbody = ArrowRightobj.GetComponent<Rigidbody>();        //rigidbody���擾
                Vector3 ForceArrowRight = new Vector3(80.0f, 0.0f, 0.0f);   //�͂�ݒ�
                LbRigidbody.AddForce(ForceArrowRight * Arrow_MaxSpeed);     //�͂�������
                Debug.Log("Arrow_Right��������");                          //�f�o�b�N���O��\��
                LBossAnim.Play("ULT_Right");
                LBossAnim.SetBool("OnlyFlg", true);
                ArrowNum ++;
                break;                                                      //case�𔲂���
            //--------------------------------------------------------------------------------

            //Arrow_Left�𓮂���---------------------------------------------------------------
            case 2:
                Vector3 Left_Pos;
                Left_Pos = new Vector3(630, 85, 0);
                GameObject ArrowLeftobj = Instantiate(Arrow_Left, Left_Pos, transform.rotation);          //��Ŏ擾����
                LbRigidbody = ArrowLeftobj.GetComponent<Rigidbody>();         //rigidbody���擾
                Vector3 ForceArrowLeft = new Vector3(-80.0f, 0.0f, 0.0f);     //�͂�ݒ�
                LbRigidbody.AddForce(ForceArrowLeft * Arrow_MaxSpeed);      //�͂�������
                Debug.Log("Arrow_Left��������");                           //�f�o�b�N���O��\��
                LBossAnim.Play("ULT_Left");
                LBossAnim.SetBool("OnlyFlg", true);
                ArrowNum++;
                break;                                                      //case�𔲂���
            //---------------------------------------------------------------------------------

            //Arrow_Up�𓮂���------------------------------------------------------------------
            case 3:
                Vector3 UP_Pos;
                UP_Pos = new Vector3(52, 430, 0);
                GameObject ArrowUpobj = Instantiate(Arrow_Up, UP_Pos, transform.rotation);          //��Ŏ擾�����ꏊ��
                LbRigidbody = ArrowUpobj.GetComponent<Rigidbody>();           //rigidbody���擾
                Vector3 ForceArrowUp = new Vector3(0.0f, -80.0f, 0.0f);    //�͂�ݒ�
                LbRigidbody.AddForce(ForceArrowUp * Arrow_MaxSpeed);        //�͂�������
                Debug.Log("Arrow_Up��������");                               //�f�o�b�N���O��\��  
                LBossAnim.Play("ULT_Down");
                LBossAnim.SetBool("OnlyFlg", true);
                ArrowNum++;
                break;                                                      //case�𔲂���
            //-----------------------------------------------------------------------------------
            default:
                Debug.Log("<dolor = red>���I���s</color>");
            break;
        }
        number.RemoveAt(Index);//���I�Ŏg�p�����l��v�f���甲���o��
        if(ArrowNum >=3)
        {
            OneTimeFlg = true;
            
            LBossAnim.SetInteger("UltCount", 0);
            OnlryFlg = true;
        }
    }
}
//--------------------------------------------------------------------------------------------------------------






