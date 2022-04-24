//==========================================================
//      �����e�̕���
//      �쐬���@2022/03/21
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
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
        
        effect = Instantiate(EffectData.EF[2]);
        effect.transform.position = position;
        effect.Play();
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
                Enemy = (GameObject)Resources.Load("Tomato_Attack");
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
            // �v���C���[����������G�ɕϐg����
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
