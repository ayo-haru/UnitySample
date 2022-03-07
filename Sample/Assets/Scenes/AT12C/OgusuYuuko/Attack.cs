using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //コピー元のオブジェクト
    GameObject prefab;

    int nDir;   //方向
    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Weapon");
        nDir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //右
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nDir = 1;
        }
        //左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nDir = -1;
        }

        //上攻撃
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject weapon = Instantiate(prefab, transform.position + transform.up * 1.0f, Quaternion.identity);
            weapon.transform.Rotate(new Vector3(0, 0, 90));
            Destroy(weapon, 0.1f);
            return;
        }
        //下攻撃
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject weapon = Instantiate(prefab, transform.position + transform.up * -1.0f, Quaternion.identity);
            weapon.transform.Rotate(new Vector3(0, 0, 90));
            Destroy(weapon, 0.1f);
            return;
        }
        //左攻撃
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject weapon = Instantiate(prefab, transform.position + transform.right * -1.0f, Quaternion.identity);
            Destroy(weapon, 0.1f);
        }

        //右攻撃
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject weapon = Instantiate(prefab, transform.position + transform.right * 1.0f, Quaternion.identity);
            Destroy(weapon, 0.1f);
        }

    }
}
