using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoundBoll : MonoBehaviour
{
    public GameObject BoundBoll;
    public LB_Attack Attack;
    // Start is called before the first frame update
    void Start()
    {
        Attack = GameObject.Find("LastBoss(Clone)").GetComponent<LB_Attack>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)//�����蔻�菈��
    {
        if (collision.gameObject.tag == "Ground")        //���������������m��Ground�^�O���t���Ă����ꍇ
        {
            
           
                Attack.Circlenum++;
            
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBoll��j��
            Debug.Log("�e��j�󂵂�");//�f�o�b�N���O��\��
        }
        else if(collision.gameObject.name == "Rulaby")
        {
            

            Attack.Circlenum++;
            
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBoll��j��
            Debug.Log("�e��j�󂵂�");//�f�o�b�N���O��\��
        }
    }
}
