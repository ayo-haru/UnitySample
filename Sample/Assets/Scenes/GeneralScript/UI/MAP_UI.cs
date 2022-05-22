//=============================================================================
//
// �}�b�v�\��
//
//
// �쐬��:2022/04/23
// �쐬��:����T�q
//
// <�J������>
// 2022/03/16 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MAP_UI : MonoBehaviour {
    public GameObject DisplayMAP;
    private Game_pad UIActionAssets;        // InputAction��UI������
    private bool isMapButton;
    // Start is called before the first frame update
    void Awake() {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
        isMapButton = false;

        //---MAP�\��
        GameObject canvas;
        canvas = GameObject.Find("Canvas2");
        if (!canvas)
        {
            canvas = GameObject.Find("Canvas");
        }

        DisplayMAP = Instantiate(DisplayMAP);
        DisplayMAP.transform.SetParent(canvas.transform, false);
    }

    private void Start() {
        DisplayMAP.SetActive(false);
        //HP���\������Ă�����
        //HP�̎�O�ɕ\������
        GameObject hpUI = GameObject.Find("HPSystem(2)(Clone)");
        if (hpUI)
        {
            int hpUIIndex = hpUI.transform.GetSiblingIndex();
            DisplayMAP.transform.SetSiblingIndex(hpUIIndex + 1);
        }
    }

    // Update is called once per frame
    void Update() {
        ////���P��������}�b�v�\��
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    DisplayMAP.SetActive(true);
        //}
        ////���Q��������}�b�v��\��
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    DisplayMAP.SetActive(false);
        //}
        //M�L�[�ŕ\����\���؂�ւ�
        //if (Input.GetKeyDown(KeyCode.M))
        if (isMapButton)
        {
            DisplayMAP.SetActive(!DisplayMAP.activeSelf);
        }
        isMapButton = false;
    }
    private void OnEnable() {
        UIActionAssets.UI.Map.canceled += OnMap;

        //---InputAction�̗L����
        UIActionAssets.UI.Enable();

    }

    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }


    private void OnMap(InputAction.CallbackContext obj) {
        isMapButton = true;
    }
}