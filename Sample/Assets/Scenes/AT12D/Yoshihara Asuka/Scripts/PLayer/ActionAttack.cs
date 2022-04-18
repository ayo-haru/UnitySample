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
        PlayerActionAssets.Player.Attack.started += OnAttack;
    }



    // Start is called before the first frame update
    void Start()
    {
        
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
        //---�X�e�B�b�N���͂̍ۂ̏���
        GameData.PlayerPos = transform.position;        // �U������u�Ԃ̃v���C���[���W���擾
        AttackDirection += Attack.ReadValue<Vector2>(); // �X�e�B�b�N�̓|�����l���擾
    }

}
