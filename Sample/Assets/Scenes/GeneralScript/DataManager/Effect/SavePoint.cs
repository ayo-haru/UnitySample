using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EffectManager.Play(EffectData.eEFFECT.EF_HEAL, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
