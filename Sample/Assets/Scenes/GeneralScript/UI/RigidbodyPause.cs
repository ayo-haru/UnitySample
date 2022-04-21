//  RigidbodyExtension.cs
//  http://kan-kikuchi.hatenablog.com/entry/Pause_Resume
//
// �����Ȃ�����l�b�g����q�؁i�ɒn�c�j
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//�ꎞ��~���̑��x��ۊǂ���N���X
public class VelocityTmp : MonoBehaviour {
    //�ꎞ��~���̑��x
    private Vector3 _angularVelocity;
    private Vector3 _velocity;

    public Vector3 AngularVelocity
    {
        get { return _angularVelocity; }
    }
    public Vector3 Velocity
    {
        get { return _velocity; }
    }

    /// <summary>
    /// Rigidbody2D����͂��đ��x��ݒ肷��
    /// </summary>
    public void Set(Rigidbody rigidbody) {
        _angularVelocity = rigidbody.angularVelocity;
        _velocity = rigidbody.velocity;
    }

}

/// <summary>
/// Rigidbody �^�̊g�����\�b�h���Ǘ�����N���X
/// </summary>
public static class RigidbodyExtension {

    //�ꎞ��~���̑��x
    private static Vector3 _angularVelocity;
    private static Vector3 _velocity;

    /// <summary>
    /// �ꎞ��~
    /// </summary>
    public static void Pause(this Rigidbody rigidbody, GameObject gameObject) {
        gameObject.AddComponent<VelocityTmp>().Set(rigidbody);
        rigidbody.isKinematic = true;
    }

    /// <summary>
    /// �ĊJ
    /// </summary>
    public static void Resume(this Rigidbody rigidbody, GameObject gameObject) {
        if (gameObject.GetComponent<VelocityTmp>() == null)
        {
            return;
        }

        rigidbody.velocity = gameObject.GetComponent<VelocityTmp>().Velocity;
        rigidbody.angularVelocity = gameObject.GetComponent<VelocityTmp>().AngularVelocity;
        rigidbody.isKinematic = false;

        GameObject.Destroy(gameObject.GetComponent<VelocityTmp>());
    }

}