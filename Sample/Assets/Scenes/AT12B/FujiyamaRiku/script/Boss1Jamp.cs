using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Jamp : MonoBehaviour
{
    Boss1Attack BossAttack;
    float JumpTime;
    [SerializeField] public float Speed;
    Vector3 JumpStartPoint;
    Vector3 JumpMiddlePoint;
    Vector3 JumpEndPoint;
    Vector3 Scale;
    Vector3 Rotate;

    // Start is called before the first frame update
    void Start()
    {
        BossAttack = this.GetComponent<Boss1Attack>();
        Scale = Boss1Manager.Boss.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossJamp()
    {
        if (!BossAttack.AnimFlg)
        {
            BossAttack.AnimFlagOnOff();
            BossAttack.BossAnim.Play("Jump");
        }
        if (!BossAttack.OnlyFlg)
        {
            Scale.x *= -1;
            if (!BossAttack.RFChange)
            {
                BossAttack.OnlyFlg = true;
                JumpStartPoint = GameObject.Find("BossPoint").transform.position;
                JumpMiddlePoint = GameObject.Find("RushMiddle").transform.position;
                JumpEndPoint = GameObject.Find("LeftBossPoint").transform.position;
                Rotate.x = 1;
            }
            else if (BossAttack.RFChange)
            {
                BossAttack.OnlyFlg = true;
                JumpStartPoint = GameObject.Find("LeftBossPoint").transform.position;
                JumpMiddlePoint = GameObject.Find("RushMiddle").transform.position;
                JumpEndPoint = GameObject.Find("BossPoint").transform.position;
                Rotate.x = -1;
            }

        }
        Debug.Log("‚’|‚Ä[‚µ‚å‚ñ" + Scale);
        Boss1Manager.Boss.transform.localScale = Scale;
        //‰ñ“]‚Ì–Ú•W’l
        Quaternion target = new Quaternion();
        //Œü‚«‚ðÝ’è
        target = Quaternion.LookRotation(Rotate);
        //‚ä‚Á‚­‚è‰ñ“]‚³‚¹‚é
        Boss1Manager.Boss.transform.rotation = Quaternion.RotateTowards(Boss1Manager.Boss.transform.rotation, target, Speed);

        JumpTime += Time.deltaTime;
        Boss1Manager.BossPos = Beziercurve.SecondCurve(JumpStartPoint,JumpMiddlePoint,JumpEndPoint, JumpTime);
        if (JumpTime >=1.0f)
        {
            if (!BossAttack.RFChange)
            {
                BossAttack.RFChange = true;
            }
            else if (BossAttack.RFChange)
            {
                BossAttack.RFChange = false;
            }
            BossAttack.OnlyFlg = false;
            BossAttack.AnimFlagOnOff();
            JumpTime = 0;
            BossMove.SetState(BossMove.Boss_State.idle);
        }

    }
}
