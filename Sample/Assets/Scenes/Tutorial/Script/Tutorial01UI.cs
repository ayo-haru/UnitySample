using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial01UI : MonoBehaviour
{
    [SerializeField]
    private GameObject characterback;
    private GameObject CharacterBack;
    [SerializeField]
    private GameObject lmove;
    private GameObject Lmove;
    [SerializeField]
    private GameObject rshield;
    private GameObject RShield;
    [SerializeField]
    private GameObject rjump;
    private GameObject RJump;
    [SerializeField]
    private GameObject goahead;
    private GameObject GoAhead;
    Canvas canvas;

    private int UIcnt;

    // Start is called before the first frame update
    void Start()
    {
        UIcnt = 0;

        // キャンバスを指定
        canvas = GetComponent<Canvas>();
        // 実態化
        CharacterBack = Instantiate(characterback);
        Lmove = Instantiate(lmove);
        RShield = Instantiate(rshield);
        RJump = Instantiate(rjump);
        GoAhead = Instantiate(goahead);

        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Lmove.transform.SetParent(this.canvas.transform, false);
        RShield.transform.SetParent(this.canvas.transform, false);
        RJump.transform.SetParent(this.canvas.transform, false);
        GoAhead.transform.SetParent(this.canvas.transform, false);

        CharacterBack.GetComponent<ImageShow>().Hide();
        Lmove.GetComponent<ImageShow>().Hide();
        RShield.GetComponent<ImageShow>().Hide();
        RJump.GetComponent<ImageShow>().Hide();
        GoAhead.GetComponent<ImageShow>().Hide();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.PlayerPos.x >= -200.0f && UIcnt == 0)
        {
            CharacterBack.GetComponent<ImageShow>().Show();
            Lmove.GetComponent<ImageShow>().Show(120);
            UIcnt++;
        }
        else if (GameData.PlayerPos.x >= 40.0f && UIcnt == 1)
        {
            Lmove.GetComponent<ImageShow>().Clear();
            RShield.GetComponent<ImageShow>().Show(120);
            UIcnt++;
        }
        else if (GameData.PlayerPos.x >= 220.0f && UIcnt == 2)
        {
            RShield.GetComponent<ImageShow>().Clear();
            RJump.GetComponent<ImageShow>().Show(120);
            UIcnt++;
        }
        else if (GameData.PlayerPos.x >= 320.0f && UIcnt == 3)
        {
            RJump.GetComponent<ImageShow>().Clear();
            GoAhead.GetComponent<ImageShow>().Show(120);
            UIcnt++;
        }
        else if(GameData.PlayerPos.x >= 400.0f && UIcnt == 4)
        {
            GoAhead.GetComponent<ImageShow>().FadeOut();
            CharacterBack.GetComponent<ImageShow>().FadeOut();
        }
    }
}
