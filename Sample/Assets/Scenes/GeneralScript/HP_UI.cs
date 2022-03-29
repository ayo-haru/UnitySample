using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    public GameObject DisplayHP;
    private void Awake()
    {
        //---UI•\Ž¦
        GameObject canvas = GameObject.Find("Canvas");
        var insstance = Instantiate(DisplayHP);
        insstance.transform.SetParent(canvas.transform,false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
