using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.Play("bgm_cover");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("play A");
            AudioManager.Instance.Play("get_point");
        }
    }
}
