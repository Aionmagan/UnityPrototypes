using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFPS : MonoBehaviour
{
    [Range(15, 60)]
    public int fps;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60; 
    }

    // Update is called once per frame
    void Update()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = fps;
    }
}
