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
    private Rigidbody rb;
    private Vector3 Enemypos;
    private EnemyDown ED;

    public bool InArea;
    float A = 2.0f;
    float B = 5.0f;
    float Width = 6.0f;     // ��щ�鉡��
    float Vertical = 0.7f;  // ��щ��c��

    private bool isCalledOnce = false;                             // ��񂾂����������邽�߂Ɏg���B

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        rb = gameObject.GetComponent<Rigidbody>();
        InArea = false;
        ED = GetComponent<EnemyDown>();
        Enemypos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ��щ�鏈��
        transform.position = new Vector3(Mathf.Sin(A * Time.time) * Width + Enemypos.x,
            Mathf.Cos(B * Time.time) * Vertical + Enemypos.y, Enemypos.z);
        
        // �T�E���h����
        /*
        if (!isCalledOnce)     // ��񂾂��Ă�
        {
            SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
            isCalledOnce = true;
        }
        */
    }

    public void OnTriggerEnter(Collider other)    // �R���C�_�[�Ńv���C���[�����G������
    {
        if (other.CompareTag("Player"))
        {
            InArea = true;
        }
    }
}
