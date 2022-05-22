using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    Boss1Attack BossAttack;
    // Start is called before the first frame update

    void Start()
    {
        BossAttack = GameObject.Find("PanCake(Clone)").GetComponent<Boss1Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            for (int i = 0; i < BossAttack.BossStrawberry.Max_Strawberry; i++)
            {
                if (this.gameObject.name == "strawberry" + i)
                {
                    BossAttack.BossStrawberry.StrawberryRefFlg[i] = true;
                }
            }
        }
        if(collision.gameObject.name == "Player(Clone)")
        {
            for (int i = 0; i < BossAttack.BossStrawberry.Max_Strawberry; i++)
            {
                if (this.gameObject.name == "strawberry" + i)
                {
                    BossAttack.BossStrawberry.StrawberryColPlayer[i] = true;
                }
            }
        }
    }
    
}
