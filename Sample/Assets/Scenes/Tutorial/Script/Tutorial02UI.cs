using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject panel;
    private GameObject Panel;
    [SerializeField]
    private GameObject nexticon;
    private GameObject NextIcon;


    private Canvas canvas;  // �\������L�����o�X

    private int UIcnt;  // UI�\����

    private Game_pad UIActionAssets;        // InputAction��UI������
    private bool isDecision;                // ���肳�ꂽ

    private Tutorial02Manager scenemanager;
    private DelayFollowCamera _delayfollowcamera;
    private BoxCollider wallcollider;

    private void Awake() {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {
        isDecision = false; // �����������

        UIcnt = 0;

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();

        scenemanager = GameObject.Find("SceneManager").GetComponent<Tutorial02Manager>();

        _delayfollowcamera = Camera.main.GetComponent<DelayFollowCamera>();

        wallcollider = GameObject.Find("wallcollider004").GetComponent<BoxCollider>();
        //----- UI������ -----
        // ���ԉ�
        CharacterBack = Instantiate(characterback);
        Chara2_1 = Instantiate(chara2_1);
        Chara2_2 = Instantiate(chara2_2);
        Chara2_3 = Instantiate(chara2_3);
        Chara2_4 = Instantiate(chara2_4);
        Panel = Instantiate(panel);
        NextIcon = Instantiate(nexticon);

        // �L�����o�X�̎q�Ɏw��
        Panel.transform.SetParent(this.canvas.transform, false);
        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara2_1.transform.SetParent(this.canvas.transform, false);
        Chara2_2.transform.SetParent(this.canvas.transform, false);
        Chara2_3.transform.SetParent(this.canvas.transform, false);
        Chara2_4.transform.SetParent(this.canvas.transform, false);
        NextIcon.transform.SetParent(this.canvas.transform, false);

        // �����\������
        CharacterBack.GetComponent<ImageShow>().Hide();
        Chara2_1.GetComponent<ImageShow>().Hide();
        Chara2_2.GetComponent<ImageShow>().Hide();
        Chara2_3.GetComponent<ImageShow>().Hide();
        Chara2_4.GetComponent<ImageShow>().Hide();
        NextIcon.transform.localPosition = new Vector3(500.0f, -380.0f, 0.0f);
        NextIcon.GetComponent<ImageShow>().Hide();
        Panel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        Panel.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update() {
        if (UIcnt == 0)
        {
            Panel.GetComponent<Image>().enabled = true;

            Time.timeScale = 0.0f;
            CharacterBack.GetComponent<ImageShow>().Show();
            Chara2_1.GetComponent<ImageShow>().Show();
            NextIcon.GetComponent<ImageShow>().Show();
            if (isDecision)
            {

                UIcnt++;
                isDecision = false;
                return;
            }
        }
        else if(UIcnt == 1)
        {
            Chara2_1.GetComponent<ImageShow>().Clear();
            Chara2_2.GetComponent<ImageShow>().Show();
            NextIcon.transform.localPosition = new Vector3(850.0f, -380.0f, 0.0f);
            NextIcon.GetComponent<Image>().enabled = true;

            if (isDecision)
            {
                NextIcon.GetComponent<ImageShow>().Hide();
                Panel.GetComponent<Image>().enabled = false;
                UIcnt++;
                isDecision = false;
                Time.timeScale = 1.0f;
                return;
            }

        }
        else if (UIcnt == 2)
        {
            NextIcon.GetComponent<ImageShow>().Clear();
            if (!TutorialPanCake.isAlive)
            {
                StartCoroutine("DelayDestroyPancake");
                Chara2_2.GetComponent<ImageShow>().Clear();
                Chara2_3.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }

            if (GameData.PlayerPos.x > TutorialPanCake.pancakePos.x)
            {
                UIcnt = 1;  // UI����O�̂�ɖ߂�
                Time.timeScale = 0.0f;  // �^�C���͎~�߂�
                scenemanager.PancakeDestroy();  // �p���P�[�L����  
                scenemanager.PancakeAppearance();   // �p���P�[���o��
                scenemanager.PlayerDestroy();   // �v���C���[����
                scenemanager.PlayerAppearance();    // �v���C���[�o��
            }
        }
        else if(UIcnt == 3 && Chara2_3.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
        {
            Chara2_4.GetComponent<ImageShow>().Show(3);
            UIcnt++;
        }
        else if(UIcnt == 4 && Chara2_4.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
        {
            CharacterBack.GetComponent<ImageShow>().FadeOut();
            _delayfollowcamera.enabled = true;
            wallcollider.enabled = false;
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

    private IEnumerator DelayDestroyPancake() {
        yield return new WaitForSeconds(0.5f);
        scenemanager.PancakeDestroy();
    }

}