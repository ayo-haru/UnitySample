using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool onryflg;/* = true;*/
    float BezieTime;
    Vector3 WeaponPos;
    Vector3 LastBossPos;
    Vector3 BeziePos;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (onryflg == true)
        {
            BezieTime += Time.deltaTime;
            this.transform.position = Beziercurve.SecondCurve(WeaponPos, BeziePos, LastBossPos, BezieTime);
            if (BezieTime > 1.0f)
            {

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Weapon(Clone)")
        {
            Debug.Log("<dolor = red>���I���s</color>");
            if (onryflg==false)
            {
                onryflg = true;
                if (this.transform.position.x >= GameData.PlayerPos.x)
                {
                    WeaponPos.x = GameObject.Find("Weapon(Clone)").transform.position.x + this.transform.localScale.x;
                    WeaponPos.y = GameObject.Find("Weapon(Clone)").transform.position.y + this.transform.localScale.y;
                }
                if (this.transform.position.x <= GameData.PlayerPos.x)
                {
                    WeaponPos.x = GameObject.Find("Weapon(Clone)").transform.position.x - this.transform.localScale.x;
                    WeaponPos.y = GameObject.Find("Weapon(Clone)").transform.position.y + this.transform.localScale.y;
                }
                WeaponPos.z = GameObject.Find("Weapon(Clone)").transform.position.z;
                LastBossPos = GameObject.Find("LastBoss(Clone)").transform.position;
                BeziePos= GameObject.Find("BeziePos").transform.position;
                BeziePos.x = LastBossPos.x;
                BeziePos.y = LastBossPos.y;
                Debug.Log("<dolor = red>���I���s</color>");
            }
        }
    }
}
