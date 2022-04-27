using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private string m_tagPortal;
    private GameObject m_portal;
    public static bool m_hasTeleported;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_hasTeleported) { return; }
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            m_hasTeleported = true;
            GetOtherPortal();
            
            if (m_portal)
            {
                Rigidbody tempRigid = other.GetComponent<Rigidbody>();
                //other.transform.position = m_portal.transform.position + m_portal.transform.forward * 3f;
                
                if (other.transform.GetComponent<PlayerController>())
                {
                    var obj = other.transform.GetComponent<PlayerController>().PlayerCamera;
                    CameraController CC = obj.GetComponent<CameraController>();

                    var enterPortalCam = GetComponentInChildren<Camera>();
                    var exitPortalCam = m_portal.GetComponentInChildren<Camera>();
                    var dir = enterPortalCam.transform.eulerAngles;
                    var otherDir = exitPortalCam.transform.eulerAngles;
                    
                    /*float rotDif = -Quaternion.Angle(this.transform.rotation, dir);
                    rotDif += 180;
                    */
                    float rotDif = -Vector3.Angle(dir, otherDir);
                    rotDif += 180;

                    var portal2player = other.transform.position - this.transform.position;
                    var posOffset = Quaternion.Euler(Vector3.up * rotDif) * portal2player;
                    //obj.transform.rotation = Quaternion.LookRotation(dir, other.transform.up);
                    //CC.CameraRotation = otherDir + CC.CameraRotation;
                    var relativeRot = Quaternion.Inverse(other.transform.rotation) * exitPortalCam.transform.rotation;
                    CC.CameraRotation = new Vector3(CC.CameraRotation.x, relativeRot.eulerAngles.y, 0);
                    other.transform.position = m_portal.transform.position + m_portal.transform.forward * 3f + posOffset;
                    tempRigid.velocity = relativeRot * tempRigid.velocity;
                }else
                {
                    other.transform.position = m_portal.transform.position + m_portal.transform.forward * 3f;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!m_hasTeleported) { return; }
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            m_hasTeleported = false;            
        }
    }

    private void GetOtherPortal()
    {
        m_portal = GameObject.FindGameObjectWithTag(m_tagPortal);
    }
}
