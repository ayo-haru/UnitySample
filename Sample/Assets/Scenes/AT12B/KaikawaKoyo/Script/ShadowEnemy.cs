//==========================================================
//      黒い影の部分
//      作成日　2022/03/21
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/03/21      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEnemy : MonoBehaviour
{
    GameObject Enemy;
    private Vector3 position;
    public int EnemyNumber;
    private bool spawn = false;
    private float Timer;
    private float LimitTime = 3.0f;
    private ParticleSystem effect;
    
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        
        effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_ENEMY_DARKAREA]);
        effect.transform.position = position;
        effect.Play();
        // 誰に変身するかの処理
        switch (EnemyNumber)
        {
            case 0:
                Enemy = (GameObject)Resources.Load("Carrot 1");
                break;
            case 1:
                Enemy = (GameObject)Resources.Load("Broccoli 1");
                break;
            case 2:
                Enemy = (GameObject)Resources.Load("Tomato 1");
                break;
            case 3:
                Enemy = (GameObject)Resources.Load("Carrot 2");
                break;
            case 4:
                Enemy = (GameObject)Resources.Load("Broccoli 2");
                break;
            case 5:
                Enemy = (GameObject)Resources.Load("Tomato 2");
                break;
        }

        //if(spawn)
        //{
        //    effect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //    if (effect.transform.localScale.x <= 0.0f)
        //    {
        //        Destroy(effect.gameObject, 0.0f);
        //        Destroy(gameObject, 0.0f);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(!Pause.isPause)
        {
            // プレイヤーを見つけたら敵に変身する
            if (other.CompareTag("Player"))
            {
                Destroy(effect.gameObject, 0.0f);
                Destroy(gameObject, 0.0f);
                if (!spawn)
                {
                    Instantiate(Enemy, position, Quaternion.identity);
                    spawn = true;
                }
            }
        }
    }
}
