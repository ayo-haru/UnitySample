using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers = new SkinnedMeshRenderer[6];
    [SerializeField] private Material _dissolveMaterial;

    private const float MinDuration = 0.001f;
    //[SerializeField] [Min(MinDuration)] private float _duration = 2.0f;
    
    private Material[] _originMaterials = new Material[6];
    private Material[] _dynamicMaterials;

    private Coroutine _playcoroutine;
  //  public float Duration 
  //  {
  //      get
		//{
  //          return _duration;
		//}
		//set
		//{
  //          _duration = Mathf.Max(value, MinDuration);
		//}
  //  }

	// Start is called before the first frame update
	void Start()
    {
        //---それぞれのキャラクターでプロパティ制御するためにマテリアルを動的に生成
        for(int i = 0;i < _skinnedMeshRenderers.Length; i++)
        {
          _originMaterials[i] = _skinnedMeshRenderers[i].materials[0];
        }
        _dynamicMaterials = new Material[_skinnedMeshRenderers.Length];

        for(var materialIndex = 0;materialIndex < _skinnedMeshRenderers.Length; ++materialIndex)
		{
            //---動的マテリアル生成
            _dynamicMaterials[materialIndex] = new Material(_dissolveMaterial);

            //---プロパティをコピー
            _dynamicMaterials[materialIndex].SetTexture("_BaseMap", _skinnedMeshRenderers[materialIndex].sharedMaterials[0].GetTexture("_BaseMap"));
            _dynamicMaterials[materialIndex].SetTexture("_BumpMap", _skinnedMeshRenderers[materialIndex].sharedMaterials[0].GetTexture("_BumpMap"));
		}
    }

	public void Play()
	{
        ////---再生中であれば止める
        Stop();

        //---マテリアルを変更する
        for (int i = 0; i < _skinnedMeshRenderers.Length; ++i)
        {
            _skinnedMeshRenderers[i].materials = _dynamicMaterials;
        }

        //---コルーチンでアニメーションさせる
        _playcoroutine = StartCoroutine(Run());
    }

	public void Stop()
	{
        //---コルーチンを止める
        if (_playcoroutine != null)
		{
            StopCoroutine(_playcoroutine);
            _playcoroutine = null;
        }

        //---マテリアルをもとに戻す
        for(int i= 0;i < _skinnedMeshRenderers.Length; i++)
        {
           //_skinnedMeshRenderers[i].materials[i] = _originMaterials[i];
           _skinnedMeshRenderers[i].materials[0] = _originMaterials[i];

        }
	}

    private void SetCutOff(float value)
	{
        for (int  materialIndex = 0;materialIndex < _dynamicMaterials.Length; ++materialIndex)
		{
            _dynamicMaterials[materialIndex].SetFloat("_Cutoff", value);
		}
	}
    
    private IEnumerator Run() 
    {
        float timer = 0.0f;
        while(timer < 2)
		{
            //---カットオフ値を変更
            SetCutOff(timer / 2);

            //---1フレーム休む
            yield return null;

            //---時間経過
            timer += Time.deltaTime;
		}
        SetCutOff(1.0f)
;    }
}
