//==========================================================
//      回復アイテムの処理
//      作成日　2022/04/05
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/05      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    Rigidbody rb;
    bool isGround = false;
    float aTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 地面についたらちょっと浮いてから空中に留まる
        if (isGround)
        {
            aTime += Time.deltaTime;
            if(aTime < 1.0f)
            {
                rb.AddForce(transform.up * (4.0f * aTime), ForceMode.Force);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                // くるくる回したい(追々)
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.useGravity = false;
            isGround = true;
        }

        // 弾かれたら消す
        if (collision.gameObject.name == "Weapon(Clone)" && isGround)
        {
            Destroy(gameObject, 0.0f);
        }


    }
}
