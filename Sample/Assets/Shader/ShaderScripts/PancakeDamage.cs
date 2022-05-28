using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers = new SkinnedMeshRenderer[6];
    [SerializeField] private Material _DamageMaterial;
    [SerializeField] private Material _OriginalMaterial;

    private Material[] _originMaterials = new Material[24];
    private Material[] _damageMaterial;

    private Coroutine _Playcoroutine;
    // Start is called before the first frame update
    void Start()
    {
        //---マテリアルの生成
        for(int i = 0; i < _skinnedMeshRenderers.Length; i++){
            _originMaterials[i] = _skinnedMeshRenderers[i].materials[0];
		}
        _damageMaterial = new Material[_skinnedMeshRenderers.Length];

		for (var materialIndex = 0; materialIndex < _skinnedMeshRenderers.Length; ++materialIndex){

			// ---変更するマテリアルの作成
			_damageMaterial[materialIndex] = new Material(_DamageMaterial);
		}

	}

    public void Play()
	{
        //---再生中であるなら止める
        //Stop();

        //---マテリアルの変更をする
        for(int i = 0; i < _skinnedMeshRenderers.Length; ++i){
            _skinnedMeshRenderers[i].materials = _damageMaterial;
		}

        //---コルーチンをスタートする(今回は特に処理はなし)
        _Playcoroutine = StartCoroutine(Run(0.3f));
	}

    public void Stop()
	{
  //      //---コルーチンを終了させる
  //      if(_Playcoroutine != null){
  //          StopCoroutine(_Playcoroutine);
  //          _Playcoroutine = null;
  //         }

        //---マテリアルをもとに戻す
        for (int i = 0; i < _skinnedMeshRenderers.Length; i++){
            _skinnedMeshRenderers[i].materials = _originMaterials;

        }
        
    }

    private IEnumerator Run(float waittime)
	{
        yield return new WaitForSeconds(waittime);
        //yield return null;
        Stop();
	}
}
