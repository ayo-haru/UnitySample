using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStartArea : MonoBehaviour
{
    GameObject KitchenImage;
    GameObject GardenImage;
    GameObject ChildrenRoomImage;
    // Start is called before the first frame update
    void Start()
    {
        KitchenImage = GameObject.Find("KitchenImage");
        GardenImage = GameObject.Find("GardenImage");
        ChildrenRoomImage = GameObject.Find("Children'sRoomImage");
    }

    // Update is called once per frame
    void Update()
    {
        //ï\é¶
        if (Input.GetKeyDown(KeyCode.F1))
        {
            KitchenImage.GetComponent<ImageShow>().Show();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GardenImage.GetComponent<ImageShow>().Show();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ChildrenRoomImage.GetComponent<ImageShow>().Show();
        }

        //ï\é¶èIóπ
        if (Input.GetKeyDown(KeyCode.F4))
        {
            KitchenImage.GetComponent<ImageShow>().Hide();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GardenImage.GetComponent<ImageShow>().Hide();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            ChildrenRoomImage.GetComponent<ImageShow>().Hide();
        }
    }
}
