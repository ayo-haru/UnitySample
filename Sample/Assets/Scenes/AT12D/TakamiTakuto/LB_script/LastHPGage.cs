//==========================================================
//      �{�X�̃Q�[�W�쐬
//      �쐬���@2022/03/10
//      �쐬�ҁ@���R����
//      
//      <�J������>
//      2022/03/10  �Q�[�W����
//      2022/03/15  HP���[���ɂȂ����Ƃ��{�X��Flg��false��
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastHPGage : MonoBehaviour
{
    int MAXHP = 100;     //�ő�HP���l�ύX��
    public static int currentHp=100;        //����HP
    //public Slider slider;             //�X���C�_�[
    //�Q�[�W�p�摜
    private Image BossHpBar;
    private int m_DelHp;         //�_���[�W���[�p�ϐ�
    private int damage = 0;
    private float DamageTimer;
    private int HpDelNow;

    // Start is called before the first frame update
    void Start()
    {
        BossHpBar = gameObject.GetComponent<Image>();
        BossHpBar.fillAmount = 1.0f;
        currentHp = MAXHP;              //���݂�HP���ő�HP�ɂ���
        m_DelHp = 0;
        //Debug.Log("Start currentHp : " + slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        //�_���[�W���󂯂����[�V�����Đ��ケ������s�����ꂩ�C����
        //�Q�[�W�̒���
        if (DamageTimer >= 0.0f && HpDelNow > 0)
        {
            DamageTimer -= 1.0f;
            currentHp -= 1;
            HpDelNow -= 1;
        }
        BossHpBar.fillAmount = (float)currentHp / MAXHP;
        if (currentHp <= 0)
        {
            //�{�X�̃^�O�t�����đS���̃{�X�Ŏg����悤�ɂ���������
            //GameData.isAliveBoss1 = false;
        }
    }
    //�_���[�W�󂯂�������
    public void DelHP(int Damage)
    {
        HpDelNow = Damage;                //�󂯂��_���[�W���󂯎��
        DamageTimer = Damage;             //���݂�HP����󂯂��_���[�W�����炷
        Debug.Log("delHP : " + Damage);
        Debug.Log("m_damage : " + Damage);

    }

}
