using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCarTrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        other.transform.parent.Rotate(Vector3.forward * 180f);
    }
}
