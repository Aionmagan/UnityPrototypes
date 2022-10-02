using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    public float lookAtHeight;  
    public float distance;
    public float rotationSpeed;
    public float yClampMin, yClampMax;
    public LayerMask layerMask; 

    private Transform pointer; 
    public Transform forwardPointer
    {
        get
        {
            return pointer; 
        }
    }

    private PlayerInput playerInput;
    private InputAction cameraAction;
    private Vector2 analog;

    private float x = 180;
    private float y = 45; 

    // Start is called before the first frame update
    void Start()
    {
        pointer = new GameObject("forwardPointer").transform; 

        playerInput = GetComponent<PlayerInput>();
        cameraAction = playerInput.actions["Camera"];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();

        pointer.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        pointer.position = transform.position;

        x += analog.x * rotationSpeed * Time.deltaTime;
        y += analog.y * rotationSpeed * Time.deltaTime;

        x = x % 360;
        y = Mathf.Clamp(y, yClampMin, yClampMax);

        var q = Quaternion.Euler(y, x, 0);

        transform.position = target.position - q * Vector3.forward * Distance();
        transform.LookAt(target.position + Vector3.up * lookAtHeight);
    }

    void UpdateInput()
    {
        analog = cameraAction.ReadValue<Vector2>();
    }

    float Distance()
    {
        var dir = transform.position - target.position;
        RaycastHit hit; 
        if (Physics.Raycast(target.position + Vector3.up * 0.5f, dir.normalized, out hit, distance, layerMask))
        {
            return hit.distance; 
        }

        return distance;
    }
}
