//=============================================================================
//
// �U��
//
// �쐬��:2022/03
// �쐬��:����T�q
//
// <�J������>
// 2022/03    �쐬
// 2022/03/12 ���f���̌����Ɋ֌W�Ȃ����͕����ɏ����o��悤�ɕύX
// 2022/03/16 ���˔��X���C�h����悤�ɂ���
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //�V�[���h�ʒu�㉺
    public float AttckPosHeight = 2.0f;
    //�V�[���h�ʒu���E
    public float AttckPosWidth = 2.0f;
    //�V�[���h�L������
    public float DestroyTime = 0.1f;

    //�R�s�[���̃I�u�W�F�N�g
    GameObject prefab;

    //�v���C���[�̍��W
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̍��W�擾
        pos = transform.position;

        //��U��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x,pos.y + AttckPosHeight, pos.z), Quaternion.identity);
            weapon.transform.Rotate(new Vector3(0, 0, 90));
            Destroy(weapon, DestroyTime);
            return;
        }
        //���U��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x, pos.y - AttckPosHeight, pos.z), Quaternion.identity);
            weapon.transform.Rotate(new Vector3(0, 0, 90));
           Destroy(weapon, DestroyTime);
            return;
        }
        //���U��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x - AttckPosWidth, pos.y, pos.z), Quaternion.identity);
           Destroy(weapon, DestroyTime);
            return;
        }

        //�E�U��
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x + AttckPosWidth, pos.y, pos.z), Quaternion.identity);
            Destroy(weapon, DestroyTime);
            return;
        }
    }
}
