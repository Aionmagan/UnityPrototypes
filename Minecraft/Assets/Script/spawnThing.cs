using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnThing : MonoBehaviour
{

    /*
     * this is terrible code, but this was just to test 
     * perlin noise
     * 
     * a real implementation would have detected cube sides and 
     * combine meshes for faster render performance 
     */

    public GameObject cube;
    public GameObject dirt;
    public GameObject grass;
    public GameObject water;

    public float layers; 
    public float sqrSpawn;
    public float secondsToSpawn;
    public float freq = 10;
    public float amp = 10;

    public float offset; 

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0, 65000);
        //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //StartCoroutine(Spawn());
        //offset = Random.Range(0, 65000);
        SpawnTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTerrain()
    {
        bool lastLayer = false;
        for (int i = 0; i < layers; ++i)
        {
            if (i == layers - 1)
            {
                //cube = grass;
                lastLayer = true;
            }

            for (int j = 1; j < sqrSpawn; ++j)
            {
                for (int k = 1; k < sqrSpawn; ++k)
                {
                   
                    var sample = Mathf.PerlinNoise(k / freq + offset, j / freq) * amp;
                    int s2 = (int)sample;

                    if (s2 + i >= 5 && lastLayer) { cube = grass; }
                    else if (s2 + i <= 2) { cube = water; }
                    else { cube = dirt; }

                    Instantiate(cube, new Vector3(k, s2 + i, j), Quaternion.identity);

                }
            }
        }
    }

    IEnumerator Spawn()
    {
        bool lastLayer = false;
        for(int i = 0; i < layers; ++i)
        {
            if (i == layers-1)
            {
                //cube = grass;
                lastLayer = true;
            }

            for(int j = 1; j < sqrSpawn; ++j)
            {
                for (int k = 1; k < sqrSpawn; ++k)
                {
                    /*var x = (float)k / sqrSpawn;
                    var y = (float)j / sqrSpawn;
                    var sample = Mathf.PerlinNoise(x, y);

                    var x2 = (float)j / sqrSpawn;
                    var y2 = (float)i / sqrSpawn;
                    var sample2 = Mathf.PerlinNoise(x2, y2);

                    int s1 = (int)(sample * sqrSpawn);*/
                    var sample2 = Mathf.PerlinNoise(k / freq + offset, j / freq) * amp;
                    int s2 = (int)sample2;// (int)(sample2 * sqrSpawn);

                    if (s2 + i >= 5 && lastLayer) { cube = grass; }
                    else if (s2 + i <= 2) { cube = water; }
                    else { cube = dirt; }

                    Instantiate(cube, new Vector3(k, s2+i, j), Quaternion.identity);
                    yield return null; // new WaitForSecondsRealtime(secondsToSpawn);
                }
            }
        }
    }
}
