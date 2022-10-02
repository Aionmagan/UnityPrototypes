using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOthoChange : MonoBehaviour
{
    private Camera cam;
    private float fov; 

    public Transform Scaler;
   
    // Start is called before the first frame update
    void Start()
    {
        //fov = cam.orthographicSize; 
        cam = GetComponent<Camera>();
        fov = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        cam.orthographicSize = fov + Scaler.localScale.x;
    }
}
