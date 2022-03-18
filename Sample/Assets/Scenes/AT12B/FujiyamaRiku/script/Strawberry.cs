using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // Õ“Ë‚µ‚½‘Šè‚ÉPlayerƒ^ƒO‚ª•t‚¢‚Ä‚¢‚é‚Æ‚«
        if (collision.gameObject.name == "Stage")
        {
            Debug.Log("Alive : " + Boss1Attack.AliveStrawberry);
            Boss1Attack.FinishTime[Boss1Attack.AliveStrawberry] = 0;
            Boss1Attack.AliveStrawberry++;
            Destroy(this.gameObject);
            //Boss1Attack.StrawberryFlg[Boss1Attack.LoopSave] = false;
        }
    }
}
