using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SavePoint : MonoBehaviour
{
    private Camera _targetCamera;   // ���̃V�[���Ŏg���Ă�J�������擾
    private Canvas _parentUIobj;    // UI�̐e�i����̓L�����o�X�j
    private GameObject _targetUIobj;    // �ړI��UI
    private RectTransform _parentUI;    // �e��UI�̃��N�g�g�����X�t�H�[��
    private RectTransform _targetUI;    // �ړI��UI�̃��N�g�g�����X�t�H�[��

    private bool isHitPlayer;   // �v���C���[�ƏՓ˂�����

    private Game_pad UIActionAssets;                // InputAction��UI������
    private InputAction LeftStickSelect;            // InputAction��select������
    private InputAction RightStickSelect;           // InputAction��select������

    [SerializeField]
    private bool isRed = true;

    // Start is called before the first frame update
    void Start()
    {
        isHitPlayer = false;    // �������������Ă��Ȃ��̂�false
        Vector3 Pos = this.transform.position;  // UI�̕`��̊�l
        if (isRed)
        {
            EffectManager.Play(EffectData.eEFFECT.EF_MAGICSQUARE_RED, new Vector3(Pos.x, Pos.y - 10.0f, Pos.z), false);   // �\������UI
        }
        else
        {
            EffectManager.Play(EffectData.eEFFECT.EF_MAGICSQUARE_BLUE, new Vector3(Pos.x, Pos.y - 10.0f, Pos.z), false);   // �\������UI
        }

        _targetCamera = Camera.main;    // ���C���J�������擾
        _parentUIobj = GameObject.Find("Canvas").GetComponent<Canvas>();    // �L�����o�X���擾
        _parentUI = _parentUIobj.GetComponent<RectTransform>(); // �L�����o�X�̃��N�g�g�����X�t�H�[��
        _targetUIobj = GameObject.Find("StickMask1(Clone)");   // �ړI��UI���擾
        _targetUI = _targetUIobj.GetComponent<RectTransform>(); // �ړI��UI�̃��N�g�g�����X�t�H�[��
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitPlayer)    // �v���C���[��������������W��ύX
        {
            OnUIPositionUpdate();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            isHitPlayer = true;
            Player.HitSavePointColorisRed = isRed;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            isHitPlayer = false;
            Player.HitSavePointColorisRed = isRed;
        }
    }

    /// <summary>
    /// �Ă΂ꂽ������UI�̈ʒu��ύX
    /// </summary>
    void OnUIPositionUpdate() {
        // �I�u�W�F�N�g�̈ʒu
        Vector3 targetWorldPos = this.transform.position;
        targetWorldPos.y = targetWorldPos.y - 18.0f;
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
