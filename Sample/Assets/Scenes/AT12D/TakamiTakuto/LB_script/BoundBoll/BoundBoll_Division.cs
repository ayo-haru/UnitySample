using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBoll_Division : MonoBehaviour
{
    //==============================================================================
    //---------------------�������e�F�ǂɓ���������e�𑝐B������----------------------
    //----------------------Prehub����BoundBoll�ɓ����Ă���--------------------------
    //==============================================================================
    public GameObject BoundBoll;                //�Q�[���I�u�W�F�N�g�F�o�E���h�{�[��
    public int BoundBollCount;                  //�o�E���h�e���ǂɓ��������񐔂��J�E���g
    [SerializeField] public int Max_BoundBoll;  //�o�E���h�e�̍ő��
    [SerializeField] public int Max_BoundCount; //�o�E���h�e�̍ő�o�E���h��
    //------------------------------------------------------------------------------

    void Start()
    {
    }

    private void OnCollisionStay(Collision collision)//�����蔻�菈��
    {
        Debug.Log("�Փ˂����I�u�W�F�N�g�F" + gameObject.name);              //�f�o�b�N���O
        Debug.Log("�Փ˂��ꂽ�I�u�W�F�N�g�F" + collision.gameObject.name);  //�f�o�b�N���O

        //--------------------------------------------------------------------------------------------------
        //�������������̂��Q�[���I�u�W�F�N�g�́h�X�e�[�W�h�̏ꍇ
        //--------------------------------------------------------------------------------------------------
        if (collision.gameObject.tag == "Ground")
        {
            BoundBollCount++;                            //�o�E���h�{�[���̒��˕Ԃ�J�E���g���P�{����
            Debug.Log("�o�E���h�{�[���J�E���g���{�P���ꂽ");//�f�o�b�N���O
           

                if (Max_BoundBoll > BoundBollCount)
                {
                    GameObject instance = (GameObject)Instantiate(BoundBoll);  �@ //BoundBoll�𐶐�
                    instance.transform.position = gameObject.transform.position;�@//��������BoundBoll�̈ʒu��
                    GetComponent<Renderer>().material.color = Color.blue;      �@ //�e�X�g�p���������
                    Debug.Log("�e�𐶐�����");                                  �@ //�f�o�b�N���O
                }
            //BoundBollCount��Max_BoundCount�����傫���Ȃ����ꍇ
            if (BoundBollCount >= Max_BoundCount)
            {
                Destroy(BoundBoll);      //BoundBoll��j��
                Debug.Log("�e��j�󂵂�");//�f�o�b�N���O��\��
            }
        }
        //-----------------------------------------------------------------------------------------------------
        
    }
}