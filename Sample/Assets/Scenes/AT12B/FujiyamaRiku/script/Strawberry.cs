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
            //Boss1Attack.FinishTime[Boss1Attack.AliveStrawberry] = 0;
            //Boss1Attack.StrawberryUseFlg[Boss1Attack.AliveStrawberry] = false;
            //Destroy(Boss1Attack.Strawberry[Boss1Attack.AliveStrawberry]);
            //Boss1Attack.AliveStrawberry++;
            Boss1Attack.RefrectFlg = true;
            //Boss1Attack.PlayerPoint[Boss1Attack.AliveStrawberry].x = GameData.PlayerPos.x + 3.0f;
            //Boss1Attack.StrawberryRefFlg[Boss1Attack.AliveStrawberry] = true;
        }
        //if (!Boss1Attack.StrawberryRefFlg[Boss1Attack.LoopSave])
        //{
            if (collision.gameObject.name == "Stage")
            {
            //Debug.Log("Save : " + Boss1Attack.LoopSave);
            //Boss1Attack.FinishTime[Boss1Attack.AliveStrawberry] = 0;
            //Boss1Attack.StrawberryUseFlg[Boss1Attack.AliveStrawberry] = false;
            //Destroy(Boss1Attack.Strawberry[Boss1Attack.AliveStrawberry]);
            //Boss1Attack.AliveStrawberry++;
            //Debug.Log("Save : " + Boss1Attack.AliveStrawberry);
        }
        //}
        
        if(collision.gameObject.name == "Boss")
        {
            
        }
    }
    
}
