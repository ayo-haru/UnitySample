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
    bool OnlyRushFlg;                                       //������
    [SerializeField] public float RushSpeed;                //�ːi�̃X�s�[�h
    bool RushRefFlg = false;                                //�ːi���͂���������
    float RushTime;                                         //�ːi�̌o�ߎ���
    float RushRefTime;                                      //�e������̎��Ԍo��
    bool BossReturnFlg;
    float BossReturnTime;                                   //�ːi��߂�܂ł̎���
    bool RushEndFlg;
    float RushReturnSpeed;
    float ReturnDelay;                                      //�߂낤�Ƃ���܂ł̎���
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

    //�x�W�G�Ȑ��p
    Vector3  StartPoint;
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
    //----------------------------------------------------------



    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("strawberry");
        Knifeobj = (GameObject)Resources.Load("knife");
        Forkobj = (GameObject)Resources.Load("fork");
        StrawberryAimObj = (GameObject)Resources.Load("StrawberryAim");
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryUseFlg = new bool[Max_Strawberry];
        StrawberryRefFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        PlayerPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];
        Ref_FinishTime = new float[Max_Strawberry];
        PlayerMiddlePoint = new Vector3[Max_Strawberry];
        StrawberryAim = new GameObject[Max_Strawberry];
        StrawberryAimScale = new Vector3[Max_Strawberry];
        BossStartPoint = GameObject.Find("BossPoint").transform.position;
        PlayerRefDir = new bool[Max_Strawberry];
        RefMiss = GameObject.Find("StrawberryMiss").transform.position;
        StrawberryRefOnlyFlg = new bool[Max_Strawberry];
        StrawberryColPlayer = new bool[Max_Strawberry];
        PreMax_Strawberry = Max_Strawberry;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();

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
    //----------------------------------------------------------
    //�ߋ���(�ːi)
    public void Boss1Fork()
    {
        //�ːi�U�����n�߂邽�߂ɖ����񂾂��������镔��
        if(!OnlyFlg)
        {
            OnlyFlg = true;
            Debug.Log("Pos : " + Boss1Manager.BossPos);
            RushStartPoint = Boss1Manager.BossPos;
            RushEndPoint = GameObject.Find("ForkEndPoint").transform.position;
            RushMiddlePoint = GameObject.Find("RushMiddle").transform.position;
            RushStartPoint.y -= 3.0f;
            Fork = Instantiate(Forkobj, RushStartPoint, Quaternion.Euler(0.0f,0.0f,90.0f));
            RushStartPoint.y +=3.0f;
            Fork.transform.parent = Boss1Manager.Boss.transform;
            SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
        }
        //���̏������I����Ă�����J�n
        if(OnlyFlg)
        {
            //�{�X���ːi�I����ɕς��鏈��
            if(BossReturnFlg)
            {
                RefrectFlg = false;
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                //�Ō�܂ōU�����I����Ă�����
                if (RushEndFlg)
                {
                    Boss1Manager.BossPos = Beziercurve.SecondCurve(RushEndPoint, RushMiddlePoint, BossStartPoint, BossReturnTime);
                }
                //�r���Œe����Ă�����
                if (!RushEndFlg)
                {
                    Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, BossStartPoint, BossReturnTime);
                }
                //�J�n�n�_�܂Ŗ߂��Ă����Ƃ��ɂ�����돉����
                if (BossReturnTime >= 1.0f)
                {
                    Destroy(Fork);
                    ReturnDelay = 0;
                    RushEndFlg = false;
                    BossReturnFlg = false;
                    BossReturnTime = 0;
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if(HPgage.currentHp < 50)
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
                RushPlayerPoint.x = GameData.PlayerPos.x + 2.0f;
                RushPlayerPoint.y = GameData.PlayerPos.y + 3.0f;
                RushPlayerPoint.z = GameData.PlayerPos.z;
                RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                
                RefrectFlg = false; 
            }
            //�e����Ă��Ȃ������ꍇ�̏���
            if (!RushRefFlg)
            {
                RushTime += Time.deltaTime * RushSpeed;
                Boss1Manager.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    //�ŏI�n�_�܂ōs�����セ�����珉���n�_�ɖ߂�܂ł̍d��
                    ReturnDelay += Time.deltaTime;
                    if (ReturnDelay >= 1.0f)
                    {
                        RushReturnSpeed = 1.5f;
                        RushEndFlg = true;
                        BossReturnFlg = true;
                        RushTime = 0;
                        
                    }
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
                        RushReturnSpeed = 2;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        HpScript.DelHP(RushDamage);
                        RushTime = 0;
                        RushRefTime = 0;
                    SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                    return;
                }
            }
        }
    }
    //----------------------------------------------------------
    //�C�`�S�U��
    public void Boss1Strawberry()
    {
        //�C�`�S�̏������S���I��������ʂ菉����
        if (AliveStrawberry >= Max_Strawberry)
        {
            AliveStrawberry = 0;
            StrawberryNum = 0;
            StrawBerryMany = 0;
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
        //�C�`�S���ЂƂÂ������鏈���A�ő吔��葽���o�Ȃ��悤�ɁB
        if (!StrawberryUseFlg[StrawberryNum] && StrawBerryMany < Max_Strawberry)
        {
            //�C�`�S�̍��W�w��
            StartPoint.x = Boss1Manager.BossPos.x;
            StartPoint.y = Boss1Manager.BossPos.y + 4;
            StartPoint.z = Boss1Manager.BossPos.z;
            //�C�`�S�̐����セ�ꂼ��̖��O�ύX
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint, Quaternion.identity);
            Strawberry[StrawberryNum].name = "strawberry" + StrawberryNum;
            StrawberryAim[StrawberryNum] = Instantiate(StrawberryAimObj, EndPoint[StrawberryNum], Quaternion.Euler(-7.952f, 0f,0f));
            StrawberryAim[StrawberryNum].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            StrawberryAimScale[StrawberryNum] = new Vector3(1.0f, 1.0f, 1.0f);
            //�C�`�S�̎g�p�󋵕ύX
            StrawberryUseFlg[StrawberryNum] = true;
            StrawBerryMany += 1;
            SoundManager.Play(SoundData.eSE.SE_BOOS1_STRAWBERRY, SoundData.GameAudioList);

        }
        //����g�p���Ă���C�`�S�̒T������
        for (int i = 0; i < Max_Strawberry; i++)
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
                   if(!PlayerRefDir[i])
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
                        StrawberryUseFlg[i] = false ;
                        StrawberryRefFlg[i] = false ;
                        StrawberryRefOnlyFlg[i] = false; ;
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
                    Strawberry[i].transform.position = Beziercurve.SecondCurve(StartPoint, MiddlePoint[i], EndPoint[i], FinishTime[i]);
                    Strawberry[i].transform.Rotate(new Vector3(0, 0, 10));
                }
                FinishTime[i] += Time.deltaTime * StrawberrySpeed;
                if (i < Max_Strawberry - 1)
                {
                    //�O�̃C�`�S���Z���炢�i�񂾂Ƃ��ɂ��̏���������B
                    if (FinishTime[i] >= 0.5f && !StrawberryUseFlg[i + 1])
                    {
                        StrawberryNum++;
                    }
                }
                //----------------------------------------------------------
                //�v���C���[�ɂ������Ƃ��ɏ��������鏈���B
                if(StrawberryColPlayer[i])
                {
                    FinishTime[i] = 0;
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                    Destroy(StrawberryAim[i]);
                    AliveStrawberry++;
                }
                //�e���������������,�U�����͂����Ă����珈�������Ȃ�
                if (FinishTime[i] >= 1.0f && !StrawberryRefFlg[i])
                {
                    FinishTime[i] = 0;
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                    Destroy(StrawberryAim[i]);
                    AliveStrawberry++;
                }
            }
        }
    }
    //----------------------------------------------------------
    public void Boss1Knife()
    {
        //�i�C�t������
        
        if(!OnlyFlg)
        {
            
            OnlyFlg = true;
            KnifeStartPoint.x = Boss1Manager.BossPos.x;
            KnifeStartPoint.y = Boss1Manager.BossPos.y + 4;
            KnifeStartPoint.z = Boss1Manager.BossPos.z;
            KnifeEndPoint = GameData.PlayerPos;
            Knife = Instantiate(Knifeobj, KnifeStartPoint, Quaternion.identity);
            Vector3 KnifeDir = GameData.PlayerPos - Knife.transform.position;
            // �^�[�Q�b�g�̕����ւ̉�]
            KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
            KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
            Knife.transform.rotation = KnifeRotDir * KnifeRotForward;

            SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
            
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
                KnifeTime = 0;
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
        
        if(KnifeRefFlg)
        {
            KnifeRefTime += Time.deltaTime * 3;
            Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss1Manager.BossPos, KnifeRefTime);
            Debug.Log("Knife " + KnifePlayerPoint);
            if (KnifeRefTime >= 1.0f)
            {
                HpScript.DelHP(KnifeDamage);
                OnlyFlg = false;
                KnifeRefFlg = false;
                KnifeTime = 0;
                KnifeRefTime = 0;
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
