using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampCamera : MonoBehaviour
{
    // ターゲット
    [SerializeField] private Transform _target;

    // 追従させるオブジェクト
    [SerializeField] private Transform _follower;

    // 目標値に到達するまでのおおよその時間
    [SerializeField] private float _SmoothTime = 0.3f;

    // 最高速度
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // 現在速度(SmoothDampの計算のために必要)
    private float _currentVelocity = 0;

    // X座標をターゲットのX座標に追従
    void LateUpdate()
    {
        // 現在位置取得
        var currentPos = _follower.position;

        // 次フレームの位置を計算
        currentPos.x = Mathf.SmoothDamp(currentPos.x, _target.position.x, ref _currentVelocity, _SmoothTime, _maxSpeed);

        // 現在位置のX座標を更新
        _follower.position = currentPos;
    }
}
