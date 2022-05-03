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
    private ParticleSystem effect;
    
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        //EffectManager.Play(EffectData.eEFFECT.EF_DARKAREA, position,false);

        effect = Instantiate(EffectData.EF[3]);
        effect.transform.position = position;
        effect.Play();
        // 誰に変身するかの処理
        switch (EnemyNumber)
        {
            case 0:
                Enemy = (GameObject)Resources.Load("Carrot");
                break;
            case 1:
                Enemy = (GameObject)Resources.Load("Broccoli");
                break;
            case 2:
                Enemy = (GameObject)Resources.Load("Tomato");
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
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
