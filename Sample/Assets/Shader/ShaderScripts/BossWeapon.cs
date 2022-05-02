using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    Boss1Rain BossRain;
    float timer = 10.0f;                    // �X�^�[�g�̎���

    [SerializeField] private MeshRenderer _meshRenderer;        // �Ώۂ̃��b�V�������_�[���i�[
    [SerializeField] private Material _alphaMaterial;           // �ύX����}�e���A�����i�[(����̓��l��ς���}�e���A��)

    public float DestoyTime = 0.5f;                             // ������܂ł̎���

    private Material[] _originalMaterials;                      // �I�u�W�F�N�g�̂��Ƃ��Ǝ����Ă���}�e���A�����i�[
    private Material[] _dynamicmaterials;                       // �ύX����}�e���A�����i�[

    private Coroutine _playCoroutine;                           // �X�^�[�g����R���[�`�����i�[
    private bool isCalledOnee = false;                          // ��񂵂��������Ȃ��t���O


    // Start is called before the first frame update
    void Start()
    {
        
        BossRain = GameObject.Find("PanCake(Clone)").GetComponent<Boss1Rain>();
        
            //---���Ƃ��Ǝ����Ă���}�e���A�����i�[
            _originalMaterials = _meshRenderer.materials;

        //---�ύX����}�e���A���̏�����([_meshRenderer.materials.Length]�͗v�f�̔z��ɂȂ�)
        _dynamicmaterials = new Material[_meshRenderer.materials.Length];

        //---���b�V�������_�[�̗v�f�̔z�񕪏���������
        for(var materialIndex = 0;materialIndex < _meshRenderer.materials.Length; ++materialIndex)
        {
            //---�z��̐������ύX����_dynamicmaterials�ɐV�����}�e���A���ɍ����ւ���
            _dynamicmaterials[materialIndex] = new Material(_alphaMaterial);

            //---�e�N�X�`���̃v���p�e�B���R�s�[
            _dynamicmaterials[materialIndex].SetTexture("_MainTex",_meshRenderer.sharedMaterials[materialIndex].GetTexture("_MainTex"));

        }
    }

    private void Update()
    {
        
    }

    public void Play()
    {
        //---�Đ����ł���Ύ~�߂�
        Stop();

        //---�}�e���A����ύX��ɍ����ւ���
        _meshRenderer.materials = _dynamicmaterials;

        //---�R���[�`�����X�^�[�g
        _playCoroutine = StartCoroutine(Run());
    }

    public void Stop()
    {
        //---�R���[�`�����~�߂�
        if(_playCoroutine != null)
        {
            StopCoroutine(_playCoroutine);
            _playCoroutine = null;
        }

        //---���̃}�e���A���ɖ߂�
        _meshRenderer.materials = _originalMaterials;
    }

    private void SetAlpha(float value)
    {
        //---���ۂɃA���t�@�l��ύX
        for(int materialIndex = 0;materialIndex < _dynamicmaterials.Length; ++materialIndex)
        {
            //---�V�F�[�_�[�O���t�̃v���p�e�B���Q��
            _dynamicmaterials[materialIndex].SetFloat("_Alpha",value);
        }
    }

    private IEnumerator Run()
    {
        
        //---0�b�ɂȂ�܂ŌJ��Ԃ�
        while (timer > 0)
        {

            SetAlpha(timer / 10);               // �A���t�@�l��ύX
                                                   //---<memo>
                                                   //   �����ł�10.0f / 10 = 1 �P��SetAlpha()�ɓn��
                                                   //   10.0f - DestroyTime = num num��n��
                                                   //   0�b�܂ŌJ��Ԃ�

            //---1�t���[���x��
            yield return null;

            // ���Ԍo��
            timer -= DestoyTime;
            
        }
        
                
        //---�ŏI�I�Ȓl��ݒ�
        SetAlpha(0.0f);
        
    }
}
