using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoundBoll : MonoBehaviour
{
    public GameObject BoundBoll;
    public LB_Attack Attack;
    // Start is called before the first frame update
    void Start()
    {
        Attack = GameObject.Find("LastBoss(Clone)").GetComponent<LB_Attack>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)//当たり判定処理
    {
        if (collision.gameObject.tag == "Ground")        //もし当たったモノにGroundタグが付いていた場合
        {
            
           
            Attack.Circlenum++;
            
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
        }
        if(collision.gameObject.name == "Rulaby"|| collision.gameObject.name == "LastBoss(Clone)")
        {
            

            Attack.Circlenum++;
            Attack.OneTimeFlg = true;
            Destroy(BoundBoll);      //BoundBollを破壊
            Debug.Log("弾を破壊した");//デバックログを表示
        }
    }
}
