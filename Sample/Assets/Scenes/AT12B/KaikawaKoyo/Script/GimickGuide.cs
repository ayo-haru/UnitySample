using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimickGuide : MonoBehaviour
{
    [SerializeField]
    private bool Guide_Right;
    [SerializeField]
    private bool Guide_RightUp;
    [SerializeField]
    private bool Guide_RightDown;
    [SerializeField]
    private bool Guide_Left;
    [SerializeField]
    private bool Guide_LeftUp;
    [SerializeField]
    private bool Guide_LeftDown;

    private ParticleSystem effect;
    private bool Play;      // �G�t�F�N�g�Đ��t���O
    private float EffectTime = 5.1f;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �͈͓��ɂ���Ԃ͌J��Ԃ��Đ�����
        if(Play)
        {
            Timer += Time.deltaTime;
            if (EffectTime < Timer)
            {
                effect.Play();
                Timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���߂Â�����G�t�F�N�g���Đ�����
        if (other.CompareTag("Player") && !Play)
        {
            // �E
            if (Guide_Right)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT]);
            }
            // �E��
            if (Guide_RightUp)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_UP]);
            }
            // �E��
            if (Guide_RightDown)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_DOWN]);
            }
            // ��
            if (Guide_Left)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT]);
            }
            // ����
            if (Guide_LeftUp)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_UP]);
            }
            // ����
            if (Guide_LeftDown)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_DOWN]);
            }
            effect.transform.position = transform.position;
            Play = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �v���C���[�����ꂽ��G�t�F�N�g���~����
        if (other.CompareTag("Player") && Play)
        {
            Destroy(effect.gameObject, 0.0f);   // �G�t�F�N�g�폜
            Timer = 0.0f;                       // �^�C�}�[�̃��Z�b�g
            Play = false;
        }
    }
}
