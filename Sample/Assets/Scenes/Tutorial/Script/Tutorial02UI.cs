using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tutorial02UI : MonoBehaviour {
    //----- �\������UI -----
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
    [SerializeField]
    private GameObject successchara;
    private GameObject SuccessChara;
    [SerializeField]
    private GameObject failedchara;
    private GameObject FailedChara;


    private Canvas canvas;  // �\������L�����o�X

    private int UIcnt;  // UI�\����

    private Game_pad UIActionAssets;        // InputAction��UI������
    private bool isDecision;                // ���肳�ꂽ

    private Tutorial02Manager scenemanager; // 02�V�[���̃}�l�[�W���[

    private DelayFollowCamera _delayfollowcamera;   // �f�B���C�t�H���[�J������ۑ����Ă���

    private BoxCollider wallcollider;   // �͂����`���[�g���A�����I���܂ł̕�

    private bool tutorialPause; // �`���[�g���A�����̃|�[�Y

    private void Awake() {
        UIActionAssets = new Game_pad();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {
        //----- �e��ϐ������� -----
        isDecision = false; // �����������
        UIcnt = 0;  // UI��\�����鏇�Ԃ��Ǘ�����
        canvas = GetComponent<Canvas>();    // �L�����o�X���w��
        scenemanager = GameObject.Find("SceneManager").GetComponent<Tutorial02Manager>();   // �V�[���}�l�[�W���[���ق���
        _delayfollowcamera = Camera.main.GetComponent<DelayFollowCamera>(); // �f�B���C�t�H���[�J������ۑ�
        wallcollider = GameObject.Find("wallcollider004").GetComponent<BoxCollider>();  // �ǂ�ۑ�
        tutorialPause = false;  // �����̓|�[�Y���Ȃ��̂�false

        //----- UI������ -----
        // ���ԉ�
        CharacterBack = Instantiate(characterback);
        Chara2_1 = Instantiate(chara2_1);
        Chara2_2 = Instantiate(chara2_2);
        Chara2_3 = Instantiate(chara2_3);
        Chara2_4 = Instantiate(chara2_4);
        Panel = Instantiate(panel);
        NextIcon = Instantiate(nexticon);
        SuccessChara = Instantiate(successchara);
        FailedChara = Instantiate(failedchara);

        // �L�����o�X�̎q�Ɏw��
        Panel.transform.SetParent(this.canvas.transform, false);
        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara2_1.transform.SetParent(this.canvas.transform, false);
        Chara2_2.transform.SetParent(this.canvas.transform, false);
        Chara2_3.transform.SetParent(this.canvas.transform, false);
        Chara2_4.transform.SetParent(this.canvas.transform, false);
        NextIcon.transform.SetParent(this.canvas.transform, false);
        SuccessChara.transform.SetParent(this.canvas.transform, false);
        FailedChara.transform.SetParent(this.canvas.transform, false);

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
        SuccessChara.GetComponent<ImageShow>().Hide();
        
    }

    void Update() {
        if (UIcnt == 0)
        {
            //---�G������܂���
            Panel.GetComponent<Image>().enabled = true;

            //Time.timeScale = 0.0f;
            Pause.isPause = true;
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
            //---�^�C�~���O�悭�e���܂��傤
            Chara2_1.GetComponent<ImageShow>().Clear();
            Chara2_2.GetComponent<ImageShow>().Show();
            NextIcon.transform.localPosition = new Vector3(850.0f, -380.0f, 0.0f);
            NextIcon.GetComponent<Image>().enabled = true;

            if (isDecision)
            {
                NextIcon.GetComponent<Image>().enabled = false;
                Panel.GetComponent<Image>().enabled = false;
                UIcnt++;
                isDecision = false;
                //Time.timeScale = 1.0f;
                Pause.isPause = false;
                return;
            }
        }
        else if (UIcnt == 2)
        {
            //---�͂�����������
            if (!TutorialPanCake.isAlive)
            {
                //SuccessChara.GetComponent<ImageShow>().Show(30);
                if(SuccessChara.GetComponent<SuccessMove>().mode == SuccessMove.eSccessState.NONE)
                {
                    SuccessChara.GetComponent<SuccessMove>().StartSccess();
                }
                StartCoroutine("DelayDestroyPancake");     // �����ɏ����̂ł͂Ȃ��x�������ď���
                Chara2_2.GetComponent<ImageShow>().Clear();
                Chara2_3.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }

            if (GameData.PlayerPos.x > TutorialPanCake.pancakePos.x)
            {
                //FailedChara.GetComponent<ImageShow>().Show(30);
                if(FailedChara.GetComponent<FailedMove>().mode == FailedMove.eFailedState.NONE)
                {
                    FailedChara.GetComponent<FailedMove>().StartFailed();
                    Pause.isPause = true;
                    //Time.timeScale = 0.0f;              // �^�C���͎~�߂�
                }
                if (FailedChara.GetComponent<FailedMove>().mode == FailedMove.eFailedState.FIN)
                {
                    FailedChara.GetComponent<FailedMove>().FinFailed();
                    scenemanager.PancakeDestroy();      // �p���P�[�L����  
                    scenemanager.PancakeAppearance();   // �p���P�[���o��
                    scenemanager.PlayerDestroy();       // �v���C���[����
                    scenemanager.PlayerAppearance();    // �v���C���[�o��
                    UIcnt = 1;                          // UI����O�̂�ɖ߂�
                }
            }
        }
        else if(UIcnt == 3 && Chara2_3.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
        {
            //---�����݂񂮂悭�͂���
            Chara2_4.GetComponent<ImageShow>().Show(3);
            UIcnt++;
        }
        else if(UIcnt == 4 && Chara2_4.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
        {
            //---���ꂪ���̐��E�̐킢��
            CharacterBack.GetComponent<ImageShow>().FadeOut();
            _delayfollowcamera.enabled = true;
            wallcollider.enabled = false;
        }
        else if(UIcnt == 5)
        {
            // ��������񂯂ǃG���[�h�~
            if (SuccessChara.GetComponent<SuccessMove>().mode == SuccessMove.eSccessState.FIN)
            {
                SuccessChara.GetComponent<SuccessMove>().FinSuccess();
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

    private IEnumerator DelayDestroyPancake() {
        yield return new WaitForSeconds(0.5f);
        scenemanager.PancakeDestroy();
    }

}