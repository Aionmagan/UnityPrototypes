using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{
    public Transform grabPos; 

    //input
    private PlayerInput input; 
    private InputAction grabAction;
    private bool isGrab = false; 

    private IGrab igrab;
    private bool grabbing = false; 

    public bool IsGrabbing
    {
        get
        {
            return grabbing; 
        }
    }

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        grabAction = input.actions["Interact"];
    }

    private void Update()
    {
        isGrab = grabAction.triggered; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            if (!grabbing)
            {
                igrab = other.GetComponent<IGrab>();

                if (isGrab)
                {
                    igrab.Grab(grabPos);
                    grabbing = true;
                    StartCoroutine(Release());
                }
            }
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForEndOfFrame();

        while(grabbing)
        {
            if (isGrab)
            {
                igrab.Release();
                igrab = null;

                yield return new WaitForSeconds(Time.deltaTime);
                grabbing = false;
            }

            yield return null; 
        }
    }
}
