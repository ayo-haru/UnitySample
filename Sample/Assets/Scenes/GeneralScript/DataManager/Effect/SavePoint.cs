using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SavePoint : MonoBehaviour
{
    private Camera _targetCamera;
    private Canvas _parentUIobj;
    private GameObject _targetUIobj;
    private RectTransform _parentUI;
    private RectTransform _targetUI;

    private bool isHitPlayer;

    private Game_pad UIActionAssets;                // InputAction��UI������
    private InputAction LeftStickSelect;            // InputAction��select������
    private InputAction RightStickSelect;           // InputAction��select������


    // Start is called before the first frame update
    void Start()
    {
        isHitPlayer = false;
        Vector3 Pos = this.transform.position;
        EffectManager.Play(EffectData.eEFFECT.EF_MAGICSQUARE, new Vector3(Pos.x, Pos.y - 5.0f, Pos.z),false);
        _targetCamera = Camera.main;
        _parentUIobj = GameObject.Find("Canvas").GetComponent<Canvas>();
        _parentUI = _parentUIobj.GetComponent<RectTransform>();
        _targetUIobj = _parentUIobj.transform.GetChild(3).gameObject;
        _targetUI = _targetUIobj.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitPlayer)
        {
            OnUIPositionUpdate();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            isHitPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            isHitPlayer = false;
        }
    }

    /// <summary>
    /// �Ă΂ꂽ������UI�̈ʒu��ύX
    /// </summary>
    void OnUIPositionUpdate() {
        // �I�u�W�F�N�g�̈ʒu
        Vector3 targetWorldPos = this.transform.position;
        targetWorldPos.y = targetWorldPos.y + 10.0f;
        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        Vector3 targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out Vector2 uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        _targetUI.localPosition = uiLocalPos;

    }
}
