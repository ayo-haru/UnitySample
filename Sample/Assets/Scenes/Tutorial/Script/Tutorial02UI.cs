using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial02UI : MonoBehaviour {
    [SerializeField]
    private GameObject characterback;
    private GameObject CharacterBack;
    [SerializeField]
    private GameObject chara2_1;
    private GameObject Chara2_1;
    [SerializeField]
    private GameObject chara2_2;
    private GameObject Chara2_2;
    [SerializeField]
    private GameObject chara2_3;
    private GameObject Chara2_3;
    [SerializeField]
    private GameObject chara2_4;
    private GameObject Chara2_4;

    private Canvas canvas;  // �\������L�����o�X

    private int UIcnt;  // UI�\����


    // Start is called before the first frame update
    void Start() {
        UIcnt = 0;

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();

        //----- UI������ -----
        // ���ԉ�
        CharacterBack = Instantiate(characterback);
        Chara2_1 = Instantiate(chara2_1);
        Chara2_2 = Instantiate(chara2_2);
        Chara2_3 = Instantiate(chara2_3);
        Chara2_4 = Instantiate(chara2_4);

        // �L�����o�X�̎q�Ɏw��
        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara2_1.transform.SetParent(this.canvas.transform, false);
        Chara2_2.transform.SetParent(this.canvas.transform, false);
        Chara2_3.transform.SetParent(this.canvas.transform, false);
        Chara2_4.transform.SetParent(this.canvas.transform, false);

        // �����\�����Ȃ�
        CharacterBack.GetComponent<ImageShow>().Hide();
        Chara2_1.GetComponent<ImageShow>().Hide();
        Chara2_2.GetComponent<ImageShow>().Hide();
        Chara2_3.GetComponent<ImageShow>().Hide();
        Chara2_4.GetComponent<ImageShow>().Hide();

    }

    // Update is called once per frame
    void Update() {
        if (GameData.PlayerPos.x >= -80.0f && UIcnt < 3)
        {

        }
    }
}