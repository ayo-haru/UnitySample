using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    public GameObject DisplayHP;
    private GameObject instance;
    private void Awake()
    {
        //---UI�\��
        GameObject canvas = GameObject.Find("Canvas");
        instance = Instantiate(DisplayHP);
        instance.transform.SetParent(canvas.transform,false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //�t�F�[�h�̉��ɕ\������
        GameObject fade = GameObject.Find("Fade");
        if (fade)
        {
            int fadeIndex = fade.transform.GetSiblingIndex();
            instance.transform.SetSiblingIndex(fadeIndex - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
