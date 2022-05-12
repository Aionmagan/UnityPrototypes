using UnityEngine;

/*trash code, too lazy to fix*/

public class RotateCube : MonoBehaviour
{
    public float rotSpeed;
    public float offsetX;
    public float offsetY;

    public float moveSpeed;
    public float moveLimit;
    public bool moveCube;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = this.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCube)
        {
            this.transform.RotateAround(this.transform.parent.position, 
                                        this.transform.parent.forward, moveSpeed);
          
            //var pos = Mathf.PingPong(Time.time * moveSpeed, moveLimit);
            //this.transform.position = new Vector3(lastPos.x + pos, lastPos.y + pos, this.transform.position.z);
        }

        this.transform.rotation = Quaternion.Euler(0, Time.time + rotSpeed * Mathf.Sin(Time.time * offsetX), 
                                                      Time.time + rotSpeed * Mathf.Cos(Time.time * offsetY));
    }
}
