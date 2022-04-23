//==========================================================
//      でけえ黒い影の部分
//      作成日　2022/04/20
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/04/20      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShadowEnemy : MonoBehaviour
{
    GameObject Enemy;
    private Vector3 position;
    public int EnemyNomber;
    private bool spawn = false;
    private bool look = false;
    private float spawnTime;
    [SerializeField]
    private float SpawnTime;
    [SerializeField]
    private int SpawnNumber;
    private int NowSpawn;
    private ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        effect = Instantiate(EffectData.EF[2]);
        effect.transform.position = position;
        effect.Play();
        SpawnNumber -= 1;
        // 誰に変身するかの処理
        switch (EnemyNomber)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > SpawnTime)
            {
                Instantiate(Enemy, position, Quaternion.identity);
                spawnTime = 0.0f;
                NowSpawn++;
            }
            if(SpawnNumber == NowSpawn)
            {
                Destroy(effect.gameObject, 0.0f);
                Destroy(gameObject, 0.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Pause.isPause)
        {
            // プレイヤーを見つけたら敵に変身する
            if (other.CompareTag("Player"))
            {
                if(!spawn)
                {
                    Instantiate(Enemy, position, Quaternion.identity);
                    spawn = true;
                }
            }
        }

    }
}
