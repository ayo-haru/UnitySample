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
        //---�}�e���A���̐���
        for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
        {
            _originMaterials[i] = _skinnedMeshRenderers[i].materials[0];
        }
        _dissolveMaterials = new Material[_skinnedMeshRenderers.Length];

        for(var materialIndex = 0;materialIndex < _skinnedMeshRenderers.Length; ++materialIndex)
        {
            //---�ύX����}�e���A���̍쐬
            _dissolveMaterials[materialIndex] = new Material(_dissolveMaterial);
        }

    }

    public void Play()
    {
        //---�Đ����ł���Ȃ�~�߂�
        Stop();
        //---�}�e���A���̕ύX������
        for (int i = 0; i < _skinnedMeshRenderers.Length; ++i)
        {
            _skinnedMeshRenderers[i].materials = _dissolveMaterials;
        }

        //---�R���[�`���X�^�[�g
         _Playcoroutine= StartCoroutine(Run());
    }

    public void Stop()
    {
        //---�R���[�`�����~�߂�
        if (_Playcoroutine != null)
        {
            StopCoroutine(_Playcoroutine);
            _Playcoroutine = null;
        }

        //---�}�e���A������ɖ߂�
        for(int i= 0;i < _skinnedMeshRenderers.Length; i++)
        {
            _skinnedMeshRenderers[i].materials[0] = _originMaterials[i];
        }

    }

    private void SetCutOff(float value)
    {
        for (int materialIndex = 0; materialIndex < _dissolveMaterials.Length; ++materialIndex)
        {
            _dissolveMaterials[materialIndex].SetFloat("_CutOff", value);
        }
    }


    private IEnumerator Run()
    {
        float timer = 0.0f;

        while(timer < 2)
        {
            SetCutOff(timer / 1);

            yield return null;

            timer += Time.deltaTime;
        }
        SetCutOff(1.0f);
    }
}
