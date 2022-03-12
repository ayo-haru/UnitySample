//=============================================================================
//
// �V�[���}�l�[�W���[
//
// �쐬��:2022/03/11
// �쐬��:����T�q
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

public class ProtoScene2Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Awake() {
        //----- �v���C���[������ -----
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;
        }

        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f);
        GameObject player = Instantiate(GameData.Player);

        //----- �}�b�v�̔ԍ���ۑ� -----
        GameData.NextMapNumber =  GameData.CurrentMapNumber = (int)GameData.SceneState.MAP2_SCENE;
    }

    // Update is called once per frame
    void Update() {
        if (GameData.CurrentMapNumber != GameData.NextMapNumber)    // �ۑ����Ă���V�[���ԍ������݂Ǝ����قȂ�����V�[���ړ�
        {
            string nextSceneName = GameData.GetNextScene(GameData.NextMapNumber);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
