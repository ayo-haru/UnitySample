//==========================================================
//      �G���G�̒e���ꂽ�Ƃ�
//      �쐬���@2022/03/20
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/03/20
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    [SerializeField]
    GameObject Item;
    GameObject Player;
    private float bouncePower = 10000.0f;
    private Vector3 Pos;
    private Vector3 velocity;
    private Vector3 vec;

    [SerializeField]
    private int DropRate;           // �񕜃A�C�e���̃h���b�v��
    private int Drop;

    public bool isAlive;

    float DeadTime = 0.0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            vec = (Player.transform.position - transform.position).normalized;
            

            // ���Ԃŏ����鏈��
            if (!isAlive)
            {
                DeadTime += Time.deltaTime;
            }

            if (DeadTime > 1.0f)
            {
                Pos = transform.position;
                Destroy(gameObject, 0.0f);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMYDOWN, Pos);
            }
        }
 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!Pause.isPause)
        {
            if (collision.gameObject.name == "Weapon(Clone)")
            {
                //�v���C���[���t�����ɒ��˕Ԃ�
                collision.rigidbody.AddForce(vec * 5.0f, ForceMode.Impulse);

                //�e���������
                isAlive = false;
                // �d�͂�����
                rb.useGravity = false;
                // ��C��R���[����
                rb.angularDrag = 0.0f;
                // ��]���𒆉���
                rb.centerOfMass = new Vector3(0, 0, 0);

                // �񕜃A�C�e���𗎂Ƃ�
                Pos = transform.position;
                Drop = Random.Range(0, 100);
                if (Drop < DropRate)
                {
                    Instantiate(Item, Pos, Quaternion.identity);
                }

                //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
                //rb.AddForce(velocity * bouncePower, ForceMode.Force);

                //�v���C���[�Ƌt�����ɒ��˕Ԃ�
                rb.AddForce(-vec * bouncePower, ForceMode.Force);

                // ��]������
                rb.AddTorque(0.0f, 0.0f, -10000.0f);

                //SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
            }

            //if (!isAlive)
            //{
            //    //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            //    rb.AddForce(velocity * bouncePower, ForceMode.Force);
            //    // ��]������
            //    rb.AddTorque(0.0f, 0.0f, -300.0f);

            //}

            if (isAlive == false && collision.gameObject.CompareTag("Ground"))
            {
                // �ǁA���ɓ��������������
                //Destroy(gameObject, 0.0f);
            }

        }
    }
}
