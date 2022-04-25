using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //----- �ϐ������� -----
    //--- �����̕ϐ�
    //int roomNumber_X;         // �������琔���ĉ�������
   
    //--- �v���C���[�̕ϐ�
    //Vector3 nowPlayerPos;
    //Vector3 oldPlayerPos;

    //--- �t�F�[�h�̕ϐ�
    float fadeOutSpeed = 0.01f;        //�����x���ς��X�s�[�h���Ǘ�
    float fadeInSpeed = 0.01f;        //�����x���ς��X�s�[�h���Ǘ�
    float red, green, blue, alfa;   //�p�l���̐F�A�s�����x���Ǘ�

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

    void Start() {
        //oldPlayerPos = nowPlayerPos = GameData.PlayerPos;   // �v���C���[�̍��W�̕ۑ�

        //roomNumber_X = (int)(nowPlayerPos.x / GameData.GetroomSize());    // �v���C���[�����������ڂɂ��邩

        fadeImage = GetComponent<Image>();  // �t�F�[�h�̐F�֘A
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;

        if (GameData.isFadeIn)
        {
            alfa = 1.0f;
        }
        if (GameData.isFadeOut)
        {
            alfa = 0.0f;
        }
    }

    void Update() {
        //roomNumber_X = (int)(nowPlayerPos.x / GameData.GetroomSize());    // �v���C���[�����������ڂɂ��邩
        //float fadePoint = roomNumber_X * GameData.GetroomSize();

        //oldPlayerPos = nowPlayerPos;
        //nowPlayerPos = GameData.PlayerPos;

        //if(oldPlayerPos.x < fadePoint + GameData.GetroomSize() && fadePoint + GameData.GetroomSize() < nowPlayerPos.x)
        //{
        //    GameData.isFadeOut = true;
        //}

        //if (nowPlayerPos.x < fadePoint && fadePoint < oldPlayerPos.x)
        //{
        //    GameData.isFadeOut = true;
        //}


        if (GameData.isFadeIn)
        {
            StartFadeIn();
            //Time.timeScale = 0;
        }

        if (GameData.isFadeOut)
        {
            StartFadeOut();
            //Time.timeScale = 0;
        }
        //Time.timeScale = 1.0f;
    }

    void StartFadeIn() {
        alfa -= fadeInSpeed;                //a)�s�����x�����X�ɉ�����
        SetAlpha();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa <= 0)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
            GameData.isFadeIn = false;
            fadeImage.enabled = false;    //d)�p�l���̕\�����I�t�ɂ���
            //Pause.isPause = false;  // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
        }
    }

    void StartFadeOut() {
        fadeImage.enabled = true;  // a)�p�l���̕\�����I���ɂ���
        alfa += fadeOutSpeed;         // b)�s�����x�����X�ɂ�����
        SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)
        {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
            GameData.isFadeOut = false;
            GameData.isFadeIn = true;
            Pause.isPause = false;  // �t�F�[�h���͋��炭�|�[�Y��������|�[�Y����߂�
        }
    }

    void SetAlpha() {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
