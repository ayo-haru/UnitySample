using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracking_Bullet : MonoBehaviour
{
    public GameObject Target;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;

    private Vector3 m_velocity;

    //í«è]Ç∑ÇÈÉvÉåÉCÉÑÅ[Çäiî[Ç∑ÇÈ
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_velocity += (Target.transform.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }
}
