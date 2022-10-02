using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public Transform startPoint;
    public Transform particle;
    public LayerMask layerMask; 

    private LineRenderer m_line;
   // private Transform endPoint; 
    // Start is called before the first frame update
    void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RayShot();
    }

    void RayShot()
    {
        RaycastHit hit; 

        if (Physics.Raycast(startPoint.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            m_line.SetPosition(0, startPoint.position);
            m_line.SetPosition(1, hit.point);

            particle.position = hit.point;
            particle.eulerAngles = hit.normal; 

            var t = hit.collider.transform.parent.GetComponentInChildren<ISwitch>();
            //Debug.Log(hit.collider.name);
            if (t != null)
            {
                Debug.Log("Cristal hit and detected");
                t.TriggerSwitch();
            }
        }
    }
}
