using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_WarpBullet : MonoBehaviour
{
    public GameObject WarpBullet;
    public static bool flag = true;
    public LB_Attack Attack;

    // Start is called before the first frame update
    void Start()
    {
        Attack = GameObject.Find("LastBoss(Clone)").GetComponent<LB_Attack>();
    }

    private void OnCollisionStay(Collision collision)//�����蔻�菈��
    {
        if (collision.gameObject.name == "Rulaby"|| collision.gameObject.name == "LastBoss(Clone)")        //���������������m��Ground�^�O���t���Ă����ꍇ
        {
            Attack.AnimFlg = false;
            Attack.OnlryFlg = true;
            Attack.WarpNum++;
            Attack.OneTimeFlg = true;
            Destroy(WarpBullet);      //BoundBoll��j��
            Debug.Log("�e��j�󂵂�");//�f�o�b�N���O��\��
            flag = true;

        }
    }
}
