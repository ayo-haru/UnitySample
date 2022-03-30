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
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            if (this.gameObject.name == "strawberry0")
            {
                Boss1Attack.StrawberryRefFlg[0] = true;
            }
            if (this.gameObject.name == "strawberry1")
            {
                Boss1Attack.StrawberryRefFlg[1] = true;
            }
            if (this.gameObject.name == "strawberry2")
            {
                Boss1Attack.StrawberryRefFlg[2] = true;
            }
            if (this.gameObject.name == "strawberry3")
            {
                Boss1Attack.StrawberryRefFlg[3] = true;
            }
            if (this.gameObject.name == "strawberry4")
            {
                Boss1Attack.StrawberryRefFlg[4] = true;
            }
            if (this.gameObject.name == "strawberry5")
            {
                Boss1Attack.StrawberryRefFlg[5] = true;
            }
            
        }
        if(collision.gameObject.name == "Boss")
        {
            
        }
    }
    
}
