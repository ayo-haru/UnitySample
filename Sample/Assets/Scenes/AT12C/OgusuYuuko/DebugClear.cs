using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //クリア表示
        if (Input.GetKey(KeyCode.F1))
        {
            GameObject ClearImage = GameObject.Find("EventSystem");
            ClearImage.SendMessage("GameClearShow");
        }
        //ゲームオーバー表示
        if (Input.GetKey(KeyCode.F2))
        {
            GameObject.Find("Canvas").GetComponent<GameOver>().GameOverShow();
            //OverImage.SendMessage("GameOverShow");
        }
    }
}
