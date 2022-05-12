using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraController : MonoBehaviour
{
    private float x, y;
    private GameObject pointer;
    public Transform Pointer
    {
        get { return pointer.transform; }
    }

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        pointer = new GameObject();

        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 12;
        //Screen.SetResolution(640, 480, true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pointer.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        transform.position = target.position; 

        x += Input.GetAxis("Mouse X");
        y -= Input.GetAxis("Mouse Y");
        y = Mathf.Clamp(y, -60, 60);
        var q = Quaternion.Euler(y, x, 0);
        transform.localRotation = q; 
    }
}
