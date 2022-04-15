//==========================================================
//      �n�`�G���̍U��
//      �쐬���@2022/04/04
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/04      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    GameObject Aim;
    GameObject Item;
    private Rigidbody rb;
    private Vector3 Enemypos;
    private Vector3 velocity;
    public float bounceVectorMultiple = 2f;
    private float bouncePower = 1000.0f;
    public bool isAlive;
    public bool InArea;
    float DeadTime = 0.0f;
    float A = 2.0f;
    float B = 5.0f;
    float Width = 6.0f;     // ��щ�鉡��
    float Vertical = 0.7f;  // ��щ��c��

    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B

    [SerializeField]
    private GameObject FiringPoint;

    [SerializeField]
    private GameObject BeeBullet;

    [SerializeField]
    private float speed = 30.0f;

    //[SerializeField]
    private float TimeOut = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        Aim = GameObject.Find("BeeAim");
        Item = (GameObject)Resources.Load("HealItem");
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        isAlive = true;
        Enemypos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPause)
        {
            // ���Ԃŏ����鏈��
            if (!isAlive)
            {
                DeadTime += Time.deltaTime;

            }
            if (DeadTime > 1.0f)
            {
                Enemypos = transform.position;
                Destroy(gameObject, 0.0f);
                EffectManager.Play(EffectData.eEFFECT.EF_ENEMYDOWN, Enemypos);
            }

            // �����Ă���ԍs������
            if (isAlive)
            {
                // ��щ�鏈��
                transform.position = new Vector3(Mathf.Sin(A * Time.time) * Width + Enemypos.x,
                    Mathf.Cos(B * Time.time) * Vertical + Enemypos.y, Enemypos.z);

                if (InArea)
                {
                    //PoisonShot();
                    TimeOut += Time.deltaTime;

                    if (TimeOut >= 3.0f)
                    {
                        GameObject newBall = Instantiate(BeeBullet, Aim.transform.position, Aim.transform.rotation);
                        Vector3 direction = newBall.transform.forward;
                        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
                        TimeOut = 0.0f;
                    }
                }

                // �T�E���h����
                /*
                if (!isCalledOnce)     // ��񂾂��Ă�
                {
                    SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
                    isCalledOnce = true;
                }
                */
            }

        }

    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �e���ꂽ�e�ɓ��������玀��
        if (collision.gameObject.name == "PoisonBullet(Clone)")
        {
            //�Փ˂����ʂ́A�ڐG�����_�ɂ�����@���x�N�g�����擾
            Vector3 normal = collision.contacts[0].normal;
            //�Փ˂������x�x�N�g����P�ʃx�N�g���ɂ���
            velocity = collision.rigidbody.velocity.normalized;
            //x,y,z�����ɑ΂��Ė@���x�N�g�����擾
            velocity += new Vector3(normal.x * bounceVectorMultiple, normal.y * bounceVectorMultiple, normal.z * bounceVectorMultiple);

            //�e���������
            isAlive = false;
            // ��C��R���[����
            rb.angularDrag = 0.0f;

            // �񕜃A�C�e���𗎂Ƃ�
            Enemypos = transform.position;
            Instantiate(Item, Enemypos, Quaternion.identity);

            //�擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            rb.AddForce(velocity * bouncePower, ForceMode.Force);
        }
    }

}
