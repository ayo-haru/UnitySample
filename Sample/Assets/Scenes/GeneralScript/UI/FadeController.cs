using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //----- �ϐ������� -----
    //--- �t�F�[�h�̕ϐ�
    float fadeOutSpeed = 0.4f;        //�����x���ς��X�s�[�h���Ǘ�
    float fadeInSpeed = 0.4f;        //�����x���ς��X�s�[�h���Ǘ�
    float fadeDeltaTime = 0;                //�t�F�[�h�Ɏg��������

    [System.NonSerialized]
    public static bool onceFadeOut;
    [System.NonSerialized]
    public static bool onceFadeIn;
    //float red, green, blue, alfa;   //�p�l���̐F�A�s�����x���Ǘ�

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

    void Start() {
        onceFadeOut = true;
        onceFadeIn = true;
        fadeImage = GetComponent<Image>();  // �t�F�[�h�̐F�֘A
        //red = fadeImage.color.r;
        //green = fadeImage.color.g;
        //blue = fadeImage.color.b;
        //alfa = fadeImage.color.a;

        //if (GameData.isFadeIn)
        //{
        //    alfa = 1.0f;
        //}
        //if (GameData.isFadeOut)
        //{
        //    alfa = 0.0f;
        //}
    }

    void Update()
    {
        //if (GameData.isFadeIn)
        //{ 
        //    FadeIn();
        //}

        //if (GameData.isFadeOut && onceFadeOut)
        //{
        //    FadeOut();
        //}
    }

    //void StartFadeIn() {
    //    alfa -= fadeInSpeed;                //a)�s�����x�����X�ɉ�����
    //    SetAlpha();                      //b)�ύX�����s�����x�p�l���ɔ��f����
    //    if (alfa <= 0)
    //    {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
    //        GameData.isFadeIn = false;
    //        fadeImage.enabled = false;    //d)�p�l���̕\�����I�t�ɂ���
    //        Pause.isPause = false;  // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
    //    }
    //}

    //void StartFadeOut() {
    //    fadeImage.enabled = true;  // a)�p�l���̕\�����I���ɂ���
    //    alfa += fadeOutSpeed;         // b)�s�����x�����X�ɂ�����
    //    SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
    //    if (alfa >= 1)
    //    {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
    //        GameData.isFadeOut = false;
    //        GameData.isFadeIn = true;
    //        Pause.isPause = false;  // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
    //        Debug.Log("�t�F�[�h�I���̉���");
    //    }
    //}

    //void SetAlpha() {
    //    fadeImage.color = new Color(red, green, blue, alfa);
    //}

    private IEnumerator FadeInCoroutine()
    {
        Debug.Log("�t�F�[�h�@");

        //float alpha = 1;                            //�F�̕s�����x
        //Color color = new Color(0, 0, 0, alpha);    //Image�̐F�ύX�Ɏg��
        this.fadeDeltaTime = 0;                     //������
        //this.fadeImage.color = color;                   //�F�̏�����(��)
        while (this.fadeDeltaTime <= this.fadeInSpeed)
        {
            float fadeAlpha = Mathf.Lerp(1f, 0f, fadeDeltaTime / fadeInSpeed);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            fadeDeltaTime += Time.deltaTime;
            //yield return null;                      //���t���[���ōĊJ
            //this.fadeInSpeed += Time.deltaTime;       //���Ԃ̉��Z
            //alpha = Mathf.Lerp(0f, 1f, fadeDeltaTime / fadeInSpeed);   //�����x�̌���
            if (fadeImage.color.a < 0)
            {
                Pause.isPause = false;
                GameData.isFadeIn = false;
            }
            yield return null;
            //color.a = alpha;                        //�F�̓����x�̌���
            //this.fadeImage.color = color;               //�F�̑��
        }
        
    }

    private IEnumerator FadeOutCoroutine()
    {
        Debug.Log("�t�F�[�h�A�E�g");
        //float alpha = 0;                            //�F�̕s�����x
        //Color color = new Color(0, 0, 0, alpha);    //Image�̐F�ύX�Ɏg��
        this.fadeDeltaTime = 0;                     //������
        //this.fadeImage.color = color;                   //�F�̏�����
        while (this.fadeDeltaTime <= this.fadeOutSpeed)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, fadeDeltaTime / fadeOutSpeed);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            fadeDeltaTime += Time.deltaTime;
            //if (fadeImage.color.a >= 1)
            if (fadeAlpha >= 1)
            {
                //Pause.isPause = false;
                GameData.isFadeOut = false;
                Debug.Log("�t�F�[�h�A�E�g�����̃|�[�Y����");
                //GameData.isFadeIn = true;
            }
            yield return null; 
            //color.a = alpha;                        //�F�̓����x�̌���
            //this.fadeImage.color = color;               //�F�̑��
        }
    }

    //�O������Ăяo�����
    public void FadeIn()
    {
        IEnumerator coroutine = FadeInCoroutine();
        StartCoroutine(coroutine);
    }
    public void FadeOut()
    {
        IEnumerator coroutine = FadeOutCoroutine();
        StartCoroutine(coroutine);
    }
}
