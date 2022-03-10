//==========================================================
//      �{�X�̃Q�[�W�쐬
//      �쐬���@2022/03/10
//      �쐬�ҁ@���R����
//      
//      <�J������>
//      2022/03/10  �Q�[�W����
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HPgage : MonoBehaviour
{
    [SerializeField] int MAXHP = 0;
    int currentHp;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        currentHp = MAXHP;
        Debug.Log("Start currentHp : " + currentHp);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            currentHp = currentHp - 1;
            Debug.Log("After currentHp : " + currentHp);
        }
        slider.value = (float)currentHp / (float)MAXHP;
    }
}
