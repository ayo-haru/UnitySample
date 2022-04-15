using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySceneManager : MonoBehaviour
{
    public GameObject Player;

    private void Awake()
    {
        //---�t���[�����[�g�Œ�
        Application.targetFrameRate = 60;

        //---�v���C���[�v���n�u�̎擾
        if (!GameData.Player)
        {
            GameData.Player = Player;   // GameData�̃v���C���[�Ɏ擾
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(-80.0f, 20.5f, 0.0f); // �v���C���[�̍��W��ݒ�
        GameObject player = Instantiate(GameData.Player);       // �v���n�u�����̉�

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
