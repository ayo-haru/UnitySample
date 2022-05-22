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
    void StartFadeIn()
    {
        alfa -= fadeInSpeed;                            //a)�s�����x�����X�ɉ�����
        SetAlpha();                                     //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa <= 0)                                  //c)���S�ɓ����ɂȂ����珈���𔲂���
        {
            GameData.isFadeIn = false;
            Pause.isPause = false;                      // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;                       // a)�p�l���̕\�����I���ɂ���
        alfa += fadeOutSpeed;                           // b)�s�����x�����X�ɂ�����
        SetAlpha();                                     // c)�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)                                  // d)���S�ɕs�����ɂȂ����珈���𔲂���
        {
            GameData.isFadeOut = false;
            GameData.isFadeIn = true;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
