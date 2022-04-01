//=============================================================================
//
// �R���g���[���[�Ή�Player
//
// �쐬��:2022/03/18
// �쐬��:�g����
//
// <�J������>
// 2022/03/18   �쐬
// 2022/03/20   �ړ��A�U�����{(���̎��_�ł�AddForce)
// 2022/03/25   �v���C���[�̋����C��(�ړ���Velocity�v�Z�ɕύX)
// 2022/03/25   �v���C���[�̋����C��(�W�����v���̋����ύX�X�g���C�t�A��)
// 2022/04/01   �A�j���[�V��������
// 2022/04/01   �ړ��Ղ��Ղ���������
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---�ϐ��錾

    //---InputSystem�֘A
    private Game_pad PlayerActionAsset;                 // InputAction�Ő����������̂��g�p
    private InputAction move;                           // InputAction��move������
    private InputAction Attack;                         // InputAction��move������

    //---�A�j���[�V�����֘A
    [SerializeField] public Animator animator;          // �A�j���[�^�[�R���|�[�l���g�擾


    //---�R���|�[�l���g�擾
    private Rigidbody rb;
    public GameObject prefab;                           // "Weapon"�v���n�u���i�[����ϐ�
    GameObject hp;                                      // HP�̃I�u�W�F�N�g���i�[
    HPManager hpmanager;                                // HPManager�̃R���|�[�l���g���擾����ϐ�

    //---�ړ��ϐ�
    private Vector3 PlayerPos;                          // �v���C���[�̍��W
    private Vector2 ForceDirection = Vector2.zero;      // �ړ�������������߂�
    private Vector2 MovingVelocity = Vector3.zero;      // �ړ�����x�N�g��
    [SerializeField] private float maxSpeed = 5;        // �ړ��X�s�[�h(��������)

    public int stopTime = 20;                           //���o�����Ƃ��Ɏ~�܂鎞��
    private int Timer = 0;                              //��~���Ԍv���p

    public float Amplitude;                             // �U�ꕝ
    private int FylFrameCount;                         //�@�t���t�����Ă鏈���Ɏg���t���[���J�E���g

    //---��]�ϐ�
    private Vector2 beforeDir;                           //�Ō�ɓ��͂��ꂽ����
    public float rotationSpeed = 30.0f;                  //��]�X�s�[�h
    private Vector3 scale;                              //�X�P�[��

    //---�W�����v�ϐ�
    public float GravityForce = -10.0f;                 // �d��
    private bool JumpNow = false;                       // �W�����v���Ă��邩�ǂ���
    private bool UnderParryNow = false;                 // ���p���B�����ǂ���(
    [SerializeField] private float JumpForce = 5;       // �W�����v��


    //---�U���ϐ�
    private Vector2 AttackDirection = Vector2.zero;     // �U������
    public float AttckPosHeight = 8.0f;                 // �V�[���h�ʒu�㉺
    public float AttckPosWidth = 8.0f;                  // �V�[���h�ʒu���E
    public float DestroyTime = 3.0f;                    // �V�[���h�������鎞��
    private Vector3 CurrentScale;                       // ���݂̃v���C���[�̃X�P�[���̒l���i�[ 



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // InputAction�C���X�^���X�𐶐�
    }


    //---�{�^�����̓��͂����ѕt����
    private void OnEnable()
    {
        //---�X�e�B�b�N���͂���邽�߂̐ݒ�
        move = PlayerActionAsset.Player.Move;           
        Attack = PlayerActionAsset.Player.Attack;

        //---Action�C�x���g�o�^(�{�^������)
        PlayerActionAsset.Player.Attack.started += OnAttack;

        PlayerActionAsset.Player.Jump.started += OnJump;            // started    ... �{�^���������ꂽ�u��
        //PlayerActionAsset.Player.Jump.performed += OnJump;        // performed  ... ���Ԃ��炢
        //PlayerActionAsset.Player.Jump.canceled += OnJump;         // canceled   ... �{�^���𗣂����u��

        //��]�ϐ��̏�����
        beforeDir = new Vector2(1.0f, 0.0f);

        //---InputAction�̗L����(���ꂩ���Ȃ��ƁA���͂Ƃ�Ȃ��B)
        PlayerActionAsset.Player.Enable();

    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= OnAttack;        // started...�{�^���������ꂽ�u��
        PlayerActionAsset.Player.Jump.started -= OnJump;            // started    ... �{�^���������ꂽ�u��


        //---InputAction�̖�����
        PlayerActionAsset.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Weapon");


        if (GameObject.Find("HPSystem(Clone)"))
        {
            hp = GameObject.Find("HPSystem(Clone)");        // HPSystem���Q��
            hpmanager = hp.GetComponent<HPManager>();       // HPSystem�̎g�p����R���|�[�l���g
        }
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update() 
    {
        //---rb.velocity�ɂ��ړ�����
        ForceDirection += move.ReadValue<Vector2>();
        ForceDirection.Normalize();
        MovingVelocity = ForceDirection * maxSpeed;

        //---�A�j���[�V�����Đ�
        if (Mathf.Abs(ForceDirection.x) > 0 && Mathf.Abs(ForceDirection.y) == 0)
        {
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
                
            }
        }
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        //---HP�I�u�W�F�N�g������
        if (!GameObject.Find("HPSystem(Clone)"))
        {
            return;
        }

        //---�o�b�N�X�y�[�X�L�[��HP�����炷(�f�o�b�O)
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameData.CurrentHP--;
            EffectManager.Play(EffectData.eEFFECT.EF_DAMAGE, this.transform.position);
            SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);
        }
        //---�R���g���[���[�L�[��HP�𑝂₷(�f�o�b�O)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(GameData.CurrentHP < hpmanager.MaxHP)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_HEAL, this.transform.position);
                GameData.CurrentHP++;
            }

        }
    }

    private void FixedUpdate()
    {
        //---�W�����v���Ȃ�ړ����������Ȃ�
        if(JumpNow == true)
        {
            Gravity(); 
            //return;
        }

        //---�ړ�����(AddForce�̏���)
        //SpeedCheck();
        //ForceDirection += move.ReadValue<Vector2>();
        //ForceDirection.Normalize();
        //rb.AddForce(ForceDirection * maxSpeed, ForceMode.Impulse);

        //---�ړ�����(velocity�̏���)
        if (Timer <= 0)
        {
            rb.velocity = new Vector3(MovingVelocity.x, rb.velocity.y - MovingVelocity.y, 0);
        }
        else
        {
            --Timer;
            rb.velocity = new Vector3(0, rb.velocity.y - MovingVelocity.y, 0);
        }

        //---��]��������
        //�������ς���Ă���X�P�[�����𔽓]
        if((ForceDirection.x > 0 && beforeDir.x < 0)||(ForceDirection.x < 0 && beforeDir.x > 0)){
            scale.x *= -1;
            transform.localScale = scale;
        }
        //��]�̖ڕW�l
        Quaternion target = new Quaternion();
        if (ForceDirection.magnitude > 0.01f)
        {
            //������ۑ�
            beforeDir = ForceDirection;
            //������ݒ�
            target = Quaternion.LookRotation(ForceDirection); 
        }
        else
        {
            //���͂���ĂȂ������Ƃ�
            //��]�����r���[�Ȏ��͕␳����
            if(transform.rotation.y != 90.0f && transform.rotation.y != -90.0f)
            {
                target = Quaternion.LookRotation(beforeDir); //������ύX����
            }
        }
        //��������]������
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed);

        ForceDirection = Vector2.zero;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {

    }

    //---�U������
    private void OnAttack(InputAction.CallbackContext obj)
    {
        //�^�C�}�[�ݒ�
        Timer = stopTime;

        //---�X�e�B�b�N����
        PlayerPos = transform.position;                             // �U������u�Ԃ̃v���C���[�̍��W���擾
        AttackDirection += Attack.ReadValue<Vector2>();             // �X�e�B�b�N�̓|�����l���擾
        Debug.Log("AttackDirection(���K���O):" + AttackDirection);
        AttackDirection.Normalize();                                // �擾�����l�𐳋K��(�x�N�g�����P�ɂ���)

        //---�A�j���[�V�����Đ�
        if(Mathf.Abs(AttackDirection.x) > 0)
        {
            animator.SetTrigger("Attack");
        }

        if(AttackDirection.y > 0.1)
        {
            animator.SetTrigger("Attack_UP");
        }


        //���f���̌����Ɣ��Ε����ɏ��o�����烂�f����]
        if ((AttackDirection.x > 0 && beforeDir.x < 0) || (AttackDirection.x < 0 && beforeDir.x > 0))
        {
            //������ۑ�
            beforeDir.x = AttackDirection.x;
            //��]
            transform.rotation = Quaternion.LookRotation(AttackDirection);
            //�X�P�[��x�𔽓]
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (AttackDirection.x < 0 && beforeDir.x > 0)
        {
            beforeDir = AttackDirection;
            transform.rotation = Quaternion.LookRotation(AttackDirection);
        }

        //---�|�����l����ɏ��̏o���ꏊ���w��
        GameObject weapon = Instantiate(prefab,new Vector3(PlayerPos.x + (AttackDirection.x * AttckPosWidth),
                                                           PlayerPos.y + (AttackDirection.y * AttckPosHeight),
                                                           PlayerPos.z),Quaternion.identity);

        //---�R���g���[���[�̓|����X�̒l���|��������y����-1����(���̊p�x�̒���)
        if (AttackDirection.x < 0.0f)
        {
            AttackDirection.y *= -1;

        }
        //---y�����|��������(���p���B�����)�W�����v���ɂ���(03/21���_)
        //---y�����|��������(���p���B�����)���p���B�t���O�ɂ���(03/25���_)
        if (AttackDirection.y < 0.0f)
        {
            UnderParryNow = true;
        }
        
        //---���̉�]��ݒ�
        weapon.transform.Rotate(new Vector3(0,0,(90 * AttackDirection.y)));
        //Debug.Log("�U�������I(Weapon)");
        Debug.Log("AttackDirection(���K����):"+ AttackDirection);
        //EffectManager.Play(EffectData.eEFFECT.EF_SHEILD2,weapon.transform.position);
        //SoundManager.Play(SoundData.eSE.SE_SHIELD, SoundData.GameAudioList);
        AttackDirection = Vector2.zero;                           // ���͂����x�A�V�����l���~�������߈�x�O�ɂ���
        Destroy(weapon,DestroyTime);
        return;
    }

    //---�W�����v����
    private void OnJump(InputAction.CallbackContext obj)
    {
        //---�W�����v���ł���΃W�����v���Ȃ�
        if(JumpNow == true)
        {
            return;
        }
        Debug.Log("�W�����v�I");
        JumpNow = true;
        rb.AddForce(transform.up * JumpForce,ForceMode.Impulse);
        SoundManager.Play(SoundData.eSE.SE_JUMP, SoundData.GameAudioList);
    }

    //---�W�����v���̏d�͂���������(�W�����v���r�q�Ɍ�������ʂ�����)
    private void Gravity()
    {
        if(JumpNow == true)
        {
            rb.AddForce(new Vector3(0.0f,GravityForce,0.0f));
            //ForceDirection = Vector2.zero;
        }
    }

    //---�I�u�W�F�N�g���t���t�������鏈��
    private void FluffyMove()
    {
        FylFrameCount += 1;
        if(1000 <= FylFrameCount)
        {
            FylFrameCount = 0;
        }
        if(0 == FylFrameCount % 2)
        {
            float posYSin = Mathf.Sin(2.0f * Mathf.PI * (float)(FylFrameCount % 200) / (200.0f - 1.0f));
            iTween.MoveAdd(gameObject, new Vector3(0, Amplitude * posYSin, 0), 0.0f);
        } 
    }

    //---AddForce�ŗ͂������肷���Ă��܂����߁AmaxSpeed�̒l�ɋ߂��l�ɌŒ肷�鏈��
    private void SpeedCheck()
    {
        Vector3 PlayerVelocity = rb.velocity;
        PlayerVelocity.z = 0;

        if (PlayerVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = PlayerVelocity.normalized * maxSpeed;
        }
    }

    //---�����蔻�菈��
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Damaged")
        {
            Debug.Log("�U�����������B");
            EffectManager.Play(EffectData.eEFFECT.EF_DAMAGE, this.transform.position);
            SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);
            if (!GameObject.Find("HPSystem(Clone)"))
            {
                return;
            }
            GameData.CurrentHP--;

            //HP��0�ɂȂ�����Q�[���I�[�o�[��\��
            if(GameData.CurrentHP <= 0)
            {
                GameObject.Find("Canvas").GetComponent<GameOver>().GameOverShow();
            }
        }
    }

    //---�����蔻�菈��(GroundCheck�̃{�b�N�X�R���C�_�[�Ŕ�������悤��)
    private void OnTriggerEnter(Collider other)
    {
        if (JumpNow == true || UnderParryNow == true)
        {
            //---Tag"Ground"�ƐڐG���Ă���Ԃ̏���
            if (other.gameObject.tag == "Ground")
            {
                Debug.Log("���n��");
                JumpNow = false;
                UnderParryNow = false;
                //ForceDirection = Vector2.zero;
                //SoundManager.Play(SoundData.eSE.SE_LAND, SoundData.GameAudioList);
            }
        }
    }


    private void OnGUI()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        //---�Q�[���p�b�h�ƂȂ����Ă��鎞�ɕ\�������B
        GUILayout.Label($"LeftStick:{Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"RightStick:{Gamepad.current.rightStick.ReadValue()}");
        GUILayout.Label($"ButtonNorth:{Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"ButtonSouth:{Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"ButtonEast:{Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"ButtonWast:{Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"LeftShoulder:{Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"LeftTrigger:{Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"RightShoulder:{Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"RighetTrigger:{Gamepad.current.rightTrigger.ReadValue()}");
        GUILayout.Label($"LeftStickUp:{Gamepad.current.leftStick.up.ReadValue()}");
        GUILayout.Label($"Space:{Keyboard.current.spaceKey.ReadValue()}");
        GUILayout.Label($"JumpFlg:{JumpNow}");
    }

}

