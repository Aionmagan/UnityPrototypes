using UnityEngine;
using System.Collections;

// ADD THIS SCRIPT TO EACH OF THE WHEEL MESHES / WHEEL MESH CONTAINER OBJECTS
public class WheelSuspention : MonoBehaviour
{

    public WheelCollider wheelC;

    Vector3 v;
    Quaternion q; 

    void Start()
    {
        //this.transform.parent = wheelC.transform;
    }

    // Display
    void LateUpdate()
    {
        wheelC.GetWorldPose(out v, out q);

        this.transform.position = v;
        this.transform.rotation = q;
    }

}