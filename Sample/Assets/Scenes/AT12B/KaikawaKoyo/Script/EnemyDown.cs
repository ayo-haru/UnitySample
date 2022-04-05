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
    GameObject Item;
    public float bounceSpeed = 5.0f;
    public float bounceVectorMultiple = 2f;
    private float bouncePower = 1000.0f;
    private Vector3 Pos;

    public bool isAlive;

    float DeadTime;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Item = (GameObject)Resources.Load("Item");
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            
            //�Փ˂����ʂ́A�ڐG�����_�ɂ�����@���x�N�g�����擾
            Vector3 normal = collision.contacts[0].normal;
            //�Փ˂������x�x�N�g����P�ʃx�N�g���ɂ���
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            //x,y,z�����ɑ΂��Ė@���x�N�g�����擾
            velocity += new Vector3(normal.x * bounceVectorMultiple, normal.y * bounceVectorMultiple, normal.z * bounceVectorMultiple);
            //�v���C���[���t�����ɒ��˕Ԃ�
            collision.rigidbody.AddForce(-velocity * bounceSpeed, ForceMode.Impulse);
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
            Instantiate(Item, Pos, Quaternion.identity);

            //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            rb.AddForce(velocity * bouncePower, ForceMode.Force);
            // ��]������
            rb.AddTorque(0.0f, 0.0f, -300.0f);

            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
        }

        if (isAlive == false && collision.gameObject.CompareTag("Ground"))
        {
            // �ǁA���ɓ��������������
            //Destroy(gameObject, 0.0f);
        }

    }


}
