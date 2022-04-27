using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Boss1Attack : MonoBehaviour
{
    //�{�X�̍U���̎��
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Idle,
    }
    static public bool RefrectFlg = false;                  //�v���C���[���p���B�ɐ����������ǂ����̎󂯎��p
    static public bool OnlyFlg;                             //���ꂼ��̏����̈�����̏����p
    static public Vector3 BossStartPoint;                   //�{�X�̏����n�_
    [SerializeField] public int RushDamage;                 //�ːi�U���̃_���[�W
    [SerializeField] public int StrawberryDamage;           //�C�`�S�U���̃_���[�W
    [SerializeField] public int KnifeDamage;                //�i�C�t�U���̃_���[�W
    [SerializeField] public float RefrectRotOver;           //�e�����p�x�̏㔻��p
    [SerializeField] public float RefrectRotUnder;          //�e�����p�x�̏㔻��p
    bool LRSwitchFlg;
    private GameObject HpObject;
    HPgage HpScript;
    Animator BossAnim;
    bool AnimFlg;
    bool MoveFlg;
    BossMove.Boss_State BossTakeCase;
    bool RFChange;                                          //���E���]
    //�������邩�킩��Ȃ����E����p
    //�ːi�p�ϐ��Q
    //----------------------------------------------------------
    GameObject Forkobj;                                     //�t�H�[�N�̃I�u�W�F�N�g�����p
    GameObject Fork;                                        //�t�H�[�N�̃I�u�W�F�N�g�i�[�p
    Vector3 RushStartPoint;                                 //�ːi�J�n�n�_
    Vector3 RushEndPoint;                                   //�ːi�I���n�_
    Vector3 RushPlayerPoint;                                //�ːi���͂������Ƃ��̃v���C���[���W�i�[�p
    Vector3 RushRefEndPoint;                                //�ːi���͂�������̓G�̍ŏI�n�_
    Vector3 RushMiddlePoint;                                //�ːi�U����߂��Ă��邽�߂̒��ԍ��W
    Vector3 ForkPos;
    bool OnlyRushFlg;                                       //������
    [SerializeField] public float RushSpeed;                //�ːi�̃X�s�[�h
    bool RushRefFlg = false;                                //�ːi���͂���������
    float RushTime;                                         //�ːi�̌o�ߎ���
    float RushRefTime;                                      //�e������̎��Ԍo��
    bool BossReturnFlg;
    float BossReturnTime;                                   //�ːi��߂�܂ł̎���
    bool RushEndFlg;
    float RushReturnSpeed;
    bool ReturnDelay;                                      //�߂낤�Ƃ���܂ł̎���
    Vector3 Scale;
    Vector3 oldScale;
    [SerializeField] public float RotateSpeed;
    [SerializeField] public Vector3 Rotate;
        
    //----------------------------------------------------------
    //�C�`�S���e�ϐ�
    //----------------------------------------------------------
    GameObject obj;                                         //�C�`�S�����p
    GameObject [] Strawberry;                               //�C�`�S������i�[
    [SerializeField] public int Max_Strawberry;             //�C�`�S�̍ő吔
    static public int PreMax_Strawberry;                    //�ő吔���ق��̕����ł��g����悤��
    int StrawberryNum;                                      //���݂̎ˏo�ς݃C�`�S�v�Z�p
    int AliveStrawberry;                                    //�C�`�S�̐����m�F�p
    static bool[] StrawberryUseFlg;                         //�C�`�S���g���Ă��邩�ǂ����̃t���O
    static public bool [] StrawberryRefFlg;                 //�C�`�S���e���ꂽ���ǂ����̃t���O
    [SerializeField] public float StrawberrySpeed;          //�C�`�S�����ł������x
    bool[] PlayerRefDir;                                    //�e�����Ƃ��̕����t���O
    Vector3 RefMiss;                                        //�e���̂Ɏ��s�����Ƃ��̍��W�i�[�p
    bool RefMissFlg;                                        //�e���̂Ɏ��s�����Ƃ��ɏ�������񂾂�����p
    int StrawBerryMany;                                     //�C�`�S���ő吔�ȏ�o���Ȃ��悤�ɂ��邽�߂̏��������Ԃ񂢂�Ȃ�
    bool[] StrawberryRefOnlyFlg;                            //�e���ꂽ���̂ň�񂾂�����������̗p
    static public bool[] StrawberryColPlayer;               //�v���C���[�ɓ����������p�̏���
    GameObject StrawberryAimObj;
    GameObject [] StrawberryAim;
    Vector3 [] StrawberryAimScale;
    bool []StrawBerryLagFlg;

    //�x�W�G�Ȑ��p
    Vector3  []StartPoint;
    Vector3 [] MiddlePoint;
    Vector3 [] EndPoint;
    float [] FinishTime;
    Vector3 RefEndPoint;
    float[] Ref_FinishTime;
     Vector3[] PlayerPoint;
    Vector3[] PlayerMiddlePoint;
    //----------------------------------------------------------
    //�i�C�t�����ϐ��Q
    //----------------------------------------------------------
    GameObject Knifeobj;                                    //�i�C�t�����p
    GameObject Knife;                                       //�i�C�t������i�[
    Vector3 KnifeStartPoint;                                //�i�C�t�̃X�^�[�g���W
    Vector3 KnifeEndPoint;                                  //�i�C�t�̏I���n�_
    Vector3 KnifePlayerPoint;                               
    float KnifeTime;
    [SerializeField] public float KnifeSpeed;               //�i�C�t�̑��x
    bool KnifeRefFlg = false;                               //�i�C�t���e���ꂽ���ǂ���
    float KnifeRefTime;
    Quaternion KnifeRotForward;                             //�i�C�t�̊p�x�ύX�p
    Quaternion KnifeRotDir;                                 //�i�C�t�̊p�x�ύX�p
    [SerializeField] Vector3 KnifeForward;                  //�i�C�t�̑O�ύX�p

    [SerializeField] public float KnifeThrowTime;
    float KnifeThrowNowTime;
    GameObject KnifeAimObj;
    GameObject KnifeAim;
    Vector3 KnifeAimPos;
    bool AimFlg;
    bool AimOnly;
    bool AimStart;
    //----------------------------------------------------------



    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("StrawBerry");
        Knifeobj = (GameObject)Resources.Load("Knife");
        Forkobj = (GameObject)Resources.Load("Fork");
        StrawberryAimObj = (GameObject)Resources.Load("StrawberryAim");
        KnifeAimObj = (GameObject)Resources.Load("KnifeAim");
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryUseFlg = new bool[Max_Strawberry];
        StrawberryRefFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        PlayerPoint = new Vector3[Max_Strawberry];
        StartPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];
        Ref_FinishTime = new float[Max_Strawberry];
        PlayerMiddlePoint = new Vector3[Max_Strawberry];
        StrawberryAim = new GameObject[Max_Strawberry];
        StrawberryAimScale = new Vector3[Max_Strawberry];
        StrawBerryLagFlg = new bool[Max_Strawberry];
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        PlayerRefDir = new bool[Max_Strawberry];
        RefMiss = GameObject.Find("StrawberryMiss").transform.position;
        StrawberryRefOnlyFlg = new bool[Max_Strawberry];
        StrawberryColPlayer = new bool[Max_Strawberry];
        PreMax_Strawberry = Max_Strawberry;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();
        BossAnim = this.gameObject.GetComponent<Animator>();
        Scale = Boss1Manager.Boss.transform.localScale;
        oldScale = Boss1Manager.Boss.transform.localScale;

        for (int i= 0;i < Max_Strawberry;i++)
        {
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            MiddlePoint[i] = GameObject.Find("Cube").transform.position; 
            EndPoint[i] = GameObject.Find("CubeEnd").transform.position; 
            MiddlePoint[i].x -= (11f * i);
            EndPoint[i].x -= (22f * i);
        }
        OnlyFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�{�X�����񂾂珈������߂�
        if (!GameData.isAliveBoss1)
        {
            //���ꂼ��̏�������������
            if (Knife != null)
            {
                Destroy(Knife);
            }
            if (Fork != null)
            {
                Destroy(Fork);
            }
            for (int i = 0; i < Max_Strawberry; i++)
            {
                if(Strawberry[i] !=null)
                {
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                }
            }
            //�{�X��|�����������N�����ʂɈړ�
            Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_END;
        }
    }
    //���ꂼ��̍U������
    void AnimFlagOnOff()
    {
        if (!AnimFlg)
        {
            AnimFlg = true;
            return;
        }
        if(AnimFlg)
        {
            AnimFlg = false;
            return;
        }
        
    }
    void BossTakeToCase()
    {
        if(BossTakeCase == BossMove.Boss_State.charge)
        {
            BossRushAnim();
        }
        
    }
    void AnimMoveFlgOnOff()
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
    //----------------------------------------------------------
    //�ߋ���(�ːi)
    public void Boss1Fork()
    {
        //�A�j���[�V�����Đ�
        if (!AnimFlg)
        {
            AnimFlagOnOff();
            BossAnim.SetBool("IdleToTake", true);
            BossTakeCase = BossMove.Boss_State.charge;
        }
        //���̏������I����Ă�����J�n

        if (OnlyFlg && MoveFlg)
        {
                //�{�X���ːi�I����ɕς��鏈��
                if (BossReturnFlg)
                {
                    RefrectFlg = false;
                    BossReturnTime += Time.deltaTime * RushReturnSpeed;
                    //�Ō�܂ōU�����I����Ă�����
                    if (RushEndFlg)
                    {
                    //Boss1Manager.BossPos = Beziercurve.SecondCurve(RushEndPoint, RushMiddlePoint, BossStartPoint, BossReturnTime);
                    //�������ς���Ă���X�P�[�����𔽓]
                    if (Scale.x != -1)
                    {
                        Scale.x *= -1;
                        Boss1Manager.Boss.transform.localScale = Scale;
                    }
                    //��]�̖ڕW�l
                    Quaternion target = new Quaternion();
                    //������ݒ�
                    target = Quaternion.LookRotation(Rotate);
                    //��������]������
                    Boss1Manager.Boss.transform.rotation = Quaternion.RotateTowards(Boss1Manager.Boss.transform.rotation, target, RotateSpeed);
                }
                
                    //�r���Œe����Ă�����
                    if (!RushEndFlg)
                    {
                        Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);
                    }
                    //�J�n�n�_�܂Ŗ߂��Ă����Ƃ��ɂ�����돉����
                    if (BossReturnTime >= 1.0f)
                    {
                    if (Fork != null)
                    {
                        Destroy(Fork);
                    }
                        ReturnDelay = false;
                        RushEndFlg = false;
                        BossReturnFlg = false;
                        AnimFlagOnOff();
                        BossAnim.SetBool("IdleToTake", false);
                        BossAnim.SetBool("RushToJump", false);
                        AnimMoveFlgOnOff();
                        BossReturnTime = 0;
                         BossAnim.speed = 1;
                    if (HPgage.currentHp >= 50)
                        {
                            BossMove.SetState(BossMove.Boss_State.idle);
                        }
                        if (HPgage.currentHp < 50)
                        {
                            Debug.Log("�A�C�h�����I�I�I�I�I�I�I�I�I�I�I");
                            BossMove.AttackCount += 1;
                            BossMove.SetState(BossMove.Boss_State.idle);
                        }
                        OnlyFlg = false;
                    }
                    return;
                }
                //�e���ꂽ���񂾂��������镔��
                if (RefrectFlg)
                {
                    RushRefFlg = true;
                    RushPlayerPoint = Boss1Manager.Boss.transform.position;
                    RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                    BossAnim.SetBool("RushToJump", false);
                    BossAnim.SetBool("Blow", true);
                    
                    RefrectFlg = false;
                }
                //�e����Ă��Ȃ������ꍇ�̏���
                if (!RushRefFlg)
                {
                    RushTime += Time.deltaTime * RushSpeed;
                    Boss1Manager.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    RushReturnSpeed = 1f;
                    RushEndFlg = true;
                    BossReturnFlg = true;
                    RushTime = 0;
                    return;
                }
            }
                //�e����Ă����ꍇ�̏���
                if (RushRefFlg)
                {
                    RushRefTime += Time.deltaTime * 2f;
                    //�ǂɂԂ��Ă���悤�Ɍ�����
                    Boss1Manager.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                    if (RushRefTime >= 1.0f)
                    {
                        Destroy(Fork);
                        BossAnim.SetBool("Blow", false);
                        BossAnim.SetTrigger("WallHit");
                        BossAnim.Play("WallHit");
                        BossAnim.speed = 0.3f;
                    if (ReturnDelay)
                    {
                        RushReturnSpeed = 1.5f;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        BossAnim.SetBool("IdleToTake", false);
                        BossAnim.SetBool("RushToJump", false);
                        HpScript.DelHP(RushDamage);
                        RushTime = 0;
                        RushRefTime = 0;
                        SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        return;
                    }
                    }
                }
            }
        
    }
    void ReturnGround()
    {
        ReturnDelay = true;
    }
    void BossRushAnim()
    {
        //�ːi�U�����n�߂邽�߂ɖ����񂾂��������镔��
        if (!OnlyFlg)
        {
            OnlyFlg = true;
            Debug.Log("Pos : " + Boss1Manager.BossPos);
            RushStartPoint = Boss1Manager.BossPos;
            RushEndPoint = GameObject.Find("ForkEndPoint").transform.position;
            RushMiddlePoint = GameObject.Find("RushMiddle").transform.position;
            ForkPos = GameObject.Find("ForkPos").transform.position;
            Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
            Fork.transform.parent = GameObject.Find("ForkPos").transform;
            BossAnim.SetTrigger("TakeToRushTr");
            BossAnim.SetBool("RushToJump",true);
            BossAnim.SetBool("IdleToTake", false);
            SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
        }
    }

    //----------------------------------------------------------
    //�C�`�S�U��
    public void Boss1Strawberry()
    {
        
        if (!OnlyFlg)
        {
            OnlyFlg = true;
            BossAnim.SetTrigger("Strawberry");
            BossAnim.Play("StrawBerry");
            
        }
        //�C�`�S�̏������S���I��������ʂ菉����
        if (AliveStrawberry >= Max_Strawberry)
        {
            AliveStrawberry = 0;
            StrawberryNum = 0;
            StrawBerryMany = 0;
            OnlyFlg = false;
            if (HPgage.currentHp >= 50)
            {
                BossMove.SetState(BossMove.Boss_State.idle);
            }
            if (HPgage.currentHp < 50)
            {
                BossMove.AttackCount += 1;
                BossMove.SetState(BossMove.Boss_State.idle);
            }
            return;
        }
        
        //����g�p���Ă���C�`�S�̒T������
        for (int i = 0; i < Max_Strawberry; i++)
        {
            if (StrawBerryLagFlg[i])
            {
                if (StrawberryUseFlg[i])
                {
                    
                    if (StrawberryAimScale[i].x <= 2.5f)
                    {
                        StrawberryAim[i].transform.localScale = new Vector3(StrawberryAimScale[i].x, StrawberryAimScale[i].y, StrawberryAimScale[i].z);
                        StrawberryAimScale[i].x += 0.025f;
                        StrawberryAimScale[i].y += 0.025f;
                        StrawberryAimScale[i].z += 0.025f;
                    }
                    //�e���ꂽ�Ƃ��Ɉ�񂾂��̏���
                    if (!StrawberryRefOnlyFlg[i] && StrawberryRefFlg[i])
                    {
                        StrawberryRefOnlyFlg[i] = true;
                        Vector2 Dir = Strawberry[i].transform.position - GameData.PlayerPos;
                        float rad = Mathf.Atan2(Dir.y, Dir.x);
                        float degree = rad * Mathf.Rad2Deg;
                        //�e�����p�x���Z�x���������ɔ��ł�������ς��鏈���B
                        //----------------------------------------------------------
                        if (degree <= RefrectRotOver && degree >= RefrectRotUnder)
                        {
                            if (degree >= 45.0f)
                            {
                                PlayerPoint[i].x = GameData.PlayerPos.x;
                                PlayerPoint[i].y = GameData.PlayerPos.y + 2.0f;
                                PlayerPoint[i].z = GameData.PlayerPos.z;
                                PlayerMiddlePoint[i].x = GameData.PlayerPos.x + 3.0f;
                                PlayerMiddlePoint[i].y = GameData.PlayerPos.y + 3.0f;
                                PlayerMiddlePoint[i].z = GameData.PlayerPos.z;
                                RefEndPoint = Boss1Manager.BossPos;
                                PlayerRefDir[i] = true;
                            }
                            else
                            {
                                PlayerPoint[i].x = GameData.PlayerPos.x + 2.0f;
                                PlayerPoint[i].y = GameData.PlayerPos.y;
                                PlayerPoint[i].z = GameData.PlayerPos.z;
                                RefEndPoint = Boss1Manager.BossPos;
                            }
                        }
                        else if (degree >= RefrectRotOver || degree <= RefrectRotUnder)
                        {
                            PlayerPoint[i].x = GameData.PlayerPos.x - 2.0f;
                            PlayerPoint[i].y = GameData.PlayerPos.y;
                            PlayerPoint[i].z = GameData.PlayerPos.z;
                            RefEndPoint = RefMiss;
                            RefMissFlg = true;
                        }
                        //----------------------------------------------------------
                    }
                    //�e���ꂽ��̏���
                    if (StrawberryRefFlg[i])
                    {
                        Ref_FinishTime[i] += Time.deltaTime * 2f;
                        //�e���������ɂ���ď����̎�ނ�ς���
                        //---------------------------------------------------------
                        if (!PlayerRefDir[i])
                        {
                            Strawberry[i].transform.position = Vector3.Lerp(PlayerPoint[i], RefEndPoint, Ref_FinishTime[i]);
                            Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                        }
                        if (PlayerRefDir[i])
                        {
                            Strawberry[i].transform.position = Beziercurve.SecondCurve(PlayerPoint[i], PlayerMiddlePoint[i],
                                                                                       RefEndPoint, Ref_FinishTime[i]);
                            Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                        }
                        //---------------------------------------------------------
                        //�e���I�������e�����C�`�S��������
                        if (Ref_FinishTime[i] >= 1.0f)
                        {
                            PlayerRefDir[i] = false;
                            //�e��������������{�X�̕������������ɂ����_���[�W�̏�������
                            if (!RefMissFlg)
                            {
                                HpScript.DelHP(StrawberryDamage);
                                SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                            }
                            RefMissFlg = false;
                            StrawberryUseFlg[i] = false;
                            StrawberryRefFlg[i] = false;
                            StrawberryRefOnlyFlg[i] = false;
                            StrawBerryLagFlg[i] = false;
                            Destroy(Strawberry[i]);
                            Destroy(StrawberryAim[i]);
                            Ref_FinishTime[i] = 0;
                            AliveStrawberry++;
                            FinishTime[i] = 0;
                        }
                    }
                    //�e����Ă����炱�����̏������Ȃ�
                    if (!StrawberryRefFlg[i])
                    {
                        Strawberry[i].transform.position = Beziercurve.SecondCurve(StartPoint[i], MiddlePoint[i], EndPoint[i], FinishTime[i]);
                        Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                    }
                    if(i == 0)
                    {
                        StrawberrySpeed = 2;
                    }
                    else if(i == 1)
                    {
                        StrawberrySpeed = 1.5f;
                    }
                    else
                    {
                        StrawberrySpeed = 1;
                    }
                    FinishTime[i] += Time.deltaTime * StrawberrySpeed;
                    //----------------------------------------------------------
                    //�v���C���[�ɂ������Ƃ��ɏ��������鏈���B
                    if (StrawberryColPlayer[i])
                    {
                        FinishTime[i] = 0;
                        StrawberryUseFlg[i] = false;
                        Destroy(Strawberry[i]);
                        Destroy(StrawberryAim[i]);
                        StrawBerryLagFlg[i] = false;
                        AliveStrawberry++;
                        
                    }
                    //�e���������������,�U�����͂����Ă����珈�������Ȃ�
                    if (FinishTime[i] >= 1.0f && !StrawberryRefFlg[i])
                    {
                        FinishTime[i] = 0;
                        StrawberryUseFlg[i] = false;
                        Destroy(Strawberry[i]);
                        Destroy(StrawberryAim[i]);
                        StrawBerryLagFlg[i] = false;
                        AliveStrawberry++;
                        
                    }
                }
            }
        }
    }

    //�C�`�S���ЂƂÂ������鏈���A�ő吔��葽���o�Ȃ��悤�ɁB
    void StrawBerryCreate()
        {
        if (!StrawberryUseFlg[StrawberryNum] && StrawberryNum < Max_Strawberry)
        {
            //�C�`�S�̍��W�w��
            if (StrawberryNum % 2 == 0)
            {
                StartPoint[StrawberryNum].x = GameObject.Find("middle2_R").transform.position.x;
                StartPoint[StrawberryNum].y = GameObject.Find("middle2_R").transform.position.y;
                StartPoint[StrawberryNum].z = GameObject.Find("middle2_R").transform.position.z;
            }
            if (StrawberryNum % 2 != 0)
            {
                StartPoint[StrawberryNum].x = GameObject.Find("middle2_L").transform.position.x;
                StartPoint[StrawberryNum].y = GameObject.Find("middle2_L").transform.position.y;
                StartPoint[StrawberryNum].z = GameObject.Find("middle2_L").transform.position.z;
            }
            //StartPoint.x = Boss1Manager.BossPos.x;
            //StartPoint.y = Boss1Manager.BossPos.y;
            //StartPoint.z = Boss1Manager.BossPos.z;
            //�C�`�S�̐����セ�ꂼ��̖��O�ύX
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint[StrawberryNum], Quaternion.identity);
            if (StrawberryNum % 2 == 0)
            {
                Strawberry[StrawberryNum].transform.parent = GameObject.Find("middle2_R").transform;
            }
            if (StrawberryNum % 2 != 0)
            {
                Strawberry[StrawberryNum].transform.parent = GameObject.Find("middle2_L").transform;
            }
            Strawberry[StrawberryNum].name = "strawberry" + StrawberryNum;
            StrawberryUseFlg[StrawberryNum] = true;
            //�C�`�S�̎g�p�󋵕ύX
            StrawberryAim[StrawberryNum] = Instantiate(StrawberryAimObj, EndPoint[StrawberryNum], Quaternion.Euler(-7.952f, 0f, 0f));
            StrawberryAim[StrawberryNum].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            StrawberryAimScale[StrawberryNum] = new Vector3(1.0f, 1.0f, 1.0f);
            StrawberryNum++;
            SoundManager.Play(SoundData.eSE.SE_BOOS1_STRAWBERRY, SoundData.GameAudioList);

        }
        
    }
    void ShotStrawberry()
    {
        if (StrawBerryMany % 2 == 0)
        {
            
            StartPoint[StrawBerryMany].x = GameObject.Find("middle2_R").transform.position.x;
            StartPoint[StrawBerryMany].y = GameObject.Find("middle2_R").transform.position.y;
            StartPoint[StrawBerryMany].z = GameObject.Find("middle2_R").transform.position.z;
            Strawberry[StrawBerryMany].transform.parent.DetachChildren();

        }
        else if (StrawBerryMany % 2 == 1)
        {
            
            StartPoint[StrawBerryMany].x = GameObject.Find("middle2_L").transform.position.x;
            StartPoint[StrawBerryMany].y = GameObject.Find("middle2_L").transform.position.y;
            StartPoint[StrawBerryMany].z = GameObject.Find("middle2_L").transform.position.z;
            Strawberry[StrawBerryMany].transform.parent.DetachChildren();

        }
        StrawBerryLagFlg[StrawBerryMany] = true;
        
        StrawBerryMany++;
    }
    //----------------------------------------------------------
    public void Boss1Knife()
    {
        if (!AnimFlg)
        {
            AnimFlagOnOff();
            BossAnim.SetBool("TakeToKnife",true);
        }
        //�i�C�t������
        
            if (!AimFlg)
            {
                if (!AimOnly)
                {
                    KnifeAim = Instantiate(KnifeAimObj, GameData.PlayerPos, Quaternion.Euler(90f, 0f, 0f));
                    AimOnly = true;
                }
                KnifeAimPos.x = GameData.PlayerPos.x;
                KnifeAimPos.y = GameData.PlayerPos.y;
                KnifeAimPos.z = GameData.PlayerPos.z + 3.0f;

                if (KnifeThrowTime >= KnifeThrowNowTime)
                {
                    KnifeAim.transform.position = KnifeAimPos;
                    KnifeThrowNowTime += Time.deltaTime;
                }
                else
                {
                    AimFlg = true;
                    AimOnly = false;
                    KnifeThrowNowTime = 0;
                }
            }

        if (AimStart)
        {
            if (!OnlyFlg)
            {
                OnlyFlg = true;
                GameObject.Find("knife(Clone)").transform.parent.DetachChildren();
                Vector3 KnifeDir = GameData.PlayerPos - Knife.transform.position;
                // �^�[�Q�b�g�̕����ւ̉�]
                KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
                KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
            }

            if (RefrectFlg)
            {
                KnifeRefFlg = true;
                KnifePlayerPoint.x = GameData.PlayerPos.x + 3.0f;
                KnifePlayerPoint.y = GameData.PlayerPos.y;
                KnifePlayerPoint.z = GameData.PlayerPos.z;
                RefrectFlg = false;
            }
            if (OnlyFlg && !KnifeRefFlg)
            {
                KnifeTime += Time.deltaTime * KnifeSpeed;
                Knife.transform.position = Vector3.Lerp(KnifeStartPoint, KnifeEndPoint, KnifeTime);
                if (KnifeTime >= 1.0f)
                {
                    OnlyFlg = false;
                    AimFlg = false;
                    AnimFlagOnOff();
                    Debug.Log("A�p�^�[�����ʂ�����[");
                    BossAnim.SetBool("TakeToKnife", false);
                    KnifeTime = 0;
                    AimOnly = false;
                    AimStart = false;
                    Destroy(Knife);
                    Destroy(KnifeAim);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }

            if (KnifeRefFlg)
            {
                KnifeRefTime += Time.deltaTime * 3;
                Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss1Manager.BossPos, KnifeRefTime);
                Debug.Log("Knife " + KnifePlayerPoint);
                if (KnifeRefTime >= 1.0f)
                {
                    HpScript.DelHP(KnifeDamage);
                    OnlyFlg = false;
                    KnifeRefFlg = false;
                    AimFlg = false;
                    BossAnim.SetBool("??ToDamage", true);
                    BossAnim.Play("Damage");
                    BossAnim.SetBool("??ToDamage", false);
                    BossAnim.SetBool("TakeToKnife", false);
                    AnimFlagOnOff();
                    AimOnly = false;
                    AimStart = false;
                    KnifeTime = 0;
                    KnifeRefTime = 0;
                    Destroy(KnifeAim);
                    SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);

                    Destroy(Knife);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }
        }
            
    }
    void StartShotAnim()
    {
        AimStart = true;
        KnifeStartPoint.x = Boss1Manager.BossPos.x;
        KnifeStartPoint.y = Boss1Manager.BossPos.y + 4;
        KnifeStartPoint.z = Boss1Manager.BossPos.z;
        KnifeEndPoint = GameData.PlayerPos;
        ForkPos = GameObject.Find("KnifePos").transform.position;
        Knife = Instantiate(Knifeobj, ForkPos, Quaternion.identity);
        Knife.transform.parent = GameObject.Find("KnifePos").transform;
        SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
    }
    
}
