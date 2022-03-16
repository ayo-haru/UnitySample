using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attack : MonoBehaviour
{
    public enum BossAttack
    {
        Attack1 = 0,
        Attack2,
        Attack3,
        Attack4,
    }
    private BossAttack Boss1AttackState = BossAttack.Attack1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Boss1AttackState)
        {
            case BossAttack.Attack1:
                {
                    break;
                }
            case BossAttack.Attack2:
                {
                    break;
                }
            case BossAttack.Attack3:
                {
                    break;
                }
            case BossAttack.Attack4:
                {
                    break;
                }
        }
    }

}
