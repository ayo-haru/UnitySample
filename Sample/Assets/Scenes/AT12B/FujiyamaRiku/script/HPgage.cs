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

public class HPgage : MonoBehaviour
{
    [SerializeField] int MAXHP = 0;     //�ő�HP���l�ύX��
    public static int currentHp;        //����HP
    //public Slider slider;               //�X���C�_�[
    //�Q�[�W�p�摜
    private Image HpGageImage;
    public static int m_DelHp ;         //�_���[�W���[�p�ϐ�
    public static int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        HpGageImage = gameObject.GetComponent<Image>();
        HpGageImage.fillAmount = 1.0f;
        currentHp = MAXHP;              //���݂�HP���ő�HP�ɂ���
        m_DelHp = 0;
        //Debug.Log("Start currentHp : " + slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        //�_���[�W���󂯂����[�V�����Đ��ケ������s�����ꂩ�C����
        if(Input.GetKeyDown(KeyCode.F1))
        {
           
                damage = 51;
                DelHP();
            Debug.Log("After currentHp : " + currentHp);
        }//�Q�[�W�̒���
        HpGageImage.fillAmount = (float)currentHp / MAXHP;
        if (currentHp <= 0)
        {
            //�{�X�̃^�O�t�����đS���̃{�X�Ŏg����悤�ɂ���������
            GameData.isAliveBoss1 = false;
        }
    }
    //�_���[�W�󂯂�������
    public static void DelHP()
    {
        m_DelHp = damage;                //�󂯂��_���[�W���󂯎��
        currentHp = currentHp - m_DelHp;               //���݂�HP����󂯂��_���[�W�����炷
        Debug.Log("delHP : " + m_DelHp);
        Debug.Log("m_damage : " + damage);
    }
    
}
