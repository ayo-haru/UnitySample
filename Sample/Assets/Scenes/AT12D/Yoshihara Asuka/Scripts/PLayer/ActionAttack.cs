//=============================================================================
//
// PLayer Attack����
//
// �쐬��:2022/03/18
// �쐬��:�g����
//
// <�J������>
// 2022/04/18   �쐬
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionAttack : MonoBehaviour
{
    //---�ϐ��錾

    //---�R���g���[���[�֘A
    private Game_pad PlayerActionAssets;                  // InputAction���\�b�h
    private InputAction Attack;

    private Rigidbody rb;
    private GameObject Weapon;                           // Weapon���i�[

    private Vector2 AttackDirection = Vector2.zero;      // �U������
    private Vector3 CurrentScale;                        // ���݂̃v���C���[�̃X�P�[���̒l���i�[ 
    private float AttckPosHeight = 6.0f;                 // �V�[���h�ʒu�㉺
    private float AttckPosWidth = 4.0f;                  // �V�[���h�ʒu���E
    private float DestroyTime = 0.5f;                    // �V�[���h�������鎞��
    private bool isAttack;                               // �U���t���O
    [System.NonSerialized]
    public static bool UnderParryNow = false;            // ���p���B���t���O


    public int StopTime = 5;                             // �����o�������Ɏ~�܂鎞��
    private int Timer = 0;                               // ��~���Ԍv��

    [System.NonSerialized]
    public static bool GroundNow = false;                // �n�ʂƐڒn���t���O

    private void Awake()
    {
        //---�e��R���|�[�l���g�擾
        rb = GetComponent<Rigidbody>();
        PlayerActionAssets = new Game_pad();
    }

    private void OnEnable()
    {
        //---�X�e�B�b�N�̓��͂̐ݒ���擾
        Attack = PlayerActionAssets.Player.Attack;

        //---Action�C�x���g�o�^(�{�^������)
        Attack.started += OnAttack;                     // �����ꂽ�u�ԂŃR�[���o�b�N

        //---���̗͂L����
        PlayerActionAssets.Player.Enable();
    }

    private void OnDisable()
    {
        Attack.started -= OnAttack;

        //---���̖͂�����
        PlayerActionAssets.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        //---����I�u�W�F�N�g�����[�h
        Weapon = (GameObject)Resources.Load("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //===================================================================
    //
    //  �U������
    //  OnAttack��Attack�ŃZ�b�g�BOnAttack�ōU���t���O�𗧂ĂāA
    //  Attack�̒��ɍU���̏���������
    //
    //===================================================================
    private void OnAttack(InputAction.CallbackContext obj)
    {
        Attacking();
    }
    private void Attacking()
    {
    //    //---�X�e�B�b�N���͂̍ۂ̏���
    //    GameData.PlayerPos = transform.position;        // �U������u�Ԃ̃v���C���[���W���擾
    //    AttackDirection += Attack.ReadValue<Vector2>(); // �X�e�B�b�N�̓|�����l���擾

    //    //---�U������(���E�E��E��)
    //    if(Mathf.Abs(AttackDirection.x) >= 1)           // ���E�U��
    //    {
    //        Timer = StopTime;                           // �^�C�}�[�ݒ�
    //        PlayerData.animator.SetTrigger("Attack");   // �A�j���[�V�����Đ�
    //    }

    //    if(AttackDirection.y >= 1)                      // ��U��
    //    {
    //        if(GroundNow == true)                       // �n�ʂɂ���Ƃ�
    //        {
    //            // ---����������
    //            rb.AddForce(transform.up * 5.0f,ForceMode.Impulse);
    //            GroundNow = false;
    //        }
    //        //---�A�j���[�V�����Đ�
    //        PlayerData.animator.SetTrigger("Attack_UP");
    //    }

    //    if(AttackDirection.y <= 1)                      // ���p���B
    //    {
    //        if(GroundNow == true)
    //        {
    //            rb.AddForce(transform.up * 5.0f, ForceMode.Impulse);
    //            GroundNow = false;
    //        }
    //        //---�A�j���[�V�����Đ�
    //        PlayerData.animator.SetTrigger("Attack_DOWN");
    //    }

    //    //---���f���̌����Ɣ��Ε����ɏ��o�����烂�f����]
    //    if ((AttackDirection.x > 0 && beforeDir.x < 0) || (AttackDirection.x < 0 && beforeDir.x > 0))
    //    {
    //        //������ۑ�
    //        beforeDir.x = AttackDirection.x;
    //        //��]
    //        transform.rotation = Quaternion.LookRotation(AttackDirection);
    //        //�X�P�[��x�𔽓]
    //        scale.x *= -1;
    //        transform.localScale = scale;
    //    }

    //    if (AttackDirection.x < 0 && beforeDir.x > 0)
    //    {
    //        beforeDir = AttackDirection;
    //        transform.rotation = Quaternion.LookRotation(AttackDirection);
    //    }

    }
}
