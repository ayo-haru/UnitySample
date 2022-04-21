using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //----------------------------------
    //
    //  �G�t�F�N�g�Đ�
    //  �쐬�F�ɒn�c�^��
    //  �ڍׁF��������EffectData�̗񋓑̂ɒ�`������[��
    //
    //----------------------------------
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
                //EffectData.activeEffect[i].GetComponent<ParticleSystem>().Pause();
                EffectData.activeEffect[i].GetComponent<ParticleSystem>().playbackSpeed = 0.0f;
            }
        }
    }

    public static void EffectUnPause() {
        if (!EffectData.isSetEffect)    // �G�t�F�N�g�f�[�^���ݒ�Ȃ���Ȃ�
        {
            return;
        }

        for (int i = 0; i < EffectData.activeEffect.Length; i++)
        {
            if (EffectData.activeEffect[i] != null)// �G�t�F�N�g�̐������|�[�Y����
            {
                //EffectData.activeEffect[i].GetComponent<ParticleSystem>()
                EffectData.activeEffect[i].GetComponent<ParticleSystem>().playbackSpeed = 1.0f;
                EffectData.activeEffect[i] = null;
            }
        }

        EffectData.oncePauseEffect = true;
    }

    /// <summary>
    /// ���ݎg���Ă���G�t�F�N�g�����ׂĒT��
    /// </summary>
    private static void SetAllActiveEffect() {
        int i = 0;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.GetComponent<ParticleSystem>())
            {
                if (i > 0)
                {
                    Debug.Log(EffectData.activeEffect[i - 1].transform.parent.gameObject);
                    Debug.Log(EffectData.activeEffect[i].transform.parent.gameObject);
                }


                    EffectData.activeEffect[i] = obj.transform.parent.gameObject;
                    Debug.Log(EffectData.activeEffect[i]);
                    Debug.Log(i);
                if (EffectData.activeEffect[i - 1].transform.parent.gameObject != EffectData.activeEffect[i].transform.parent.gameObject)
                {
                    i++;
                }
            }
        }
    }
}
