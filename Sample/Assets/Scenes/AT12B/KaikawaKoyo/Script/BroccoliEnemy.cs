//==========================================================
//      �u���b�R���[�G���̍U��
//      �쐬���@2022/03/18
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/18      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private Rigidbody rb;
    private EnemyDown ED;
    private int count = 0;

    [SerializeField]
    float MoveSpeed = 5.0f;
    int DetecDist = 8;
    bool InArea = false;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Target = Player.transform;                    // �v���C���[�̍��W�擾
        rb = gameObject.GetComponent<Rigidbody>();
        ED = GetComponent<EnemyDown>();
        transform.Rotate(new Vector3(0, 0, 15));
        rb.centerOfMass = new Vector3(0, -1, 0);
    }

    private void Update()
    {
        // �v���C���[����������U���J�n
        if (InArea && ED.isAlive)
        {
            Vector3 pos = rb.position;
            // �v���C���[�Ɍ������ē��U����
            float step = MoveSpeed * Time.deltaTime;
            rb.position = Vector3.MoveTowards(pos, Target.position, step);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    Destroy(gameObject, 0.0f);
        //}
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
