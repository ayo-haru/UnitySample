using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TextureShow()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    public void TextureHide()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
    }

}
