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
    //�C�`�S���e�ϐ�
    //----------------------------------------------------------
    GameObject obj;                             //�C�`�S�����p
    static public GameObject [] Strawberry;            //�C�`�S������i�[
    static public bool[] StrawberryFlg;                      //����񂫂������
    [SerializeField] public int Max_Strawberry;  //�ł����C�`�S�̔��f
    private int StrawberryNum;
    static public int AliveStrawberry;
    private Vector3 StrawberryPos;
    static public int LoopSave;

    //�x�W�G�Ȑ��p
    private Vector3  StartPoint;
    private Vector3 [] MiddlePoint;
    private Vector3 [] EndPoint;
    [SerializeField] private Vector3 FirstMiddlePoint;
    [SerializeField] private Vector3 FirstEndPoint;
    static public float [] FinishTime;
    
    //----------------------------------------------------------



    private BossAttack Boss1AttackState = BossAttack.Attack1;
    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("strawberry");
        //Strawberry = new GameObject[StrawberryNum];
        StrawberryNum = 0;
        Strawberry = new GameObject[Max_Strawberry];
        StrawberryFlg = new bool[Max_Strawberry];
        MiddlePoint = new Vector3[Max_Strawberry];
        EndPoint = new Vector3[Max_Strawberry];
        FinishTime = new float[Max_Strawberry];

        StartPoint.x = Boss.BossPos.x;
        StartPoint.y = Boss.BossPos.y += 2;
        StartPoint.z = Boss.BossPos.z;
        for (int i=0;i < Max_Strawberry;i++)
        {
            StrawberryFlg[i] = false;
            MiddlePoint[i] = FirstMiddlePoint;
            EndPoint[i] = FirstEndPoint;
            MiddlePoint[i].x += (1.7f * i);
            EndPoint[i].x += (4f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        for (int i = AliveStrawberry; i < StrawberryNum + 1; i++)
            {
                
            //�C�`�S
            if (!StrawberryFlg[i])
                {
                    Strawberry[i] = Instantiate(obj, StartPoint, Quaternion.identity);
                    Debug.Log("Flg : " + StrawberryFlg[i]);
                    StrawberryFlg[i] = true;
                }
            if (StrawberryFlg[i])
            {
                //�C�`�S�������珉��������񂾂�by��
                FinishTime[i] += Time.deltaTime;
            }
                Vector3 S = Vector3.Lerp(StartPoint, MiddlePoint[i], FinishTime[i]);
                Vector3 E = Vector3.Lerp(MiddlePoint[i], EndPoint[i], FinishTime[i]);
                Strawberry[i].transform.position = Vector3.Lerp(S, E, FinishTime[i]);
                if (FinishTime[i] > 0.5f && !StrawberryFlg[i + 1])
                {
                    StrawberryNum++;
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
