using UnityEngine;
using System.Collections;

public class BoundBall_Deceleration : MonoBehaviour
{

    public float speed = 5f;
    // 速さの最大値を指定する変数を追加
    public float minSpeed = 10f;
    // 速さの最小値を指定する変数を追加
    public float maxSpeed = 10f;

    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(speed, speed, 0f);
    }

    // 毎フレーム速度をチェックする
    void Update()
    {
        // 現在の速度を取得
        Vector3 velocity = myRigidbody.velocity;
        // 速さを計算
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, minSpeed, maxSpeed);
        // 速度を変更
        myRigidbody.velocity = velocity.normalized * clampedSpeed;
    }
}
