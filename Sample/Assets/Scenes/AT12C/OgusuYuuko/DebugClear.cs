//�f�o�b�O�p�̃v���O����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClear : MonoBehaviour
{
    //private GameObject MAP_UI;
    //int currenthp = 100;
    // HPGage hpGage;
    //int count = 0;
    //  UVScroll Moon;
    // PieceManager pieceManagewr;
    GameObject hpsystem;
   // public GameObject Effect;
    // Start is called before the first frame update
    void Awake()
    {
        //hpGage = GameObject.Find("MaskImage").GetComponent<HPGage>();
        // Moon = GameObject.Find("HPBar").GetComponent<UVScroll>();
        //  pieceManagewr = GameObject.Find("PieceHPManager").GetComponent<PieceManager>();
       
        //GameObject canvas = GameObject.Find("Canvas");
        
        //MAP_UI.transform.SetParent(canvas.transform, false);
    }

    private void Start()
    {
        //MAP_UI = GameObject.Find("MapManager(Clonet)");
        //MAP_UI.SetActive(false);
       hpsystem = GameObject.Find("HPSystem(2)(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        ////�N���A�\��
        //if (Input.GetKey(KeyCode.F1))
        //{
        //    GameObject ClearImage = GameObject.Find("EventSystem");
        //    ClearImage.SendMessage("GameClearShow");
        //}
        ////�Q�[���I�[�o�[�\��
        //if (Input.GetKey(KeyCode.F2))
        //{
        //    GameObject.Find("Canvas").GetComponent<GameOver>().GameOverShow();
        //    //OverImage.SendMessage("GameOverShow");
        //}


        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    ++count;
        //    Moon.SetFrame(count);
        //}
        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    --count;
        //    Moon.SetFrame(count);
        //}

        ////������Q�b�g
        //if(Input.GetKeyDown(KeyCode.F6))
        //{
        //    pieceManagewr.GetPiece();
        //    Debug.Log("������Q�b�g");
        //}
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    pieceManagewr.DelPiece();
        //    Debug.Log("�����猸����");
        //}        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    ++count;
        //    Moon.SetFrame(count);
        //}
        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    --count;
        //    Moon.SetFrame(count);
        //}

        ////������Q�b�g
        //if(Input.GetKeyDown(KeyCode.F6))
        //{
        //    pieceManagewr.GetPiece();
        //    Debug.Log("������Q�b�g");
        //}
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    pieceManagewr.DelPiece();
        //    Debug.Log("�����猸����");
        //}

        if (Input.GetKeyDown(KeyCode.F10))
        {
            hpsystem.GetComponent<HPManager>().GetPiece();
            Debug.Log("HP" + GameData.CurrentHP);
            Debug.Log("Piece" + GameData.CurrentPiece);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            hpsystem.GetComponent<HPManager>().Damaged();
            Debug.Log("HP" + GameData.CurrentHP);
            Debug.Log("Piece" + GameData.CurrentPiece);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            hpsystem.GetComponent<HPManager>().GetItem();
            Debug.Log("HP" + GameData.CurrentHP);
        }

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    MAP_UI.SetActive(false);
        //}
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    MAP_UI.SetActive(true);
        //}
        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage001;
        //    Debug.Log("�V�[��" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage002;
        //    Debug.Log("�V�[��" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F5))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage003;
        //    Debug.Log("�V�[��" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F6))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage004;
        //    Debug.Log("�V�[��" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage005;
        //    Debug.Log("�V�[��" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F8))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage006;
        //    Debug.Log("�V�[��" + GameData.CurrentMapNumber);
        //}



        //�̗͌��炷
        //if (Input.GetKeyDown(KeyCode.F10))
        //{
        //    currenthp -= 1;
        //    hpGage.HpGageDel(currenthp);
        //    Debug.Log(currenthp);
        //}




        //�G�t�F�N�g����
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //   Instantiate(Effect,new Vector3(100.0f,100.0f,0.0f),Quaternion.identity);

        //}


    }
    }
