//==========================================================
//      �n�`�G���̏o���ł̒e
//      �쐬���@2022/04/12
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/12      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    GameObject BeeEnemy;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField]
    GameObject PoisonArea;

    [SerializeField]
    private float MoveSpeed = 1.0f;
    private float speed = 0.0f;

    private bool reflect = false;

    // Start is called before the first frame update
    void Start()
    {
        BeeEnemy = GameObject.Find("Bee");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = BeeEnemy.transform.position;

        if(reflect)
        {
            if (speed <= 1)
            {
                speed += MoveSpeed * Time.deltaTime;
            }
            rb.position = Vector3.Lerp(startPosition, targetPosition, speed);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɓ��������������
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }

        // �n�ʂɓ���������ł̏����o���ď�����
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �ł̏�����
            Instantiate(PoisonArea, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.0f);
        }

        // �e���ꂽ��n�`�ɔ��ł�
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            //rb.velocity = Vector3.zero;
            startPosition = transform.position;
            reflect = true;
            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
        }

        // �e���ꂽ��Ƀn�`�ɓ��������������
        if(reflect && collision.gameObject.name == "Bee")
        {
            Destroy(gameObject, 0.0f);
        }
    }
}
