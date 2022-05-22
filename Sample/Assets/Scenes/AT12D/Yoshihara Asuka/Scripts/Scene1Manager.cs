//=============================================================================
//
// �e�V�[���̃f�[�^�Ǘ�[Scene1Manager]
//
// �쐬��:2022/03/11
// �쐬��:�g����
//
// <�J������>
// 2022/03/11
// 2022/03/22   GameData����
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public GameObject playerPrefab;                             // �v���C���[�̃v���n�u������
    public GameObject HPSystem;     
    //private int SceneNumber = (int)GameData.eSceneState.Kitchen1_SCENE;

    private void Awake()
    {

        Debug.Log("Awake");
        //---�t���[�����[�g�Œ�
        Application.targetFrameRate = 60;


        //---�v���C���[�v���n�u�̎擾
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;                     // GameData�̃v���C���[�Ɏ擾
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f); // �v���C���[�̍��W��ݒ�
        GameObject player = Instantiate(GameData.Player);       // �v���n�u�����̉�

        //---UI��\��
        GameObject canvas = GameObject.Find("Canvas");          // �V�[�����Canvas���Q�Ƃ�,canvas�ɒ�`
        var instance = Instantiate(HPSystem);                    // HPUI���擾
        instance.transform.SetParent(canvas.transform,false);   // canvas�̎q�I�u�W�F�N�g�ɃA�^�b�`

        //for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        //{
        //    SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        //}
        SoundManager.Play(SoundData.eBGM.BGM_TITLE, SoundData.GameAudioList);

        //Debug.Log(SceneNumber);
        Debug.Log("Awake");
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
    //    {
    //        SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
    //    }
    //    SoundManager.Play(SoundData.eBGM.BGM_TITLE,SoundData.GameAudioList);
    //}

    private void Start()
    {
        Debug.Log("start");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("log");

    }
}
