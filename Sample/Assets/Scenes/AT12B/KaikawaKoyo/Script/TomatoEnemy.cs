//==========================================================
//      �g�}�g�G���̍U��
//      �쐬���@2022/03/17
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/17      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private float distance;
    private EnemyDown ED;

    [SerializeField]
    float MoveSpeed = 2.0f;
    int DetecDist = 8;
    bool InArea = false;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        distance = 0.7f;
        ED = GetComponent<EnemyDown>();
    }

    private void Update()
    {
        if (InArea && ED.isAlive)
        {
            Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
            Ray ray = new Ray(rayPosition, Vector3.down);
            bool isGround = Physics.Raycast(ray, distance); // �ڒn����
            Vector3 pos = rb.position;
            // �v���C���[����������U���J�n
            if (isGround == false)
            {
                // �v���C���[�Ɍ������ē��U����
                float step = MoveSpeed * Time.deltaTime;
                rb.position = Vector3.MoveTowards(pos, Target.position, step);
            }

            // ���˂鏈��
            if (isGround)
            {
                rb.AddForce(transform.up * 10.0f, ForceMode.Force);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɓ��������������
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }
    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InArea = false;
        }
    }
}
