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
    GameObject obj;
    //[SerializeField] public GameObject Strawberry;
    bool [] StrawberryFlg;
    [SerializeField] public int StrawberryNum;

    private BossAttack Boss1AttackState = BossAttack.Attack1;
    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("strawberry");
        //Strawberry = new GameObject[StrawberryNum];
        StrawberryFlg = new bool[StrawberryNum];
        StrawberryFlg[0] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!StrawberryFlg[0])
            {
                obj.transform.position = new Vector3(10.0f, 2.0f, -2f);
                Instantiate(obj);
                Debug.Log("State : " + Boss1AttackState);
                //Rigidbody rb = obj.GetComponent<Rigidbody>();  // 
                Vector3 force = new Vector3(-40.0f, 40.0f, 0.0f);    // �͂�ݒ�
                GameObject.Find("strawberry(Clone)").GetComponent<Rigidbody>().AddForce(force);
                //rb.AddForce(force);  // �͂�������
                StrawberryFlg[0] = true;
            }
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
        
        //�C�`�S
        
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
