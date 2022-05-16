//==========================================================
//      �ł��������e�̕���
//      �쐬���@2022/04/20
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
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
    public int EnemyNumber;
    private bool spawn = false;
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

        effect = Instantiate(EffectData.EF[3]);
        effect.transform.position = position;
        effect.transform.localScale = new Vector3(SpawnNumber, SpawnNumber, SpawnNumber);
        effect.Play();
        SpawnNumber -= 1;
        // �N�ɕϐg���邩�̏���
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
        if(spawn)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > SpawnTime)
            {
                Instantiate(Enemy, position, Quaternion.identity);
                spawnTime = 0.0f;
                NowSpawn++;
                effect.transform.localScale = new Vector3(SpawnNumber - NowSpawn, SpawnNumber - NowSpawn, SpawnNumber - NowSpawn);
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
            // �v���C���[����������G�ɕϐg����
            if (other.CompareTag("Player"))
            {
                if(!spawn)
                {
                    Instantiate(Enemy, position, Quaternion.identity);
                    effect.transform.localScale = new Vector3(SpawnNumber, SpawnNumber, SpawnNumber);
                    spawn = true;
                }
            }
        }

    }
}
