using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour 
{
	public List<Transform> nodes;
	private int selectedNode = 0; 

	// Use this for initialization
	void Start () 
	{
		//if (nodes.Count < 1)
		//nodes = new List<Transform>();
		float d = 100.0f;
		int s = 0; 
		for(int i = 0; i < nodes.Count; ++i)
        {
			nodes[i].GetComponent<MeshRenderer>().enabled = false;
			if (d < Vector3.Distance(transform.position, nodes[i].position))
            {
				selectedNode = i; 
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(transform.position, nodes[selectedNode].position) < 1.0f)
        {
			selectedNode++;
			if (selectedNode >= nodes.Count) selectedNode = 0;
        }

		lookForward(nodes[selectedNode].position);
	}

	void lookForward(Vector3 forward)
    {
		var v = forward - transform.position;
		v.y = 0;

		transform.rotation = Quaternion.Slerp(transform.rotation,
							 Quaternion.LookRotation(v), 5 * Time.deltaTime);
	}
}
