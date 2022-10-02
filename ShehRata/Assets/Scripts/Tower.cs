using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ISwitch
{
    public float rotateInSecs;
    public float rotSpeed;

    private bool isRot = false;

    public void TriggerSwitch()
    {
        if (isRot) { return; }
        StartCoroutine(RotateTower());
    }

    IEnumerator RotateTower()
    {
        float curRot = transform.rotation.y;

        isRot = true; 

        while (curRot > -360.0f)
        {
            curRot -= rotSpeed * Time.deltaTime;
            yield return new WaitForSeconds(rotateInSecs);
            transform.rotation = Quaternion.Euler(0, curRot, 0);
        }

        isRot = false;
        transform.rotation = Quaternion.identity;
    }
}
