using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;                           // �R���g���[���[���g���ɓ������Ẵ��C�u����

public class Player_pad : MonoBehaviour
{
    //---�ϐ��錾
    private Controller PlayerAction;                    // 

    private InputAction move;
    private Rigidbody rb;

    [SerializeField] private float MaxSpeed = 5.0f;     // �v���C���[�̍ō����x
    private Vector3 ForceDirection = Vector2.zero;      // �v���C���[�̓�������(�����l0)
    private Quaternion TargetRotation;                  // �v���C���[�̉�]�𐧌�
    private bool AbleMove;                              // �v���C���[�̓����邩�ǂ���(true = �ړ��Afalse = ���͂��󂯕t���Ȃ�)

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();                 // RigidBody�̃R���|�[�l���g���擾
        PlayerAction = new Controller();                // Controller�̃R���|�[�l���g�擾

        AbleMove = true;                                // �����ltrue(�ړ�����)
        TargetRotation = transform.rotation;            // ���݂̉�]���擾
    }

    //---�{�^�����͂Ɗ֐��̌��т�
    private void OnEnable()                             // OnEnable...�I�u�W�F�N�g���\������Ă��鎞�ɗL��
    {
        PlayerAction.Player.Attack.started += Attack;   // started ... �{�^���������ꂽ�Ƃ��ɏ��������
                                                        // canceld ... �{�^�����b���ꂽ�Ƃ��ɏ��������
        PlayerAction.Player.Enable();                   // �A�N�V�����{�^���̗L��

        //---�ϐ���move�A�N�V�������i�[
        move = PlayerAction.Player.Move;
    }

    private void OnDisable()                           // OnDisable ... �I�u�W�F�N�g����\���̎��L��
    {
        PlayerAction.Player.Attack.started -= Attack;  // Player�����������Ƀ}�C�i�X���邱�ƂłȂ��Ȃ�B
        PlayerAction.Player.Disable();                 // �A�N�V�����{�^���̖���


    }
    // Start is called before the first frame update
    void Start()


    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //ForceDirection += move.ReadValue<Vector2>().x; //* GetCameraRight();
        rb.AddForce(ForceDirection, ForceMode.Impulse); // ForceMode.Impulse ... ���W�b�h�{�f�B�ł����鎿�ʂ���u�ōs���B
    }


    //---�U���A�j���[�V�����̐؂�ւ�
    private void Attack(InputAction.CallbackContext obj)
    {
        /* ������� */
    }

    //---�J�����̌��������Ɉړ�����
    //private Vector3 GetCameraRight()
    //{
    //    Vector3 right = PlayerCamera.transform.right;
    //    right.y = 0;

    //    return right.normalized;    // normalized ... �x�N�g���̐��K��
    //}
}
