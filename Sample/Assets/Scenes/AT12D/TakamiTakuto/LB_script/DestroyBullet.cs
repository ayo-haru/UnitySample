using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject Bullet;
    public LB_Attack Attack;
    public LastHPGage HpScript;
    public static bool Flg = false;
    // Start is called before the first frame update
    void Start()
    {
      
        HpScript = GameObject.Find("HPGage").GetComponent<LastHPGage>();
        Attack = GameObject.Find("LastBoss(Clone)").GetComponent<LB_Attack>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Rulaby")
        {
            
            Attack.OneTimeFlg = true;
            Attack.AnimFlg = false;
            Destroy(Bullet);
            Flg = true;
        }
        if (collision.gameObject.name == "LastBoss(Clone)")
        {
            Attack.AnimFlg = false;
            Attack.OneTimeFlg = true;
            Destroy(Bullet);
            HpScript.DelHP(Attack.BOundDamage);
            Flg = true;
        }
    }
}
