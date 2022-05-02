//デバッグ用のプログラム

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
        ////クリア表示
        //if (Input.GetKey(KeyCode.F1))
        //{
        //    GameObject ClearImage = GameObject.Find("EventSystem");
        //    ClearImage.SendMessage("GameClearShow");
        //}
        ////ゲームオーバー表示
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

        ////かけらゲット
        //if(Input.GetKeyDown(KeyCode.F6))
        //{
        //    pieceManagewr.GetPiece();
        //    Debug.Log("かけらゲット");
        //}
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    pieceManagewr.DelPiece();
        //    Debug.Log("かけら減った");
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

        ////かけらゲット
        //if(Input.GetKeyDown(KeyCode.F6))
        //{
        //    pieceManagewr.GetPiece();
        //    Debug.Log("かけらゲット");
        //}
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    pieceManagewr.DelPiece();
        //    Debug.Log("かけら減った");
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
        //    Debug.Log("シーン" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage002;
        //    Debug.Log("シーン" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F5))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage003;
        //    Debug.Log("シーン" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F6))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage004;
        //    Debug.Log("シーン" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage005;
        //    Debug.Log("シーン" + GameData.CurrentMapNumber);
        //}
        //if (Input.GetKeyDown(KeyCode.F8))
        //{
        //    GameData.CurrentMapNumber = (int)GameData.eSceneState.KitchenStage006;
        //    Debug.Log("シーン" + GameData.CurrentMapNumber);
        //}



        //体力減らす
        //if (Input.GetKeyDown(KeyCode.F10))
        //{
        //    currenthp -= 1;
        //    hpGage.HpGageDel(currenthp);
        //    Debug.Log(currenthp);
        //}




        //エフェクト発生
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //   Instantiate(Effect,new Vector3(100.0f,100.0f,0.0f),Quaternion.identity);

        //}


    }
    }
