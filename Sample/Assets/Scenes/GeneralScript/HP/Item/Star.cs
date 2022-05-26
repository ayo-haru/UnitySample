using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // HPマネージャー
    GameObject hp;                                      // HPのオブジェクトを格納
    HPManager hpmanager;

    //識別用ID
    private int id;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("HPSystem(2)(Clone)"))
        {
            hp = GameObject.Find("HPSystem(2)(Clone)");         // HPSystemを参照
            hpmanager = hp.GetComponent<HPManager>();           // HPSystemの使用するコンポーネント
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Weapon")   // 盾と当たったらUIを変更して音だしてエフェクトだして消す
        {
            //取得済みのフラグ立てる
            if (GameData.CurrentMapNumber != (int)GameData.eSceneState.Tutorial3)
            {
                GameData.isStarGet[GameData.CurrentMapNumber - 1, id] = true;
            }
            if (GameObject.Find("HPSystem(2)(Clone)"))
            {

                hpmanager.GetItem();
            }
            SoundManager.Play(SoundData.eSE.SE_REFLECTION_STAR, SoundData.GameAudioList);
            Vector3 effekctPos = this.transform.position;
            //effekctPos.y -= 2.5f;
            EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_HEALITEM, effekctPos);
            Destroy(gameObject);
            //Debug.Log("げっとあいてむ");
        }
    }

    public void SetID(int ID)
    {
        id = ID;
    }
}
