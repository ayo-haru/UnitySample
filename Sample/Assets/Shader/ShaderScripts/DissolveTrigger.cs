using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTrigger : MonoBehaviour
{
    private Dissolve _dissolve;
    // Start is called before the first frame update
    void Start()
    {
        //---コンポーネント取得
        _dissolve = GetComponent<Dissolve>();

        //---ランダムにスタート
        _dissolve.Invoke("Play", Random.Range(1.0f, 2.0f));
    }
}
