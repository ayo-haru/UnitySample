using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1KnifeThrow : MonoBehaviour
{
    Boss1Attack BossAttack;
    //----------------------------------------------------------
    //�i�C�t�����ϐ��Q
    //----------------------------------------------------------
    GameObject Knifeobj;                                    //�i�C�t�����p
    GameObject Knife;                                       //�i�C�t������i�[
    Vector3 KnifeStartPoint;                                //�i�C�t�̃X�^�[�g���W
    Vector3 KnifeEndPoint;                                  //�i�C�t�̏I���n�_
    Vector3 KnifePlayerPoint;
    float KnifeTime;
    [SerializeField] public float KnifeSpeed;               //�i�C�t�̑��x
    bool KnifeRefFlg = false;                               //�i�C�t���e���ꂽ���ǂ���
    float KnifeRefTime;
    Quaternion KnifeRotForward;                             //�i�C�t�̊p�x�ύX�p
    Quaternion KnifeRotDir;                                 //�i�C�t�̊p�x�ύX�p
    [SerializeField] Vector3 KnifeForward;                  //�i�C�t�̑O�ύX�p
    Vector3 KnifePos;
    [SerializeField] float DelayTime;                  //�i�C�t�̑O�ύX�p
    float DelayNowTime;

    [SerializeField] public float KnifeThrowTime;
    float KnifeThrowNowTime;
    GameObject KnifeAimObj;
    GameObject KnifeAim;
    Vector3 KnifeAimPos;
    Vector3 AimSize;
    Vector3 AimStartSize;
    Vector2 AimRand;
    bool AimFlg;
    bool AimOnly;
    bool AimStart;
    // Start is called before the first frame update
    void Start()
    {
        Knifeobj = (GameObject)Resources.Load("Knife");
        KnifeAimObj = (GameObject)Resources.Load("KnifeAim");
        BossAttack = this.GetComponent<Boss1Attack>();
        AimRand.x = (Random.Range(0, 2) * 2 - 1) * 3.0f;
        AimRand.y = (Random.Range(0, 2) * 2 - 1) * 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
            //���ꂼ��̏�������������
            if (Knife != null)
            {
                Destroy(Knife);
            }
        }
    }

    public void Boss1Knife()
    {
        if (!BossAttack.AnimFlg)
        {
            BossAttack.AnimFlagOnOff();
            BossAttack.BossAnim.SetBool("TakeToKnife", true);
        }
        //�i�C�t������

        if (AimStart)
        {
            if (!AimOnly)
            {

                KnifeAimPos.x = GameData.PlayerPos.x;
                KnifeAimPos.y = GameData.PlayerPos.y;
                KnifeAimPos.z = GameData.PlayerPos.z + 5.0f;

                KnifeAim = Instantiate(KnifeAimObj, KnifeAimPos, Quaternion.Euler(90f, 0f, 0f));
                AimSize = KnifeAim.transform.localScale;
                AimStartSize = AimSize;
                AimOnly = true;
            }
            BossAttack.BossAnim.speed = 0;
            KnifeAim.transform.position = KnifeAimPos;
            Debug.Log("�Ȃ񂾂Ă߂��I" + (KnifeThrowTime / 0.9f));
            if (KnifeThrowTime >= KnifeThrowNowTime)
            {
                KnifeThrowNowTime += 1.0f / 60.0f;
                if (KnifeThrowNowTime >= (KnifeThrowTime * 0.8f))
                {
                    //AimSize.x -= (1.0f / 60.0f) * AimStartSize.x;
                    //AimSize.z -= (1.0f / 60.0f) * AimStartSize.z;
                    //KnifeAim.transform.localScale = AimSize;
                    KnifeAimPos.x = GameData.PlayerPos.x;
                    KnifeAimPos.y = GameData.PlayerPos.y;
                    KnifeAimPos.z = GameData.PlayerPos.z + 5.0f;
                }
                else
                {
                    if ((int)(KnifeThrowNowTime * 60) % 11 == 10)
                    {
                        AimRand.x = (Random.Range(0, 2) * 2 - 1) * 0.5f;
                        AimRand.y = (Random.Range(0, 2) * 2 - 1)* 0.5f;
                    }
                    
                    KnifeAimPos.x = KnifeAimPos.x + AimRand.x;
                    KnifeAimPos.y = KnifeAimPos.y + AimRand.y;
                }
            }
            else
            {
                
                AimFlg = true;
                AimOnly = false;
                AimStart = false;
                KnifeThrowNowTime = 0;
            }
        }

        if (AimFlg)
        {
            
            if (!BossAttack.OnlyFlg)
            {
                KnifeAimPos.x = GameData.PlayerPos.x;
                KnifeAimPos.y = GameData.PlayerPos.y;
                KnifeAimPos.z = GameData.PlayerPos.z + 5.0f;
                BossAttack.OnlyFlg = true;
                GameObject.Find("knife(Clone)").transform.parent.DetachChildren();
                Vector3 KnifeDir = GameData.PlayerPos - Knife.transform.position;
                // �^�[�Q�b�g�̕����ւ̉�]
                if (BossAttack.RFChange)
                {
                    KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.forward);
                    KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                }
                if (!BossAttack.RFChange)
                {
                    KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
                    KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                }
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
            }
            if (BossAttack.RefrectFlg)
            {
                KnifeRefFlg = true;
                KnifePlayerPoint = KnifePos;
                Vector3 KnifeDir = Boss1Manager.Boss.gameObject.transform.position - Knife.transform.position;
                // �^�[�Q�b�g�̕����ւ̉�]
                KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
                KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
                BossAttack.RefrectFlg = false;
            }
            if (BossAttack.OnlyFlg && !KnifeRefFlg)
            {
                KnifeTime += Time.deltaTime * KnifeSpeed;
                Knife.transform.position = Vector3.Lerp(KnifeStartPoint, KnifeEndPoint, KnifeTime);
                KnifePos = GameObject.Find("knife(Clone)").transform.position;
                if (KnifeTime >= 1.0f)
                {
                    BossAttack.OnlyFlg = false;
                    AimFlg = false;
                    BossAttack.AnimFlagOnOff();
                    Debug.Log("A�p�^�[�����ʂ�����[");
                    BossAttack.BossAnim.SetBool("TakeToKnife", false);
                    KnifeTime = 0;
                    AimOnly = false;
                    AimStart = false;
                    Destroy(Knife);
                    Destroy(KnifeAim);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }

            if (KnifeRefFlg)
            {
                KnifeRefTime += Time.deltaTime * 3;
                Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss1Manager.BossPos, KnifeRefTime);
                Debug.Log("Knife " + KnifePlayerPoint);
                if (KnifeRefTime >= 1.0f)
                {
                    BossAttack.DamageColor.Invoke("Play", 0.0f);
                    EffectManager.Play(EffectData.eEFFECT.EF_BOSS_KNIFEDAMAGE, Boss1Manager.BossPos);
                    BossAttack.HpScript.DelHP(BossAttack.KnifeDamage);
                    BossAttack.OnlyFlg = false;
                    KnifeRefFlg = false;
                    AimFlg = false;
                    BossAttack.BossAnim.SetBool("??ToDamage", true);
                    BossAttack.BossAnim.Play("Damage");
                    BossAttack.BossAnim.SetBool("??ToDamage", false);
                    BossAttack.BossAnim.SetBool("TakeToKnife", false);
                    BossAttack.AnimFlagOnOff();
                    AimOnly = false;
                    AimStart = false;
                    KnifeTime = 0;
                    KnifeRefTime = 0;
                    Destroy(KnifeAim);
                    SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                    Destroy(Knife);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }
        }

    }
    void StartShotAnim()
    {
        AimStart = true;
        BossAttack.WeaponAttackFlg = false;
        KnifeStartPoint = GameObject.Find("KnifePos").transform.position;
        KnifeEndPoint = GameData.PlayerPos;
        KnifePos = GameObject.Find("KnifePos").transform.position;
        if (!BossAttack.RFChange)
        {
            Knife = Instantiate(Knifeobj, KnifePos, Quaternion.Euler(0, 0, 90));
        }
        if (BossAttack.RFChange)
        {
            Knife = Instantiate(Knifeobj, KnifePos, Quaternion.Euler(180, 0, -90));
        }
            Knife.transform.parent = GameObject.Find("KnifePos").transform;
        SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
    }
}
