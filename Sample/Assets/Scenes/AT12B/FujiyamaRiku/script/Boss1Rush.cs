using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rush : MonoBehaviour
{
    Boss1Attack BossAttack;
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
    [SerializeField] public float RushSpeed;                //�ːi�̃X�s�[�h
    bool RushRefFlg = false;                                //�ːi���͂���������
    float RushTime;                                         //�ːi�̌o�ߎ���
    float RushRefTime;                                      //�e������̎��Ԍo��
    bool BossReturnFlg;
    float BossReturnTime;                                   //�ːi��߂�܂ł̎���
    bool RushEndFlg;
    float RushReturnSpeed;
    bool ReturnDelay;                                      //�߂낤�Ƃ���܂ł̎���
    Vector3 Scale;
    Vector3 oldScale;
    [SerializeField] public float RotateSpeed;
    Vector3 Rotate;
    // Start is called before the first frame update
    void Start()
    {
        Forkobj = (GameObject)Resources.Load("Fork");
        Scale = Boss1Manager.Boss.transform.localScale;
        BossAttack = this.GetComponent<Boss1Attack>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
            //���ꂼ��̏�������������
            if (Fork != null)
            {
                Destroy(Fork);
            }
            
        }
    }

    public void Boss1Fork()
    {
        //�A�j���[�V�����Đ�
        if (!BossAttack.AnimFlg)
        {
            BossAttack.AnimFlagOnOff();
            BossAttack.BossAnim.SetBool("IdleToTake", true);
            //BossTakeCase = BossMove.Boss_State.charge;
        }
        //���̏������I����Ă�����J�n

        if (BossAttack.OnlyFlg && BossAttack.MoveFlg)
        {
            //�{�X���ːi�I����ɕς��鏈��
            if (BossReturnFlg)
            {
                BossAttack.RefrectFlg = false;
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                //�Ō�܂ōU�����I����Ă�����
                if (RushEndFlg)
                {
                    Debug.Log("�����������������������������������������������I�I�I");
                    //�������ς���Ă���X�P�[�����𔽓]
                    Boss1Manager.Boss.transform.localScale = Scale;
                    //��]�̖ڕW�l
                    Quaternion target = new Quaternion();
                    //������ݒ�
                    target = Quaternion.LookRotation(Rotate);
                    //��������]������
                    Boss1Manager.Boss.transform.rotation = Quaternion.RotateTowards(Boss1Manager.Boss.transform.rotation, target, RotateSpeed);
                }

                //�r���Œe����Ă�����
                if (!RushEndFlg)
                {
                    Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, RushStartPoint, BossReturnTime);
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
                        if (!BossAttack.RFChange)
                        {
                            BossAttack.RFChange = true;
                        }
                        else if (BossAttack.RFChange)
                        {
                            BossAttack.RFChange = false;
                        }
                    }
                    ReturnDelay = false;
                    RushEndFlg = false;
                    BossReturnFlg = false;
                    BossAttack.AnimFlagOnOff();
                    BossAttack.BossAnim.SetBool("IdleToTake", false);
                    BossAttack.BossAnim.SetBool("RushToJump", false);
                    BossAttack.AnimMoveFlgOnOff();
                    BossReturnTime = 0;
                    BossAttack.BossAnim.speed = 1;
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        Debug.Log("�A�C�h�����I�I�I�I�I�I�I�I�I�I�I");
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    BossAttack.OnlyFlg = false;
                }
                return;
            }
            //�e���ꂽ���񂾂��������镔��
            if (BossAttack.RefrectFlg)
            {
                RushRefFlg = true;
                RushPlayerPoint = Boss1Manager.Boss.transform.position;
                BossAttack.BossAnim.SetBool("RushToJump", false);
                BossAttack.BossAnim.SetBool("Blow", true);
                BossAttack.RefrectFlg = false;
            }
            //�e����Ă��Ȃ������ꍇ�̏���
            if (!RushRefFlg)
            {
                RushTime += Time.deltaTime * RushSpeed;
                Boss1Manager.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);
                if (RushTime >= 1.0f)
                {
                    Scale.x *= -1;
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
                RushRefTime += Time.deltaTime * 2f;
                //�ǂɂԂ��Ă���悤�Ɍ�����
                Boss1Manager.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    Destroy(Fork);
                    RushEndFlg = false;
                    BossAttack.BossAnim.SetBool("Blow", false);
                    BossAttack.BossAnim.SetTrigger("WallHit");
                    BossAttack.BossAnim.Play("WallHit");
                    BossAttack.BossAnim.speed = 0.3f;
                    if (ReturnDelay)
                    {
                        RushReturnSpeed = 3f;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        BossAttack.BossAnim.SetBool("IdleToTake", false);
                        BossAttack.BossAnim.SetBool("RushToJump", false);
                        BossAttack.HpScript.DelHP(BossAttack.RushDamage);
                        RushTime = 0;
                        RushRefTime = 0;
                        SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        return;
                    }
                }
            }
        }

    }
    void ReturnGround()
    {
        ReturnDelay = true;
    }
    void BossRushAnim()
    {
        //�ːi�U�����n�߂邽�߂ɖ����񂾂��������镔��
        if (!BossAttack.OnlyFlg)
        {
            BossAttack.WeaponAttackFlg = false;
            //�E���獶
            if (!BossAttack.RFChange)
            {
                BossAttack.OnlyFlg = true;
                Debug.Log("Pos : " + Boss1Manager.BossPos);
                RushStartPoint = GameObject.Find("BossPoint").transform.position;
                RushEndPoint = GameObject.Find("LeftBossPoint").transform.position;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                Rotate.x = 1;
                BossAttack.BossAnim.SetTrigger("TakeToRushTr");
                BossAttack.BossAnim.SetBool("RushToJump", true);
                BossAttack.BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
            //������E
            else if (BossAttack.RFChange)
            {
                BossAttack.OnlyFlg = true;
                RushStartPoint = GameObject.Find("LeftBossPoint").transform.position;
                RushEndPoint = GameObject.Find("BossPoint").transform.position;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = GameObject.Find("LeftForkRefEndPoint").transform.position;
                Rotate.x = -1;
                BossAttack.BossAnim.SetTrigger("TakeToRushTr");
                BossAttack.BossAnim.SetBool("RushToJump", true);
                BossAttack.BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
        }
    }
}