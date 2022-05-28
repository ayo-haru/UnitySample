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
        //---�}�e���A���̐���
        for(int i = 0; i < _skinnedMeshRenderers.Length; i++){
            _originMaterials[i] = _skinnedMeshRenderers[i].materials[0];
		}
        _damageMaterial = new Material[_skinnedMeshRenderers.Length];

		for (var materialIndex = 0; materialIndex < _skinnedMeshRenderers.Length; ++materialIndex){

			// ---�ύX����}�e���A���̍쐬
			_damageMaterial[materialIndex] = new Material(_DamageMaterial);
		}

	}

    public void Play()
	{
        //---�Đ����ł���Ȃ�~�߂�
        //Stop();

        //---�}�e���A���̕ύX������
        for(int i = 0; i < _skinnedMeshRenderers.Length; ++i){
            _skinnedMeshRenderers[i].materials = _damageMaterial;
		}

        //---�R���[�`�����X�^�[�g����(����͓��ɏ����͂Ȃ�)
        _Playcoroutine = StartCoroutine(Run(0.3f));
	}

    public void Stop()
	{
  //      //---�R���[�`�����I��������
  //      if(_Playcoroutine != null){
  //          StopCoroutine(_Playcoroutine);
  //          _Playcoroutine = null;
  //         }

        //---�}�e���A�������Ƃɖ߂�
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
