using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoBom : MonoBehaviour
{
    private EnemyDown ED;
    private Transform EnemyPos;
    private Vector3 vec;
    private float bouncePower = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            ED = other.gameObject.GetComponent<EnemyDown>();
            EnemyPos = other.gameObject.GetComponent<Transform>();
            vec = (EnemyPos.position - transform.position).normalized;
            ED.EnemyDead(vec);
        }
    }
}
