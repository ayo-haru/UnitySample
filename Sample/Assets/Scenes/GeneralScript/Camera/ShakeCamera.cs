using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [Header("Shake")]
    public Transform ShakeObject = null;                    // �����ɓ������J������ݒ肷��
    public float ShakePower = 0.02f;                          // �J�����̗h��̋���
    public float ShakeDecay = 0.002f;                        // �h��̌��Z�l
    public float ShakeAmount = 0.2f;                         // �h��̋����W��

    Vector3 OriginPosition;
    Quaternion OriginRotation;

    // Start is called before the first frame update
    void Start()
    {
        OriginPosition = ShakeObject.localPosition;
        OriginRotation = ShakeObject.localRotation;
    }

    public void  Do()
	{
        StopAllCoroutines();
        StartCoroutine(Shake());
	}

    public IEnumerator Shake()
	{
        float shakePower = ShakePower;
        while(shakePower > 0)
		{
            ShakeObject.localPosition = OriginPosition + Random.insideUnitSphere * shakePower;
            ShakeObject.localRotation = new Quaternion(
                OriginRotation.x + Random.Range(-shakePower, shakePower) * ShakeAmount,
                OriginRotation.y + Random.Range(-shakePower, shakePower) * ShakeAmount,
                OriginRotation.z + Random.Range(-shakePower, shakePower) * ShakeAmount,
                OriginRotation.w + Random.Range(-shakePower, shakePower) * ShakeAmount);
            shakePower -= ShakeDecay;
            yield return false;
		}
        ShakeObject.localPosition = OriginPosition;
        ShakeObject.localRotation = OriginRotation;
	}
}
