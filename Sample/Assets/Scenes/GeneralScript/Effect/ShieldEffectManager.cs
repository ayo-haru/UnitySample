using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffectManager : MonoBehaviour
{
    private ParticleSystem ShileEffect;
    private GameObject Player;
    private Player2 player2;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.Find(GameData.Player.name);
        player2 = Player.GetComponent<Player2>();

        //---盾エフェクト再生
        //ShileEffect = Instantiate(EffectData.eEFFECT.EF_SHIELD);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
