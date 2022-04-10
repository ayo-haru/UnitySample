//=============================================================================
//
// HP�Q�[�W
//
// �쐬��:2022/04/10
// �쐬��:����T�q
//
// <�J������>
// 2022/04/10 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGage : MonoBehaviour
{
    //�Q�[�W�p�摜
    private Image HpGageImage;
    //�ő�HP
    public int MaxHP = 100;
    //���݂�HP
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g�擾
        HpGageImage = gameObject.GetComponent<Image>();
        HpGageImage.fillAmount = 1.0f;
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[�W�̒���
        HpGageImage.fillAmount = (float)currentHP / MaxHP;
    }

    //HP�������������ɂ�����Ă�
    //���� : ���݂�HP
    public void HpGageDel(int nHP)
    {
        currentHP = nHP;
    }
}
