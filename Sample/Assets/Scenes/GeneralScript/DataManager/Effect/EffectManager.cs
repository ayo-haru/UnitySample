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

    public static void EffectPause() {
        for(int i = 0;i < (int)EffectData.eEFFECT.MAX_EF; i++)
        {
            //if (EffectData.EF[i].isPlaying)
            //{
                //EffectData.EF[i].Pause();
            //}
        }
    }
}
