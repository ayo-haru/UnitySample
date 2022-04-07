//==========================================================
//      �n�`�G���̃G�C��
//      �쐬���@2022/04/06
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/04/06      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAim : MonoBehaviour
{
    Transform Target;
    GameObject Player;
    private BeeEnemy BE;
    private Vector3 aim;
    private Quaternion look;
    private Vector3 targetPosition;
    private float FiringTime;

    [SerializeField]
    private GameObject FiringPoint;

    [SerializeField]
    private GameObject BeeBullet;

    [SerializeField]
    private float speed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");    // �v���C���[�̃I�u�W�F�N�g��T��
        BE = GetComponent<BeeEnemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = Player.transform;          // �v���C���[�̍��W�擾
        targetPosition = Target.position;

        aim = targetPosition - transform.position;
        look = Quaternion.LookRotation(aim);
        transform.localRotation = look;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PoisonShot();
        }

    }

    private void PoisonShot()
    {
        GameObject newBall = Instantiate(BeeBullet, transform.position, transform.rotation);
        Vector3 direction = newBall.transform.forward;
        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);

    }

}
