using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] private GameObject Enemy; 

	public void SpawnObject(Vector3 position)
    {
        Instantiate(Enemy, position, Quaternion.identity);
    }
}
