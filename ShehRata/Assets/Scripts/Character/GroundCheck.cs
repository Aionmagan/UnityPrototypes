using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Vector3 positionOffset;
    public float distanceToGroundCheck;
    public Vector3 directionCheck;
    public LayerMask layerMask;

    public bool OnGround()
    {
#if UNITY_EDITOR
        Debug.DrawRay(transform.position + positionOffset, directionCheck * distanceToGroundCheck, Color.yellow);
#endif

        if (Physics.Raycast(transform.position + positionOffset, directionCheck, distanceToGroundCheck, layerMask))
        {
            return true;
        }
        return false;
    }
}
