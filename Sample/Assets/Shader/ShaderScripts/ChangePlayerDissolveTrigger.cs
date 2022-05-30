using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerDissolveTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<ChangePlayerDissolve>().Invoke("Play",0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
