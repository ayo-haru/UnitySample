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
    public static void Play(EffectData.eEFFECT _effectDataNumber,Vector3 _pos) {
        //�G�t�F�N�g�𐶐�����
        ParticleSystem effect = Instantiate(EffectData.EF[(int)_effectDataNumber]);
        //�G�t�F�N�g����������ꏊ�����肷��(�G�I�u�W�F�N�g�̏ꏊ)
        effect.transform.position = _pos;
        effect.Play();
        Destroy(effect.gameObject, 5.0f);
    }

    public static void Play(EffectData.eEFFECT _effectDataNumber, Vector3 _pos,float _destroyTime) {
        //�G�t�F�N�g�𐶐�����
        ParticleSystem effect = Instantiate(EffectData.EF[(int)_effectDataNumber]);
        //�G�t�F�N�g����������ꏊ�����肷��(�G�I�u�W�F�N�g�̏ꏊ)
        effect.transform.position = _pos;

        effect.Play();
        // �������Ԃ�ݒ肵�����̂ɂ���
        Destroy(effect.gameObject, _destroyTime);
    }

}
