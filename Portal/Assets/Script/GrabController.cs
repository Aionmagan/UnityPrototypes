using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private float m_rayDistance;
    [SerializeField] private float m_grabDistance;
    private bool m_isGrabbed = false;
    private GameObject m_item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_isGrabbed)
            {
                m_isGrabbed = false;
                m_item = null;
            }
            else
            {
                m_isGrabbed = true;
                GrabItem();
            }
        }

        if (m_item != null)
        {
            m_item.transform.position = this.transform.position + this.transform.forward * m_grabDistance;
        }
    }

    private void GrabItem()
    {
        RaycastHit hit;

        Debug.DrawRay(this.transform.position, this.transform.forward * m_rayDistance, Color.red);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, m_rayDistance, m_layerMask))
        {
            Debug.Log(hit.transform.name);
            m_item = hit.transform.gameObject;
        }
    }
}
