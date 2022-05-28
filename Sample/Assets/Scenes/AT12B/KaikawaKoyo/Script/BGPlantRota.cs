//==========================================================
//      プラントまわすよ
//      作成日　2022/05/27
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/05/27      
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGPlantRota : MonoBehaviour
{
    [SerializeField]
    private bool TurnRight;

    [SerializeField]
    private float TurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPause)
        {
            if (TurnRight)
            {
                transform.Rotate(new Vector3(0.0f, TurnSpeed, 0.0f), Space.Self);
            }
            if(!TurnRight)
            {
                transform.Rotate(new Vector3(0.0f, -TurnSpeed, 0.0f), Space.Self);
            }
        }
    }
}
