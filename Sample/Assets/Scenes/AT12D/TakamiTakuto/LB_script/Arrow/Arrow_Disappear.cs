using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Disappear : MonoBehaviour
{
    public GameObject ArrowRight;
    public GameObject ArrowLeft;
    public GameObject ArrowUp;
    public Vector3 ArrowRightinitpos;
    public Vector3 ArrowLeftinitpos;
    public Vector3 ArrowUpinitpos;
    public int ArrowDisapperCount;
    public bool Attackspace=false;
    [SerializeField]  public int Arrow_MaxCount;
    Rigidbody ArrowRigidbody;
    public LB_Attack LbAttack;

    // Start is called before the first frame update
    void Start()
    {
        ArrowRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider Arrow_Collision)//�����蔻�菈��
    {
        Debug.Log("�����ɓ�������");                              //�f�o�b�N���O��\��
        if (Arrow_Collision.gameObject.name == "Stage")
        {
            ArrowUp.transform.position = new Vector3(ArrowUpinitpos.x, ArrowUpinitpos.y, ArrowUpinitpos.z);
            ArrowRigidbody.velocity = Vector3.zero;                     //Rigidbody���X�g�b�v�����ē������~�߂�
            Debug.Log("ArrowUp�����[�v����");                             //�f�o�b�N���O��\��
            ArrowDisapperCount = 0;                                     //�J�E���g���Z�b�g
            Debug.Log("Arrow_Disapper_Count���O�ɂ���");                 //�f�o�b�N���O��\��
            LbAttack.ArrowUseFlag = true;
            Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //�f�o�b�N���O
            LbAttack.ArrowCount++;
            Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //�f�o�b�N���O

        }
        else if (Arrow_Collision.gameObject.tag== "Ground")        //���������������m��Ground�^�O���t���Ă����ꍇ
        {
           Debug.Log("Tag:Ground�����I�u�W�F�N�g�ɓ�������");   //�f�o�b�N���O��\��
            ArrowDisapperCount++;                           �@ //�A���[�������J�E���g�����Z
            
            if (ArrowDisapperCount >= Arrow_MaxCount){           //�A���[�J�E���g���A���[�}�b�N�X�Ɠ����A�܂��͂���ȏ�̎�

                //Arrow_Right�̏ꍇ================================================================================
                if (gameObject.name == "Arrow_Right"){           //�ڐG���Ă���̂�gameObject��"Arrow_Right"�̎�
                    ArrowRight.transform.position = new Vector3(ArrowRightinitpos.x, ArrowRightinitpos.y, ArrowRightinitpos.z);
                    ArrowRigidbody.velocity = Vector3.zero;      //Rigidbody���X�g�b�v�����ē������~�߂�
                    Debug.Log("Arrow_Right�����[�v����");         //�f�o�b�N���O��\��
                    ArrowDisapperCount = 0;                      //�J�E���g���Z�b�g
                    Debug.Log("Arrow_Disapper_Count���O�ɂ���");  //�f�o�b�N���O��\��
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag); //�f�o�b�N���O
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //�f�o�b�N���O
                }
                //-------------------------------------------------------------------------------------------------

                //Arrow_Left�̏ꍇ==================================================================================
                if (gameObject.name == "Arrow_Left"){          //�ڐG���Ă���̂�gameObject��"Arrow_Left"�̏ꍇ
                    ArrowLeft.transform.position = new Vector3(ArrowLeftinitpos.x, ArrowLeftinitpos.y, ArrowLeftinitpos.z);
                    ArrowRigidbody.velocity = Vector3.zero;    //Rigidbody���X�g�b�v�����ē������~�߂�
                    Debug.Log("Arrow_Left�����[�v����");        //�f�o�b�N���O��\��
                    ArrowDisapperCount = 0;                    //�J�E���g���Z�b�g
                    Debug.Log("Arrow_Disapper_Count���O�ɂ���");//�f�o�b�N���O��\��
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //�f�o�b�N���O
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //�f�o�b�N���O
                }
                //-------------------------------------------------------------------------------------------------
            }
        }
    }
}

