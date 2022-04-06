//デバッグ用のプログラム

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClear : MonoBehaviour
{
    int count = 0;
    UVScroll Moon;
    PieceManager pieceManagewr;
    // Start is called before the first frame update
    void Start()
    {
        Moon = GameObject.Find("HPBar").GetComponent<UVScroll>();
        pieceManagewr = GameObject.Find("PieceHPManager").GetComponent<PieceManager>();
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


        if (Input.GetKeyDown(KeyCode.F3))
        {
            ++count;
            Moon.SetFrame(count);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            --count;
            Moon.SetFrame(count);
        }

        //かけらゲット
        if(Input.GetKeyDown(KeyCode.F6))
        {
            pieceManagewr.GetPiece();
        }


    }
}
