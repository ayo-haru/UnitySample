using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //----- �ϐ������� -----
    //--- �t�F�[�h�̕ϐ�
    float fadeOutSpeed = 1.0f;        //�����x���ς��X�s�[�h���Ǘ�
    float fadeInSpeed = 1.0f;        //�����x���ς��X�s�[�h���Ǘ�
    float fadeDeltaTime = 0;                //�t�F�[�h�Ɏg��������

    ObservedValue<bool> onceFadeOut;
    ObservedValue<bool> onceFadeIn;
    //float red, green, blue, alfa;   //�p�l���̐F�A�s�����x���Ǘ�

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

    void Start() {
        onceFadeOut = new ObservedValue<bool>(GameData.isFadeOut);
        onceFadeOut.OnValueChange += () => { if (GameData.isFadeOut) { FadeOut(); } };
        onceFadeIn = new ObservedValue<bool>(GameData.isFadeIn);
        onceFadeIn.OnValueChange += () => { if (GameData.isFadeIn) { FadeIn(); } };

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
        onceFadeOut.Value = GameData.isFadeOut;
        onceFadeIn.Value = GameData.isFadeIn;

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
        this.fadeDeltaTime = 0;                     //������
        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        while (this.fadeDeltaTime <= this.fadeInSpeed)
        {
            fadeDeltaTime += Time.deltaTime;

            float fadeAlpha = Mathf.Lerp(1f, 0f, fadeDeltaTime / fadeInSpeed);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            Debug.Log("�t�F�[�h�l"+fadeAlpha);
            if (fadeImage.color.a <= 0)
            {
                //Pause.isPause = false;
                GameData.isFadeIn = false;
            }
            yield return null;
        }
        
    }

    private IEnumerator FadeOutCoroutine()
    {
        Debug.Log("�t�F�[�h�A�E�g");
        this.fadeDeltaTime = 0;                     //������
        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        while (this.fadeDeltaTime <= this.fadeOutSpeed)
        {
            fadeDeltaTime += Time.deltaTime;
            float fadeAlpha = Mathf.Lerp(0f, 1f, fadeDeltaTime / fadeOutSpeed);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            //Debug.Log("�t�F�[�h�l" + fadeAlpha);
            if (fadeImage.color.a >= 1)
            //if (fadeAlpha >= 1)
            {
                GameData.isFadeOut = false;
                Debug.Log("�t�F�[�h�A�E�g����");
                GameData.isFadeIn = true;
            }
            yield return null; 
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
