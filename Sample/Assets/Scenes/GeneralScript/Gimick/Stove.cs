using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    private GameObject fireswitch;

    // Start is called before the first frame update
    void Start()
    {
        //fireswitch = GameObject.Find("FireSwitch");
    }

    // Update is called once per frame
    void Update()
    {
        if (FireSwitch.OnOff)
        {

        }
    }

    /// <summary>
    /// ì_âŒ
    /// </summary>
    public void Ignition() {
        Vector3 effectPos = this.transform.position;
        effectPos.y += 6.0f;
        EffectManager.Play(EffectData.eEFFECT.EF_FIRE, effectPos, false);
    }

    /// <summary>
    /// è¡âŒ
    /// </summary>
    public void Extinguish() {
        Destroy(GameObject.Find("Fire_kari(Clone)"));
    }
}
