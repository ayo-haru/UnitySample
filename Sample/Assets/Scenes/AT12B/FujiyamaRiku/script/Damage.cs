//==========================================================
//      �{�X�ւ̃_���[�W
//      �쐬���@2022/03/11
//      �쐬�ҁ@���R����
//      
//      <�J������>
//      2022/03/11  �_���[�W�v�Z
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   
    [SerializeField]  private int ShortRangeAttack = 0; //�ߋ����U���̃_���[�W

    static public int  damage;                            //�_���[�W�i�[�p
    

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        //SucRefrect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            damage = ShortRangeAttack;
            Debug.Log("damage : " + damage);
        }

    }
}
