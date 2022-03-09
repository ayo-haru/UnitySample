//=============================================================================
//
// �v���C���[�̒Ǐ]�J����(�x��)[DelayFollowCamera]
//
// <Refelence>
// �����x��ĒǏ]����J�����ݒ�B����MainCamera�̒l�œ����̂ł���Ɉˑ��B
// ����g�����킩��Ȃ�����A�ꉞ�����Ƃ��܂��B
//
// �쐬��:2022/03/08
// �쐬��:�g����
//
// <�J������>
// 2022/03/08 �쐬
//=============================================================================

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
        transform.position = Vector3.Lerp(transform.position,
                                          Player.transform.position + OffSet,
                                          16.0f * Time.deltaTime);
    }
}
