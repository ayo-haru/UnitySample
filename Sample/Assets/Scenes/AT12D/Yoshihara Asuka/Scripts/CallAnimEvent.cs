using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�j���[�V�����C�x���g����
/// </summary>
public class CallAnimEvent : MonoBehaviour
{
    //---�v���C���[�̂��Ă�R���|�[�l���g�擾
    GameObject player;
    Player2 player2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(GameData.Player.name);
        player2 = player.GetComponent<Player2>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �o�^����OnCreateShiled()�ōs���������L��
    /// </summary>
    //   public void OnCreateShiled()
    //{
    //       player2.Attack();
    //       Debug.Log("�A�j���[�V�����C�x���g�̂ق�");
    //}

    //   /// <summary>
    //   /// �A�j���[�V�����J�n���ɃA�^�b�N�t���O�𗧂Ă�
    //   /// </summary>
    //   public void StartAnim()
    //{
    //       player2.SetAttackFlg(true);
    //}

    /// <summary>
    /// �A�j���[�V�����I�����ɃA�^�b�N���t���O���낷
    /// </summary>
    public void SideShield()
    {
        player2.CreateShiled();
    }

    public void UnderShiled()
    {
        player2.CreateShiled();
    }

    public void OverShield()
    {
        player2.CreateShiled();
    }


}
