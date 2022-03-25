using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaunceEnemy : MonoBehaviour
{
    public float bounceSpeed = 5.0f;


    private Vector3 targetPos;

    [System.NonSerialized]
    public bool isBounce = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBounce)  // 跳ね返っていないときは跳ね返りの力をかけない
        {
            return;
        }

        // 座標変更
        if(this.transform.position.x < this.targetPos.x)
        {
            transform.position += new Vector3(bounceSpeed,0.0f, 0.0f) * Time.deltaTime;
        }
        else if (this.transform.position.x > this.targetPos.x)
        {
            transform.position -= new Vector3(bounceSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    

        // オブジェクトの消去
        Destroy(gameObject,0.5f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            //衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = collision.contacts[0].normal;

            // 武器に当たった瞬間の武器の座標をとってはじいたところからちょっと遠くへ飛ばす
            Vector3 weaponPos = GameObject.Find("Weapon(Clone)").transform.position;
            targetPos = new Vector3(weaponPos.x + 5.0f * normal.x, weaponPos.y + 5.0f * normal.y, weaponPos.z + 5.0f * normal.z);

            // 跳ね返った
            isBounce = true;

            // 跳ね返された時だけ重力の計算を消す
            this.GetComponent<Rigidbody>().isKinematic = true;

            // 音だす
            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
        }
    }
}
