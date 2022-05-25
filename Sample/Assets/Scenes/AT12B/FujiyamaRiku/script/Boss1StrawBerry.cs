using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StrawBerry : MonoBehaviour
{
    Boss1Attack BossAttack;

    GameObject obj;                                         //�C�`�S�����p
    GameObject[] Strawberry;                               //�C�`�S������i�[
    [SerializeField] public int Max_Strawberry;             //�C�`�S�̍ő吔
    static public int PreMax_Strawberry;                    //�ő吔���ق��̕����ł��g����悤��
    int StrawberryNum;                                      //���݂̎ˏo�ς݃C�`�S�v�Z�p
    int AliveStrawberry;                                    //�C�`�S�̐����m�F�p
    bool[] StrawberryUseFlg;                         //�C�`�S���g���Ă��邩�ǂ����̃t���O
    public bool[] StrawberryRefFlg;                 //�C�`�S���e���ꂽ���ǂ����̃t���O
    [SerializeField] public float StrawberrySpeed;          //�C�`�S�����ł������x
    bool[] PlayerRefDir;                                    //�e�����Ƃ��̕����t���O
    Vector3 RefMiss;                                        //�e���̂Ɏ��s�����Ƃ��̍��W�i�[�p
    bool RefMissFlg;                                        //�e���̂Ɏ��s�����Ƃ��ɏ�������񂾂�����p
    int StrawBerryMany;                                     //�C�`�S���ő吔�ȏ�o���Ȃ��悤�ɂ��邽�߂̏��������Ԃ񂢂�Ȃ�
    bool[] StrawberryRefOnlyFlg;                            //�e���ꂽ���̂ň�񂾂�����������̗p
    public bool[] StrawberryColPlayer;               //�v���C���[�ɓ����������p�̏���
    GameObject StrawberryAimObj;
    GameObject[] StrawberryAim;
    Vector3[] StrawberryAimScale;
    bool[] StrawBerryLagFlg;
    Vector3 WeaponPos;

    //�x�W�G�Ȑ��p
    Vector3[] StartPoint;
    Vector3[] MiddlePoint;
    Vector3[] EndPoint;
    float[] FinishTime;
    Vector3 RefEndPoint;
    float[] Ref_FinishTime;
    Vector3[] PlayerPoint;
    Vector3[] PlayerMiddlePoint;
    // Start is called before the first frame update
    void Start()
    {
        BossAttack = this.GetComponent<Boss1Attack>();
        obj = (GameObject)Resources.Load("strawberry");
        StrawberryAimObj = (GameObject)Resources.Load("StrawberryAim");
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
        PlayerRefDir = new bool[Max_Strawberry];
        StrawberryRefOnlyFlg = new bool[Max_Strawberry];
        StrawberryColPlayer = new bool[Max_Strawberry];
        PreMax_Strawberry = Max_Strawberry;
        for (int i = 0; i < Max_Strawberry; i++)
        {
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            MiddlePoint[i] = GameObject.Find("Strawberry").transform.position;
            EndPoint[i] = GameObject.Find("StrawberryEnd").transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
            for (int i = 0; i < Max_Strawberry; i++)
            {
                if (Strawberry[i] != null)
                {
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                }
            }
        }
    }
    public void Boss1Strawberry()
    {

        if (!BossAttack.OnlyFlg)
        {
            BossAttack.OnlyFlg = true;
            BossAttack.BossAnim.SetTrigger("Strawberry");
            BossAttack.BossAnim.Play("StrawBerry");

        }
        //�C�`�S�̏������S���I��������ʂ菉����
        if (AliveStrawberry >= Max_Strawberry)
        {
            AliveStrawberry = 0;
            StrawberryNum = 0;
            StrawBerryMany = 0;
            BossAttack.OnlyFlg = false;
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
        if (GameObject.Find("Weapon(Clone)"))
        {
            WeaponPos = GameObject.Find("Weapon(Clone)").transform.position;
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
                        Debug.Log("�͂�������������������������" + degree);
                        //�e�����p�x���Z�x���������ɔ��ł�������ς��鏈���B
                        //----------------------------------------------------------
                        if (!BossAttack.RFChange)
                        {
                            if (degree <= BossAttack.RefrectRotOver && degree >= BossAttack.RefrectRotUnder)
                            {
                                if (degree >= 45.0f)
                                {
                                    PlayerPoint[i].x = WeaponPos.x;
                                    PlayerPoint[i].y = WeaponPos.y + 2.0f;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    PlayerMiddlePoint[i].x = WeaponPos.x + 3.0f;
                                    PlayerMiddlePoint[i].y = WeaponPos.y + 3.0f;
                                    PlayerMiddlePoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                    PlayerRefDir[i] = true;
                                }
                                else
                                {
                                    PlayerPoint[i].x = WeaponPos.x + 2.0f;
                                    PlayerPoint[i].y = WeaponPos.y;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                }
                            }
                            else if (degree <= BossAttack. RefrectRotOver || degree >= BossAttack.RefrectRotUnder)
                            {
                                PlayerPoint[i].x = WeaponPos.x;
                                PlayerPoint[i].y = WeaponPos.y;
                                PlayerPoint[i].z = WeaponPos.z;
                                RefMiss = GameObject.Find("StrawberryMiss").transform.position;
                                RefEndPoint = RefMiss;
                                RefMissFlg = true;
                            }
                        }
                        if (BossAttack.RFChange)
                        {
                            if (degree >= BossAttack.RefrectRotOver && degree >= BossAttack.RefrectRotUnder * -1)
                            {
                                if (degree >= 135.0f)
                                {
                                    PlayerPoint[i].x = WeaponPos.x;
                                    PlayerPoint[i].y = WeaponPos.y - 2.0f;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    PlayerMiddlePoint[i].x = WeaponPos.x - 3.0f;
                                    PlayerMiddlePoint[i].y = WeaponPos.y - 3.0f;
                                    PlayerMiddlePoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                    PlayerRefDir[i] = true;
                                }
                                else
                                {
                                    PlayerPoint[i].x = WeaponPos.x - 2.0f;
                                    PlayerPoint[i].y = WeaponPos.y;
                                    PlayerPoint[i].z = WeaponPos.z;
                                    RefEndPoint = Boss1Manager.BossPos;
                                }
                            }
                            else if (degree <= BossAttack.RefrectRotOver || degree >= BossAttack.RefrectRotUnder)
                            {
                                PlayerPoint[i].x = WeaponPos.x;
                                PlayerPoint[i].y = WeaponPos.y;
                                PlayerPoint[i].z = WeaponPos.z;
                                RefMiss = GameObject.Find("LeftStrawberryMiss").transform.position;
                                RefEndPoint = RefMiss;
                                RefMissFlg = true;
                            }
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
                            BossAttack.DamageColor.Invoke("Play", 0.0f);
                            EffectManager.Play(EffectData.eEFFECT.EF_BOSS_STRAWBERRY, Strawberry[i].transform.position);
                            PlayerRefDir[i] = false;
                            //�e��������������{�X�̕������������ɂ����_���[�W�̏�������
                            if (!RefMissFlg)
                            {
                                BossAttack.HpScript.DelHP(BossAttack.StrawberryDamage);
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
                    if (i == 0)
                    {
                        StrawberrySpeed = 2;
                    }
                    else if (i == 1)
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
            
            if (BossAttack.RFChange)
            {
                MiddlePoint[StrawberryNum] = GameObject.Find("LeftStrawberry").transform.position;
                EndPoint[StrawberryNum] = GameObject.Find("LeftStrawberryEnd").transform.position;
                BossAttack.RFNum = 1;
            }
            if (!BossAttack.RFChange)
            {
                MiddlePoint[StrawberryNum] = GameObject.Find("Strawberry").transform.position;
                EndPoint[StrawberryNum] = GameObject.Find("StrawberryEnd").transform.position;
                BossAttack.RFNum = -1;
            }
            MiddlePoint[StrawberryNum].x += (11f * StrawberryNum) * BossAttack.RFNum;
            EndPoint[StrawberryNum].x += (22f * StrawberryNum) * BossAttack.RFNum;
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
}
