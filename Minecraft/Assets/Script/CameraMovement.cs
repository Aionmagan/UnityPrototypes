using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 movement;
    float x, y;

    // Start is called before the first frame update
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 12;
        //Screen.SetResolution(640, 480, true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = Camera.main.transform.TransformDirection(movement);
        //transform.Translate(movement);
        transform.position += movement + Vector3.up * Input.mouseScrollDelta.y; 

        x += Input.GetAxis("Mouse X");
        y -= Input.GetAxis("Mouse Y");
        var q = Quaternion.Euler(y, x, 0);
        transform.rotation = q;
    }
}
