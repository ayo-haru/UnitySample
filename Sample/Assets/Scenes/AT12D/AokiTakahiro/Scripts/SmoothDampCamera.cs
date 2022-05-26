using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampCamera : MonoBehaviour
{
    // �^�[�Q�b�g
    [SerializeField] private Transform _target;

    // �Ǐ]������I�u�W�F�N�g
    [SerializeField] private Transform _follower;

    // �ڕW�l�ɓ��B����܂ł̂����悻�̎���
    [SerializeField] private float _SmoothTime = 0.3f;

    // �ō����x
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // ���ݑ��x(SmoothDamp�̌v�Z�̂��߂ɕK�v)
    private float _currentVelocity = 0;

    // X���W���^�[�Q�b�g��X���W�ɒǏ]
    void LateUpdate()
    {
        // ���݈ʒu�擾
        var currentPos = _follower.position;

        // ���t���[���̈ʒu���v�Z
        currentPos.x = Mathf.SmoothDamp(currentPos.x, _target.position.x, ref _currentVelocity, _SmoothTime, _maxSpeed);

        // ���݈ʒu��X���W���X�V
        _follower.position = currentPos;
    }
}
