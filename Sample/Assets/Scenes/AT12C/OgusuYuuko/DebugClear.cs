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
        if (Input.GetKey(KeyCode.Return))
        {
            GameObject ClearImage = GameObject.Find("GameClearImage");
            ClearImage.SendMessage("Show");
        }
    }
}
