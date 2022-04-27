using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public CameraController cam; 

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            cam.ShakeCamera();
            Debug.Log(collision.gameObject.name);
        }
    }
}
