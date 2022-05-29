using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Disappear : MonoBehaviour
{
    public GameObject ArrowRight;
    public GameObject ArrowLeft;
    public GameObject ArrowUp;
    public Vector3 ArrowUpinitpos;
    public int ArrowDisapperCount;
    public bool Attackspace=false;
    [SerializeField]  public int Arrow_MaxCount;
    Rigidbody ArrowRigidbody;
    public GameObject LastBoss;
    public LB_Attack LbAttack;

    // Start is called before the first frame update
    void Start()
    {
        LastBoss = GameObject.Find("LastBoss(Clone)");
        LbAttack = LastBoss.GetComponent<LB_Attack>();
        ArrowRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider Arrow_Collision)//�����蔻�菈��
    {
        Debug.Log("�����ɓ�������");                              //�f�o�b�N���O��\��
        if (Arrow_Collision.gameObject.name == "Stage")
        {
            Destroy(ArrowUp);
            Debug.Log("ArrowUp�����[�v����");                             //�f�o�b�N���O��\��
            ArrowDisapperCount = 0;                                     //�J�E���g���Z�b�g
            Debug.Log("Arrow_Disapper_Count���O�ɂ���");                 //�f�o�b�N���O��\��
            LbAttack.ArrowUseFlag = true;
            Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //�f�o�b�N���O
            LbAttack.ArrowCount++;
            Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //�f�o�b�N���O
            LbAttack.LBossAnim.SetBool("OnlyFlg", false);

        }
        else if (Arrow_Collision.gameObject.tag== "Ground")        //���������������m��Ground�^�O���t���Ă����ꍇ
        {
           Debug.Log("Tag:Ground�����I�u�W�F�N�g�ɓ�������");   //�f�o�b�N���O��\��
            ArrowDisapperCount++;                           �@ //�A���[�������J�E���g�����Z
            
            if (ArrowDisapperCount >= Arrow_MaxCount){           //�A���[�J�E���g���A���[�}�b�N�X�Ɠ����A�܂��͂���ȏ�̎�

                //Arrow_Right�̏ꍇ================================================================================
                if (gameObject.name == "Arrow_Right(Clone)"){           //�ڐG���Ă���̂�gameObject��"Arrow_Right"�̎�
                    Destroy(ArrowRight);
                    Debug.Log("Arrow_Right�����[�v����");         //�f�o�b�N���O��\��
                    ArrowDisapperCount = 0;                      //�J�E���g���Z�b�g
                    Debug.Log("Arrow_Disapper_Count���O�ɂ���");  //�f�o�b�N���O��\��
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag); //�f�o�b�N���O
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);//�f�o�b�N���O
                    LbAttack.LBossAnim.SetBool("OnlyFlg", false);
                }
                //-------------------------------------------------------------------------------------------------

                //Arrow_Left�̏ꍇ==================================================================================
                if (gameObject.name == "Arrow_Left(Clone)"){          //�ڐG���Ă���̂�gameObject��"Arrow_Left"�̏ꍇ
                    Destroy(ArrowLeft);
                    Debug.Log("Arrow_Left�����[�v����");        //�f�o�b�N���O��\��
                    ArrowDisapperCount = 0;                    //�J�E���g���Z�b�g
                    Debug.Log("Arrow_Disapper_Count���O�ɂ���");//�f�o�b�N���O��\��
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //�f�o�b�N���O
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //�f�o�b�N���O
                    LbAttack.LBossAnim.SetBool("OnlyFlg", false);
                }
                //-------------------------------------------------------------------------------------------------
            }
        }
    }
}

