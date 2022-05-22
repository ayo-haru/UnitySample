//==========================================================
//      ハチ雑魚の出す毒の弾
//      作成日　2022/04/12
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/12      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    GameObject BeeEnemy;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField]
    GameObject PoisonArea;

    [SerializeField]
    private float MoveSpeed = 1.0f;
    private float speed = 0.0f;

    private bool reflect = false;

    // Start is called before the first frame update
    void Start()
    {
        BeeEnemy = GameObject.Find("Bee");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = BeeEnemy.transform.position;

        if(reflect)
        {
            if (speed <= 1)
            {
                speed += MoveSpeed * Time.deltaTime;
            }
            rb.position = Vector3.Lerp(startPosition, targetPosition, speed);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たったら消える
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
        }

        // 地面に当たったら毒の床を出して消える
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 毒の床だす
            Instantiate(PoisonArea, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.0f);
        }

        // 弾かれたらハチに飛んでく
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            //rb.velocity = Vector3.zero;
            startPosition = transform.position;
            reflect = true;
            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
        }

        // 弾かれた後にハチに当たったら消える
        if(reflect && collision.gameObject.name == "Bee")
        {
            Destroy(gameObject, 0.0f);
        }
    }
}
