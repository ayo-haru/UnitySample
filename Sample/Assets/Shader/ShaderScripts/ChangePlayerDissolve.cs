using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerDissolve : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers = new SkinnedMeshRenderer[17];
    [SerializeField] private Material _OriginMaterial;
    [SerializeField] private Material _dissolveMaterial;

    private Material[] _originMaterials = new Material[17];
    private Material[] _dissolveMaterials;

    private Coroutine _Playcoroutine;


    // Start is called before the first frame update
    void Start()
    {
        //---マテリアルの生成
        for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
        {
            _originMaterials[i] = _skinnedMeshRenderers[i].materials[0];
        }
        _dissolveMaterials = new Material[_skinnedMeshRenderers.Length];

        for(var materialIndex = 0;materialIndex < _skinnedMeshRenderers.Length; ++materialIndex)
        {
            //---変更するマテリアルの作成
            _dissolveMaterials[materialIndex] = new Material(_dissolveMaterial);
        }

    }

    public void Play()
    {
        //---再生中であるなら止める

        //---マテリアルの変更をする
        for (int i = 0; i < _skinnedMeshRenderers.Length; ++i)
        {
            _skinnedMeshRenderers[i].materials = _dissolveMaterials;
        }

        //---コルーチンスタート
         _Playcoroutine= StartCoroutine(Run());
    }

    public void Stop()
    {

    }

    private void SetCutOff(float value)
    {
    }


    private IEnumerator Run()
    {
        float timer = 0.0f;

        while(timer < 2)
        {
            yield return null;

            timer += Time.deltaTime;
        }
    }
}
