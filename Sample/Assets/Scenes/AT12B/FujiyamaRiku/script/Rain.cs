using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
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
        if (BossAttack.WeaponAttackFlg)
        {
            if (collision.gameObject.name == "Weapon(Clone)")
            {
                for (int i = 0; i < BossAttack.BossRain.MaxWeapon; i++)
                {
                    if (this.gameObject.name == "BossWeapon" + i)
                    {
                        BossAttack.BossRain.g_Weapon[i].RainRefrectFlg = true;
                        BossAttack.BossRain.g_Weapon[i].RainRefOnlyFlg = true;
                    }
                }
            }
        }
    }
}
