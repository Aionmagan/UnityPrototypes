using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Car carScript;

    [SerializeField]
    private  GameObject player;

    public float distance;
    public float height;
    public float lookAtHeight;
    public float rotationSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        carScript = player.GetComponent<Car>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //calculating camera movement and rotation 
        var dis = carScript.Pivot.transform.forward * CamCollision(distance);
        var plyr = player.transform.up * height;

        this.transform.position = Vector3.Lerp(this.transform.position, 
                                               carScript.Pivot.transform.position - dis + plyr, 7 * Time.deltaTime);

        this.transform.LookAt(carScript.Pivot.transform.position + player.transform.up * lookAtHeight);
    }

    private float CamCollision(float dis)
    {
        //used to determined if camera has collided with a wall (still buggy but i don't care enough to fix it) 
        RaycastHit hit;
        if (Physics.Linecast(player.transform.position, this.transform.position, out hit))
        {
            float localDis = Vector3.Distance(player.transform.position, hit.transform.position);

            if (localDis < dis) { dis = localDis; }
        }

        return dis;
    }
}
