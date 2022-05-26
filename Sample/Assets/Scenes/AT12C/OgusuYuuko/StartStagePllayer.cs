//=============================================================================
//
// �J�n���o
//
// �쐬��:2022/05/26
// �쐬��:����T�q
//
// <�J������>
// 2022/05/26 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStagePllayer : MonoBehaviour
{
    public float width = 10.0f;                                     //�U�ꕝ
    public Vector3 startPos = new Vector3(-250.0f, 50.0f, 0.0f);    //�����ʒu
    public float finishPosX = -200.0f;                              //�I���ʒu
    private Vector2 moveSpeed = new Vector3(0.1f, 1.0f);            //�ړ����x
    private Vector3 playerPos;
    private float theta;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�Q����A�j���[�^�[�擾
        playerAnimator = gameObject.GetComponent<Player2>().animator;

        //�d�͂Ƃ����͂𖳎��������̂�player2��rigidbody����
        gameObject.GetComponent<Player2>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        //�����ʒu�ݒ�
        gameObject.transform.position = startPos;
        playerPos = startPos;

        theta = 0.0f;

        //�E��������
        gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
        //�A�j���[�V�����Đ�
        if (!playerAnimator.GetBool("Walk"))
        {
            playerAnimator.SetBool("Walk", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //x���W�X�V
        playerPos.x += moveSpeed.x;
        //y���W�X�V
        theta += moveSpeed.y;
        if(theta >= 180.0f)
        {
            theta -= 360.0f;
        }
        float SinY = Mathf.Sin(Mathf.Deg2Rad * theta) * width;
        playerPos.y = startPos.y + SinY;

        gameObject.transform.position = playerPos;

        if(gameObject.transform.position.x > finishPosX)
        {
            //�A�j���[�V�����Đ� �V���{���ʊ���
            playerAnimator.SetTrigger("Attack_UP");
            //���o���I������̂ŁAplayer2��rigidbody�����ɖ߂�
            gameObject.GetComponent<Player2>().enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            //�R���|�[�l���g����
            Destroy(this);
        }

    }
}
