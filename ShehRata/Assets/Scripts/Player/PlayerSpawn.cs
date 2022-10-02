using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour, ISwitch
{
    public Transform spawnPoint;

    private void Start()
    {
        TriggerSwitch();
    }
    public void TriggerSwitch()
    {
        transform.parent.position = spawnPoint.position;
    }
}
