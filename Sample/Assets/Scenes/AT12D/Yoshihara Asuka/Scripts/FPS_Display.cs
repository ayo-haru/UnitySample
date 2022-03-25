//=============================================================================
//
// FPS�\��
//
// �쐬��:2022/03/26
// �쐬��:�g����
//
// <�J������>
// 2022/03/26   �쐬
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Display : MonoBehaviour
{
    //---�ϐ��錾
    private int FrameCount;                             // �t���[���������o���ꂽ�񐔂��J�E���g
    private float PrevTime;                             // �o�ߎ���
    private float FPS;                                  // FPS�l
    // Start is called before the first frame update
    void Start()
    {
        //---����������
        FrameCount = 0;
        PrevTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FrameCount++;                                  // Update�̍ŏ��ɃJ�E���g����B(�t���[���������o���ꂽ�񐔂ƂȂ�)
        float time = Time.realtimeSinceStartup - PrevTime;
        
        
        if(time >= 0.5f)                               // 0.5�b���Ƃ�FPS�l���Z�o
        {
            FPS = FrameCount / time;                   // ���t���[���Atime(0.5�b)�����邱�ƂŁAFPS�̋ߎ��l���Z�o
            Debug.Log("FPS�l:"+ FPS);

            FrameCount = 0;
            PrevTime = Time.realtimeSinceStartup;
        }
    }

    private void OnGUI()
    {
        // FPS�̒l��UI�\��
        GUILayout.Label(FPS.ToString());
    }
}
