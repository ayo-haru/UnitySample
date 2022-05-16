using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial02UI : MonoBehaviour {
    [SerializeField]
    private GameObject characterback;
    private GameObject CharacterBack;
    [SerializeField]
    private GameObject chara2_1;
    private GameObject Chara2_1;
    [SerializeField]
    private GameObject chara2_2;
    private GameObject Chara2_2;
    [SerializeField]
    private GameObject chara2_3;
    private GameObject Chara2_3;
    [SerializeField]
    private GameObject chara2_4;
    private GameObject Chara2_4;

    private Canvas canvas;  // �\������L�����o�X

    private int UIcnt;  // UI�\����

    private Game_pad UIActionAssets;        // InputAction��UI������
    private bool isDecision;                // ���肳�ꂽ


    private void Awake() {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {
        isDecision = false; // �����������

        UIcnt = 0;

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();

        //----- UI������ -----
        // ���ԉ�
        CharacterBack = Instantiate(characterback);
        Chara2_1 = Instantiate(chara2_1);
        Chara2_2 = Instantiate(chara2_2);
        Chara2_3 = Instantiate(chara2_3);
        Chara2_4 = Instantiate(chara2_4);

        // �L�����o�X�̎q�Ɏw��
        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara2_1.transform.SetParent(this.canvas.transform, false);
        Chara2_2.transform.SetParent(this.canvas.transform, false);
        Chara2_3.transform.SetParent(this.canvas.transform, false);
        Chara2_4.transform.SetParent(this.canvas.transform, false);

        // �����\�����Ȃ�
        CharacterBack.GetComponent<ImageShow>().Hide();
        Chara2_1.GetComponent<ImageShow>().Hide();
        Chara2_2.GetComponent<ImageShow>().Hide();
        Chara2_3.GetComponent<ImageShow>().Hide();
        Chara2_4.GetComponent<ImageShow>().Hide();

    }

    // Update is called once per frame
    void Update() {
        if (UIcnt == 0)
        {
            //Debug.Log("<color=blue></color>"+UIcnt);
            Time.timeScale = 0.0f;
            CharacterBack.GetComponent<ImageShow>().Show();
            Chara2_1.GetComponent<ImageShow>().Show();
            if (isDecision)
            {
                //Debug.Log("<color=blue>����</color>");

                UIcnt++;
                isDecision = false;
                return;
            }
        }
        else if(UIcnt == 1)
        {
            Chara2_1.GetComponent<ImageShow>().Clear();
            Chara2_2.GetComponent<ImageShow>().Show();
            if (isDecision)
            {
                UIcnt++;
                isDecision = false;
                Time.timeScale = 1.0f;
                return;
            }

        }
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        UIActionAssets.UI.Decision.started += OnDecision;

        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }



    /// <summary>
    /// ����{�^��
    /// </summary>
    private void OnDecision(InputAction.CallbackContext obj) {
        isDecision = true;
    }

}