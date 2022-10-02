using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour, IGrab
{
    private Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform owner)
    {
        transform.GetChild(0).gameObject.SetActive(false);

        transform.SetParent(owner);
        m_rigidbody.isKinematic = true;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; 
        //m_rigidbody.isKinematic = true; 
    }

    public void Release()
    {
        transform.parent = null;
        m_rigidbody.isKinematic = false;

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
