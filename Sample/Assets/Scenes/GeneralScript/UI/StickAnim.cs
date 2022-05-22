using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAnim : MonoBehaviour
{
    private UVScroll _uvscroll;   // ���g��UV�X�N���[�����i�[ 
    private int AnimTimer;        // �A�j���[�V�����̃^�C�}�[
    private int ANIMTIMER = 66 * 2;   // �A�j���[�V�����̃^�C�}�[

    // Start is called before the first frame update
    void Start()
    {
        _uvscroll = this.GetComponent<UVScroll>();  // ���g��UV�X�N���[�����擾
        AnimTimer = ANIMTIMER;
    }

    // Update is called once per frame
    void Update()
    {
        AnimTimer--;
        if(AnimTimer < 0)
        {
            if (_uvscroll.GetnFrame() == 0)
            {
                _uvscroll.SetNext();
            }
            else if(_uvscroll.GetnFrame() == 1)
            {
                _uvscroll.SetPrev();
            }

            AnimTimer = ANIMTIMER;
        }
    }
}
