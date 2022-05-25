using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessMove : MonoBehaviour {
    public enum eSccessState {  // �\���̕ύX��������X�e�[�g
        NONE,       // �������Ȃ�
        EXPANSION,  // �g��
        UP,         // ��ɂЂ���Ă���
        FIN         // �I��
    }

    private const float maxSizeRate = 1.2f;      // �g��̍ő�l
    private const float minSizeRate = 0.5f;      // �g��̍ŏ��l�i���̒l����g����n�߂�j
    private const float MaxWidth = 600.0f * maxSizeRate;
    private const float MaxHeight = 200.0f * maxSizeRate;
    private const float upforce = 10.0f;        // ��ɔ�΂���
    private const float expantionSpeed = 3.0f;   // �g��X�s�[�h

    //------ �ϐ���` -----
    public eSccessState mode;       // �\���̕ύX�̃X�e�[�g

    private float width;             // �����傫��
    private float height;             // �����傫��

    private RectTransform rectTransform;  // ����UI��RectTransform

    private bool onceCoroutine = true;

    // Start is called before the first frame update
    void Start() {
        mode = eSccessState.NONE;   // �ŏ��͉������Ȃ�
        width = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        height = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        rectTransform = this.GetComponent<RectTransform>(); // ����UI��RectTransform���i�[
        this.GetComponent<Image>().enabled = false; // �\���Ȃ�
    }

    // Update is called once per frame
    void Update() {
        if (mode == eSccessState.EXPANSION)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + width, rectTransform.sizeDelta.y + height);
            width += Time.deltaTime * expantionSpeed * 3;    //3:1�̊���������R������
            height += Time.deltaTime * expantionSpeed;
            if (rectTransform.sizeDelta.x > MaxWidth)
            {
                mode = eSccessState.UP;
            }
        }
        if (mode == eSccessState.UP)
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x,rectTransform.localPosition.y+upforce,rectTransform.localPosition.z);
            if (rectTransform.localPosition.y > 50.0f && onceCoroutine)
            {
                StartCoroutine("WaitFin");
                onceCoroutine = false;
            }
        }
    }

    public void StartSccess() {
        mode = eSccessState.EXPANSION;
        rectTransform.sizeDelta = new Vector2(0, 0);
        width = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        height = 0.0f;     // �ŏ��͏�����������Ԃ���n�߂Ă���
        rectTransform.localPosition = new Vector3(0.0f,0.0f,0.0f);
        this.GetComponent<Image>().enabled = true;
        onceCoroutine = true;
    }

    public void FinSuccess() {
        mode = eSccessState.NONE;
        this.GetComponent<Image>().enabled = false;
    }

    private IEnumerator WaitFin() {
        yield return new WaitForSeconds(1.5f);
        mode = eSccessState.FIN;

    }
}

