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
    public int EnemyNomber;
    private bool spawn = false;
    //[SerializeField] private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        //particle.Play();

        position = transform.position;

        // �N�ɕϐg���邩�̏���
        switch (EnemyNomber)
        {
            case 0:
                Enemy = (GameObject)Resources.Load("Carrot");
                break;
            case 1:
                Enemy = (GameObject)Resources.Load("BroccoliEnemy");
                break;
            case 2:
                Enemy = (GameObject)Resources.Load("tomato");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[����������G�ɕϐg����
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 0.0f);
            //particle.Stop();
            if(!spawn)
            {
                Instantiate(Enemy, position, Quaternion.identity);
                spawn = true;
            }
            
        }
    }
}
