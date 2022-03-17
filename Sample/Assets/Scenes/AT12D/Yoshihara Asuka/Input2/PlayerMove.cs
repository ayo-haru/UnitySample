using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    //---�ϐ��錾
    private Game_pad PlayerAction_Pad;                      // �Q�[���p�b�h�Őݒ肵����������
    private InputAction move;                               // InputSystem�̐ݒ��move������
    private InputAction jump;                               // InputSystem�̐ݒ��jump������
    private InputAction attack;                             // InputSystem�̐ݒ��attack������

    private Vector2 Player_Move;                            // �v���C���[�̈ړ��������
    private Rigidbody rb;                                   

    [SerializeField] private float MasSpeed = 5.0f;         // �v���C���[�̍ō����x
    [SerializeField] private float JumpPower = 5.0f;        // �v���C���[�̃W�����v��

   private void Awake()
    {
        rb = GetComponent<Rigidbody>();                     // RigidBody�̃R���|�[�l���g�擾
        PlayerAction_Pad = new Game_pad();                  // Game_pad�̃R���|�[�l���g�擾

    }

    //--�{�^���̓��͂Ɗ֐������ѕt��
    private void OnEnable()
    {
        move = PlayerAction_Pad.Player.Move;                // move�̎擾�ƕ��������ѕt���B
    }
    private void OnDisable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Player_Move = move.ReadValue<Vector2>();
        rb.AddForce(Player_Move,ForceMode.Impulse);
    }
}
