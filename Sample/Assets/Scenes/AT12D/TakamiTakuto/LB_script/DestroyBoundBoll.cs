using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoundBoll : MonoBehaviour
{
    public GameObject BoundBoll;
    public LB_Attack Attack;
    LastHPGage HpScript;
    // Start is called before the first frame update
    void Start()
    {
        Attack = GameObject.Find("LastBoss(Clone)").GetComponent<LB_Attack>();
        HpScript = GameObject.Find("HPGage").GetComponent<LastHPGage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)//当たり判定処理
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "GroundDameged")        //もし当たったモノにGroundタグが付いていた場合
        {
            
           
            Attack.Circlenum++;
            
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
        }
        if(collision.gameObject.name == "LastBoss(Clone)")
        {
            HpScript.DelHP(Attack.BulletDamage);
            Attack.Circlenum++;
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBollを破壊
        }
        if(collision.gameObject.name == "Rulaby")
        {
            

            Attack.Circlenum++;
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
        }
    }
}
