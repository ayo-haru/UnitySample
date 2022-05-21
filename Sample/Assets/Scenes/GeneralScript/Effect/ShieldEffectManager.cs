using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffectManager : MonoBehaviour
{
    private ParticleSystem ShileEffect;
    private GameObject Player;
    private Player2 player2;
    private bool isAlive;
    private Vector2 AttackDirection;
    // Start is called before the first frame update
    void Start()
    {
        
        //Player = GameObject.Find(GameData.Player.name);
        //player2 = Player.GetComponent<Player2>();



        //---盾エフェクト再生(盾の座標と同じ場所にセット)
        ShileEffect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_PLAYER_SHIELD],
                                  this.transform.position,
                                  Quaternion.identity);

        //---エフェクト回転
        //if (GameData.PlayerPos.x > this.transform.position.x)
        //{
        //    ShileEffect.transform.Rotate(new Vector3(ShileEffect.transform.rotation.x, 60.0f, ShileEffect.transform.rotation.z));
        //}
        //if (GameData.PlayerPos.x < this.transform.position.x)
        //{
        //    ShileEffect.transform.Rotate(new Vector3(ShileEffect.transform.rotation.x, 60.0f, ShileEffect.transform.rotation.z));
        //}
        //if (GameData.PlayerPos.y > this.transform.position.y)
        //{
        //    ShileEffect.transform.Rotate(new Vector3(-45.0f, ShileEffect.transform.rotation.y, ShileEffect.transform.rotation.z));
        //}
        //if (GameData.PlayerPos.y < this.transform.position.y)
        //{
        //    ShileEffect.transform.Rotate(new Vector3(60.0f, ShileEffect.transform.rotation.y, ShileEffect.transform.rotation.z));
        //}

        if (this.AttackDirection.x > 0.2f)  //右
        {
            ShileEffect.transform.Rotate(new Vector3(ShileEffect.transform.rotation.x, 60.0f, ShileEffect.transform.rotation.z));
        }
        if (this.AttackDirection.x < -0.2f) // 左
        {
            ShileEffect.transform.Rotate(new Vector3(ShileEffect.transform.rotation.x, 60.0f, ShileEffect.transform.rotation.z));
        }
        if (this.AttackDirection.y < -0.2f) // 下
        {
            ShileEffect.transform.Rotate(new Vector3(-60.0f, ShileEffect.transform.rotation.y, ShileEffect.transform.rotation.z));
        }
        if (this.AttackDirection.y > 0.2f)  // 上
        {
            ShileEffect.transform.Rotate(new Vector3(45.0f, ShileEffect.transform.rotation.y, ShileEffect.transform.rotation.z));
        }

        //ShileEffect.transform.Rotate(new Vector3(0, 0, 90));

        //---エフェクト再生
        ShileEffect.Play();
        isAlive = true;
        Debug.Log("ShieldEffectManager:盾生成するよー");
    }

    // Update is called once per frame
    void Update()
    {
        //---盾の回転

        //---盾の動きに同期(左右攻撃時は高さを少し補正)
        if (GameData.PlayerPos.x > this.transform.position.x || GameData.PlayerPos.x < this.transform.position.x)
        {
            ShileEffect.transform.position = new Vector3(this.transform.position.x,
                                             this.transform.position.y + 3f,
                                             this.transform.position.z);

        }
        else
        {
            ShileEffect.transform.position = this.transform.position;

        }

        if(isAlive == true)
        {
            Destroy(ShileEffect.gameObject,0.5f);
            isAlive = false;
            Debug.Log("ShieldEffectManager:盾消すよー");
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
            //---当たった瞬間の座標
            Vector3 HitPos = collision.transform.position;
            HitPos.z = -10f;
            
            //---ヒットエフェクトの再生
            EffectManager.Play(EffectData.eEFFECT.EF_PLAYER_HIT, HitPos);

		}
	}

    /// <summary>
    /// Player2から呼び出し。攻撃の方向をとる。
    /// </summary>
    /// <param name="_attackdirection"></param>
    public void SetPlayerAttackDire(Vector2 _attackdirection)
    {
        this.AttackDirection = _attackdirection;
    }
}
