//==========================================================
//      �j���W���G���̍U��
//      �쐬���@2022/03/16
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/16      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private Vector3 startPosition, targetPosition;
    private EnemyDown ED;
    private Vector3 aim;
    private Quaternion look;

    [SerializeField]
    float MoveSpeed = 0.005f;
    float speed = 0.0f;
    bool InArea;
    bool Look;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        Look = false;
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {

        // �v���C���[����������U���J�n
        if (InArea && ED.isAlive)
        {

            if (speed <= 1)
            {
                speed += MoveSpeed * Time.deltaTime;
            }
            // �v���C���[�Ɍ������ē��U����
            rb.position = Vector3.Lerp(startPosition, targetPosition, speed);

            aim = targetPosition - transform.position;
            look = Quaternion.LookRotation(aim);
            transform.localRotation = look;

            transform.Rotate(90, 0, 0);

            //var goal = targetPosition + aim;

        }

        if (rb.position == targetPosition)
        {
            transform.Rotate(-90, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            Destroy(gameObject, 1.0f);
        }
    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player") && Look == false)
        {
            Target = Player.transform;          // �v���C���[�̍��W�擾
            targetPosition = Target.position;
            startPosition = rb.position;
            speed = 0.0f;
            InArea = true;
            Look = true;
        }
    }

}