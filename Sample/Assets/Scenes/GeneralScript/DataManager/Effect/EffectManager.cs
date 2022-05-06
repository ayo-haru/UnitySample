using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    /// <summary>
    /// �G�t�F�N�g�̍Đ�
    /// </summary>
    /// <param name="_effectDataNumber">EffectData�Œ�`�������</param>
    /// <param name="_pos">�\�����������W�B�I�u�W�F�N�g�̍��W�̂܂܂��ƒ��S�_�ɏo��̂Œ��ӁB</param>
    /// <param name="_destroy">���������Ȃ��ꍇ�̂�false�Ŏw��</param>
    public static void Play(EffectData.eEFFECT _effectDataNumber,Vector3 _pos,bool _destroy = true) {
        if (!EffectData.isSetEffect)
        {
            return; // �G�t�F�N�g�f�[�^���ݒ�
        }
        //�G�t�F�N�g�𐶐�����
        ParticleSystem effect = Instantiate(EffectData.EF[(int)_effectDataNumber]);
        //�G�t�F�N�g����������ꏊ�����肷��(�G�I�u�W�F�N�g�̏ꏊ)
        effect.transform.position = _pos;
        effect.Play();
        if (_destroy)
        {
            Destroy(effect.gameObject, 5.0f);
        }
    }

    /// <summary>
    /// �G�t�F�N�g�̍Đ�
    /// </summary>
    /// <param name="_effectDataNumber">EffectData�Œ�`�������</param>
    /// <param name="_pos">�\�����������W�B�I�u�W�F�N�g�̍��W�̂܂܂��ƒ��S�_�ɏo��̂Œ��ӁB</param>
    /// <param name="_destroyTime">������b���w��</param>
    public static void Play(EffectData.eEFFECT _effectDataNumber, Vector3 _pos,float _destroyTime) {
        if (!EffectData.isSetEffect)
        {  
            return; // �G�t�F�N�g�f�[�^���ݒ�
        }
        //�G�t�F�N�g�𐶐�����
        ParticleSystem effect = Instantiate(EffectData.EF[(int)_effectDataNumber]);
        //�G�t�F�N�g����������ꏊ�����肷��(�G�I�u�W�F�N�g�̏ꏊ)
        effect.transform.position = new Vector3(_pos.x,_pos.y,-5);

        effect.Play();
        // �������Ԃ�ݒ肵�����̂ɂ���
        Destroy(effect.gameObject, _destroyTime);
    }

    /// <summary>
    /// �G�t�F�N�g�|�[�Y
    /// </summary>
    public static void EffectPause() {
        if (!EffectData.isSetEffect)    // �G�t�F�N�g�f�[�^���ݒ�Ȃ���Ȃ�
        {
            return;
        }

        if (EffectData.oncePauseEffect) // �|�[�Y�ɓ��������񂾂����s�i�I�u�W�F�N�g��T���������d�����߁j�j
        {
            SetAllActiveEffect();   // �d���������΂ɖ��t���[�����Ȃ�
            EffectData.oncePauseEffect = false; // ��x������邽�߂Ƀt���O������
        }

        for (int i = 0; i < EffectData.activeEffect.Length; i++) {  
            if (EffectData.activeEffect[i] != null)// �G�t�F�N�g�̐������|�[�Y����
            {
                EffectData.activeEffect[i].GetComponent<ParticleSystem>().playbackSpeed = 0.0f;
                //EffectData.activeEffect[i].playbackSpeed = 0.0f;
            }
        }
    }

    /// <summary>
    /// �G�t�F�N�g�̃|�[�Y����
    /// </summary>
    public static void EffectUnPause() {
        if (!EffectData.isSetEffect)    // �G�t�F�N�g�f�[�^���ݒ�Ȃ���Ȃ�
        {
            return;
        }

        for (int i = 0; i < EffectData.activeEffect.Length; i++)
        {
            if (EffectData.activeEffect[i] != null)// �G�t�F�N�g�̐������|�[�Y����
            {
                EffectData.activeEffect[i].GetComponent<ParticleSystem>().playbackSpeed = 1.0f;
                //EffectData.activeEffect[i].playbackSpeed = 1.0f;
                EffectData.activeEffect[i] = null;
            }
        }

        EffectData.oncePauseEffect = true;
    }

    /// <summary>
    /// ���ݎg���Ă���G�t�F�N�g�����ׂĒT��
    /// </summary>
    private static void SetAllActiveEffect() {
       int index = 0;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))    // ���̃V�[���̂��ׂẴI�u�W�F�N�g������
        {
            if (obj.GetComponent<ParticleSystem>()) // �o�����I�u�W�F�N�g���p�[�e�B�N���V�X�e����������G�t�F�N�g�̃f�[�^�ɓn��
            {
                EffectData.activeEffect[index] = obj;
                //Debug.Log(EffectData.activeEffect[index]);
                //Debug.Log(index);
                index++;
            }
        }

        //foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        //{
        //    if (obj.GetComponent<ParticleSystem>())
        //    {
        //        if (!obj.transform.parent)
        //        {
        //            EffectData.activeEffect[index] = obj.GetComponent<ParticleSystem>();
        //            Debug.Log(EffectData.activeEffect[index]);
        //            Debug.Log(index);
        //            index++;
        //        }
        //    }
        //}

    }
}
