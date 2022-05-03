using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //----- �ϐ������� -----
    //--- �t�F�[�h�̕ϐ�
    float fadeOutSpeed = 0.1f;                  //�����x���ς��X�s�[�h���Ǘ�
    float fadeInSpeed = 0.1f;                   //�����x���ς��X�s�[�h���Ǘ�
    float fadeOutDeltaTime = 0;                 //�t�F�[�h�Ɏg��������
    float fadeInDeltaTime = 0;                  //�t�F�[�h�Ɏg��������

    ObservedValue<bool> onceFadeOut;
    ObservedValue<bool> onceFadeIn;
    float red, green, blue, alfa;   //�p�l���̐F�A�s�����x���Ǘ�

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

    void Start() {

        fadeImage = GetComponent<Image>();  // �t�F�[�h�̐F�֘A
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;

        onceFadeOut = new ObservedValue<bool>(GameData.isFadeOut);
        onceFadeOut.OnValueChange += () => { if (GameData.isFadeOut) { Pause.isPause = true; alfa = 0.0f; } };
        onceFadeIn = new ObservedValue<bool>(GameData.isFadeIn);
        onceFadeIn.OnValueChange += () => { if (GameData.isFadeIn) { Pause.isPause = true; alfa = 1.0f; } };



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

         onceFadeOut.Value = GameData.isFadeOut;
         onceFadeIn.Value = GameData.isFadeIn;


        if (GameData.isFadeIn)
        {
            StartFadeIn();
        }

        if (GameData.isFadeOut)
        {
            StartFadeOut();
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

    void StartFadeIn()
    {
        Debug.Log("�t�F�[�h�C�������");
        alfa -= fadeInSpeed;                            //a)�s�����x�����X�ɉ�����
        SetAlpha();                                     //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa <= 0)                                  //c)���S�ɓ����ɂȂ����珈���𔲂���
        {
            GameData.isFadeIn = false;
            //fadeImage.enabled = false;                //d)�p�l���̕\�����I�t�ɂ���
            Pause.isPause = false;                      // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
        }
    }

    void StartFadeOut()
    {
        Debug.Log("�t�F�[�h�A�E�g�����");

        fadeImage.enabled = true;                       // a)�p�l���̕\�����I���ɂ���
        alfa += fadeOutSpeed;                           // b)�s�����x�����X�ɂ�����
        SetAlpha();                                     // c)�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)                                  // d)���S�ɕs�����ɂȂ����珈���𔲂���
        {
            GameData.isFadeOut = false;
            GameData.isFadeIn = true;
            Pause.isPause = false;  // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
            //Debug.Log("�t�F�[�h�I���̉���");
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
    //private IEnumerator FadeInCoroutine()
    //{
    //    Debug.Log("�t�F�[�h�@");
    //    this.fadeInDeltaTime = 0;                     //������
    //    fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

    //    while (this.fadeInDeltaTime <= this.fadeInSpeed)
    //    {
    //        fadeInDeltaTime += Time.deltaTime;
    //        //fadeInDeltaTime += 0.01f;

    //        float fadeAlpha = Mathf.Lerp(1f, 0f, fadeInDeltaTime / fadeInSpeed);
    //        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
    //        Debug.Log("�t�F�[�h�l" + fadeAlpha);
    //        if (fadeImage.color.a <= 0)
    //        {
    //            //Pause.isPause = false;
    //            GameData.isFadeIn = false;
    //        }
    //        yield return null;
    //    }

    //}

    //private IEnumerator FadeOutCoroutine()
    //{
    //    Debug.Log("�t�F�[�h�A�E�g");
    //    this.fadeOutDeltaTime = 0;                     //������
    //    fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    //    while (this.fadeOutDeltaTime <= this.fadeOutSpeed)
    //    {
    //        fadeOutDeltaTime += Time.deltaTime;
    //        float fadeAlpha = Mathf.Lerp(0f, 1f, fadeOutDeltaTime / fadeOutSpeed);
    //        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
    //        //Debug.Log("�t�F�[�h�l" + fadeAlpha);
    //        if (fadeImage.color.a >= 1)
    //        //if (fadeAlpha >= 1)
    //        {
    //            GameData.isFadeOut = false;
    //            Debug.Log("�t�F�[�h�A�E�g����");
    //            //GameData.isFadeIn = true;
    //        }
    //        yield return null; 
    //    }
    //}

    ////�O������Ăяo�����
    //public void FadeIn()
    //{
    //    IEnumerator coroutine = FadeInCoroutine();
    //    StartCoroutine(coroutine);
    //}
    //public void FadeOut()
    //{
    //    IEnumerator coroutine = FadeOutCoroutine();
    //    StartCoroutine(coroutine);
    //}
}
