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

    private bool Play;      // �G�t�F�N�g�Đ��t���O
    private float EffectTime = 4.0f;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Play)
        {
            Timer += Time.deltaTime;
            if (EffectTime < Timer)
            {
                // �E
                if (Guide_Right)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT, transform.position, EffectTime);
                }
                // �E��
                if (Guide_RightUp)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_UP, transform.position, EffectTime);
                }
                // �E��
                if (Guide_RightDown)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_DOWN, transform.position, EffectTime);
                }
                // ��
                if (Guide_Left)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT, transform.position, EffectTime);
                }
                // ����
                if (Guide_LeftUp)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_UP, transform.position, EffectTime);
                }
                // ����
                if (Guide_LeftDown)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_DOWN, transform.position, EffectTime);
                }
                Timer = 0.0f;
            }
            //// �E
            //if (Guide_Right)
            //{
            //    Timer += Time.deltaTime;
            //    if(EffectTime < Timer)
            //    {
            //        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT, transform.position, EffectTime);
            //        Timer = 0.0f;
            //    }
            //}
            //// �E��
            //if (Guide_RightUp)
            //{
            //    Timer += Time.deltaTime;
            //    if (EffectTime < Timer)
            //    {
            //        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_UP, transform.position, EffectTime);
            //        Timer = 0.0f;
            //    }   
            //}
            //// �E��
            //if (Guide_RightDown)
            //{
            //    Timer += Time.deltaTime;
            //    if (EffectTime < Timer)
            //    {
            //        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_DOWN, transform.position, EffectTime);
            //        Timer = 0.0f;
            //    }
            //}
            //// ��
            //if (Guide_Left)
            //{
            //    Timer += Time.deltaTime;
            //    if (EffectTime < Timer)
            //    {
            //        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT, transform.position, EffectTime);
            //        Timer = 0.0f;
            //    }
            //}
            //// ����
            //if (Guide_LeftUp)
            //{
            //    Timer += Time.deltaTime;
            //    if (EffectTime < Timer)
            //    {
            //        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_UP, transform.position, EffectTime);
            //        Timer = 0.0f;
            //    }
            //}
            //// ����
            //if (Guide_LeftDown)
            //{
            //    Timer += Time.deltaTime;
            //    if (EffectTime < Timer)
            //    {
            //        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_DOWN, transform.position, EffectTime);
            //        Timer = 0.0f;
            //    }
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���߂Â�����G�t�F�N�g���Đ�����
        if (other.CompareTag("Player"))
        {
            // �E
            if (Guide_Right)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT, transform.position, EffectTime);
            }
            // �E��
            if (Guide_RightUp)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_UP, transform.position, EffectTime);
            }
            // �E��
            if (Guide_RightDown)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_DOWN, transform.position, EffectTime);
            }
            // ��
            if (Guide_Left)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT, transform.position, EffectTime);
            }
            // ����
            if (Guide_LeftUp)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_UP, transform.position, EffectTime);
            }
            // ����
            if (Guide_LeftDown)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_DOWN, transform.position, EffectTime);
            }
            Play = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �v���C���[�����ꂽ��G�t�F�N�g���~����
        if (other.CompareTag("Player"))
        {
            Play = false;
        }
    }
}
