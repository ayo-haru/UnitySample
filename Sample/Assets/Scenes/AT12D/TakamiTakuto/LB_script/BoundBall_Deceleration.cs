using UnityEngine;
using System.Collections;

public class BoundBall_Deceleration : MonoBehaviour
{

    public float speed = 5f;
    // �����̍ő�l���w�肷��ϐ���ǉ�
    public float minSpeed = 10f;
    // �����̍ŏ��l���w�肷��ϐ���ǉ�
    public float maxSpeed = 10f;

    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(speed, speed, 0f);
    }

    // ���t���[�����x���`�F�b�N����
    void Update()
    {
        // ���݂̑��x���擾
        Vector3 velocity = myRigidbody.velocity;
        // �������v�Z
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, minSpeed, maxSpeed);
        // ���x��ύX
        myRigidbody.velocity = velocity.normalized * clampedSpeed;
    }
}
