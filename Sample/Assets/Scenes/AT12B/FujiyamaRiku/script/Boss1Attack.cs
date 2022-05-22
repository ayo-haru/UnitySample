using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Boss1Attack : MonoBehaviour
{
    public Boss1Rush BossRush;
    public Boss1StrawBerry BossStrawberry;
    public Boss1KnifeThrow BossKnife;
    public Boss1Rain BossRain;
    public Boss1Jamp BossJump;
    //�{�X�̍U���̎��
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Attack4,
        Idle,
    }
    [Header("�ːi�U���̃_���[�W")]
    [SerializeField] public int RushDamage;                 //�ːi�U���̃_���[�W
    [SerializeField] public int StrawberryDamage;           //�C�`�S�U���̃_���[�W
    [SerializeField] public int KnifeDamage;                //�i�C�t�U���̃_���[�W
    [SerializeField] public float RefrectRotOver;           //�e�����p�x�̏㔻��p
    [SerializeField] public float RefrectRotUnder;          //�e�����p�x�̏㔻��p
    [System.NonSerialized] public bool RefrectFlg = false;                  //�v���C���[���p���B�ɐ����������ǂ����̎󂯎��p
    [System.NonSerialized] public bool OnlyFlg;                             //���ꂼ��̏����̈�����̏����p
    [System.NonSerialized]  public bool LRSwitchFlg;
    static public Vector3 BossStartPoint;                   //�{�X�̏����n�_
    [System.NonSerialized] public bool AnimFlg;
    [System.NonSerialized] public bool MoveFlg;
    private GameObject HpObject;
    public HPgage HpScript;
    public Animator BossAnim;
    [System.NonSerialized]  public Vector3 Scale;
    [System.NonSerialized] public bool WeaponAttackFlg;                            //����g���U���ύX true = �J�U�� false = �ːior�i�C�t
    [System.NonSerialized] public bool RFChange;                                          //���E���] true = ������U�� false = �E����U��
    BossMove.Boss_State BossTakeCase;
    [System.NonSerialized] public bool JampFlg;
    //�������邩�킩��Ȃ����E����p
    [System.NonSerialized] public int RFNum;
    GameObject Forkobj;                                     //�t�H�[�N�̃I�u�W�F�N�g�����p
    GameObject Knifeobj;                                    //�i�C�t�����p
    
    // Start is called before the first frame update
    void Start()
    {
        BossRush = this.GetComponent<Boss1Rush>();
        BossStrawberry = this.GetComponent<Boss1StrawBerry>();
        BossKnife = this.GetComponent<Boss1KnifeThrow>();
        BossRain = this.GetComponent<Boss1Rain>();
        BossJump = this.GetComponent<Boss1Jamp>();
        Scale = Boss1Manager.Boss.transform.localScale;
        HpObject = GameObject.Find("HPGage");
        HpScript = HpObject.GetComponent<HPgage>();
        BossAnim = this.gameObject.GetComponent<Animator>();
        
        OnlyFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Pause.isPause)
        {
            BossAnim.speed = 0.0f;  // �A�j���[�V�����|�[�Y
        }
        else
        {
            /*
             �G�ɃA�j���[�V�����ĊJ
             */
            BossAnim.speed = 1.0f;
        }

        //�{�X�����񂾂珈������߂�
        if (!GameData.isAliveBoss1)
        {
            //���ꂼ��̏�������������
            BossAnim.speed = 0f;
            //�{�X��|�����������N�����ʂɈړ�
            Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_END;
        }
    }
    //���ꂼ��̍U������
    public void AnimFlagOnOff()
    {
        if (!AnimFlg)
        {
            AnimFlg = true;
            return;
        }
        if(AnimFlg)
        {
            AnimFlg = false;
            return;
        }
        
    }
    public void AnimMoveFlgOnOff()
    {
        if (!MoveFlg)
        {
            MoveFlg = true;
            return;
        }
        if (MoveFlg)
        {
            MoveFlg = false;
            return;
        }
    }
   
    
}
