using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanCake : MonoBehaviour
{
    //Boss1Attack BossAttack;
    //�ːi�p�ϐ��Q
    //----------------------------------------------------------
    GameObject Forkobj;                                     //�t�H�[�N�̃I�u�W�F�N�g�����p
    GameObject Fork;                                        //�t�H�[�N�̃I�u�W�F�N�g�i�[�p
    Vector3 RushStartPoint;                                 //�ːi�J�n�n�_
    Vector3 RushEndPoint;                                   //�ːi�I���n�_
    Vector3 RushPlayerPoint;                                //�ːi���͂������Ƃ��̃v���C���[���W�i�[�p
    Vector3 RushRefEndPoint;                                //�ːi���͂�������̓G�̍ŏI�n�_
    Vector3 RushMiddlePoint;                                //�ːi�U����߂��Ă��邽�߂̒��ԍ��W
    Vector3 ForkPos;
    bool OnlyRushFlg;                                       //������
    private float RushSpeed = 0.7f;                //�ːi�̃X�s�[�h
    bool RushRefFlg = false;                                //�ːi���͂���������
    float RushTime;                                         //�ːi�̌o�ߎ���
    float RushRefTime;                                      //�e������̎��Ԍo��
    bool BossReturnFlg = false;
    float BossReturnTime;                                   //�ːi��߂�܂ł̎���
    bool RushEndFlg;
    float RushReturnSpeed;
    bool ReturnDelay;                                      //�߂낤�Ƃ���܂ł̎���
    Vector3 oldScale;
    private float RotateSpeed = 5.0f;
    Vector3 Rotate;

    // Boss1Attack1�ɂ��������
    private bool AnimFlg;
    private bool MoveFlg;
    private Animator BossAnim;
    public static bool RefrectFlg;               //�v���C���[���p���B�ɐ����������ǂ����̎󂯎��p
    private Vector3 Scale;
    private bool RFChange;                                          //���E���] true = ������U�� false = �E����U��
    private bool WeaponAttackFlg;                            //����g���U���ύX true = �J�U�� false = �ːior�i�C�t



    private bool onceUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        RushStartPoint = new Vector3(45.2f, 23.0f, 0.0f);
        RushEndPoint = new Vector3(-81.2f, 23.0f, 0.0f);

        this.Forkobj = (GameObject)Resources.Load("TutorialFork");
        //BossAttack = this.GetComponent<Boss1Attack>();
        this.Scale = this.transform.localScale;

        this.BossAnim = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!onceUpdate)
        {
            TutorialPanCakeMove.SetState(TutorialPanCakeMove.Boss_State.charge);
            MoveFlg = true;
            onceUpdate = true;
        }
            ForkAction();
    }

    private void ForkAction() {
        //�A�j���[�V�����Đ�
        if (!this.AnimFlg)
        {
            AnimFlagOnOff();
            BossAnim.SetBool("IdleToTake", true);
            //BossTakeCase = BossMove.Boss_State.charge;
        }
        //���̏������I����Ă�����J�n

        if (this.MoveFlg)
        {
            //�{�X���ːi�I����ɕς��鏈��
            if (BossReturnFlg)
            {
                RefrectFlg = false;
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                //�Ō�܂ōU�����I����Ă�����
                if (RushEndFlg)
                {
                    //�������ς���Ă���X�P�[�����𔽓]
                    this.transform.localScale = this.Scale;
                    //��]�̖ڕW�l
                    Quaternion target = new Quaternion();
                    //������ݒ�
                    target = Quaternion.LookRotation(Rotate);
                    //��������]������
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target, RotateSpeed);
                }

                //�r���Œe����Ă�����
                if (!RushEndFlg)
                {
                    this.transform.position = Vector3.Lerp(RushRefEndPoint, RushStartPoint, BossReturnTime);
                }
                //�J�n�n�_�܂Ŗ߂��Ă����Ƃ��ɂ�����돉����
                if (BossReturnTime >= 1.0f)
                {
                    if (Fork != null)
                    {
                        Destroy(Fork);
                    }
                    if (RushEndFlg)
                    {
                        if (!this.RFChange)
                        {
                           this.RFChange = true;
                        }
                        else if (this.RFChange)
                        {
                            this.RFChange = false;
                        }
                    }
                    ReturnDelay = false;
                    RushEndFlg = false;
                    BossReturnFlg = false;
                    this.AnimFlagOnOff();
                    this.BossAnim.SetBool("IdleToTake", false);
                    this.BossAnim.SetBool("RushToJump", false);
                    //this.AnimMoveFlgOnOff();
                    MoveFlg = false;
                    BossReturnTime = 0;
                    this.BossAnim.speed = 1;
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    //BossAttack.OnlyFlg = false;
                }
                return;
            }
            //�e���ꂽ���񂾂��������镔��
            if (RefrectFlg)
            {
                RushRefFlg = true;
                RushPlayerPoint = this.transform.position;
                this.BossAnim.SetBool("RushToJump", false);
                this.BossAnim.SetBool("Blow", true);
                RefrectFlg = false;
            }
            //�e����Ă��Ȃ������ꍇ�̏���
            if (!RushRefFlg)
            {
                Debug.Log("<color=blue>�V���v���ړ�</color>");
                RushTime += Time.deltaTime * RushSpeed;
                this.transform.position = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    this.Scale.x *= -1;
                    RushReturnSpeed = 1f;
                    RushEndFlg = true;
                    BossReturnFlg = true;
                    RushTime = 0;
                    return;
                }
            }
            //�e����Ă����ꍇ�̏���
            if (RushRefFlg)
            {
                Debug.Log("<color=blue>�͂����ꂽ</color>");
                RushRefTime += Time.deltaTime * 2f;
                //�ǂɂԂ��Ă���悤�Ɍ�����
                this.transform.position = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    Destroy(Fork);
                    RushEndFlg = false;
                    this.BossAnim.SetBool("Blow", false);
                    this.BossAnim.SetTrigger("WallHit");
                    this.BossAnim.Play("WallHit");
                    this.BossAnim.speed = 0.3f;
                    if (ReturnDelay)
                    {
                        RushReturnSpeed = 3f;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        this.BossAnim.SetBool("IdleToTake", false);
                        this.BossAnim.SetBool("RushToJump", false);
                        RushTime = 0;
                        RushRefTime = 0;
                        SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        return;
                    }
                }
            }
        }
    }


    public void AnimFlagOnOff() {
        if (!AnimFlg)
        {
            AnimFlg = true;
            return;
        }
        if (AnimFlg)
        {
            AnimFlg = false;
            return;
        }
    }

    public void AnimMoveFlgOnOff() {
        //if (!MoveFlg)
        //{
        //    MoveFlg = true;
        //    return;
        //}
        //if (MoveFlg)
        //{
        //    MoveFlg = false;
        //    return;
        //}
    }

    void ReturnGround() {
        ReturnDelay = true;
    }
    void BossRushAnim() {
        //�ːi�U�����n�߂邽�߂ɖ����񂾂��������镔��
        //if (!BossAttack.OnlyFlg)
        //{
            this.WeaponAttackFlg = false;
            //�E���獶
            if (!this.RFChange)
            {
                //BossAttack.OnlyFlg = true;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = new Vector3(45.2f, 60.0f, 0.0f);
            Rotate.x = 1;
                this.BossAnim.SetTrigger("TakeToRushTr");
                this.BossAnim.SetBool("RushToJump", true);
                this.BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
        }
    //}

}
