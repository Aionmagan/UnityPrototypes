using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour, ISwitch
{
    public GameObject canvas;

    public void TriggerSwitch()
    {
        canvas.SetActive(true);
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(5);
        //SceneManager.LoadScene(0);
        Application.Quit();
    }
}
