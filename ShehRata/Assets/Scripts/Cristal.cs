using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour, ISwitch
{
    public GameObject targetTrigger;
    private ISwitch iswich; 

    void Start()
    {
        iswich = targetTrigger.GetComponent<ISwitch>();
    }

    public void TriggerSwitch()
    {
        //HOW DARE YOU TOUCH ME!!

        iswich.TriggerSwitch();
    }
}
