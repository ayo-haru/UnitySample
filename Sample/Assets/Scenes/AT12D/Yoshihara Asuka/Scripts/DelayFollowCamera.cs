using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayFollowCamera : MonoBehaviour
{
    //---�ϐ��錾
    private GameObject Player;
    private Vector3 OffSet;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("SD_unitychan_humanoid");          // �Ǐ]����I�u�W�F�N�g��ݒ�
        OffSet = transform.position - Player.transform.position;    // �v���C���[�̍��W�������I�t�Z�b�g��������

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(Player.transform.position,
                                          Player.transform.position + OffSet,
                                          16.0f * Time.deltaTime);
    }
}
