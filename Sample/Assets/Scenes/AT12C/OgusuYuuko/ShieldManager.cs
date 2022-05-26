//=============================================================================
//
// �V�[���h���Ǘ�����N���X
//
// �쐬��:2022/03/39
// �쐬��:����T�q
//
// <�J������>
// 2022/03/29   �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public int now_Quantity = 0;   //���݂̏��̌�
    public int max_Quantity = 1;   //���̍ő吔

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddShield()
    {
        if (Player.isHitSavePoint)
        {
            return false;
        }
        //���̐��X�V
        ++now_Quantity;
        Debug.Log("���o����" + now_Quantity);
        //�ő吔�𒴂��Ă�����
        if(now_Quantity > max_Quantity)
        {
            return false;
        }
        return true;

    }

    public void DestroyShield()
    {
        //���̐��X�V
        --now_Quantity;
        Debug.Log("��������" + now_Quantity);
    }

}
