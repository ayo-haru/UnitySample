using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossHit : MonoBehaviour
{
    public LB_Attack Attack;
    private GameObject HpObject;
    public LastHPGage HpScript;

    // Start is called before the first frame update
    void Start()
    {
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<LastHPGage>();
        Attack = GetComponent<LB_Attack>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision Collision)//ìñÇΩÇËîªíËèàóù
    {
        if (Collision.gameObject.name == "BoundBoll(Clone)")
        {
           Debug.Log("BoundBollDamage");
        }
        if (Collision.gameObject.name == "Bulletl(Clone)")
        {
            HpScript.DelHP(Attack.BulletDamage);
            Debug.Log("BulletDamage");
        }
        if (Collision.gameObject.name == " WarpBullet(Clone)")
        {
            HpScript.DelHP(Attack.WapeDamage);
            Debug.Log("WeapBulletDamage");
        }
    }
}