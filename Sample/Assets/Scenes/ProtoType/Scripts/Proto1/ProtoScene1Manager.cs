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
    public int roomSize = 50;
    public GameObject playerPrefab;

    private AudioSource[] audioSourceList = new AudioSource[5];    // ���ɓ����ɂȂ点�鐔

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
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // �ۑ����Ă���V�[���ԍ������݂Ǝ����قȂ�����V�[���ړ�
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }

        if (Input.GetKey(KeyCode.U))
        {
            SoundManager.Play(SoundData.eSE.SE_CLICK, audioSourceList);
        }
        if (Input.GetKey(KeyCode.I))
        {
            SoundManager.Play(SoundData.eSE.SE_DORA, audioSourceList);
        }
        if (Input.GetKey(KeyCode.O))
        {
            SoundManager.Play(SoundData.eSE.SE_BYON, audioSourceList);
        }
        if (Input.GetKey(KeyCode.P))
        {
            SoundManager.Play(SoundData.eSE.SE_SPON, audioSourceList);
        }


    }
}