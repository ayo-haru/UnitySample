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
        //---���ꂼ��̃L�����N�^�[�Ńv���p�e�B���䂷�邽�߂Ƀ}�e���A���𓮓I�ɐ���
        for(int i = 0;i < _skinnedMeshRenderers.Length; i++)
        {
          _originMaterials[i] = _skinnedMeshRenderers[i].materials[0];
        }
        _dynamicMaterials = new Material[_skinnedMeshRenderers.Length];

        for(var materialIndex = 0;materialIndex < _skinnedMeshRenderers.Length; ++materialIndex)
		{
            //---���I�}�e���A������
            _dynamicMaterials[materialIndex] = new Material(_dissolveMaterial);

            //---�v���p�e�B���R�s�[
            _dynamicMaterials[materialIndex].SetTexture("_BaseMap", _skinnedMeshRenderers[materialIndex].sharedMaterials[0].GetTexture("_BaseMap"));
            _dynamicMaterials[materialIndex].SetTexture("_BumpMap", _skinnedMeshRenderers[materialIndex].sharedMaterials[0].GetTexture("_BumpMap"));
		}
    }

	public void Play()
	{
        ////---�Đ����ł���Ύ~�߂�
        Stop();

        //---�}�e���A����ύX����
        for (int i = 0; i < _skinnedMeshRenderers.Length; ++i)
        {
            _skinnedMeshRenderers[i].materials = _dynamicMaterials;
        }

        //---�R���[�`���ŃA�j���[�V����������
        _playcoroutine = StartCoroutine(Run());
    }

	public void Stop()
	{
        //---�R���[�`�����~�߂�
        if (_playcoroutine != null)
		{
            StopCoroutine(_playcoroutine);
            _playcoroutine = null;
        }

        //---�}�e���A�������Ƃɖ߂�
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
            //---�J�b�g�I�t�l��ύX
            SetCutOff(timer / 2);

            //---1�t���[���x��
            yield return null;

            //---���Ԍo��
            timer += Time.deltaTime;
		}
        SetCutOff(1.0f)
;    }
}
