using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSound : MonoBehaviour
{
    private void Start()
    {
        //�{�����[���ݒ�
        gameObject.GetComponent<AudioSource>().volume = SoundManager.bgmVolume;

        //�V�[���؂�ւ���Ă��������p������
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //�{�X�V�[���ƃ^�C�g���V�[���͂�������؂�ւ��邽�߃I�u�W�F�N�g��j������
        if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE || GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE)
        {
            Destroy(gameObject);
        }
    }
}
