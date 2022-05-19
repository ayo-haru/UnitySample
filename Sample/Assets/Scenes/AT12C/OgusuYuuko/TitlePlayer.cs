//=============================================================================
//
// �^�C�g���pPlayer
//
// �쐬��:2022/05/18
// �쐬��:����T�q
//
// <�J������>
// 2022/05/18   �쐬 
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public Animator animator;                           // �A�j���[�^�[�R���|�[�l���g�擾
    private TitleSceneManager titleSceneManager;        //�^�C�g���V�[���}�l�[�W��
    public bool pressAnyButtonFlag;                     //PressAnyButton�������ꂽ��
    private bool selectFlag;                            //�I����ʂ��ǂ���
    public GameObject[] selectPosObject;                //�I�����̈ʒu
    public GameObject[] selectUI;                       //�I������UI
    public float moveSpeed = 0.1f;                      //�v���C���[�ړ����x
    private bool rightFlag;                             //�E�����Ă邩
    private Vector3 scale;                              //�v���C���[��]���̃X�P�[���ύX�p
    public int max_timer = 60;                          //���o�҂�����
    public int first_timer = 45;                        //pressanybutton�̎��̉��o�҂�����
    private int timer;                                  //���o���Ԍv���p
    public bool decisionFlag;                           //����{�^���������ꂽ��

    AnimatorStateInfo animeStateInfo;                   //�A�j���[�V�����̏��

    // Start is called before the first frame update
    void Start()
    {
        //�^�C�g���V�[���}�l�[�W���擾
        titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
        //�v���C���[��]�@�E�Ɍ�������
        transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
        //�ϐ�������
        pressAnyButtonFlag = false;
        selectFlag = false;
        rightFlag = true;
        scale = transform.localScale;
        timer = 0;
        decisionFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //-----PressAnyButton�������ꂽ���̏���
        if (pressAnyButtonFlag)
        {
            if(timer <= 0)
            {
                animator.SetTrigger("Attack");
                transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));

                timer = first_timer;
            }
            else
            {
                --timer;
                if(timer <= 0)
                {
                    pressAnyButtonFlag = false;
                    selectFlag = true;
                    Camera.main.GetComponent<TitleCamera>().startFlag = true;
                }
            }


        }

        if (!selectFlag)
        {
            return;
        }
        //-----���j���[�I�𒆂̏���
        //�I�����̉E�ɂ���ꍇ
        if (transform.position.z > selectPosObject[titleSceneManager.select].transform.position.z)
        {
            if (rightFlag)
            {
                //�����]��
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                scale.x *= -1;
                transform.localScale = scale;
                rightFlag = false;
            }
            //�A�j���[�V�����Đ�
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }

            //���ɓ�����
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed);
            if (transform.position.z < selectPosObject[titleSceneManager.select].transform.position.z)
            {
                //�s����������߂�
                transform.position = new Vector3(transform.position.x, transform.position.y, selectPosObject[titleSceneManager.select].transform.position.z);
            }
        }
        //�I�����̍��ɂ���ꍇ
        else if (transform.position.z < selectPosObject[titleSceneManager.select].transform.position.z)
        {
            if (!rightFlag)
            {
                //�����]��
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                transform.localScale = scale;
                rightFlag = true;
            }
            //�A�j���[�V�����Đ�
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
            //�E�ɓ�����
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed);
            if (transform.position.z > selectPosObject[titleSceneManager.select].transform.position.z)
            {
                //�s����������߂�
                transform.position = new Vector3(transform.position.x, transform.position.y, selectPosObject[titleSceneManager.select].transform.position.z);
            }
        }
        else//���E�ړ����Ȃ������Ƃ�
        {
            //�A�j���[�V�����Đ�
            if (animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", false);
            }

            //����{�^����������Ă���
            if (decisionFlag)
            {
                //�A�j���[�V�����̏�Ԏ擾
                animeStateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (timer == 0)
                {
                    //�A�j���[�V�����Đ�
                    animator.SetTrigger("Attack_DOWN");
                    // �e����
                    SoundManager.Play(SoundData.eSE.SE_SHIELD, SoundData.TitleAudioList);
                    
                }

                //���p���C�̃��[�V�����ŃA�j���[�V�������������߂��Ă���
                if (animeStateInfo.normalizedTime > 0.5f && animeStateInfo.IsName("Attack_DOWN"))
                {
                    //UI�e��
                    selectUI[titleSceneManager.select].GetComponent<Move2DTheta>().UnderParry();
                }

                //�^�C�}�[���I�����Ă���
                if (timer > max_timer)
                {
                    //�^�C�}�[������
                    timer = 0;
                    //�t���O����
                    decisionFlag = false;
                }
                else
                {
                    //�^�C�}�[�X�V
                    ++timer;
                }
            }

        }
    }

}
