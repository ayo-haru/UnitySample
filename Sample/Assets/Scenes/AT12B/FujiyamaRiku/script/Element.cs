using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    Camera g_camera;
    GameObject g_object;
    Image g_image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetElements(Camera camera)
    {
        g_camera = camera;
    }
    public void SetElements(GameObject obj)
    {
        g_object = obj;
    }
    public void SetElements(Image image)
    {
        g_image = image;
    }
    public Camera GetCameraElement()
    {
        return g_camera;
    }
    public GameObject GetObjElement()
    {
        return g_object;
    }
    public Image GetImageElement()
    {
        return g_image;
    }


}
