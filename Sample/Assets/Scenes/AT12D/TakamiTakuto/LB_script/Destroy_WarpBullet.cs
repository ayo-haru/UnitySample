using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_WarpBullet : MonoBehaviour
{
    public GameObject WarpBullet;
    public static bool flag = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)//�����蔻�菈��
    {
        if (collision.gameObject.tag == "Ground")        //���������������m��Ground�^�O���t���Ă����ꍇ
        {
            Destroy(WarpBullet);      //BoundBoll��j��
            Debug.Log("�e��j�󂵂�");//�f�o�b�N���O��\��
            flag = true;

        }
        if (collision.gameObject.tag == "Player")        //���������������m��Ground�^�O���t���Ă����ꍇ
        {
            Destroy(WarpBullet);      //BoundBoll��j��
            Debug.Log("�e��j�󂵂�");//�f�o�b�N���O��\��
            flag = true;
        }
    }
}
