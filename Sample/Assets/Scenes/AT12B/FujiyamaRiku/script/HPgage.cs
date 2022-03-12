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
    [SerializeField] int MAXHP = 0;     //�ő�HP���l�ύX��
    int currentHp;                      //����HP
    public Slider slider;               //�X���C�_�[
    int m_DelHp ;                      //�_���[�W���[�p�ϐ�
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;               //�X���C�_�[�̍ő�l
        currentHp = MAXHP;              //���݂�HP���ő�HP�ɂ���
        m_DelHp = 0;
        Debug.Log("Start currentHp : " + currentHp);
    }

    // Update is called once per frame
    void Update()
    {
        //�_���[�W���󂯂����[�V�����Đ��ケ������s�����ꂩ�C����
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DelHP();
            Debug.Log("After currentHp : " + currentHp);
        }
        slider.value = (float)currentHp / (float)MAXHP;  //�X���C�_�[�̒����̌v�Z
    }
    //�_���[�W�󂯂�������
    private void DelHP()
    {
        m_DelHp = Damage.damage;                //�󂯂��_���[�W���󂯎��
        currentHp = currentHp - m_DelHp;               //���݂�HP����󂯂��_���[�W�����炷
        Debug.Log("delHP : " + m_DelHp);
        Debug.Log("m_damage : " + Damage.damage);
    }
}
