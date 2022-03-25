//=============================================================================
//
// �V�[���}�l�[�W���[
//
// �쐬��:2022/03/11
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/11 �쐬
// 2022/03/13 �V�[���J�ڂ���
// 2022/03/13 �v���C���[����ɂ���
// 2022/03/13 �V�[���J�ڂ��y�ɂ����͂�
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoScene1Manager : MonoBehaviour {
    public int roomSize = 50;           // �����̑傫���B�t�F�[�h�Ɏg�p
    public GameObject playerPrefab;     // �v���C���[�̃v���n�u������Ƃ���

    //[System.NonSerialized]
    //public AudioSource[] audioSourceList = new AudioSource[5];    // ���ɓ����ɂȂ点�鐔
    private GameObject GardenImage;                                // �J�n���o�ŏo���摜
    private bool isCalledOnce = false;                             // �J�n���o�Ŏg�p�B��񂾂����������邽�߂Ɏg���B


    // Start is called before the first frame update
    void Awake() {
        Application.targetFrameRate = 60;           // �t���[�����[�g���Œ�

        GameData.SetroomSize(roomSize);             // �����̃T�C�Y���Z�b�g

        //----- �v���C���[������ -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        GameObject player = Instantiate(GameData.Player);

        //----- �}�b�v�̔ԍ���ۑ� -----
        GameData.NextMapNumber = GameData.CurrentMapNumber = (int)GameData.eSceneState.MAP1_SCENE;
        SaveManager.saveLastMapNumber(GameData.CurrentMapNumber);

        // audioSourceList�z��̐�����AudioSource�����g�ɐ������Ĕz��Ɋi�[
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }

        //----- �J�n���o -----
        GardenImage = GameObject.Find("Garden");
    }

    // Update is called once per frame
    void Update() {
        if (!isCalledOnce)     // ��񂾂��Ă�
        {
            GardenImage.GetComponent<ImageShow>().Show(2);
            isCalledOnce = true;
        }


        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // �ۑ����Ă���V�[���ԍ������݂Ǝ����قȂ�����V�[���ړ�
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}