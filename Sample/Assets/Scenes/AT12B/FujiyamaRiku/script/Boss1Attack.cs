using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1Attack : MonoBehaviour
{
    //�{�X�̍U���̎��
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Attack4, 
    }
    static public bool RefrectFlg = false;                //�v���C���[���p���B�ɐ����������ǂ����̎󂯎��p
    //�C�`�S���e�ϐ�
    //----------------------------------------------------------
    GameObject obj;                                       //�C�`�S�����p
    static public GameObject [] Strawberry;               //�C�`�S������i�[
    [SerializeField] public int Max_Strawberry;           //�ł����C�`�S�̔��f
    private int StrawberryNum;
    static public int AliveStrawberry;
    private Vector3 StrawberryPos;
    static public bool[] StrawberryUseFlg;
    static public bool [] StrawberryRefFlg;
    private bool[] StrawberrySpnFlg;
    static public int LoopSave;

    //�x�W�G�Ȑ��p
    private Vector3  StartPoint;
    private Vector3 [] MiddlePoint;
    private Vector3 [] EndPoint;
    [SerializeField] private Vector3 FirstMiddlePoint;
    [SerializeField] private Vector3 FirstEndPoint;
    static public float [] FinishTime;
    static public float[] Ref_FinishTime;
    public static Vector3[] PlayerPoint;
    //----------------------------------------------------------



    private BossAttack Boss1AttackState = BossAttack.Attack1;
    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("strawberry");
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryUseFlg = new bool[Max_Strawberry];
        StrawberrySpnFlg = new bool[Max_Strawberry];
        StrawberryRefFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        PlayerPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];
        Ref_FinishTime = new float[Max_Strawberry];
        

        StartPoint.x = Boss.BossPos.x;
        StartPoint.y = Boss.BossPos.y += 2;
        StartPoint.z = Boss.BossPos.z;
        for (int i=0;i < Max_Strawberry;i++)
        {
            StrawberryRefFlg[i] = false;
            StrawberryUseFlg[i] = false;
            StrawberrySpnFlg[i] = false;
            MiddlePoint[i] = FirstMiddlePoint;
            EndPoint[i] = FirstEndPoint;
            MiddlePoint[i].x -= (1.7f * i);
            EndPoint[i].x -= (4f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Boss1AttackState = BossAttack.Attack1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Boss1AttackState = BossAttack.Attack2;
        }

        //�U���ɂ���ď�����ς��鏈��
        switch(Boss1AttackState)
        {
            case BossAttack.Attack1:
                {
                    break;
                }
            case BossAttack.Attack2:
                {
                    Boss1Attack2();
                    break;
                }
            case BossAttack.Attack3:
                {
                    break;
                }
            case BossAttack.Attack4:
                {
                    break;
                }
        }
    }
    //���ꂼ��̍U������
    private void Boss1Attack1()
    {
        //�ߋ���
    }
    private void Boss1Attack2()
    {
        if(AliveStrawberry>=Max_Strawberry)
        {
            return;
        }
        if (!StrawberryUseFlg[StrawberryNum])
        {
            Strawberry[StrawberryNum] = Instantiate(obj, StartPoint, Quaternion.identity);
            StrawberryUseFlg[StrawberryNum] = true;

            //Debug.Log("Flg1 : " + StrawberryUseFlg[StrawberryNum]);
            //Debug.Log("Number : " + StrawberryNum);
        }
        for (int i = 0; i < Max_Strawberry; i++)
            {
            //�C�`�S
            if (StrawberryUseFlg[i])
            {
                //�e���ꂽ�Ƃ�
                if (RefrectFlg)
                {
                    StrawberryRefFlg[i] = true;
                    PlayerPoint[i].x = GameData.PlayerPos.x + 3.0f;
                    PlayerPoint[i].y = GameData.PlayerPos.y;
                    PlayerPoint[i].z = GameData.PlayerPos.z;
                    RefrectFlg = false;
                    //AliveStrawberry++;
                }
                //�e���ꂽ��
                if (StrawberryRefFlg[i])
                {

                    Strawberry[i].transform.position = Vector3.Lerp(PlayerPoint[i], Boss.BossPos, Ref_FinishTime[i]);
                    Ref_FinishTime[i] += Time.deltaTime;

                    if (Ref_FinishTime[i] >= 1.0f)
                    {
                        Damage.damage = 5;
                        HPgage.DelHP();
                        //StrawberryUseFlg[i] = false ;
                        //StrawberryRefFlg[i] = false ;
                        Destroy(Strawberry[i]);
                        Ref_FinishTime[i] = 0;
                        //FinishTime[i] = 0;
                        //Debug.Log("Save : " + LoopSave);
                    }
                }
                //�C�`�S�������珉��������񂾂�by��
                //�e����Ă����炱�����̏������Ȃ�
                if (!StrawberryRefFlg[i])
                {
                    Vector3 S = Vector3.Lerp(StartPoint, MiddlePoint[i], FinishTime[i]);
                    Vector3 E = Vector3.Lerp(MiddlePoint[i], EndPoint[i], FinishTime[i]);
                    Strawberry[i].transform.position = Vector3.Lerp(S, E, FinishTime[i]);
                }
                
                FinishTime[i] += Time.deltaTime;
                //����������Ɉ������Ă�B
                //�e���ƍő�l������Ȃ���
                //�e���������炢�s������V�����̐�������
                if (i < Max_Strawberry - 1)
                {
                    if (FinishTime[i] >= 0.5f && !StrawberryUseFlg[i + 1])
                    {
                            StrawberryNum++;
                            Debug.Log("I : " + (i));
                            Debug.Log("StrawberryNum : " + StrawberryNum);
                    }
                }
                //----------------------------------------------------------
                //�e���������������,�U�����͂����Ă����珈�������Ȃ�
                if (FinishTime[i] >= 1.0f && !StrawberryRefFlg[i])
                {
                    FinishTime[i] = 0;
                    StrawberryUseFlg[i] = false;
                    Destroy(Strawberry[i]);
                    AliveStrawberry++;
                }
            }
        }
    }
    
    private void Boss1Attack3()
    { 
        //�i�C�t�t�H�[�N������
    }
    private void Boss1Attack4()
    {
        //�X�[�p�[���n
    }
}
