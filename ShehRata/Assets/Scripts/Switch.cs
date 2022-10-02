using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("ISwitchInterface")]
    public GameObject iswichObject;

    private ISwitch iswitch;

    private void Start()
    {
        iswitch = iswichObject.GetComponent<ISwitch>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ignore")) { return; }
        iswitch.TriggerSwitch();
    }

    //dirty fix because lazy
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ignore")) { return; }
        iswitch.TriggerSwitch();
    }
}
