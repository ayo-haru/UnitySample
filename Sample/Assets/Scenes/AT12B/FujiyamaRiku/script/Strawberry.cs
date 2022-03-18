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
        
        if (collision.gameObject.name == "Stage")
        {
            //Debug.Log("Alive : " + Boss1Attack.AliveStrawberry);
            Boss1Attack.FinishTime[Boss1Attack.AliveStrawberry] = 0;
            Boss1Attack.StrawberryUseFlg[Boss1Attack.AliveStrawberry] = false;
            Destroy(Boss1Attack.Strawberry[Boss1Attack.AliveStrawberry]);
            Boss1Attack.AliveStrawberry++;
        }
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            Boss1Attack.FinishTime[Boss1Attack.AliveStrawberry] = 0;
            Boss1Attack.StrawberryUseFlg[Boss1Attack.AliveStrawberry] = false;
            Destroy(Boss1Attack.Strawberry[Boss1Attack.AliveStrawberry]);
            Boss1Attack.AliveStrawberry++;
            Damage.damage = 5;
            HPgage.DelHP();
            Boss1Attack.RefrectFlg = true;
        }
        if(collision.gameObject.name == "Boss")
        {
            
        }
    }
    
}
