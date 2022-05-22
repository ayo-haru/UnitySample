using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTrigger : MonoBehaviour
{
    private BossWeapon _bossWeapon;
    // Start is called before the first frame update
    void Start()
    {
        _bossWeapon = this.GetComponent<BossWeapon>();
        _bossWeapon.Invoke("Play",0.1f);
    }
}
