using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenSceneMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        float dz = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.position = new Vector3(transform.position.x + dx, 0.5f, transform.position.z + dz);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Kitchen001")
        {
            SceneManager.LoadScene("Kitchen001");
            this.transform.position = new Vector3(20.0f, 11.5f, -1.0f);
        }
    }
}
