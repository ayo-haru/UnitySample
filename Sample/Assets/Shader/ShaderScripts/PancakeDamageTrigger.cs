using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeDamageTrigger : MonoBehaviour
{
    private PancakeDamage _pancakeDamage;
    // Start is called before the first frame update
    void Start()
    {
        _pancakeDamage = GetComponent<PancakeDamage>();

        _pancakeDamage.Invoke("Play",0.0f);
    }

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Escape))
		//{
  //          _pancakeDamage.Invoke("Play", 0.0f);
  //      }

  //      if (Input.GetKeyDown(KeyCode.Return)){
  //          _pancakeDamage.Invoke("Stop", 0.0f);
  //      }

    }
}
