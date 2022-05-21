using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public  class ShakeCamera :MonoBehaviour
{
    //   [Header("Shake")]
    //   public Transform ShakeObject = null;                    // ここに動かすカメラを設定する
    //   public float ShakePower = 0.02f;                          // カメラの揺れの強さ
    //   public float ShakeDecay = 0.002f;                        // 揺れの減算値
    //   public float ShakeAmount = 0.2f;                         // 揺れの強さ係数

    //   Vector3 OriginPosition;
    //   Quaternion OriginRotation;

    //   // Start is called before the first frame update
    //   void Start()
    //   {
    //       OriginPosition = ShakeObject.localPosition;
    //       OriginRotation = ShakeObject.localRotation;
    //   }

    //   public void  Do()
    //{
    //       StopAllCoroutines();
    //       StartCoroutine(Shake());
    //}

    //   public IEnumerator Shake()
    //{
    //       float shakePower = ShakePower;
    //       while(shakePower > 0)
    //	{
    //           ShakeObject.localPosition = OriginPosition + Random.insideUnitSphere * shakePower;
    //           ShakeObject.localRotation = new Quaternion(
    //               OriginRotation.x + Random.Range(-shakePower, shakePower) * ShakeAmount,
    //               OriginRotation.y + Random.Range(-shakePower, shakePower) * ShakeAmount,
    //               OriginRotation.z + Random.Range(-shakePower, shakePower) * ShakeAmount,
    //               OriginRotation.w + Random.Range(-shakePower, shakePower) * ShakeAmount);
    //           shakePower -= ShakeDecay;
    //           yield return false;
    //	}
    //       ShakeObject.localPosition = OriginPosition;
    //       ShakeObject.localRotation = OriginRotation;
    //}
    /// <summary>
    /// カメラ振動演出
    /// </summary>
    /// <param name="width"></param>    カメラの振れ幅
    /// <param name="cnt"></param>      往復回数
    /// <param name="duration"></param> 時間
    /// 
    public void Shake(float width, int cnt, float duration)
    {
        var camera = Camera.main.transform;
        var seq = DOTween.Sequence();

        var partDuration = duration / cnt / 2f;

        var widthHalf = width / 2f;

        for (int i = 0; i < cnt - 1; i++)
        {
            seq.Append(camera.DOLocalRotate(new Vector3(-width, 0f), partDuration));
            seq.Append(camera.DOLocalRotate(new Vector3(width, 0f), partDuration));
        }

        seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf, 0f), partDuration));
        seq.Append(camera.DOLocalRotate(Vector3.zero, partDuration));
    }
}
