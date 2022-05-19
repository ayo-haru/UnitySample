using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.FireOnOff)
        {
            Ignition();
        }
        else
        {
            Extinguish();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// ì_âŒ
    /// </summary>
    public void Ignition() {
        Vector3 effectPos = this.transform.position;
        effectPos.y += 6.0f;
        EffectManager.Play(EffectData.eEFFECT.EF_GIMICK_FIRE, effectPos, false);
    }

    /// <summary>
    /// è¡âŒ
    /// </summary>
    public void Extinguish() {
        Destroy(GameObject.Find("Fire_kari(Clone)"));
    }
}
