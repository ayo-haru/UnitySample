using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Pos = this.transform.position;
        EffectManager.Play(EffectData.eEFFECT.EF_MAGICSQUARE, new Vector3(Pos.x, Pos.y - 5.0f, Pos.z),false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
