// Smooth towards the target

using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{
    public Transform target;
    public float distance = 0.3F;
    public float height; 
    private Vector3 velocity = Vector3.zero;
    private Transform local;

    void Start()
    {
        local = new GameObject("LOCAL").transform;
    }

    void LateUpdate()
    {
        //local.position = target.position;
        //local.eulerAngles = Vector3.up * target.eulerAngles.y;
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.position - local.position ;
        var q = Quaternion.LookRotation(targetPosition.normalized);
        var d = Mathf.Clamp(Vector3.Distance(target.position, transform.position), 2, distance);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1.0f * Time.deltaTime);
            //transform.eulerAngles = Vector3.Lerp(transform.localEulerAngles, targetPosition, 3.0f * Time.deltaTime);
        
            // Smoothly move the camera towards that target position
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            this.transform.position = target.position - transform.forward * d;
        
            transform.LookAt(target.position + Vector3.up * height);

        if (Vector3.Distance(local.position, target.position)>1.0f)
        local.position = target.position;
    }
}