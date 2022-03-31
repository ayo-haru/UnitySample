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
            for (int i = 0; i < Boss1Attack.PreMax_Strawberry; i++)
            {
                if (this.gameObject.name == "strawberry" + i)
                {
                    Boss1Attack.StrawberryRefFlg[i] = true;
                }
            }
        }
        if(collision.gameObject.name == "Player(Clone)")
        {
            for (int i = 0; i < Boss1Attack.PreMax_Strawberry; i++)
            {
                if (this.gameObject.name == "strawberry" + i)
                {
                    Boss1Attack.StrawberryColPlayer[i] = true;
                }
            }
        }
    }
    
}
