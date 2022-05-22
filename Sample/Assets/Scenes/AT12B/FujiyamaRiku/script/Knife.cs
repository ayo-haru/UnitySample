using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
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
        if (!BossAttack.WeaponAttackFlg)
        {
            if (collision.gameObject.name == "Weapon(Clone)")
            {
                GetComponent<Collider>().enabled = false;
                BossAttack.RefrectFlg = true;
            }
        }
    }
}
