using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailedMove : MonoBehaviour
{
    public enum eFailedState {  // �\���̕ύX��������X�e�[�g
        NONE,       // �������Ȃ�
        EXPANSION,  // �g��
        TILT,       // �X����
        FIN         // �I��
    }

    private const float maxSizeRate = 1.2f;      // �g��̍ő�l
    private const float minSizeRate = 0.5f;      // �g��̍ŏ��l�i���̒l����g����n�߂�j
    private const float MaxWidth = 600.0f * maxSizeRate;
    private const float MaxHeight = 200.0f * maxSizeRate;
    private const float maxAngle = -45.0f;        // �X����p�x�̍ő�
    private const float angle = 1.0f;        // �X����p�x
    //private const float expantionSpeed = 1.5f;   // �g��X�s�[�h
    private const float expantionSpeed = 3.0f;   // �g��X�s�[�h
    private const float tiltSpeed = 0.5f;        // �X����X�s�[�h

    //------ �ϐ���` -----
    public eFailedState mode;       // �\���̕ύX�̃X�e�[�g

    private float width;             // �����傫��
    private float height;             // �����傫��

    private RectTransform rectTransform;  // ����UI��RectTransform

    private bool onceCoroutine = true;

    // Start is called before the first frame update
    void Start()
    {
        mode = eFailedState.NONE;   // �ŏ��͉������Ȃ�
        width = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        height = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        rectTransform = this.GetComponent<RectTransform>(); // ����UI��RectTransform���i�[
        this.GetComponent<Image>().enabled = false; // �\���Ȃ�
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == eFailedState.EXPANSION)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + width, rectTransform.sizeDelta.y + height);
            width += Time.deltaTime * expantionSpeed *3;    //3:1�̊���������R������
            height += Time.deltaTime * expantionSpeed;
            if (rectTransform.sizeDelta.x > MaxWidth)
            {
                mode = eFailedState.TILT;
                //mode = eFailedState.FIN;
            }
        }
        if (mode == eFailedState.TILT)
        {
            if (onceCoroutine)
            {
                StartCoroutine("DelayTilt");
                StartCoroutine("WaitFin");
                onceCoroutine = false;
            }
        }
    }

    public void StartFailed() {
        mode = eFailedState.EXPANSION;
        rectTransform.sizeDelta = new Vector2(0, 0);
        //rectTransform.sizeDelta = new Vector2(600, 200);
        width = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        height = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        rectTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        this.GetComponent<Image>().enabled = true;
        onceCoroutine = true;
    }

    public  void FinFailed() {
        mode = eFailedState.NONE;
        this.GetComponent<Image>().enabled = false;
    }


    private IEnumerator DelayTilt() {
        yield return new WaitForSeconds(0.5f);
        rectTransform.eulerAngles = new Vector3(0.0f, 0.0f, -10.0f);
    }

    private IEnumerator WaitFin() {
        yield return new WaitForSeconds(1.5f);
        mode = eFailedState.FIN;

    }
}
