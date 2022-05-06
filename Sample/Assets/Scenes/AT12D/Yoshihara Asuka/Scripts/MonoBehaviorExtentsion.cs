using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// MonoBehavior�g���N���X
/// </summary>
public static  class MonoBehaviorExtentsion
{
    /// <summary>
    /// �w�肳�ꂽ���\�b�h���w�莞�Ԍ�Ɏ��s����
    /// </summary>
    public static IEnumerator DelayMethod<T1,T2>(this MonoBehaviour mono,float waitTime, Action<T1,T2> action,T1 t1,T2 t2)
    {
        yield return new WaitForSeconds(waitTime);
        action(t1,t2);
    }


    /// <summary>
    /// �w�肳�ꂽ���\�b�h���w�莞�Ԍ�Ɏ��s����
    /// </summary>
    public static IEnumerator DelayMethod<T>(this MonoBehaviour mono, float waitTime, Action<T> action, T t)
    {
        yield return new WaitForSeconds(waitTime);
        action(t);
    }

    /// <summary>
    /// �w�肳�ꂽ���\�b�h���w�莞�Ԍ�Ɏ��s����
    /// </summary>
    public static IEnumerator DelayMethod(this MonoBehaviour mono, float waitTime,Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }



}
