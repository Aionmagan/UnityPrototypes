using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*trash code but to lazy to fix*/
public class moveCube : MonoBehaviour
{
    public AudioSource _asource;

    public float[] _sample;

    public GameObject[] childCube;
    public Transform[] childPos;
    public float spread;
    public float addSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _sample = new float[512];//_asource.clip.samples * _asource.clip.channels];

        childPos = new Transform[childCube.Length];
        for (int i = 0; i < childPos.Length; ++i)
        {
            childPos[i] = new GameObject("PointDir").transform;
            childPos[i].parent = this.transform;
            //childPos[i].position = (childCube[i].transform.position - this.transform.position).normalized;
            childPos[i].position = this.transform.position;
            childPos[i].LookAt(childCube[i].transform.position);
        }
        Invoke("CallCoroutine", 3);
    }

    void CallCoroutine()
    {

        StartCoroutine(MoveCubePP());
    }

    // Update is called once per frame
    void Update()
    {
        _asource.GetSpectrumData(_sample, 0, FFTWindow.Blackman);
    }

    IEnumerator MoveCubePP()
    {
        float move = 4;
        float cursam = 0;
        Vector3 lastPos = this.transform.position;

        while(true)
        {
            //move = Mathf.PingPong(Time.time, spread);
            
            foreach(var sam in _sample)
            {
                var v = sam * spread + 2;
                if (cursam < v) { cursam = v; }
            }

            //cursam = _sample[0] * spread + 2;
            /*if (cursam > move)
            {
                move += addSpeed;
            }else if (cursam < move)
            {
                if (move > 4)
                move -= addSpeed;
            }*/
            
            move = Mathf.Lerp(move, cursam, addSpeed * Time.deltaTime);

            for (int i = 0; i < childCube.Length; ++i)
            {
                childCube[i].transform.position = this.transform.position + childPos[i].forward * move;
            }

            yield return new WaitForFixedUpdate();

            cursam = 0;
        }
    }
}
