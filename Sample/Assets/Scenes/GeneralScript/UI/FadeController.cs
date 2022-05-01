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
    //float red, green, blue, alfa;   //�p�l���̐F�A�s�����x���Ǘ�

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

    void Start() {
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
        if (GameData.isFadeIn)
        {
            FadeIn();
        }

        if (GameData.isFadeOut)
        {
            FadeOut();
        }
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
        float alpha = 1;                            //�F�̕s�����x
        Color color = new Color(0, 0, 0, alpha);    //Image�̐F�ύX�Ɏg��
        this.fadeDeltaTime = 0;                     //������
        this.fadeImage.color = color;                   //�F�̏�����(��)
        do
        {
            yield return null;                      //���t���[���ōĊJ
            this.fadeInSpeed += Time.unscaledDeltaTime;       //���Ԃ̉��Z
            alpha = 1 - (this.fadeDeltaTime / this.fadeInSpeed);   //�����x�̌���
            if (alpha < 0)
            {
                alpha = 0;                          //alpha�̒l�̐���
                Pause.isPause = false;
                GameData.isFadeIn = false;
            }
            color.a = alpha;                        //�F�̓����x�̌���
            this.fadeImage.color = color;               //�F�̑��
        }
        while (this.fadeDeltaTime <= this.fadeInSpeed);
    }

    private IEnumerator FadeOutCoroutine()
    {
        float alpha = 0;                            //�F�̕s�����x
        Color color = new Color(0, 0, 0, alpha);    //Image�̐F�ύX�Ɏg��
        this.fadeDeltaTime = 0;                     //������
        this.fadeImage.color = color;                   //�F�̏�����
        do
        {
            yield return null;                      //���t���[���ōĊJ
            this.fadeDeltaTime += Time.unscaledDeltaTime;       //���Ԃ̉��Z
            alpha = this.fadeDeltaTime / this.fadeOutSpeed;         //�����x�̌���
            if (alpha > 1)
            {
                alpha = 1;                          //alpha�̒l�̐���
                Pause.isPause = false;
                GameData.isFadeOut = false;
                GameData.isFadeIn = true;
            }
            color.a = alpha;                        //�F�̓����x�̌���
            this.fadeImage.color = color;               //�F�̑��
        }
        while (this.fadeDeltaTime <= this.fadeOutSpeed);
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
